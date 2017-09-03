using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransForce.API.DTOS;
using TransForce.API.Entitys;
using TransForce.API.Models;
using TransForce.API.TF_DataLayer;
using TransForce.API.TF_InterfaceLayer;

namespace TransForce.API.TF_BusinessLayer
{
    public class Common
    {
        readonly CommonService _service;
        public Common()
        {
            _service = new CommonService();
        }
        public List<AspNetRole> GetRoles()
        {
            return _service.GetRoles();
        }
        public List<UserDTO> GetUsersByAccountNo(string accountNo)
        {
            return _service.GetUsersByAccountNo(accountNo);
        }
        public AspNetUser AssingUserToAccount(string AccountNo, string UserID, string ModifiedBy)
        {
            return _service.AssingUserToAccount(AccountNo, UserID, ModifiedBy);
        }
    }
}