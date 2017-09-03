using log4net;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using TransForce.API.Entitys;
using TransForce.API.Models;
using TransForce.API.TF_BusinessLayer;
using TransForce.API.TF_Exceptions;
using static TransForce.API.TF_Exceptions.TFExceptions;

namespace TransForce.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/DriverInfo")]
    public class DriverInfoController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region bulkInsert
        /// <summary>
        /// Bulkupload Drivers
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadBulkDrivers")]
        public async Task<StatusResult> UploadBulkDrivers(UploadDrivers model)
        {
            StatusResult c = new StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList();
                    Log.Error(Logfornet.LogMessage(model.CompanyID, "UploadBulkDrivers", errors.ToArray(), ""));
                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    DriverConfig DriverConfig = new DriverConfig();
                    string UserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var res = await DriverConfig.UploadDrivers(model, UserID);
                    if (res != null)
                    {
                        Log.Error(Logfornet.LogMessage(model.CompanyID, "UploadBulkDrivers", "Please upload company logo", ""));
                        c.Status = Status.Success.ToString();
                        c.Result = res.DriverData;
                        return c;
                    }
                    else
                    {
                        Log.Warn(Logfornet.LogMessage(model.CompanyID, "UploadBulkDrivers", ErrorMessages.AlreadyExists, ""));
                        c.Status = Status.Fail.ToString();
                        c.Result = "Please upload standard file.";
                        return c;
                    }
                }
                else
                {
                    Log.Warn(Logfornet.LogMessage(model.CompanyID, "UploadBulkDrivers", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(model.CompanyID, "UploadBulkDrivers", ex.Message, ex.StackTrace));
                c.Status = ExceptionStatus.SqlException.ToString();
                c.StatusCode = (int)ExceptionStatus.SqlException;
                c.Result = "Please upload standard file.";
                return c;
            }
        }
        #endregion

        #region manual Insert
        /// <summary>
        /// Insert Driver manual
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertDriverinfo")]

        public async Task<StatusResult> InsertDriverinfo(Driver model)
        {
            StatusResult c = new StatusResult();
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
                    DriverConfig DriverConfig = new DriverConfig();
                    string UserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var res = await DriverConfig.InsertDriver(model, UserID);
                    if (res)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = "Driver information inserted successfully";
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "DOT Number does not belongs to this company or Same licence avilable in this company.";
                        return c;
                    }
                }
                else
                {
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }
        /// <summary>
        /// Update driver single
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateDriverInfo")]
        public async Task<StatusResult> UpdateDriverInfo(Driver model)
        {
            StatusResult c = new StatusResult();
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
                    DriverConfig DriverConfig = new DriverConfig();
                    string UserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var res = await DriverConfig.UpdateDriver(model, UserID);
                    if (res != null)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Company is not active";
                        return c;
                    }
                }
                else
                {
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }
        /// <summary>
        /// Edit Driver by company and driverID
        /// </summary>
        /// <param name="DriverId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditDriverInfo/{DriverId}/{CompanyId}")]
        public StatusResult EditDriverInfo(int DriverId, int CompanyId)
        {
            StatusResult c = new StatusResult();
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
                    DriverConfig DriverConfig = new DriverConfig();
                    var res = DriverConfig.EditDriver(DriverId, CompanyId);
                    if (res != null)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Company is not active";
                        return c;
                    }
                }
                else
                {
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }
        /// <summary>
        /// Delete Driver by Company and Id
        /// </summary>
        /// <param name="DriverId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteDriverInfo/{DriverId}/{CompanyId}")]
        public async Task<StatusResult> DeleteDriverInfo(int DriverId, int CompanyId)
        {
            StatusResult c = new StatusResult();
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
                    DriverConfig DriverConfig = new DriverConfig();
                    string UserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var res = await DriverConfig.DeleteDriver(DriverId, CompanyId, UserID);
                    if (res)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = "Driver deleted successfully";
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Company is not active";
                        return c;
                    }
                }
                else
                {
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }
        /// <summary>
        /// Get all drivers by Company
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDrivers/{CompanyId}")]
        public async Task<StatusResult> GetDrivers(int CompanyId)
        {
            StatusResult c = new StatusResult();
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
                    DriverConfig DriverConfig = new DriverConfig();
                    var res = await DriverConfig.GetDrivers(CompanyId);
                    if (res.Count > 0)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Drivers not avilable for this company";
                        return c;
                    }
                }
                else
                {
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }

        /// <summary>
        /// Get all drivers by Company
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetActiveDrivers/{CompanyId}")]
        public async Task<StatusResult> GetActiveDrivers(int CompanyId)
        {
            StatusResult c = new StatusResult();
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
                    DriverConfig DriverConfig = new DriverConfig();
                    var res = await DriverConfig.GetActiveDrivers(CompanyId);
                    if (res.Count > 0)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Drivers not avilable for this company";
                        return c;
                    }
                }
                else
                {
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }
        /// <summary>
        /// Get Drivers by first or second startwith method
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDriversByName/{CompanyId}/{Name}")]
        public async Task<StatusResult> GetDriversByName(int CompanyId, string Name)
        {
            StatusResult c = new StatusResult();
            try
            {
                if (!ModelState.IsValid && string.IsNullOrEmpty(Name.Trim()))
                {
                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    DriverConfig DriverConfig = new DriverConfig();
                    var res = await DriverConfig.GetDriversByName(CompanyId, Name);
                    if (res.Count > 0)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Drivers not avilable for this company";
                        return c;
                    }
                }
                else
                {
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }
        #endregion
    }
}
