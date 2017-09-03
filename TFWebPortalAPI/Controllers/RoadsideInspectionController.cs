using log4net;
using System;
using System.Collections.Generic;
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
    [RoutePrefix("api/RoadsideInspection")]
    public class RoadsideInspectionController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Bulk upload inspection entries
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        #region BulkUpload
        [HttpPost]
        [Route("UploadInspectionXml")]
        public async Task<StatusResult> UploadInspectionXml(Inspection model)
        {
            StatusResult c = new StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList();
                    Log.Error(Logfornet.LogMessage(model.CompanyId, "UploadInspectionXml", errors.ToArray(), ""));

                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    InspectionConfig InspectionConfig = new InspectionConfig();
                    string UserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var res = await InspectionConfig.UploadRoadsideInspection(model, UserID);
                    if (res.Contains("Successfully"))
                    {
                        Log.Info(Logfornet.LogMessage(model.CompanyId, "UploadInspectionXml", ErrorMessages.Success, ""));
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        Log.Warn(Logfornet.LogMessage(model.CompanyId, "UploadInspectionXml", ErrorMessages.NoResults, ""));
                        c.Status = Status.Fail.ToString();
                        c.Result = res;
                        return c;
                    }
                }
                else
                {
                    Log.Warn(Logfornet.LogMessage(model.CompanyId, "UploadInspectionXml", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(model.CompanyId, "UploadInspectionXml", ex.Message, ex.StackTrace));
                c.Status = ExceptionStatus.SqlException.ToString();
                c.StatusCode = (int)ExceptionStatus.SqlException;
                c.Result = ex.InnerException;
                return c;
            }
        }
        /// <summary>
        /// Get all inspections by Date
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="InspectionDate"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("GetInspectionByDate")]
        public async Task<StatusResult> GetInspectionByDate(InspectionDriver inspection)
        {
            StatusResult c = new StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList();
                    Log.Error(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionByDate", errors.ToArray(), ""));

                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    InspectionConfig InspectionConfig = new InspectionConfig();
                    var res = await InspectionConfig.GetInspectionByDate(inspection.CompanyId, inspection.InspectionDate);
                    if (res != null)
                    {
                        Log.Info(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionByDate", ErrorMessages.Success, ""));
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        Log.Warn(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionByDate", ErrorMessages.NoResults, ""));
                        c.Status = Status.Fail.ToString();
                        c.Result = "Company is not active";
                        return c;
                    }
                }
                else
                {
                    Log.Warn(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionByDate", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionByDate", ex.Message, ex.StackTrace));
                c.Status = ExceptionStatus.SqlException.ToString();
                c.StatusCode = (int)ExceptionStatus.SqlException;
                c.Result = ex.InnerException;
                return c;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="InspectionId"></param>
        /// <returns></returns>
        #region
        [HttpPost]
        [Route("GetInspectionById")]
        public async Task<StatusResult> GetInspectionById(InspectionDriver inspection)
        {
            StatusResult c = new StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList();
                    Log.Error(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionById", errors.ToArray(), ""));

                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    InspectionConfig InspectionConfig = new InspectionConfig();
                    var res = await InspectionConfig.GetInspectionById(inspection.CompanyId, inspection.InspectionId);
                    if (res != null)
                    {
                        Log.Info(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionById", ErrorMessages.Success, ""));
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        Log.Warn(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionById", ErrorMessages.NoResults, ""));
                        c.Status = Status.Fail.ToString();
                        c.Result = "Company is not active";
                        return c;
                    }
                }
                else
                {
                    Log.Warn(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionById", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionById", ex.Message, ex.StackTrace));
                c.Status = ExceptionStatus.SqlException.ToString();
                c.StatusCode = (int)ExceptionStatus.SqlException;
                c.Result = ex.InnerException;
                return c;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="InspectionId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetInspectionDetailsByDriverName")]
        public StatusResult GetInspectionDetailsByDriverName(InspectionDriver inspection)
        {
            StatusResult c = new StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList();
                    Log.Error(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionDetailsByDriverName", errors.ToArray(), ""));

                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    InspectionConfig InspectionConfig = new InspectionConfig();
                    var res = InspectionConfig.GetInspectionDetailsByDriverName(inspection.DriverLicence, inspection.InspectionDate, inspection.CompanyId);
                    if (res != null)
                    {
                        Log.Info(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionDetailsByDriverName", ErrorMessages.Success, ""));
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        Log.Warn(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionDetailsByDriverName", ErrorMessages.NoResults, ""));
                        c.Status = Status.Fail.ToString();
                        c.Result = "Company is not active";
                        return c;
                    }
                }
                else
                {
                    Log.Warn(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionDetailsByDriverName", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(inspection.CompanyId, "GetInspectionDetailsByDriverName", ex.Message, ex.StackTrace));
                c.Status = ExceptionStatus.SqlException.ToString();
                c.StatusCode = (int)ExceptionStatus.SqlException;
                c.Result = ex.InnerException;
                return c;
            }
        }

        [HttpPost]
        [Route("InsertInspectionEntry")]
        public StatusResult InsertInspectionEntry(ManualRSI Rsi)
        {
            StatusResult c = new StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList();
                    Log.Error(Logfornet.LogMessage(Rsi.CompanyID, "InsertInspectionEntry", errors.ToArray(), ""));

                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    InspectionConfig InspectionConfig = new InspectionConfig();
                    Rsi.CreatedBy = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    Rsi.ModifiedBy = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    Rsi.CreatedOn = DateTime.Now;
                    Rsi.ModifiedOn = DateTime.Now;
                    var res = InspectionConfig.InsertInspectionEntry(Rsi);
                    Log.Info(Logfornet.LogMessage(Rsi.CompanyID, "InsertInspectionEntry", ErrorMessages.Success, ""));
                    c.Status = Status.Success.ToString();
                    c.Result = res;
                    return c;
                }
                else
                {
                    Log.Warn(Logfornet.LogMessage(Rsi.CompanyID, "InsertInspectionEntry", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(Rsi.CompanyID, "InsertInspectionEntry", ex.Message, ex.StackTrace));
                c.Status = ExceptionStatus.SqlException.ToString();
                c.StatusCode = (int)ExceptionStatus.SqlException;
                c.Result = ex.InnerException;
                return c;
            }
        }

        [HttpPost]
        [Route("UploadInspectionDocumnetss")]
        public async Task<StatusResult> UploadInspectionDocumnets(List<BlobUploadModel> documentData)
        {
            StatusResult c = new StatusResult();
            var CompanyId = documentData.FirstOrDefault().CompanyId;
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList();
                    Log.Error(Logfornet.LogMessage(CompanyId, "UploadInspectionDocumnets", errors.ToArray(), ""));

                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    InspectionConfig InspectionConfig = new InspectionConfig();
                    await InspectionConfig.UploadInspectionDocumnets(documentData, CompanyId);
                    Log.Info(Logfornet.LogMessage(CompanyId, "UploadInspectionDocumnets", ErrorMessages.Success, ""));
                    c.Status = Status.Success.ToString();
                    c.Result = "Inserted Successfully";
                    return c;
                }
                else
                {
                    Log.Warn(Logfornet.LogMessage(CompanyId, "UploadInspectionDocumnets", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(CompanyId, "UploadInspectionDocumnets", ex.Message, ex.StackTrace));
                c.Status = ExceptionStatus.SqlException.ToString();
                c.StatusCode = (int)ExceptionStatus.SqlException;
                c.Result = ex.InnerException;
                return c;
            }
        }

        [HttpPost]
        [Route("DeleteDocument")]
        public async Task<StatusResult> DeleteDocument(BlobUploadModel documentData)
        {
            StatusResult c = new StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList();
                    Log.Error(Logfornet.LogMessage(documentData.CompanyId, "DeleteDocument", errors.ToArray(), ""));

                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    InspectionConfig InspectionConfig = new InspectionConfig();
                    await InspectionConfig.DeleteDocument(documentData);
                    Log.Info(Logfornet.LogMessage(documentData.CompanyId, "DeleteDocument", ErrorMessages.Success, ""));
                    c.Status = Status.Success.ToString();
                    c.Result = "Inserted Successfully";
                    return c;
                }
                else
                {
                    Log.Warn(Logfornet.LogMessage(documentData.CompanyId, "DeleteDocument", ErrorMessages.NoAccessDenied, ""));
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logfornet.LogMessage(documentData.CompanyId, "DeleteDocument", ex.Message, ex.StackTrace));
                c.Status = ExceptionStatus.SqlException.ToString();
                c.StatusCode = (int)ExceptionStatus.SqlException;
                c.Result = ex.InnerException;
                return c;
            }
        }
        #endregion
    }
}
