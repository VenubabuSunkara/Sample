using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransForce.Web.Portal.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class ResetToken
    {
        [Required]
        public string UserId { set; get; }
        [Required]
        public string Code { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter account number")]
        [Display(Name = "Account Number")]
        [RegularExpression("^[0-9a-zA-Z]+$", ErrorMessage = "Account number should not allow special characters")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string AccountNo { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        [Display(Name = "Email")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression("^(?=.{6,})(?=.*[a-z])(?=.*[0-9])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Passwords must have at least one special character, one uppercase, one number,one lowercase letter.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }


    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class RegisterBindingModel
    {
        public bool IsActive { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public List<Role> Role { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        public string Title { get; set; }

        public string UserStaus { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public Nullable<int> CompanyID { get; set; }

        public string ErrorMessage { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression("^(?=.{6,})(?=.*[a-z])(?=.*[0-9])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Passwords must have at least one special character, one uppercase, one number,one lowercase letter.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }

    //public class RegisterBindingModel
    //{
    //    [Required]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm password")]
    //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }
    //    public bool IsActive { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string AccountNumber { get; set; }
    //    [Required]
    //    [Display(Name = "Role Name")]
    //    public string RoleName { get; set; }
    //    public string CreatedBy { get; set; }
    //    public DateTime? CreatedOn { get; set; }
    //    public string ModifiedBy { get; set; }
    //    public DateTime? ModifiedOn { get; set; }
    //    public string Title { get; set; }
    //}

    public class UpdateRegisterBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string AccountNumber { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Title { get; set; }

        public string Id { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "The Account Number field is required.")]
        [Display(Name = "Account Number")]
        [RegularExpression("^[0-9a-zA-Z]+$", ErrorMessage = "Account number should not allow special characters")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string AccountNo { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

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
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }

        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public List<string> Roles { get; set; }



        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }

    //public enum Roles
    //{
    //    AccountAdmin,
    //    SuperAdmin,
    //    PortalAdmin,
    //    ReportAdmin,
    //}
    public class Roles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string PortalAdmin = "PortalAdmin";
        public const string AccountAdmin = "AccountAdmin";
        public const string ReportAdmin = "ReportAdmin";

    }
    public enum Status
    {
        InternalServerError = 3,
        BadRequest = 2,
        Success = 1,
        Fail = 0
    }
}
