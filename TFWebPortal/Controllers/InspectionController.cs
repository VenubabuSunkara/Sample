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
using TransForce.Web.Portal.App_Start;
using TransForce.Web.Portal.Models;

namespace TransForce.Web.Portal.Controllers
{
    public class InspectionController : Controller
    {
        private readonly HttpClient client;
        private readonly string _url = ConfigurationManager.AppSettings["webapibaseurl"];
        private readonly UserModel userData;
        public InspectionController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            userData = System.Web.HttpContext.Current.Session["UserData"] != null ? (UserModel)System.Web.HttpContext.Current.Session["UserData"] : null;
        }

        /// <summary>
        /// get action method for Uploading Inspection Xml file
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles.AccountAdmin, Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> UploadInspectionXml()
        {
            //List<string> currentRole = Session["UserRole"] as List<string>;
            CustomerProfile cProfile = new CustomerProfile();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Customer/GetCustomerProfileByUserID", "");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    List<CustomerProfile> customerData = JsonConvert.DeserializeObject<List<CustomerProfile>>(statusResult.Result.ToString());
                    if (userData.Roles.Contains(Roles.AccountAdmin))
                    {
                        cProfile.CompanyID = customerData.FirstOrDefault().CompanyID;
                        return View(cProfile);
                    }
                    else
                    {
                        var data = from c in customerData
                                   select new
                                   {
                                       CompanyId = c.CompanyID,
                                       ComapnyName = c.CustomerName
                                   };
                        SelectList list = new SelectList(data, "CompanyId", "ComapnyName");
                        TempData["CompanyList"] = list;
                        cProfile.CompanyList = list;
                        //Session["CompanyList"] = list;
                        return View(cProfile);
                    }
                }
            }
            return View("Error");
        }

        /// <summary>
        /// post action method for Uploading Inspection Xml file
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="fileXML"></param>
        /// <returns></returns>
        [HttpPost]
        [AsyncTimeout(7200)]
        [CustomAuthorize(Roles.AccountAdmin, Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> UploadInspectionXml(int CompanyID, HttpPostedFileBase fileXML)
        {
            CustomerProfile cProfile = new CustomerProfile();
            try
            {
                if (fileXML.ContentLength > 0)
                {
                    cProfile.CompanyList = (SelectList)TempData["CompanyList"];
                    TempData.Keep();
                    ViewBag.FileName = System.IO.Path.GetFileName(fileXML.FileName);
                    string savefilename = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(fileXML.FileName));
                    fileXML.SaveAs(savefilename);
                    var filevalidations = CommonFunctions.ReadDOTXmlfile(savefilename);

                    Inspection InspectionData = new Models.Inspection();
                    InspectionData.CompanyId = CompanyID;
                    InspectionData.xml = filevalidations;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                    client.Timeout = TimeSpan.FromMinutes(60);
                    HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "RoadsideInspection/UploadInspectionXml", InspectionData);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = responseMessage.Content.ReadAsStringAsync().Result;
                        var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                        if (statusResult.Status == "Success")
                        {
                            ViewBag.SuccessMessage = "Your file has been uploaded successfully.";
                            System.IO.File.Delete(Server.MapPath("~/Uploads/" + fileXML.FileName));
                            return View(cProfile);
                        }
                        else
                        {
                            System.IO.File.Delete(Server.MapPath("~/Uploads/" + fileXML.FileName));
                            //ViewBag.ErrorMessage = "Some entries where not successfully uploaded.Please make necessary corrections and upload a new file.";
                            ViewBag.ErrorMessage = statusResult.Result.ToString();
                            return View(cProfile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cProfile.CompanyList = (SelectList)TempData["CompanyList"];
                TempData.Keep();
                //cProfile.StatusMessage = ex.Message;
                ViewBag.ErrorMessage = ex.Message;
                return View(cProfile);
            }
            return View(cProfile);
        }

        [CustomAuthorize(Roles.AccountAdmin, Roles.PortalAdmin, Roles.SuperAdmin)]
        public async Task<ActionResult> RoadsideInspectionEntry()
        {
            //List<string> currentRole = Session["UserRole"] as List<string>;
            CustomerProfile cProfile = new CustomerProfile();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Customer/GetCustomerProfileByUserID", "");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    List<CustomerProfile> customerData = JsonConvert.DeserializeObject<List<CustomerProfile>>(statusResult.Result.ToString());
                    if (userData.Roles.Contains(Roles.AccountAdmin))
                    {
                        cProfile.CompanyID = customerData.FirstOrDefault().CompanyID;
                        return View(cProfile);
                    }
                    else
                    {
                        var data = from c in customerData
                                   select new
                                   {
                                       CompanyId = c.CompanyID,
                                       ComapnyName = c.CustomerName
                                   };
                        SelectList list = new SelectList(data, "CompanyId", "ComapnyName");
                        TempData["CompanyList"] = list;
                        cProfile.CompanyList = list;
                        //Session["CompanyList"] = list;
                        return View(cProfile);
                    }
                }
            }
            return View("Error");
            //return View();
        }

        [HttpPost]
        [CustomAuthorize(Roles.AccountAdmin, Roles.PortalAdmin, Roles.SuperAdmin)]
        public async Task<ActionResult> RoadsideInspectionEntry(InspectionDriver model)
        {
            //model.InspectionDate.ToShortDateString().Replace("/", "-");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "RoadsideInspection/GetInspectionByDate/", model);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    List<G_RPT_REG_CP_INSP_IDModel> inspectionsData = JsonConvert.DeserializeObject<List<G_RPT_REG_CP_INSP_IDModel>>(statusResult.Result.ToString());
                    //return Json(inspectionsData, JsonRequestBehavior.AllowGet);
                    var jsonResult = Json(new { data = inspectionsData }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json(new { data = new List<G_RPT_REG_CP_INSP_IDModel>() }, JsonRequestBehavior.AllowGet);
                }
            }
            return View("Error");
            //return View();
        }

        [CustomAuthorize(Roles.AccountAdmin, Roles.PortalAdmin, Roles.SuperAdmin)]
        public async Task<ActionResult> RoadsideDriversInspectionEntry(int CompanyID, string DriverName)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "DriverInfo/GetDriversByName/" + CompanyID + "/" + DriverName, "");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    List<Driver> inspectionsDriverData = JsonConvert.DeserializeObject<List<Driver>>(statusResult.Result.ToString());
                    //return Json(inspectionsData, JsonRequestBehavior.AllowGet);
                    var jsonResult = Json(new { data = inspectionsDriverData }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json(new { data = new List<Driver>() }, JsonRequestBehavior.AllowGet);
                }
            }
            return View("Error");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            //HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "RoadsideInspection/GetInspectionDetailsByDriverName/", model);
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var result = responseMessage.Content.ReadAsStringAsync().Result;
            //    var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
            //    if (statusResult.Status.Equals(Status.Success.ToString()))
            //    {
            //        List<ManualRSI> inspectionsDriverData = JsonConvert.DeserializeObject<List<ManualRSI>>(statusResult.Result.ToString());
            //        //return Json(inspectionsData, JsonRequestBehavior.AllowGet);
            //        var jsonResult = Json(new { data = inspectionsDriverData }, JsonRequestBehavior.AllowGet);
            //        jsonResult.MaxJsonLength = int.MaxValue;
            //        return jsonResult;
            //    }
            //    else if (statusResult.Status.Equals(Status.Fail.ToString()))
            //    {
            //        return Json(new { data = new List<Driver>() }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            //return View("Error");
            //return View();
        }

        [CustomAuthorize(Roles.AccountAdmin, Roles.PortalAdmin, Roles.SuperAdmin)]
        public async Task<ActionResult> GetCompanyInspection(InspectionDriver model)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "RoadsideInspection/GetInspectionById/", model);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    //G_RPT_REG_CP_INSP_IDModel inspectionsData = JsonConvert.DeserializeObject<G_RPT_REG_CP_INSP_IDModel>(statusResult.Result.ToString());
                    ManualRSIModel inspectionsData = JsonConvert.DeserializeObject<ManualRSIModel>(statusResult.Result.ToString());
                    return Json(inspectionsData, JsonRequestBehavior.AllowGet);
                }
                else if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            return View("Error");
        }

        [CustomAuthorize(Roles.AccountAdmin, Roles.PortalAdmin, Roles.SuperAdmin)]
        public async Task<ActionResult> GetDriverInspection(int CompanyID, string DriverId)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "DriverInfo/EditDriverInfo/" + DriverId + "/" + CompanyID, "");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    Driver inspectionsDriverData = JsonConvert.DeserializeObject<Driver>(statusResult.Result.ToString());
                    return Json(inspectionsDriverData, JsonRequestBehavior.AllowGet);
                }
                else if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            return View("Error");
            //return View();
        }

        public class LocationList
        {
            public string LocationName { get; set; }
            public string LocationCode { get; set; }
        }

        [CustomAuthorize(Roles.AccountAdmin, Roles.PortalAdmin, Roles.SuperAdmin)]
        public async Task<ActionResult> GetLocations(int CompanyID)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Location/GetLocations/" + CompanyID, "");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    List<LocationList> locationsData = JsonConvert.DeserializeObject<List<LocationList>>(statusResult.Result.ToString());
                    return Json(locationsData, JsonRequestBehavior.AllowGet);
                }
                else if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            return View("Error");
        }

        [CustomAuthorize(Roles.AccountAdmin, Roles.PortalAdmin, Roles.SuperAdmin)]
        public async Task<ActionResult> AddManualInspection(ManualRSIModel inspectionModel)
        {
            if (!ModelState.IsValid)
            {
                //return Json(new { status = "Error" }, JsonRequestBehavior.AllowGet);
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "RoadsideInspection/InsertInspectionEntry", inspectionModel);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                StatusResult statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    ManualRSIModel inspectionData = JsonConvert.DeserializeObject<ManualRSIModel>(statusResult.Result.ToString());
                    return Json(new { status = statusResult.Status, inspectionData = inspectionData }, JsonRequestBehavior.AllowGet);
                }
                else if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json(new { status = Status.Fail.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = "Error" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> UploadInspectionDocumnets(int InspectionId, int CompanyId)
        {

            List<BlobUploadModel> documentData = new List<BlobUploadModel>();
            //  Get all files from Request object  
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                BlobUploadModel objBUM = new BlobUploadModel();
                HttpPostedFileBase file = files[i];
                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    fileData = binaryReader.ReadBytes(file.ContentLength);
                }
                objBUM.FileName = file.FileName;
                objBUM.FileData = fileData;
                documentData.Add(objBUM);
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "RoadsideInspection/UploadInspectionDocumnetss", documentData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                StatusResult statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    //ManualRSIModel inspectionData = JsonConvert.DeserializeObject<ManualRSIModel>(statusResult.Result.ToString());
                    return Json(new { status = statusResult.Status }, JsonRequestBehavior.AllowGet);
                }
                else if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json(new { status = Status.Fail.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = "Error" }, JsonRequestBehavior.AllowGet);
        }
    }
}
