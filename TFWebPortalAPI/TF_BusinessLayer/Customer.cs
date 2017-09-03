using System.Collections.Generic;
using System.Security.Claims;
using TransForce.API.DTOS;
using TransForce.API.Entitys;
using TransForce.API.TF_DataLayer;
using TransForce.API.TF_InterfaceLayer;
namespace TransForce.API.TF_BusinessLayer
{
    public class Customer
    {
        /// <summary>
        /// Add New customer Profile
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public CustomerProfileDTO AddCustomer(CustomerProfile model, string UserID)
        {
            ICustomerService Customer = new CustomerService();
            return Customer.CreateCustomerProfile(model, UserID);
        }
        /// <summary>
        /// Update Customer Profile
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Roles"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public CustomerProfileDTO UpdateCustomer(CustomerProfile model, string Roles, string UserID)
        {
            ICustomerService Customer = new CustomerService();
            return Customer.UpdateCustomerProfile(model, Roles, UserID);
        }
        /// <summary>
        /// Edit Customer Profile
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public CustomerProfileDTO EditCustomer(int Id, string UserId)
        {
            ICustomerService Customer = new CustomerService();
            return Customer.EditCustomerProfile(Id, UserId);
        }
        /// <summary>
        /// Delete Customer BY CompanyID
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool DeleteCustomer(int CompanyID, string UserID)
        {
            ICustomerService Customer = new CustomerService();
            return Customer.DeleteCustomer(CompanyID, UserID);
        }
        /// <summary>
        /// Check Customer is Active or Not
        /// </summary>
        /// <param name="AccountNum"></param>
        /// <returns></returns>
        public int? IsCustomerActive(string AccountNum)
        {
            ICustomerService Customer = new CustomerService();
            return Customer.IsCustomerActive(AccountNum);
        }
        /// <summary>
        /// Get Active Customers list
        /// </summary>
        /// <returns></returns>
        public List<CustomerProfileDTO> GetActiveCustomerProfiles()
        {
            ICustomerService Customer = new CustomerService();
            return Customer.GetActiveCustomerProfiles();
        }
        /// <summary>
        /// Get Customer Profile by Super admin 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<CustomerProfileDTO> GetCustomerProfileBySuperPortalAdmin(string UserID)
        {
            ICustomerService Customer = new CustomerService();
            return Customer.GetCustomerProfileByPortalAdmin(UserID);
        }
        /// <summary>
        /// Get Customer Profile by User
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<CustomerProfileDTO> GetCustomerProfileByUserID(string UserID)
        {
            ICustomerService Customer = new CustomerService();
            return Customer.GetCustomerProfileByUserID(UserID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public CustomerProfileDTO CheckCustomerExists(string CustomerName)
        {
            ICustomerService Customer = new CustomerService();
            return Customer.CheckCustomerExists(CustomerName);
        }
    }
}