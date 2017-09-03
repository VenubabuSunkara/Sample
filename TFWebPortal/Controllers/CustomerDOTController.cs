using DataTables.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TransForce.Web.Portal.Models;

namespace TransForce.Web.Portal.Controllers
{
    public class CustomerDOTController : Controller
    {
        private readonly HttpClient client;
        private readonly string _url = ConfigurationManager.AppSettings["webapibaseurl"];
        private StatusResult statusResult;
        private readonly UserModel userData;
        public CustomerDOTController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            userData = System.Web.HttpContext.Current.Session["UserData"] != null ? (UserModel)System.Web.HttpContext.Current.Session["UserData"] : null;
        }

        /// <summary>
        /// Add Dot To Customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("Create")]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> AddCustomerDOTNumber(CustomerDOTNumber model)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "CustomerDOT/Create", model);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    return Json(statusResult.Status, JsonRequestBehavior.AllowGet);
                }
                if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json(statusResult.Result, JsonRequestBehavior.AllowGet);
                }
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Partial view display in model popup
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin)]
        public PartialViewResult CreateDot()
        {
            var model = new CustomerDOTNumber();
            model.DOTStaus = "Create";
            return PartialView("_CreateDot", model);
        }

        /// <summary>
        /// Get all Customer DotNumbers
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> GetCustomerDOTNumber(string CustomerId)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "CustomerDOT/GetCustomerDOTByCustomerId/" + CustomerId, "");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    List<CustomerDOTNumber> customerData = JsonConvert.DeserializeObject<List<CustomerDOTNumber>>(statusResult.Result.ToString());
                    var jsonResult = Json(new { data = customerData }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else if (statusResult.Status.Equals(Status.Fail.ToString()))
                {
                    return Json(new { data = new CustomerDOTNumber() }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// partial view for EditDotNumber
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin)]
        public PartialViewResult EditDotNumber(int Id, int ItemId)
        {
            CustomerDOTNumber customerData = new CustomerDOTNumber();
            customerData.ItemID = ItemId;
            return PartialView("_CreateDot", customerData);
        }

        /// <summary>
        /// Update DotNumber
        /// </summary>
        /// <param name="customerVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> UpdateDotNumber(CustomerDOTNumber customerVM)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "CustomerDOT/Update", customerVM);
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
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete DotNumber
        /// </summary>
        /// <param name="customerVM"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin)]
        public async Task<ActionResult> DeleteDotNumber(int Id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "CustomerDOT/Delete/" + Id, Id);
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
            return Json("Error", JsonRequestBehavior.AllowGet);
        }
    }
}