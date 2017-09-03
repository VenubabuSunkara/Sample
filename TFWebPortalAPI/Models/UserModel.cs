using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransForce.API.Models
{
    //helper class for access user and access_token
    public class UserModel
    {
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public string AccessFailedCount { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }

        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public List<string> Roles { get; set; }
        public int CompanyId { get; set; }
    }
    //public enum Roles
    //{
    //    SuperAdmin = 1,
    //    PortalAdmin = 2,
    //    AccountAdmin = 3,
    //    ReportAdmin = 4
    //}
    public static class Roles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string PortalAdmin = "PortalAdmin";
        public const string AccountAdmin = "AccountAdmin";
        public const string ReportAdmin = "ReportAdmin";

    }
    public enum Status
    {
        Fail = 0,
        Success = 1,
        BadRequest = 2,
        InternalServerError = 3,
        NoResult = 4,
        NoAccess = 5

    }
    public class StatusResult
    {
        public string Status { get; set; }
        public object Result { get; set; }
        public int StatusCode { get; set; }
    }

}