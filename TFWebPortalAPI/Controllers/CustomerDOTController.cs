using log4net;
using System;
using System.Security.Claims;
using System.Web.Http;
using TransForce.API.Entitys;
using TransForce.API.Models;
using TransForce.API.TF_BusinessLayer;

namespace TransForce.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/CustomerDOT")]
    public class CustomerDOTController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get All CustomerDOTNumbers
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCustomerDOTNumbers")]
        public StatusResult GetCustomerDOTNumbers()
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
                    CustomerDOT cusDOT = new CustomerDOT();
                    //var CDOT = cusDOT.GetAll();
                    var CDOT = "";
                    if (CDOT == null)
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "There is no Customer DOTNumber data.";
                    }
                    else
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = CDOT;
                    }
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
        /// Get CustomerDOT by DOTNumber
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCustomerDOT/{CompanyID}/{DOTNumber}")]
        public StatusResult GetCustomerDOT(int CompanyID, string DOTNumber)
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
                    CustomerDOT cusDOT = new CustomerDOT();

                    var CDOT = cusDOT.GetCustomerDOTByDOTNumber(CompanyID, DOTNumber);
                    if (CDOT == null)
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Customer DOTNumber Not Found.";
                    }
                    else
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = CDOT;
                    }
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
        /// Get CustomerDOT by CustomerId
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCustomerDOTByCustomerId/{CustomerId}")]
        public StatusResult GetCustomerDOTByCustomerId(int CustomerId)
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
                    CustomerDOT cusDOT = new CustomerDOT();

                    var CDOT = cusDOT.GetCustomerDOTByCustomerId(CustomerId);
                    if (CDOT == null || CDOT.Count == 0)
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Customer Id or DOTNumber Not Found.";
                    }
                    else
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = CDOT;
                    }
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
        /// Add Customer DOTNumber
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/CustomerDOT/Create
        [HttpPost]
        [Route("Create")]
        public StatusResult AddCustomerDOTNumber(CustomerDOTNumber model)
        {
            StatusResult c = new StatusResult();
            //try
            //{
            if (!ModelState.IsValid)
            {
                c.Status = Status.BadRequest.ToString();
                c.Result = BadRequest();
                return c;
            }
            if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin))
            {
                CustomerDOT cusDOT = new CustomerDOT();
                var custDOT = GetCustomerDOT(model.CustomerID, model.DOTNumber);
                if (custDOT.Status != Status.Fail.ToString())
                {
                    c.Status = Status.Fail.ToString();
                    c.Result = "Customer DOTNumber already exist.";
                }
                else
                {
                    model.CreatedBy = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var CDOT = cusDOT.AddCustomerDOT(model);
                    c.Status = Status.Success.ToString();
                    c.Result = CDOT;
                }

                return c;
            }
            else
            {
                c.Status = Status.NoAccess.ToString();
                c.Result = "No Access";
                return c;
            }
            //}
            //catch (Exception ex)
            //{
            //    c.Status = Status.InternalServerError.ToString();
            //    c.Result = ex.InnerException;
            //    return c;
            //}
        }

        /// <summary>
        /// Edit CustomerDOTNumber Details
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        // PUT: api/CustomerDOT/Edit
        [HttpPost]
        [Route("Edit/{CompanyID}/{itemID}")]
        public StatusResult EditCustomerDOTNumber(int CompanyID, int itemID)
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
                    CustomerDOT cusDOT = new CustomerDOT();
                    var CDOT = cusDOT.EditCustomerDOT(CompanyID, itemID);
                    if (CDOT == null)
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Customer DOTNumber object Not Found.";
                    }
                    else
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = CDOT;
                    }
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
        /// Update CustomerDOTNumber Details
        /// </summary>
        /// <param name="model"></param>     
        /// <returns></returns>
        // PUT: api/CustomerDOT/Update
        [HttpPost]
        [Route("Update")]
        public StatusResult UpdateCustomerDOTNumber(CustomerDOTNumber model)
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
                    CustomerDOT cusDOT = new CustomerDOT();
                    model.ModifiedBy = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var CDOT = cusDOT.UpdateCustomerDOT(model);
                    if (CDOT == null)
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Customer DOTNumber already exists.";
                    }
                    else
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = CDOT;
                    }
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
        /// Delete CustomerDOTNumber
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // DELETE: api/CustomerDOT/Delete
        [HttpPost]
        [Route("Delete/{itemID}")]
        public StatusResult Delete(int itemID)
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
                    CustomerDOTNumber model = new CustomerDOTNumber();
                    CustomerDOT cusDOT = new CustomerDOT();
                    model.ItemID = itemID;
                    model.ModifiedBy = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var CDOT = cusDOT.DeleteCustomerDOT(model);
                    if (CDOT == false)
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "Customer DOTNumber object Not Found.";
                    }
                    else
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = CDOT;
                    }
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
    }
}
