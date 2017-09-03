using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TransForce.Web.Portal.Models;
using TransForce.Web.Portal.App_Start;
using System.Data;
using System.Text;

namespace TransForce.Web.Portal.Controllers
{
    public class LocationConfigurationController : Controller
    {
        private readonly HttpClient client;
        private readonly string _url = ConfigurationManager.AppSettings["webapibaseurl"];
        private readonly UserModel userData;
        private StatusResult statusResult;
        public LocationConfigurationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            userData = System.Web.HttpContext.Current.Session["UserData"] != null ? (UserModel)System.Web.HttpContext.Current.Session["UserData"] : null;
        }

        // GET: LocationConfiguartion
        public async Task<ActionResult> Index()
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
        /// Create Area to Location
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddArea(LocationConfigurationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = "Error" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Location/AddLocationParent", model);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    StatusResult statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                    if (statusResult.Status.Equals(Status.Success.ToString()))
                    {
                        LocationConfigurationViewModel locArea = JsonConvert.DeserializeObject<LocationConfigurationViewModel>(statusResult.Result.ToString());
                        return Json(new { status = statusResult.Status, locationData = locArea }, JsonRequestBehavior.AllowGet);
                    }
                    else if (statusResult.Status.Equals(Status.Fail.ToString()))
                    {
                        return Json(new { status = Status.Fail.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Create Region to Location
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddRegion(LocationConfigurationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = "Error" }, JsonRequestBehavior.AllowGet);
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Location/AddLocationGrandParent", model);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                StatusResult statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    LocationConfigurationViewModel locRegion = JsonConvert.DeserializeObject<LocationConfigurationViewModel>(statusResult.Result.ToString());
                    return Json(new { status = statusResult.Status, locationData = locRegion }, JsonRequestBehavior.AllowGet);
                }
                else if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json(new { status = Status.Fail.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = "Error" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete LocationConfig
        /// </summary>
        /// <param name="locationConfigId"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeleteLocationConfig(LocationConfigurationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = "Error" }, JsonRequestBehavior.AllowGet);
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Location/DeletingLocationConfig/" + model.LocConfigID + "/" + model.CompanyID, model);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                StatusResult statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    return Json(new { status = statusResult.Status, result = statusResult.Result }, JsonRequestBehavior.AllowGet);
                }
                else if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json(new { status = Status.Fail.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = "Error" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Location Config By CompanyID
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetLocationConfigByCompanyID(int CompanyID)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Location/GetLocationConfigByCompanyID/" + CompanyID, "");

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                StatusResult statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    List<LocationConfigurationViewModel> locConfig = JsonConvert.DeserializeObject<List<LocationConfigurationViewModel>>(statusResult.Result.ToString());
                    var jsonResult = Json(new { status = statusResult.Status, locationData = locConfig.Skip(1).ToList() }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json(new { status = Status.Fail.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = "Error" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [CustomAuthorize(Roles.AccountAdmin, Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> UploadLocation()
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

        [HttpPost]
        [CustomAuthorize(Roles.AccountAdmin, Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> UploadLocation(int CompanyID, HttpPostedFileBase fileLocation)
        {
            CustomerProfile cProfile = new CustomerProfile();
            cProfile.CompanyID = CompanyID;
            try
            {
                if (fileLocation.ContentLength > 0)
                {
                    cProfile.CompanyList = (SelectList)TempData["CompanyList"];
                    TempData.Keep();
                    ValidationFile filevalidations;
                    ViewBag.FileName = System.IO.Path.GetFileName(fileLocation.FileName);
                    var extension = Path.GetExtension(fileLocation.FileName);
                    string savefilename = Path.Combine(Server.MapPath("~/Uploads"),
                                     Path.GetFileName(fileLocation.FileName));
                    fileLocation.SaveAs(savefilename);
                    if (extension.Contains(".txt"))
                    {
                        filevalidations = CommonFunctions.ReadExecuteDelimitedFile(savefilename);
                    }
                    else
                    {
                        filevalidations = CommonFunctions.ReadExecuteCsvFile(savefilename);
                    }
                    List<HttpResponseMessage> customerData = new List<HttpResponseMessage>();
                    int count = 0;
                    Parallel.ForEach(filevalidations.ChunkvalidData, location =>
                      {
                          var client = new HttpClient();
                          client.BaseAddress = new Uri(_url);
                          client.DefaultRequestHeaders.Accept.Clear();
                          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                          UploadLocation locationDataModel = new Models.UploadLocation();
                          locationDataModel.CompanyID = CompanyID;
                          locationDataModel.Locationdata = location;
                          client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                          client.Timeout = TimeSpan.FromMinutes(60);
                          Task<HttpResponseMessage> responseMessage = client.PostAsJsonAsync(_url + "Location/UploadBulkLocations", locationDataModel);
                          customerData.Add(responseMessage.Result);
                          count++;
                          ViewBag.ProcessStatus = count * 1000 + " Records processed.";
                          responseMessage.Dispose();
                          client.Dispose();
                      });
                    List<LocationData> responceResults = null;
                    foreach (var customer in customerData)
                    {
                        if (customer.IsSuccessStatusCode)
                        {
                            var result = await customer.Content.ReadAsStringAsync();
                            if (JsonConvert.DeserializeObject<StatusResult>(result).Status.Equals(Status.Success.ToString()))
                            {
                                responceResults = new List<LocationData>();
                                responceResults.AddRange(JsonConvert.DeserializeObject<List<LocationData>>(JsonConvert.DeserializeObject<StatusResult>(result).Result.ToString()));
                            }
                        }
                    }
                    if (responceResults == null)
                    {
                        cProfile.SuccessMesage = "Something went wrong";
                        System.IO.File.Delete(Server.MapPath("~/Uploads/" + fileLocation.FileName));
                        return View(cProfile);
                    }
                    else if (responceResults.Count == 0)
                    {
                        cProfile.SuccessMesage = "Your file has been uploaded successfully";
                        System.IO.File.Delete(Server.MapPath("~/Uploads/" + fileLocation.FileName));
                        return View(cProfile);
                    }
                    else
                    {
                        TempData["FailResult"] = responceResults;
                        System.IO.File.Delete(Server.MapPath("~/Uploads/" + fileLocation.FileName));
                        cProfile.UploadFailMessage = "Some entries where not successfully uploaded.Please make necessary corrections and upload a new file.";
                        return View(cProfile);
                    }
                }
                else
                {
                    cProfile.ExMessage = statusResult.Status;
                    return View(cProfile);
                }
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(Server.MapPath("~/Uploads/" + fileLocation.FileName));
                cProfile.ExMessage = ex.Message + "Please upload a standard file";
                return View(cProfile);
            }
        }

        public FileResult DownloadFail(string FileName)
        {
            var sb = new StringBuilder();
            if (FileName.Contains(".csv"))
            {
                sb.AppendLine("LocationName" + "," + "LocCode" + "," + "FirstContactName" + "," + "FirstContactEmail" + "," + "isActive" + "," + "SecondContactName" + "," +
                    "SecondContactEmail" + "," + "ThirdContactName" + "," + "ThirdContactEmail" + "," + "Remarks");
                foreach (var data in (List<LocationData>)TempData["FailResult"])
                {
                    //sb.AppendLine(data.LocCode + "," + data.LocationName + ", " + data.Remarks);
                    sb.AppendLine(data.LocationName + "," + data.LocCode + "," + data.FirstContactName + "," + data.FirstContactEmail + "," + data.isActive + "," + data.SecondContactName + "," + data.SecondContactEmail
                       + "," + data.ThirdContactName + "," + data.ThirdContactEmail + "," + data.Remarks);
                }
            }
            else
            {
                sb.AppendLine("LocationName \t" + "LocCode \t" + "FirstContactName \t" + "FirstContactEmail \t" + "isActive \t" + "SecondContactName \t" +
                    "SecondContactEmail \t" + "ThirdContactName \t" + "ThirdContactEmail \t" + "Remarks");
                foreach (var data in (List<LocationData>)TempData["FailResult"])
                {
                    //sb.AppendLine(data.LocCode + "," + data.LocationName + ", " + data.Remarks);
                    sb.AppendLine(data.LocationName + "\t" + data.LocCode + "\t" + data.FirstContactName + "\t" + data.FirstContactEmail + "\t" + data.isActive + "\t" + data.SecondContactName + "\t" + data.SecondContactEmail
                       + "\t" + data.ThirdContactName + "\t" + data.ThirdContactEmail + "\t" + data.Remarks);
                }
            }
            TempData.Keep("FailResult");
            ViewBag.ErrorMessage = "Locations Uploaded successfully";
            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", FileName);
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
