using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using TransForce.API.DTOS;
using TransForce.API.Entitys;
using TransForce.API.Models;
using TransForce.API.TF_BusinessLayer;
using TransForce.API.TF_Exceptions;
using static TransForce.API.TF_Exceptions.TFExceptions;

namespace TransForce.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Add New customer in cpm portal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/Customer/AddCustomer
        [HttpPost]
        [Route("AddCustomer")]
        [Authorize]
        public StatusResult AddCustomer(CustomerProfile model)
        {
            StatusResult c = new Models.StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList();
                    Log.Error(Logfornet.LogMessage(model.CustomerName, "CreateCustomer", errors.ToArray(), ""));
                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (string.IsNullOrEmpty(model.CompanyLogo))
                {
                    Log.Error(Logfornet.LogMessage(model.CustomerName, "CreateCustomer", "Please upload company logo", ""));
                    c.Status = Status.BadRequest.ToString();
                    c.Result = "Please upload companylogo";
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin))
                {
                    Customer Cus = new Customer();
                    var res = Cus.AddCustomer(model, ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value);
                    if (res != null)
                    {
                        Log.Info(Logfornet.LogMessage(model.CustomerName, "CreateCustomer", ErrorMessages.Success, ""));
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        Log.Warn(Logfornet.LogMessage(model.CustomerName, "CreateCustomer", ErrorMessages.AlreadyExists, ""));
                        c.Status = Status.Fail.ToString();
                        c.Result = "Aready Exists";
                        return c;
                    }
                }
                else
                {
                    Log.Error(Logfornet.LogMessage(model.CustomerName, "CreateCustomer", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (TFSqlException ex)
            {
                Log.Error(Logfornet.LogMessage(model.CustomerName, "CreateCustomer", ex.Message, ex.StackTrace));
                c.Status = ExceptionStatus.SqlException.ToString();
                c.StatusCode = (int)ExceptionStatus.SqlException;
                c.Result = ex.InnerException;
                return c;
            }
        }
        /// <summary>
        /// Update Customer Details by roles
        /// </summary>
        /// <param name="model"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        // PUT: api/Customer/Update
        [HttpPost]
        [Route("UpdateCustomer")]
        [Authorize]
        public StatusResult UpdateCustomer(CustomerProfile model)
        {
            StatusResult c = new Models.StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList();
                    Log.Error(Logfornet.LogMessage(model.CustomerName, "UpdateCustomer", errors.ToArray(), ""));
                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    Customer Cus = new Customer();
                    List<string> RoleNames = ClaimsPrincipal.Current.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
                    var res = Cus.UpdateCustomer(model, String.Join(",", RoleNames.ToArray()), ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value);
                    if (res == null)
                    {
                        Log.Warn(Logfornet.LogMessage(model.CustomerName, "UpdateCustomer", ErrorMessages.AlreadyExists, ""));
                        c.Status = Status.Fail.ToString();
                        c.Result = "Already Exists";
                        return c;
                    }
                    else
                    {
                        Log.Info(Logfornet.LogMessage(model.CustomerName, "UpdateCustomer", ErrorMessages.Success, ""));
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                }
                else
                {
                    Log.Error(Logfornet.LogMessage(model.CustomerName, "UpdateCustomer", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(model.CustomerName, "UpdateCustomer", ex.Message, ex.StackTrace));
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;

            }
        }

        /// <summary>
        /// Update Customer Details by roles
        /// </summary>
        /// <param name="model"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        // PUT: api/Customer/Update
        [HttpPost]
        [Route("EditCustomer/{CompanyId}")]
        // [Authorize(Roles = "Super Admin,Portal Admin,Account Admin")]
        public StatusResult EditCustomer(int CompanyId)
        {
            StatusResult c = new Models.StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList();
                    Log.Error(Logfornet.LogMessage(CompanyId, "EditCustomer", errors.ToArray(), ""));
                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    Customer Cus = new Customer();
                    string UserId = string.Empty;
                    if (User.IsInRole(Roles.AccountAdmin))
                    {
                        UserId = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    }
                    var res = Cus.EditCustomer(CompanyId, UserId);
                    if (res != null)
                    {
                        Log.Info(Logfornet.LogMessage(CompanyId, "EditCustomer", ErrorMessages.Success, ""));
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        Log.Warn(Logfornet.LogMessage(CompanyId, "UpdateCustomer", ErrorMessages.NoResults, ""));
                        c.Status = Status.Fail.ToString();
                        c.Result = "No Customer found";
                        return c;
                    }
                }
                else
                {
                    Log.Error(Logfornet.LogMessage(CompanyId, "EditCustomer", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(CompanyId, "EditCustomer", ex.Message, ex.StackTrace));
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;

            }
        }
        /// <summary>
        /// Delete Customers into CPM Portal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // DELETE: api/Delete/5
        [HttpPost]
        [Route("Delete/{CompanyId}")]
        // [Authorize(Roles = "Super Admin,Portal Admin")]
        public StatusResult Delete(int CompanyID)
        {
            StatusResult c = new Models.StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin))
                {
                    Customer Cus = new Customer();
                    if (Cus.DeleteCustomer(CompanyID, ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value))
                    {
                        Log.Info(Logfornet.LogMessage(CompanyID, "DeleteCustomer", ErrorMessages.Success, ""));
                        c.Status = Status.Success.ToString();
                        c.Result = true;
                        return c;
                    }
                    else
                    {
                        Log.Warn(Logfornet.LogMessage(CompanyID, "DeleteCustomer", ErrorMessages.NoResults, ""));
                        c.Status = Status.Fail.ToString();
                        c.Result = "Not found";
                        return c;
                    }
                }
                else
                {
                    Log.Error(Logfornet.LogMessage(CompanyID, "DeleteCustomer", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(CompanyID, "DeleteCustomer", ex.Message, ex.StackTrace));
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCustomerProfileByUserID")]
        // [Authorize(Roles = "Super Admin,Portal Admin")]
        public StatusResult GetCustomerProfileByUserID()
        {
            StatusResult c = new Models.StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                string UserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                Customer Cus = new Customer();
                List<CustomerProfileDTO> cusdetails = new List<CustomerProfileDTO>();
                if (User.IsInRole(Roles.AccountAdmin))
                {
                    cusdetails = Cus.GetCustomerProfileByUserID(UserID);
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin))
                {
                    cusdetails = Cus.GetCustomerProfileBySuperPortalAdmin(UserID);
                }
                if (cusdetails == null)
                {
                    Log.Error(Logfornet.LogMessage(UserID, "GetCustomersByUserid", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
                else
                {
                    Log.Info(Logfornet.LogMessage(UserID, "GetCustomerByUserID", ErrorMessages.Success, ""));
                    c.Status = Status.Success.ToString();
                    c.Result = cusdetails;
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value, "GetCustomersByUser", ex.Message, ex.StackTrace));
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckCustomerExists/{CustomerName}")]
        // [Authorize(Roles = "Super Admin,Portal Admin")]
        public StatusResult CheckCustomerExists(string CustomerName)
        {
            StatusResult c = new Models.StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    Customer Cus = new Customer();
                    var res = Cus.CheckCustomerExists(CustomerName);
                    if (res == null)
                    {
                        Log.Info(Logfornet.LogMessage(CustomerName, "CustomerExists", ErrorMessages.Success, ""));
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        Log.Warn(Logfornet.LogMessage(CustomerName, "CustomerExists", ErrorMessages.NoResults, ""));
                        c.Status = Status.NoResult.ToString();
                        c.Result = "Company not avilable or not active";
                        return c;
                    }
                }
                else
                {
                    Log.Error(Logfornet.LogMessage(CustomerName, "CustomerExists", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(CustomerName, "CustomerExists", ex.Message, ex.StackTrace));
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }
    }
}
