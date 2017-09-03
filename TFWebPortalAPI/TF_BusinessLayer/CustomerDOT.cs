using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransForce.API.TF_DataLayer;
using TransForce.API.TF_InterfaceLayer;
using TransForce.API.Entitys;
using TransForce.API.DTOS;

namespace TransForce.API.TF_BusinessLayer
{
    public class CustomerDOT 
    {
        //public List<CustomerDOTNumber> GetAll(int CompanyID)
        //{
        //    ICustomerDOTService customerDOT = new CustomerDOTService(CompanyID);
        //    return customerDOT.GetCustomerDOTNumbersAll();
        //}
        public CustomerDOTNumberDTO GetCustomerDOTByDOTNumber(int CompanyID ,string DOTNumber)
        {
            ICustomerDOTService customerDOT = new CustomerDOTService(CompanyID);
            return customerDOT.GetByDOTNumber(CompanyID,DOTNumber);
        }
        public List<CustomerDOTNumberDTO> GetCustomerDOTByCustomerId(int CompanyID)
        {
            ICustomerDOTService customerDOT = new CustomerDOTService(CompanyID);
            return customerDOT.GetDOTNumbersByCustomerId(CompanyID);
        }
        public CustomerDOTNumberDTO AddCustomerDOT(CustomerDOTNumber model)
        {
            ICustomerDOTService customerDOT = new CustomerDOTService(model.CustomerID);
            return customerDOT.CreateCustomerDOTNumber(model);
        }
        public CustomerDOTNumberDTO EditCustomerDOT(int CompanyID,int itemID)
        {
            ICustomerDOTService customerDOT = new CustomerDOTService(CompanyID);
            return customerDOT.EditCustomerDOTNumber(CompanyID,itemID);
        }
        public CustomerDOTNumberDTO UpdateCustomerDOT(CustomerDOTNumber model)
        {
            ICustomerDOTService customerDOT = new CustomerDOTService(model.CustomerID);
            return customerDOT.UpdateCustomerDOTNumber(model);
        }
        public bool DeleteCustomerDOT(CustomerDOTNumber model)
        {
            ICustomerDOTService customerDOT = new CustomerDOTService();
            return customerDOT.DeleteCustomerDOTNumber(model);
        }
    }
}