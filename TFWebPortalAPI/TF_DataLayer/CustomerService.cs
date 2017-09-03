using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TransForce.API.App_Start;
using TransForce.API.DTOS;
using TransForce.API.Entitys;
using TransForce.API.Models;
using TransForce.API.TF_BusinessLayer;
using TransForce.API.TF_InterfaceLayer;

namespace TransForce.API.TF_DataLayer
{
    public class CustomerService : ICustomerService
    {
        public CustomerProfileDTO CheckCustomerExists(string CustomerName)
        {
            TFPMEntities context = new TFPMEntities();
            var CProfile = context.CustomerProfiles.Where(o => o.CustomerName.Equals(CustomerName) && o.IsDelete == false)
                .Select(x => new CustomerProfileDTO
                {
                    CompanyID = x.CompanyID,
                    CustomerName = x.CustomerName,
                    City = x.City,
                    State = x.State,
                    AccountNumber = x.AccountNumber,
                    AccountOwnerEmail = x.AccountOwnerEmail,
                    AccountOwnerName = x.AccountOwnerName,
                    AddressLine1 = x.AddressLine1,
                    AddressLine2 = x.AddressLine2,
                    ZipCode = x.ZipCode,
                    CompanyLogo = x.CompanyLogo,
                    CreatedOn = x.CreatedOn,
                    DotCount = x.CustomerDOTNumbers.Count(a => a.IsDelete == false),
                    UserCount = x.AspNetUsers.Count(a => a.IsActive == true),
                    ModifiedOn = x.ModifiedOn,
                    isActive = x.isActive,
                    CreatedBy = x.CreatedBy,
                    ModifiedBy = x.ModifiedBy
                }).FirstOrDefault();
            if (CProfile != null)
            {
                return CProfile;
            }
            return null;
        }
        /// <summary>
        /// Create Customer/Company Profiler when Initial Time
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CustomerProfileDTO CreateCustomerProfile(CustomerProfile model, string UserID)
        {
            TFPMEntities context = new TFPMEntities();
            var CProfile = context.CustomerProfiles.Where(o => o.CustomerName.Equals(model.CustomerName))
                .Select(x => new CustomerProfileDTO
                {
                    CompanyID = x.CompanyID,
                    CustomerName = x.CustomerName,
                    City = x.City,
                    State = x.State,
                    AccountNumber = x.AccountNumber,
                    AccountOwnerEmail = x.AccountOwnerEmail,
                    AccountOwnerName = x.AccountOwnerName,
                    AddressLine1 = x.AddressLine1,
                    AddressLine2 = x.AddressLine2,
                    ZipCode = x.ZipCode,
                    CompanyLogo = x.CompanyLogo,
                    CreatedOn = x.CreatedOn,
                    DotCount = x.CustomerDOTNumbers.Count(a => a.IsDelete == false),
                    UserCount = x.AspNetUsers.Count(a => a.IsActive == true),
                    ModifiedOn = x.ModifiedOn,
                    isActive = x.isActive,
                    CreatedBy = x.CreatedBy,
                    ModifiedBy = x.ModifiedBy

                }).FirstOrDefault(); ;
            if (CProfile == null)
            {
                var customer = new CustomerProfile
                {
                    CustomerName = model.CustomerName.Trim(),
                    CompanyLogo = CommonFunctions.Trimstring(model.CompanyLogo),
                    AccountOwnerEmail = CommonFunctions.Trimstring(model.AccountOwnerEmail),
                    AccountOwnerName = CommonFunctions.Trimstring(model.AccountOwnerName),
                    AddressLine1 = CommonFunctions.Trimstring(model.AddressLine1),
                    AddressLine2 = CommonFunctions.Trimstring(model.AddressLine2),
                    City = CommonFunctions.Trimstring(model.City),
                    State = CommonFunctions.Trimstring(model.State),
                    ZipCode = CommonFunctions.Trimstring(model.ZipCode),
                    CreatedBy = UserID,
                    CreatedOn = DateTime.Now,
                    isActive = true,
                    IsDelete = false

                };
                context.CustomerProfiles.Add(customer);
                context.SaveChanges();
                customer.AccountNumber = model.CustomerName.Trim()[0].ToString().ToLower() + customer.CompanyID.ToString();//Account NUmber Generate logic                
                context.SaveChanges();
                LocationConfig loc = new TF_BusinessLayer.LocationConfig();
                loc.CreateLocationChild(customer.CompanyID, UserID);
                var CustomerDTO = new CustomerProfileDTO
                {
                    CompanyID = customer.CompanyID,
                    CustomerName = customer.CustomerName,
                    City = customer.City,
                    State = customer.State,
                    AccountNumber = customer.AccountNumber,
                    AccountOwnerEmail = customer.AccountOwnerEmail,
                    AccountOwnerName = customer.AccountOwnerName,
                    AddressLine1 = customer.AddressLine1,
                    AddressLine2 = customer.AddressLine2,
                    ZipCode = customer.ZipCode,
                    CompanyLogo = customer.CompanyLogo,
                    CreatedOn = customer.CreatedOn,
                    DotCount = customer.CustomerDOTNumbers.Count(a => a.IsDelete == false),
                    UserCount = customer.AspNetUsers.Count(a => a.IsActive == true),
                    ModifiedOn = customer.ModifiedOn,
                    isActive = customer.isActive,
                    CreatedBy = customer.CreatedBy,
                    ModifiedBy = customer.ModifiedBy
                };
                return CustomerDTO;
            }
            else
            {
                return CProfile;
            }
        }
        /// <summary>
        /// Update Customer Profile
        /// </summary>
        /// <param name="model"></param>
        /// <param name="RoleNames"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public CustomerProfileDTO UpdateCustomerProfile(CustomerProfile model, string RoleNames, string UserID)
        {

            TFPMEntities context = new TFPMEntities();
            var CProfile = context.CustomerProfiles.Where(o => o.CompanyID.Equals(model.CompanyID)).FirstOrDefault();
            if (CProfile != null)
            {
                if (RoleNames.Contains(Roles.PortalAdmin) || RoleNames.Contains(Roles.SuperAdmin))
                {
                    //Same Company Data Update                   
                    if (CProfile.CustomerName == model.CustomerName)
                    {
                        CProfile.CompanyLogo = CommonFunctions.Trimstring(model.CompanyLogo);
                        CProfile.AccountOwnerEmail = CommonFunctions.Trimstring(model.AccountOwnerEmail);
                        CProfile.AccountOwnerName = CommonFunctions.Trimstring(model.AccountOwnerName);
                        CProfile.AddressLine1 = CommonFunctions.Trimstring(model.AddressLine1);
                        CProfile.AddressLine2 = CommonFunctions.Trimstring(model.AddressLine2);
                        CProfile.City = CommonFunctions.Trimstring(model.City);
                        CProfile.State = CommonFunctions.Trimstring(model.State);
                        CProfile.ZipCode = CommonFunctions.Trimstring(model.ZipCode);
                        CProfile.ModifiedOn = DateTime.Now;
                        CProfile.ModifiedBy = UserID;
                        CProfile.isActive = model.isActive;
                    }
                    else
                    {
                        //New company data Update 
                        if (!context.CustomerProfiles.Where(o => o.CustomerName == model.CustomerName).Any())
                        {
                            CProfile.CustomerName = CommonFunctions.Trimstring(model.CustomerName);
                            CProfile.CompanyLogo = CommonFunctions.Trimstring(model.CompanyLogo);
                            CProfile.AccountOwnerEmail = CommonFunctions.Trimstring(model.AccountOwnerEmail);
                            CProfile.AccountOwnerName = CommonFunctions.Trimstring(model.AccountOwnerName);
                            CProfile.AddressLine1 = CommonFunctions.Trimstring(model.AddressLine1);
                            CProfile.AddressLine2 = CommonFunctions.Trimstring(model.AddressLine2);
                            CProfile.City = CommonFunctions.Trimstring(model.City);
                            CProfile.State = CommonFunctions.Trimstring(model.State);
                            CProfile.ZipCode = CommonFunctions.Trimstring(model.ZipCode);
                            CProfile.ModifiedOn = DateTime.Now;
                            CProfile.ModifiedBy = UserID;
                            CProfile.isActive = model.isActive;
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
                else
                {
                    CProfile.AddressLine1 = CommonFunctions.Trimstring(model.AddressLine1);
                    CProfile.AddressLine2 = CommonFunctions.Trimstring(model.AddressLine2);
                    CProfile.City = CommonFunctions.Trimstring(model.City);
                    CProfile.State = CommonFunctions.Trimstring(model.State);
                    CProfile.ZipCode = CommonFunctions.Trimstring(model.ZipCode);
                    CProfile.ModifiedOn = DateTime.Now;
                    CProfile.ModifiedBy = UserID;
                }
                context.SaveChanges();
                var CustomerDTO = new CustomerProfileDTO
                {
                    CompanyID = CProfile.CompanyID,
                    CustomerName = CProfile.CustomerName,
                    City = CProfile.City,
                    State = CProfile.State,
                    AccountNumber = CProfile.AccountNumber,
                    AccountOwnerEmail = CProfile.AccountOwnerEmail,
                    AccountOwnerName = CProfile.AccountOwnerName,
                    AddressLine1 = CProfile.AddressLine1,
                    AddressLine2 = CProfile.AddressLine2,
                    ZipCode = CProfile.ZipCode,
                    CompanyLogo = CProfile.CompanyLogo,
                    CreatedOn = CProfile.CreatedOn,
                    DotCount = CProfile.CustomerDOTNumbers.Count(a => a.IsDelete == false),
                    UserCount = CProfile.AspNetUsers.Count(a => a.IsActive == true),
                    ModifiedOn = CProfile.ModifiedOn,
                    isActive = CProfile.isActive,
                    CreatedBy = CProfile.CreatedBy,
                    ModifiedBy = CProfile.ModifiedBy
                };
                return CustomerDTO;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Edit Customere Profile
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public CustomerProfileDTO EditCustomerProfile(int Id, string UserId)
        {

            TFPMEntities context = new TFPMEntities();

            var accountNO = string.Empty;
            if (!string.IsNullOrEmpty(UserId))
            {
                accountNO = context.AspNetUsers.FirstOrDefault(x => x.Id.Equals(UserId) && x.IsActive.Equals(true)).AccountNumber;
                return context.CustomerProfiles.Where(y => y.CompanyID == Id && y.IsDelete == false && y.AccountNumber == accountNO)
                    .Select(x => new CustomerProfileDTO
                    {
                        CompanyID = x.CompanyID,
                        CustomerName = x.CustomerName,
                        City = x.City,
                        State = x.State,
                        AccountNumber = x.AccountNumber,
                        AccountOwnerEmail = x.AccountOwnerEmail,
                        AccountOwnerName = x.AccountOwnerName,
                        AddressLine1 = x.AddressLine1,
                        AddressLine2 = x.AddressLine2,
                        ZipCode = x.ZipCode,
                        CompanyLogo = x.CompanyLogo,
                        CreatedOn = x.CreatedOn,
                        DotCount = x.CustomerDOTNumbers.Count(a => a.IsDelete == false),
                        UserCount = x.AspNetUsers.Count(a => a.IsActive == true),
                        ModifiedOn = x.ModifiedOn,
                        isActive = x.isActive,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy
                    }).FirstOrDefault();
            }
            else
            {
                return context.CustomerProfiles.Where(y => y.CompanyID == Id && y.IsDelete == false)
                    .Select(x => new CustomerProfileDTO
                    {
                        CompanyID = x.CompanyID,
                        CustomerName = x.CustomerName,
                        City = x.City,
                        State = x.State,
                        AccountNumber = x.AccountNumber,
                        AccountOwnerEmail = x.AccountOwnerEmail,
                        AccountOwnerName = x.AccountOwnerName,
                        AddressLine1 = x.AddressLine1,
                        AddressLine2 = x.AddressLine2,
                        ZipCode = x.ZipCode,
                        CompanyLogo = x.CompanyLogo,
                        CreatedOn = x.CreatedOn,
                        DotCount = x.CustomerDOTNumbers.Count(a => a.IsDelete == false),
                        UserCount = x.AspNetUsers.Count(a => a.IsActive == true),
                        ModifiedOn = x.ModifiedOn,
                        isActive = x.isActive,
                        CreatedBy = x.CreatedBy,
                        ModifiedBy = x.ModifiedBy
                    }).FirstOrDefault();
            }
        }
        /// <summary>
        /// Delete Customer Profile
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool DeleteCustomer(int CompanyID, string UserID)
        {

            TFPMEntities context = new TFPMEntities();
            var CProfile = context.CustomerProfiles.FirstOrDefault(o => o.CompanyID.Equals(CompanyID));
            if (CProfile != null)
            {
                CProfile.isActive = false;
                CProfile.IsDelete = true;
                CProfile.ModifiedBy = UserID;
                CProfile.ModifiedOn = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Check is User Active or not
        /// </summary>
        /// <param name="AccountNum"></param>
        /// <returns></returns>
        public int? IsCustomerActive(string AccountNum)
        {

            TFPMEntities context = new TFPMEntities();
            var Customer = context.CustomerProfiles.FirstOrDefault(o => o.AccountNumber.Equals(AccountNum.ToLower()) && o.IsDelete == false);
            if (Customer != null)
                return Customer.CompanyID;
            return null;
        }
        /// <summary>
        /// Get Active Customer profiles
        /// </summary>
        /// <returns></returns>
        public List<CustomerProfileDTO> GetActiveCustomerProfiles()
        {
            TFPMEntities context = new TFPMEntities();
            return context.CustomerProfiles.Where(o => o.IsDelete == false)
                .Select(x => new CustomerProfileDTO
                {
                    CompanyID = x.CompanyID,
                    CustomerName = x.CustomerName,
                    City = x.City,
                    State = x.State,
                    AccountNumber = x.AccountNumber,
                    AccountOwnerEmail = x.AccountOwnerEmail,
                    AccountOwnerName = x.AccountOwnerName,
                    AddressLine1 = x.AddressLine1,
                    AddressLine2 = x.AddressLine2,
                    ZipCode = x.ZipCode,
                    CompanyLogo = x.CompanyLogo,
                    CreatedOn = x.CreatedOn,
                    DotCount = x.CustomerDOTNumbers.Count(a => a.IsDelete == false),
                    UserCount = x.AspNetUsers.Count(a => a.IsActive == true),
                    ModifiedOn = x.ModifiedOn,
                    isActive = x.isActive,
                    CreatedBy = x.CreatedBy,
                    ModifiedBy = x.ModifiedBy
                }).ToList();
        }
        /// <summary>
        /// Get Customer by Portal admin
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<CustomerProfileDTO> GetCustomerProfileByPortalAdmin(string UserID)
        {

            TFPMEntities context = new TFPMEntities();

            return context.CustomerProfiles
                .Where(o => o.IsDelete == false && o.AccountNumber != "99999")
                .Select(x => new CustomerProfileDTO
                {
                    CompanyID = x.CompanyID,
                    CustomerName = x.CustomerName,
                    City = x.City,
                    State = x.State,
                    AccountNumber = x.AccountNumber,
                    AccountOwnerEmail = x.AccountOwnerEmail,
                    AccountOwnerName = x.AccountOwnerName,
                    AddressLine1 = x.AddressLine1,
                    AddressLine2 = x.AddressLine2,
                    ZipCode = x.ZipCode,
                    CompanyLogo = x.CompanyLogo,
                    CreatedOn = x.CreatedOn,
                    DotCount = x.CustomerDOTNumbers.Count(a => a.IsDelete == false),
                    UserCount = x.AspNetUsers.Count(a => a.IsActive == true),
                    ModifiedOn = x.ModifiedOn,
                    isActive = x.isActive,
                    CreatedBy = x.CreatedBy,
                    ModifiedBy = x.ModifiedBy
                }).ToList();
        }
        /// <summary>
        /// Customer profile by Userid
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>

        public List<CustomerProfileDTO> GetCustomerProfileByUserID(string UserID)
        {
            TFPMEntities context = new TFPMEntities();
            var accountNO = context.AspNetUsers.FirstOrDefault(x => x.Id.Equals(UserID) && x.IsActive.Equals(true));
            if (accountNO != null)
            {
                return context.CustomerProfiles.Where(x => x.AccountNumber == accountNO.AccountNumber && x.IsDelete == false)
                   .Select(x => new CustomerProfileDTO
                   {
                       CompanyID = x.CompanyID,
                       CustomerName = x.CustomerName,
                       City = x.City,
                       State = x.State,
                       AccountNumber = x.AccountNumber,
                       AccountOwnerEmail = x.AccountOwnerEmail,
                       AccountOwnerName = x.AccountOwnerName,
                       AddressLine1 = x.AddressLine1,
                       AddressLine2 = x.AddressLine2,
                       ZipCode = x.ZipCode,
                       CompanyLogo = x.CompanyLogo,
                       CreatedOn = x.CreatedOn,
                       DotCount = x.CustomerDOTNumbers.Count(a => a.IsDelete == false),
                       UserCount = x.AspNetUsers.Count(a => a.IsActive == true),
                       ModifiedOn = x.ModifiedOn,
                       isActive = x.isActive,
                       CreatedBy = x.CreatedBy,
                       ModifiedBy = x.ModifiedBy
                   }).ToList();
            }
            return null;
        }
        /// <summary>
        /// Get All Company Users
        /// </summary>
        /// <param name="AccountNo"></param>
        /// <returns></returns>
        public List<AspNetUser> GetCompanyUsers(string AccountNo)
        {
            TFPMEntities context = new TFPMEntities();
            if (context.CustomerProfiles.Any(x => x.AccountNumber.Equals(AccountNo.ToLower()) && x.IsDelete.Equals(false)))
                return context.AspNetUsers.Where(x => x.AccountNumber.Equals(AccountNo.ToLower())).ToList();
            else
                return null;
        }

        public List<AspNetUser> GetCompanyUsers(string AccountNo, string OrderBy, int PageIndex, int PageSize, bool Assending)
        {
            TFPMEntities context = new TFPMEntities();
            var skip = PageSize * (PageIndex - 1);
            if (context.CustomerProfiles.Any(x => x.AccountNumber.Equals(AccountNo.ToLower()) && x.IsDelete.Equals(false)))
                return context.AspNetUsers.Where(x => x.AccountNumber.Equals(AccountNo.ToLower()))
                     .Skip(skip)
                     .Take(PageSize)
                    .OrderBy(x => x.Email).ToList();
            else
                return null;
        }
    }
}