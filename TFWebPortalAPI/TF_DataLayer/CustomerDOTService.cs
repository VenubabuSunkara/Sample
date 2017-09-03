using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransForce.API.App_Start;
using TransForce.API.DTOS;
using TransForce.API.Entitys;
using TransForce.API.Models;
using TransForce.API.TF_InterfaceLayer;

namespace TransForce.API.TF_DataLayer
{
    public class CustomerDOTService : ICustomerDOTService
    {
        private readonly TFPMEntities context;
        private readonly bool IsActiveProfile;

        public CustomerDOTService()
        {
            context = new TFPMEntities();
            IsActiveProfile = true;
        }
        public CustomerDOTService(int CompanyID)
        {
            context = new Entitys.TFPMEntities();
            IsActiveProfile = context.CustomerProfiles.Any(x => x.CompanyID == CompanyID && x.IsDelete == false);
        }

        /// <summary>
        /// Get All CustomerDOTNumbers
        /// </summary>
        /// <returns></returns>
        public List<CustomerDOTNumberDTO> GetCustomerDOTNumbersAll()
        {
            if (IsActiveProfile)
            {
                return context.CustomerDOTNumbers.Where(x => x.IsDelete == false)
                     .Select(x => new CustomerDOTNumberDTO
                     {
                         ItemID = x.ItemID,
                         DOTNumber = x.DOTNumber,
                         DOTTag = x.DOTTag,
                         CustomerID = x.CustomerID,
                         CreatedOn = x.CreatedOn,
                         ModifiedOn = x.ModifiedOn,
                         IsActive = x.IsActive,
                         CreatedBy = x.CreatedBy,
                         ModifiedBy = x.ModifiedBy
                     }).ToList();
            }
            return null;
        }

        /// <summary>
        /// Get CustomerDOT by DOTNumber
        /// </summary>
        /// <returns></returns>
        public CustomerDOTNumberDTO GetByDOTNumber(int CompanyID, string DOTNumber)
        {
            if (IsActiveProfile)
            {
                return context.CustomerDOTNumbers.Where(o => o.DOTNumber == DOTNumber && o.CustomerID == CompanyID && o.IsDelete == false)
                     .Select(x => new CustomerDOTNumberDTO
                     {
                         ItemID = x.ItemID,
                         DOTNumber = x.DOTNumber,
                         DOTTag = x.DOTTag,
                         CustomerID = x.CustomerID,
                         CreatedOn = x.CreatedOn,
                         ModifiedOn = x.ModifiedOn,
                         IsActive = x.IsActive,
                         CreatedBy = x.CreatedBy,
                         ModifiedBy = x.ModifiedBy
                     }).FirstOrDefault();
            }
            return null;
        }

        /// <summary>
        /// Get DOTNumbers By CustomerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CustomerDOTNumberDTO> GetDOTNumbersByCustomerId(int itemID)
        {
            if (IsActiveProfile)
            {
                return context.CustomerDOTNumbers.Where(o => o.CustomerID == itemID && o.IsDelete == false)
                     .Select(x => new CustomerDOTNumberDTO
                     {
                         ItemID = x.ItemID,
                         DOTNumber = x.DOTNumber,
                         DOTTag = x.DOTTag,
                         CustomerID = x.CustomerID,
                         CreatedOn = x.CreatedOn,
                         ModifiedOn = x.ModifiedOn,
                         IsActive = x.IsActive,
                         CreatedBy = x.CreatedBy,
                         ModifiedBy = x.ModifiedBy
                     }).ToList();
            }
            return null;
        }

        /// <summary>
        /// Create CustomerDOTNumber
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CustomerDOTNumberDTO CreateCustomerDOTNumber(CustomerDOTNumber model)
        {
            if (IsActiveProfile)
            {
                var CDOTNumber = context.CustomerDOTNumbers.Where(o => o.DOTNumber == model.DOTNumber && o.CustomerID == model.CustomerID)
                    .Select(x => new CustomerDOTNumberDTO
                    {
                        ItemID = x.ItemID,
                        DOTNumber = x.DOTNumber,
                        DOTTag = x.DOTTag,
                        CustomerID = x.CustomerID,
                        CreatedOn = x.CreatedOn,
                        ModifiedOn = x.ModifiedOn,
                        IsActive = x.IsActive,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy
                    }).FirstOrDefault();
                if (CDOTNumber == null)
                {
                    var customerDOT = new CustomerDOTNumber
                    {
                        CustomerID = model.CustomerID,
                        DOTNumber = CommonFunctions.Trimstring(model.DOTNumber),
                        DOTTag = CommonFunctions.Trimstring(model.DOTTag),
                        IsActive = true,
                        CreatedOn = DateTime.Now,
                        CreatedBy = model.CreatedBy,
                        ModifiedBy = model.CreatedBy,
                        ModifiedOn = DateTime.Now,
                        IsDelete = false

                    };
                    context.CustomerDOTNumbers.Add(customerDOT);
                    context.SaveChanges();
                    var CustomerDOT = new CustomerDOTNumberDTO
                    {
                        ItemID = customerDOT.ItemID,
                        DOTNumber = customerDOT.DOTNumber,
                        DOTTag = customerDOT.DOTTag,
                        CustomerID = customerDOT.CustomerID,
                        CreatedOn = customerDOT.CreatedOn,
                        ModifiedOn = customerDOT.ModifiedOn,
                        IsActive = customerDOT.IsActive,
                        CreatedBy = customerDOT.CreatedBy,
                        ModifiedBy = customerDOT.ModifiedBy
                    };
                    return CustomerDOT;
                }
                else
                {
                    return CDOTNumber;
                }
            }
            return null;
        }

        /// <summary>
        /// Edit CustomerDOTNumber
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="RoleNames"></param>
        /// <returns></returns>
        public CustomerDOTNumberDTO EditCustomerDOTNumber(int CompanyID, int itemID)
        {
            if (IsActiveProfile)
            {
                var CDOTNumber = context.CustomerDOTNumbers.Where(o => o.ItemID == itemID && o.CustomerID == CompanyID)
                    .Select(x => new CustomerDOTNumberDTO
                    {
                        ItemID = x.ItemID,
                        DOTNumber = x.DOTNumber,
                        DOTTag = x.DOTTag,
                        CustomerID = x.CustomerID,
                        CreatedOn = x.CreatedOn,
                        ModifiedOn = x.ModifiedOn,
                        IsActive = x.IsActive,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy
                    }).FirstOrDefault();
                if (CDOTNumber != null)
                {
                    return CDOTNumber;
                }
                else
                {
                    return CDOTNumber;
                }
            }
            return null;
        }

        /// <summary>
        /// Update CustomerDOTNumber
        /// </summary>
        /// <param name="model"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public CustomerDOTNumberDTO UpdateCustomerDOTNumber(CustomerDOTNumber model)
        {
            if (IsActiveProfile)
            {
                var CDOTNumber = context.CustomerDOTNumbers.Where(o => o.ItemID == model.ItemID && o.CustomerID == model.CustomerID).FirstOrDefault();
                if (CDOTNumber != null)
                {
                    if (CDOTNumber.DOTNumber == model.DOTNumber)
                    {
                        CDOTNumber.DOTTag = CommonFunctions.Trimstring(model.DOTTag);
                        CDOTNumber.IsActive = model.IsActive;
                        CDOTNumber.ModifiedOn = DateTime.Now;
                        CDOTNumber.ModifiedBy = model.ModifiedBy;
                        context.SaveChanges();
                    }
                    else
                    {
                        //New Dot data Update 
                        if (!context.CustomerDOTNumbers.Where(o => o.DOTNumber == model.DOTNumber).Any())
                        {
                            CDOTNumber.DOTNumber = model.DOTNumber;
                            CDOTNumber.DOTTag = CommonFunctions.Trimstring(model.DOTTag);
                            CDOTNumber.IsActive = model.IsActive;
                            CDOTNumber.ModifiedOn = DateTime.Now;
                            CDOTNumber.ModifiedBy = model.ModifiedBy;
                            context.SaveChanges();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    var CustomerDOT = new CustomerDOTNumberDTO
                    {
                        ItemID = CDOTNumber.ItemID,
                        DOTNumber = CDOTNumber.DOTNumber,
                        DOTTag = CDOTNumber.DOTTag,
                        CustomerID = CDOTNumber.CustomerID,
                        CreatedOn = CDOTNumber.CreatedOn,
                        ModifiedOn = CDOTNumber.ModifiedOn,
                        IsActive = CDOTNumber.IsActive,
                        CreatedBy = CDOTNumber.CreatedBy,
                        ModifiedBy = CDOTNumber.ModifiedBy
                    };
                    return CustomerDOT;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Delete CustomerDOTNumber
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteCustomerDOTNumber(CustomerDOTNumber model)
        {
            var CDOTNumber = context.CustomerDOTNumbers.Where(o => o.ItemID == model.ItemID).FirstOrDefault();
            if (CDOTNumber != null)
            {
                CDOTNumber.IsActive = false;
                CDOTNumber.IsDelete = true;
                CDOTNumber.ModifiedBy = model.ModifiedBy;
                CDOTNumber.ModifiedOn = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}