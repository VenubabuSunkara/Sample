using System.Collections.Generic;
using TransForce.API.DTOS;
using TransForce.API.Entitys;

namespace TransForce.API.TF_InterfaceLayer
{
    interface ICommonService
    {
        List<AspNetRole> GetRoles();
        List<UserDTO> GetUsersByAccountNo(string accountNo);
        AspNetUser AssingUserToAccount(string AccountNo, string UserID, string ModifiedBy);
    }
}
