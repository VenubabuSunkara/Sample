using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TransForce.Web.Models;
using TransForce.Web.Portal.App_Start;
using TransForce.Web.Portal.Models;

namespace TransForce.Web.Portal.Controllers
{
    public class DriverInfoController : Controller
    {

        private readonly HttpClient client;
        private readonly string _url = ConfigurationManager.AppSettings["webapibaseurl"];
        private readonly UserModel userData;
        private StatusResult statusResult;

        public DriverInfoController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            userData = System.Web.HttpContext.Current.Session["UserData"] != null ? (UserModel)System.Web.HttpContext.Current.Session["UserData"] : null;
        }

        #region DiverBulk Upload

        /// <summary>
        /// Bulk Upload Drivers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomAuthorize(Roles.AccountAdmin, Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> UploadDrivers()
        {
            try
            {
                CustomerProfile cProfile = new CustomerProfile();
                if (userData.Roles.Contains(Roles.AccountAdmin))
                {
                    cProfile.CompanyID = userData.CompanyId;
                    return View(cProfile);

                }
                else
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                    HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Customer/GetCustomerProfileByUserID", "");
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = responseMessage.Content.ReadAsStringAsync().Result;
                        statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                        if (statusResult.Status.Equals(Status.Success.ToString()))
                        {
                            List<CustomerProfile> customerData = JsonConvert.DeserializeObject<List<CustomerProfile>>(statusResult.Result.ToString());
                            return View(BindDropDown(customerData, cProfile));
                        }
                    }
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        /// <summary>
        /// Dirver Bulk Upload
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="fileDriver"></param>
        /// <returns></returns>
        [HttpPost]
        [AsyncTimeout(7200)]
        [CustomAuthorize(Roles.AccountAdmin, Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> UploadDrivers(int CompanyID, HttpPostedFileBase fileDriver)
        {
            CustomerProfile cProfile = new CustomerProfile();
            cProfile.CompanyID = CompanyID;
            try
            {
                if (fileDriver.ContentLength > 0)
                {
                    cProfile.CompanyList = (SelectList)TempData["CompanyList"];
                    TempData.Keep();
                    UploadDrivers filevalidations;
                    ViewBag.FileName = System.IO.Path.GetFileName(fileDriver.FileName);
                    var extension = Path.GetExtension(fileDriver.FileName);
                    string savefilename = Path.Combine(Server.MapPath("~/Uploads"),
                                     Path.GetFileName(fileDriver.FileName));
                    fileDriver.SaveAs(savefilename);
                    if (extension.Contains(".txt"))
                    {

                        filevalidations = CommonFunctions.ReadExecuteDelimitedDriverFile(savefilename);
                    }
                    else
                    {
                        filevalidations = CommonFunctions.ReadExecuteDriverCsvFile(savefilename);
                    }
                    List<HttpResponseMessage> customerData = new List<HttpResponseMessage>();
                    int count = 0;
                    Parallel.ForEach(filevalidations.ChunkDriverdata, driverRes =>
                    {
                        var client = new HttpClient();
                        client.BaseAddress = new Uri(_url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        UploadDrivers DriverDataModel = new UploadDrivers();
                        DriverDataModel.CompanyID = CompanyID;
                        DriverDataModel.Driverdata = driverRes;
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                        client.Timeout = TimeSpan.FromMinutes(60);
                        Task<HttpResponseMessage> responseMessage = client.PostAsJsonAsync(_url + "DriverInfo/UploadBulkDrivers", DriverDataModel);
                        customerData.Add(responseMessage.Result);
                        count++;
                        ViewBag.ProcessStatus = count * 1000 + " Records processed.";
                        responseMessage.Dispose();
                        client.Dispose();
                    });
                    List<DriversModel> responceResults = null;
                    foreach (var customer in customerData)
                    {
                        if (customer.IsSuccessStatusCode)
                        {
                            var result = await customer.Content.ReadAsStringAsync();
                            if (JsonConvert.DeserializeObject<StatusResult>(result).Status.Equals(Status.Success.ToString()))
                            {
                                responceResults = new List<Web.Models.DriversModel>();
                                responceResults.AddRange(JsonConvert.DeserializeObject<List<DriversModel>>(JsonConvert.DeserializeObject<StatusResult>(result).Result.ToString()));
                            }
                        }
                    }
                    if (responceResults == null)
                    {
                        cProfile.SuccessMesage = "Some thing went wrong.";
                        System.IO.File.Delete(Server.MapPath("~/Uploads/" + fileDriver.FileName));
                        return View(cProfile);
                    }
                    else if (responceResults.Count == 0)
                    {
                        cProfile.SuccessMesage = "Your file has been uploaded successfully";
                        System.IO.File.Delete(Server.MapPath("~/Uploads/" + fileDriver.FileName));
                        return View(cProfile);
                    }
                    else
                    {
                        TempData["FailDriverResult"] = responceResults;
                        System.IO.File.Delete(Server.MapPath("~/Uploads/" + fileDriver.FileName));
                        cProfile.UploadFailMessage = "Some entries where not successfully uploaded.Please make necessary corrections and upload a new file.";
                        return View(cProfile);
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(Server.MapPath("~/Uploads/" + fileDriver.FileName));
                cProfile.ExMessage = ex.Message + "Please upload a standard file";
                return View(cProfile);
            }
            return View();
        }

        /// <summary>
        /// Download file in txt or csv formate
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public FileResult DownloadFail(string FileName)
        {
            var sb = new StringBuilder();
            if (FileName.Contains(".csv"))
            {
                sb.AppendLine("DotNumber" + "," + "FirstName" + "," + "LastName" + "," + "LicenseState" + "," + "isActive" + "," +
                    "LicenseNumber" + "," + "DOB" + "," + "Remarks");
                foreach (var data in (List<DriversModel>)TempData["FailDriverResult"])
                {
                    //sb.AppendLine(data.LocCode + "," + data.LocationName + ", " + data.Remarks);
                    sb.AppendLine(data.DotNumber + "," + data.FirstName + "," + data.LastName + "," + data.LicenseState + "," + data.isActive + "," + data.LicenseNumber
                       + "," + data.DOB + "," + data.Remarks);
                }
            }
            else
            {
                sb.AppendLine("DotNumber \t" + "FirstName \t" + "LastName \t" + "LicenseState \t" + "isActive \t" +
                    "LicenseNumber \t" + "DOB \t" + "Remarks");
                foreach (var data in (List<DriversModel>)TempData["FailDriverResult"])
                {
                    //sb.AppendLine(data.LocCode + "," + data.LocationName + ", " + data.Remarks);
                    sb.AppendLine(data.DotNumber + "\t" + data.FirstName + "\t" + data.LastName + "\t" + data.LicenseState + "\t" + data.isActive + "\t" + data.LicenseNumber + "\t" + data.DOB
                       + "\t" + data.Remarks);
                }
            }
            TempData.Keep("FailDriverResult");
            ViewBag.ErrorMessage = "Locations Uploaded successfully";
            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", FileName);
        }

        #endregion

        [HttpGet]
        [CustomAuthorize(Roles.AccountAdmin, Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> GetDrivers()
        {
            CustomerProfile cProfile = new CustomerProfile();
            if (userData.Roles.Contains(Roles.AccountAdmin))
            {
                cProfile.CompanyID = userData.CompanyId;
                return View(cProfile);

            }
            else
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Customer/GetCustomerProfileByUserID", "");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                    if (statusResult.Status.Equals(Status.Success.ToString()))
                    {
                        List<CustomerProfile> customerData = JsonConvert.DeserializeObject<List<CustomerProfile>>(statusResult.Result.ToString());
                        return View(BindDropDown(customerData, cProfile));
                    }
                }
            }
            return View("Error");
        }


        [HttpGet]
        [CustomAuthorize(Roles.AccountAdmin, Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> GetDriversByCompanyId(int CompanyId)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "DriverInfo/GetDrivers/" + CompanyId, "");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    List<Driver> customerData = JsonConvert.DeserializeObject<List<Driver>>(statusResult.Result.ToString());
                    var jsonResult = Json(new { data = customerData }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json(new { data = new Driver() }, JsonRequestBehavior.AllowGet);
                }
            }
            return View("Error");
        }


        /// <summary>
        /// Create New Driver
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public PartialViewResult CreateDriver()
        {
            var model = new Driver();
            ViewBag.Action = "Create";
            return PartialView("_Driver", model);
        }

        /// <summary>
        /// Create New Driver
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> CreateDriver(Driver model)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "DriverInfo/InsertDriverinfo", model);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    return Json(statusResult.Status, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(statusResult.Result, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }

        /// <summary>
        /// Create New Driver
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> EditDriver(int DriverId, int CompanyId)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "DriverInfo/EditDriverInfo/" + DriverId + "/" + CompanyId, "");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    ViewBag.UserStatus = "EditDriver";
                    Driver driverData = JsonConvert.DeserializeObject<Driver>(statusResult.Result.ToString());
                    driverData.DOB.ToShortDateString();
                    return PartialView("_Driver", driverData);
                }
                else
                {
                    return Json(statusResult.Result, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }

        /// <summary>
        /// Update Driver
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> UpdateDriver(Driver model)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "DriverInfo/UpdateDriverInfo", model);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    return Json(statusResult.Status, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(statusResult.Result, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }


        /// <summary>
        /// Delete Driver
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> DeleteDriver(int DriverId, int CompanyId)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "DriverInfo/DeleteDriverInfo/" + DriverId + "/" + CompanyId, "");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    return Json(statusResult.Status, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(statusResult.Result, JsonRequestBehavior.AllowGet);
                }
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Bind Customers to Dropdown
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        private CustomerProfile BindDropDown(List<CustomerProfile> customerData, CustomerProfile cProfile)
        {
            var data = from c in customerData
                       select new
                       {
                           CompanyId = c.CompanyID,
                           ComapnyName = c.CustomerName
                       };
            SelectList list = new SelectList(data, "CompanyId", "ComapnyName");
            TempData["CompanyList"] = list;
            TempData.Keep("CompanyList");
            cProfile.CompanyList = list;
            return cProfile;
        }
    }

}