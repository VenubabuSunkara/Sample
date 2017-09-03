using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using TransForce.Web.Portal.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using log4net;
using TransForce.Web.Portal.TFWebExceptions;

namespace TransForce.Web.Portal.Controllers
{
    [CustomAuthorize]
    //[TestAuth(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
    public class CustomerController : Controller
    {
        private readonly HttpClient client;
        private readonly string _url = ConfigurationManager.AppSettings["webapibaseurl"];
        private StatusResult statusResult;
        private readonly UserModel userData;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public CustomerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            userData = System.Web.HttpContext.Current.Session["UserData"] != null ? (UserModel)System.Web.HttpContext.Current.Session["UserData"] : null;
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin)]
        public ActionResult GetCustomers()
        {
            TempData["UserRole"] = Roles.PortalAdmin;
            return View();
        }

        /// <summary>
        /// Get All Customer
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles.AccountAdmin)]
        public ActionResult GetCustomer()
        {
            TempData["UserRole"] = Roles.AccountAdmin;
            return View();
        }



        /// <summary>
        /// Get Customer
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> GetAllCustomer()
        {
            CustomerProfile objCprofile = new CustomerProfile();
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Customer/GetCustomerProfileByUserID", "");

                var result = responseMessage.Content.ReadAsStringAsync().Result;
                statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (statusResult.Status.Equals(Status.Success.ToString()))
                    {
                        List<CustomerProfile> customerData = JsonConvert.DeserializeObject<List<CustomerProfile>>(statusResult.Result.ToString());
                        var jsonResult = Json(new { data = customerData }, JsonRequestBehavior.AllowGet);
                        jsonResult.MaxJsonLength = int.MaxValue;
                        return jsonResult;
                    }
                    else
                    {
                        objCprofile.ExMessage = statusResult.Status;
                        Log.Error(Logfornet.LogMessage("", "GetAllCustomer", statusResult.Status, ""));
                    }
                }
                else
                {
                    objCprofile.ExMessage = responseMessage.StatusCode.ToString();
                    Log.Error(Logfornet.LogMessage("", "GetAllCustomer", responseMessage.StatusCode, ""));
                }
                return Json(new { data = objCprofile }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                objCprofile.ExMessage = ex.Message;
                Log.Error(Logfornet.LogMessage("", "GetAllCustomer", ex.Message, ""));
                return Json(new { data = objCprofile }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Create New Customer
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> EditCustomer(int CompanyId)
        {
            CustomerProfile customerData = new CustomerProfile();
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Customer/EditCustomer/" + CompanyId, "");
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (statusResult.Status.Equals(Status.Success.ToString()))
                    {
                        customerData = JsonConvert.DeserializeObject<CustomerProfile>(statusResult.Result.ToString());
                        ViewBag.UserRole = TempData["UserRole"];
                        TempData.Keep();
                    }
                    else
                    {
                        customerData.ExMessage = statusResult.Status;
                        Log.Error(Logfornet.LogMessage(CompanyId, "EditCustomer", statusResult.Result, statusResult.Status));
                    }
                }
                else
                {
                    customerData.ExMessage = responseMessage.StatusCode.ToString();
                }
                return View("CreateCustomer", customerData);
            }
            catch (Exception ex)
            {
                customerData.ExMessage = ex.Message;
                return View("CreateCustomer", customerData);
            }
        }


        /// <summary>
        /// Create New Customer
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin)]
        public ActionResult CreateCustomer()
        {
            return View();
        }


        /// <summary>
        /// create new customer profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> CreateNewCustomer(CustomerProfile model)
        {
            CustomerProfile customerData = new CustomerProfile();
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Customer/AddCustomer", model);

                var result = responseMessage.Content.ReadAsStringAsync().Result;
                statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (statusResult.Status.Equals(Status.Success.ToString()))
                    {
                        Log.Error(Logfornet.LogMessage("", "CreateNewCustomer", statusResult.Result, ""));
                        customerData = JsonConvert.DeserializeObject<CustomerProfile>(statusResult.Result.ToString());
                        return Json(customerData, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Log.Error(Logfornet.LogMessage("", "CreateNewCustomer", statusResult.Result, ""));
                        customerData.ExMessage = statusResult.Status;
                    }
                }
                else
                {
                    Log.Error(Logfornet.LogMessage("", "CreateNewCustomer", responseMessage.StatusCode, ""));
                    customerData.ExMessage = responseMessage.StatusCode.ToString();
                }
                return Json(new { data = customerData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                customerData.ExMessage = ex.Message;
                Log.Error(Logfornet.LogMessage("", "GetAllCustomer", ex.Message, ""));
                return Json(new { data = customerData }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Update Customer profile
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> UpdateCustomer(CustomerProfile model)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Customer/UpdateCustomer", model);

            var result = responseMessage.Content.ReadAsStringAsync().Result;
            statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
            if (responseMessage.IsSuccessStatusCode)
            {
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    return Json(statusResult, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(statusResult, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Log.Error(Logfornet.LogMessage(model.CompanyID, "UpdateCustomer", statusResult.Result, statusResult.Status));
            }
            return View("Error");
        }

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> DeleteCustomer(int CompanyID)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Customer/Delete/" + CompanyID, "");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    return Json(statusResult.Status, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(statusResult.Result, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Log.Error(Logfornet.LogMessage(CompanyID, "DeleteCustomer", statusResult.Result, statusResult.Status));
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }
    }
}