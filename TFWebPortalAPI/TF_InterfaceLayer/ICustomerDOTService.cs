using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransForce.API.DTOS;
using TransForce.API.Entitys;

namespace TransForce.API.TF_InterfaceLayer
{
    interface ICustomerDOTService
    {
        //List<CustomerDOTNumber> GetCustomerDOTNumbersAll();
        CustomerDOTNumberDTO GetByDOTNumber(int CompanyID, string DOTNumber);
        List<CustomerDOTNumberDTO> GetDOTNumbersByCustomerId(int CompanyID);
        CustomerDOTNumberDTO CreateCustomerDOTNumber(CustomerDOTNumber model);
        CustomerDOTNumberDTO EditCustomerDOTNumber(int CompanyID, int itemID);
        CustomerDOTNumberDTO UpdateCustomerDOTNumber(CustomerDOTNumber model);
        bool DeleteCustomerDOTNumber(CustomerDOTNumber model);
    }
}
