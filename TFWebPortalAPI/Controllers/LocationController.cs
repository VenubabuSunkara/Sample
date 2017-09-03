using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using TransForce.API.App_Start;
using TransForce.API.Entitys;
using TransForce.API.Models;
using TransForce.API.TF_BusinessLayer;

namespace TransForce.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Location")]
    public class LocationController : ApiController
    {
        /// <summary>
        /// Create Location For Parent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddLocationParent")]
        public StatusResult AddLocationParent(Location_Configuration model)
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
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin))
                {
                    LocationConfig locationConfig = new LocationConfig();
                    model.CreatedBy = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var location = locationConfig.CreateLocationForParent(model);
                    if (location == null)
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = location;
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = location;
                        return c;
                    }
                }
                else
                {
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No authorization";
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
        /// Add Location Region
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddLocationGrandParent")]
        public StatusResult AddLocationGrandParent(Location_Configuration model)
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
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin))
                {
                    LocationConfig locationConfig = new LocationConfig();
                    model.CreatedBy = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var location = locationConfig.CreateLocationForGrandParent(model);
                    if (location == null)
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = location;
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = location;
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
        /// Get Location menu data
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("GetLocationConfigByCompanyID/{CompanyID}")]
        public StatusResult GetLocationConfigByCompanyID(int CompanyID)
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
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin))
                {
                    LocationConfig locationConfig = new LocationConfig();
                    var locationMenus = locationConfig.GetLocationConfigByCompanyID(CompanyID);
                    if (locationMenus.Count > 0)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = locationMenus;
                        return c;
                    }
                    c.Status = Status.NoResult.ToString();
                    c.Result = locationMenus;
                    return c;
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
        /// 
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeletingLocationConfig/{LocConfigID}/{CompanyID}")]
        public StatusResult DeletingLocationConfig(int LocConfigID, int CompanyID)
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
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin))
                {
                    LocationConfig locationConfig = new LocationConfig();
                    if (locationConfig.DeletingLocationConfig(LocConfigID, CompanyID))
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = "Delete Success";
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Unable to delete because parent is exists";
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


        #region  Create/Edit/Delete Locationdetails, Region and Area
        /// <summary>
        /// Creating Location Meta
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateLocationMeta")]
        public StatusResult CreateLocationMeta(LocationMetadata model)
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
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin))
                {
                    LocationConfig locationConfig = new LocationConfig();
                    model.CreatedBy = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    model.CreatedOn = DateTime.Now;
                    if (locationConfig.AddLocationMeta(model))
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Already Exists";
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = locationConfig;
                        return c;
                    }
                }
                else
                {
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "Access Denied";
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateLocationMeta")]
        public StatusResult UpdateLocationMeta(LocationMetadata model)
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
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin))
                {
                    LocationConfig locationConfig = new LocationConfig();
                    model.ModifiedBy = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    model.ModifiedOn = DateTime.Now;
                    var res = locationConfig.UpdateLocationMeta(model);
                    if (res == null)
                    {
                        c.Status = Status.NoAccess.ToString();
                        c.Result = "Already Exists";
                        return c;
                    }
                    else
                    {
                        c.Status = Status.NoAccess.ToString();
                        c.Result = res;
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

        #region bulkInsert
        /// <summary>
        /// Bulkupload locations csv and txt format
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [System.Web.Mvc.AsyncTimeout(600000)]
        [Route("UploadBulkLocations")]
        public async Task<StatusResult> UploadBulkLocations(UploadLocation model)
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
                    LocationConfig locationConfig = new LocationConfig();
                    string UserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var res = await locationConfig.UploadLocations(model, UserID);
                    if (res != null)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = res.Locationdata;
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Please upload standard file.";
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
                c.Result = ex.Message + "Please upload standard file.";
                return c;
            }
        }
        #endregion
        #region Manual insert location
        /// <summary>
        /// manual location insert
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertLocation")]
        public async Task<StatusResult> InsertLocation(Location model)
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
                    LocationConfig locationConfig = new LocationConfig();
                    string UserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var res = await locationConfig.AddLocation(model, UserID);
                    if (res)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = "Location Added Successfully";
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
        /// Update loacation single
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("UpdateLocation")]
        public async Task<StatusResult> UpdateLocation(Location model)
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
                    LocationConfig locationConfig = new LocationConfig();
                    string UserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var res = await locationConfig.UpdateLocation(model, UserID);
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
        /// Delete Location by id an dcompany
        /// </summary>
        /// <param name="LocId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteLocation/{LocId}/{CompanyId}")]
        public async Task<StatusResult> DeleteLocation(int LocId, int CompanyId)
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
                    LocationConfig locationConfig = new LocationConfig();
                    string UserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var res = await locationConfig.DeleteLocation(LocId, CompanyId, UserID);
                    if (res)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = "Location inactivated successfully";
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
        /// Edit location by id and company
        /// </summary>
        /// <param name="LocId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditLocation/{LocId}/{CompanyId}")]
        public StatusResult EditLocation(int LocId, int CompanyId)
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
                    LocationConfig locationConfig = new LocationConfig();
                    var res = locationConfig.EditLocation(LocId, CompanyId);
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
        /// Get location Companies
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetLocations/{CompanyId}")]
        public async Task<StatusResult> GetLocations(int CompanyId)
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
                    LocationConfig locConfig = new LocationConfig();
                    var res = await locConfig.GetLocations(CompanyId);
                    if (res.Count > 0)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = res;
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Locations not avilable for this company";
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