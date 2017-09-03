using System;
using System.Collections.Generic;
using System.Linq;
using TransForce.API.DTOS;
using TransForce.API.Entitys;
using TransForce.API.TF_InterfaceLayer;

namespace TransForce.API.TF_DataLayer
{
    public class CommonService : ICommonService
    {
        private readonly TFPMEntities context;
        public CommonService()
        {
            context = new TFPMEntities();
        }
        /// <summary>
        /// Assign Userto Account No
        /// </summary>
        /// <param name="AccountNo"></param>
        /// <param name="UserID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public AspNetUser AssingUserToAccount(string AccountNo, string UserID, string ModifiedBy)
        {
            var Userinfo = context.AspNetUsers.Where(x => x.Id.Equals(UserID) && x.IsDelete.Equals(false) && string.IsNullOrEmpty(x.AccountNumber)).FirstOrDefault();
            if (Userinfo != null)
            {
                Userinfo.AccountNumber = AccountNo;
                Userinfo.ModifiedOn = DateTime.Now;
                Userinfo.ModifiedBy = ModifiedBy;
                context.SaveChanges();
                return Userinfo;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        public List<AspNetRole> GetRoles()
        {
            return context.AspNetRoles.ToList();
        }
        /// <summary>
        /// Get User By Account No
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        public List<UserDTO> GetUsersByAccountNo(string accountNo)
        {
            return context.AspNetUsers.Where(o => o.AccountNumber.Equals(accountNo.ToLower()) && o.IsDelete == false)
                .Select(x => new UserDTO
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    UserName = x.UserName,
                    PhoneNumber = x.PhoneNumber,
                    IsActive = x.IsActive,
                    Title = x.Title,
                    Roles = x.AspNetRoles.Select(y => y.Name).ToList(),
                    AccountNumber = x.AccountNumber,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn,
                }).ToList();
        }
    }
}