using System.Collections.Generic;
using TransForce.API.DTOS;
using TransForce.API.Entitys;

namespace TransForce.API.TF_InterfaceLayer
{
    public interface ICustomerService
    {
        CustomerProfileDTO CheckCustomerExists(string CustomerName);
        CustomerProfileDTO CreateCustomerProfile(CustomerProfile model, string UserID);
        CustomerProfileDTO UpdateCustomerProfile(CustomerProfile model, string RoleNames, string UserID);
        CustomerProfileDTO EditCustomerProfile(int Id,string UserId);
        bool DeleteCustomer(int CompanyID, string UserID);
        int? IsCustomerActive(string AccountNum);
        List<CustomerProfileDTO> GetActiveCustomerProfiles();
        List<CustomerProfileDTO> GetCustomerProfileByPortalAdmin(string UserID);
        List<CustomerProfileDTO> GetCustomerProfileByUserID(string UserID);
        List<AspNetUser> GetCompanyUsers(string AccountNo);
        List<AspNetUser> GetCompanyUsers(string AccountNo, string OrderBy, int PageIndex, int PageSize, bool Assending);
    }
}