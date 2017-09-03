using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using TransForce.API.Models;
using TransForce.API.Providers;
using TransForce.API.Results;
using System.Net.Http.Formatting;
using System.Linq;
using TransForce.API.TF_BusinessLayer;
using TransForce.API.App_Start;
using System.Configuration;

namespace TransForce.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
        #region ManagerUser
        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }


        // POST api/Account/Logout
        [HttpPost]
        [Route("Logout")]
        public StatusResult Logout()
        {
            StatusResult c = new Models.StatusResult();
            try
            {
                Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
                c.Status = Status.Success.ToString();
                c.Result = "User LogedOut Successfully";
                return c;
            }
            catch (Exception ex)
            {
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return null;
            }
            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();
            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }
            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }
            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }
        #endregion
        #region External Logins
        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);
            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }
            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);
            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }
            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result;
            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }
            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            if (externalLogin == null)
            {
                return InternalServerError();
            }
            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }
            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));
            bool hasRegistered = user != null;
            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager, OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager, CookieAuthenticationDefaults.AuthenticationType);
                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName, user.Id);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }
            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }
        #endregion
        #region User Registration
        // POST api/Account/Register
        [HttpPost]
        //[AllowAnonymous]
        [Route("Register")]
        public async Task<StatusResult> Register(RegisterBindingModel model)
        {
            StatusResult c = new Models.StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    var user = new ApplicationUser()
                    {
                        Title = CommonFunctions.Trimstring(model.Title),
                        UserName = CommonFunctions.Trimstring(model.Email),
                        Email = CommonFunctions.Trimstring(model.Email),
                        IsActive = true,
                        IsDelete = false,
                        FirstName = CommonFunctions.Trimstring(model.FirstName),
                        LastName = CommonFunctions.Trimstring(model.LastName),
                        CreatedOn = DateTime.Now,
                        CreatedBy = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value,
                        ModifiedOn = DateTime.Now,
                        ModifiedBy = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value,
                        AccountNumber = model.AccountNumber,
                        CompanyID = model.CompanyId

                    };
                    IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        IdentityResult roleresult = await UserManager.AddToRolesAsync(user.Id, model.RoleName.Split(',').ToArray());
                        if (!roleresult.Succeeded)
                        {
                            c.Status = Status.Fail.ToString();
                            c.Result = string.Join(",", result.Errors.ToArray());
                            return c;
                        }
                        //Send Email
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = ConfigurationManager.AppSettings["portalURL"].ToString() + "/Account/ConfirmEmail?userId=" + user.Id + "&code=" + code;

                        EmailService email = new EmailService();
                        IdentityMessage msg = new IdentityMessage();
                        msg.Destination = user.Email;
                        msg.Body = "Hi " + user.FirstName + " " + user.LastName + " below are your login details  <br />"
                            + "UserName/Email :" + user.Email + "<br /> "
                            + "and Password :" + model.Password + "<br /> "
                            + "Portal Site link : <a href =\"" + callbackUrl + "\">here</a>";
                        msg.Subject = "New User Registration";
                        await email.SendAsync(msg);
                        c.Status = Status.Success.ToString();
                        c.Result = user;
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = result.Errors.Skip(1).Take(1);
                        return c;
                    }
                }
                else
                {
                    c.Status = Status.Fail.ToString();
                    c.Result = "Access Denied";
                    return c;
                }
            }
            catch (Exception ex)
            {
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<StatusResult> UpdateUser(UpdateRegisterBindingModel model)
        {
            StatusResult c = new Models.StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    var user = UserManager.FindByEmailAsync(model.Email).Result;
                    if (user != null)
                    {
                        user.Title = CommonFunctions.Trimstring(model.Title);
                        user.IsActive = model.IsActive;
                        user.FirstName = CommonFunctions.Trimstring(model.FirstName);
                        user.LastName = CommonFunctions.Trimstring(model.LastName);
                        user.ModifiedOn = DateTime.Now;
                        user.ModifiedBy = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                        IdentityResult result = await UserManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            await UserManager.RemoveFromRolesAsync(user.Id, UserManager.GetRolesAsync(user.Id).Result.ToArray());
                            var UpdatedRoles = await UserManager.AddToRolesAsync(user.Id, model.RoleName.Split(',').ToArray());
                            if (!UpdatedRoles.Succeeded)
                            {
                                c.Status = Status.Fail.ToString();
                                c.Result = string.Join(",", result.Errors.ToArray());
                                return c;
                            }

                            c.Status = Status.Success.ToString();
                            c.Result = user;
                            return c;
                        }
                        else
                        {
                            c.Status = Status.Fail.ToString();
                            c.Result = string.Join(",", result.Errors.ToArray());
                            return c;
                        }
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "User not registered.";
                        return c;
                    }
                }
                else
                {
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "Access.";
                    return c;
                }
            }
            catch (Exception ex)
            {
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }

        [HttpPost]
        [Route("EditUser/{UserID}")]
        public async Task<StatusResult> EditUser(string UserID)
        {
            StatusResult c = new Models.StatusResult();

            var user = await UserManager.FindByIdAsync(UserID);
            if (user != null)
            {
                c.Status = Status.Success.ToString();
                c.Result = user;
                return c;
            }
            else
            {
                c.Status = Status.Fail.ToString();
                c.Result = "User is not registed.";
                return c;
            }
        }

        [HttpPost]
        [Route("DeleteUser/{UserID}")]
        public async Task<StatusResult> DeleteUserByID(string UserID)
        {
            StatusResult c = new Models.StatusResult();
            var user = UserManager.FindByIdAsync(UserID).Result;
            if (user != null)
            {
                user.IsActive = false;
                user.IsDelete = true;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    c.Status = Status.Fail.ToString();
                    c.Result = result.Errors;
                    return c;
                }
                c.Status = Status.Success.ToString();
                c.Result = "User deleted successfully.";
                return c;
            }
            else
            {
                c.Status = Status.Fail.ToString();
                c.Result = "User has not deleted.";
                return c;
            }
        }

        [HttpPost]
        [Route("GetUsersByAccountNo/{accountNo}")]
        public StatusResult GetUsersByAccountNo(string accountNo)
        {
            StatusResult c = new StatusResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    c.Status = Status.BadRequest.ToString();
                    c.Result = BadRequest();
                    return c;
                }
                if (User.IsInRole(Roles.SuperAdmin) || User.IsInRole(Roles.PortalAdmin) || User.IsInRole(Roles.AccountAdmin))
                {
                    Common user = new Common();
                    var usersByAccountNo = user.GetUsersByAccountNo(accountNo)
                        .Select(x => new UpdateRegisterBindingModel
                        {
                            Id = x.Id,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Email = x.Email,
                            AccountNumber = x.AccountNumber,
                            CreatedOn = x.CreatedOn,
                            ModifiedOn = x.ModifiedOn,
                            Title = x.Title,
                            RoleName = String.Join(",", x.Roles.ToArray()),
                            IsActive = x.IsActive
                        }).ToList();
                    if (usersByAccountNo.Count == 0)
                    {
                        c.Status = Status.BadRequest.ToString();
                        c.Result = "Customer Account Number Not Found.";
                    }
                    else
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = usersByAccountNo;
                    }
                    return c;
                }
                else
                {
                    c.Status = Status.NoAccess.ToString();
                    c.Result = "No Access";
                    return c;
                }
            }
            catch (Exception ex)
            {
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }

        #endregion
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        // [ValidateAntiForgeryToken]
        public async Task<StatusResult> Login(LoginViewModel model)
        {
            StatusResult c = new Models.StatusResult();
            if (!ModelState.IsValid)
            {
                c.Status = Status.BadRequest.ToString();
                c.Result = BadRequest();
                return c;
            }

            Customer cus = new Customer();
            var companyId = cus.IsCustomerActive(model.AccountNo);
            if (companyId == null)
            {
                c.Status = Status.Fail.ToString();
                c.Result = "User Not assigned any customer";
                return c;
            }
            ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
            if (user == null || !user.IsActive || user.AccountNumber != model.AccountNo.ToLower())
            {
                c.Status = Status.Fail.ToString();
                c.Result = "User is Inactive Or Account not valid";
                return c;
            }
            else
            {
                var UserRoles = await UserManager.GetRolesAsync(user.Id);
                var form = new Dictionary<string, string>
               {
                   {"grant_type", "password"},
                   {"username", user.UserName},
                   {"password", model.Password},
               };
                var client = new HttpClient();
                Uri siteuri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                string pathQuery = siteuri.PathAndQuery;
                string hostName = siteuri.ToString().Replace(pathQuery, "");
                var tokenResponse = client.PostAsync(hostName + "/Token", new FormUrlEncodedContent(form)).Result;
                var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;

                var usermodel = new UserModel
                {
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    Id = user.Id,
                    UserName = user.UserName,
                    AccessToken = token.AccessToken,
                    ExpiresIn = token.ExpiresIn,
                    RefreshToken = token.RefreshToken,
                    TokenType = token.TokenType,
                    Roles = UserRoles.ToList()
                };
                if (UserRoles.Contains(Roles.AccountAdmin))
                {
                    usermodel.CompanyId = companyId != null ? Convert.ToInt32(companyId) : 0;
                }
                c.Status = Status.Success.ToString();
                c.Result = usermodel;
                return c;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ConfirmEmail")]
        public async Task<StatusResult> ConfirmEmail(ResetToken token)
        {
            StatusResult c = new Models.StatusResult();
            if (!ModelState.IsValid)
            {
                c.Status = Status.Fail.ToString();
                c.Result = false;
                return c;
            }
            var result = await UserManager.ConfirmEmailAsync(token.UserId, token.Code);
            if (result.Succeeded)
            {
                c.Status = Status.Success.ToString();
                c.Result = true;
            }
            else
            {
                c.Status = Status.Fail.ToString();
                c.Result = result.Errors.First();
            }
            return c;
        }

        #region Create Roles
        // POST api/Account/CreateRole
        [AllowAnonymous]
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IHttpActionResult> CreateRole(RoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new IdentityRole() { Name = model.Name };
            //  role.description = string.Empty;
            IdentityResult result = await RoleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        #endregion
        #region Register Extrenal
        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null && _roleManager != null)
            {
                _userManager.Dispose();
                _roleManager.Dispose();
                _userManager = null;
                _roleManager = null;
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Assign User to Customer Profile
        /// <summary>
        /// Assign User and Account 
        /// </summary>
        /// <param name="AccountNo"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AssingUserToAccount/{AccountNo}/{UserID}")]
        public StatusResult AssingUserToAccount(string AccountNo, string UserID)
        {
            StatusResult c = new Models.StatusResult();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                List<Claim> claims = identity.Claims.Where(x => (x.Value == Roles.PortalAdmin || x.Value == Roles.SuperAdmin || x.Value == Roles.AccountAdmin)).ToList();
                if (!string.IsNullOrEmpty(AccountNo) && !string.IsNullOrEmpty(UserID) && claims.Count > 0)
                {
                    Common com = new Common();
                    var assignUser = com.AssingUserToAccount(AccountNo, UserID, identity.Claims.FirstOrDefault(x => (x.Type == "user_id")).Value);
                    if (assignUser != null)
                    {
                        c.Status = Status.Success.ToString();
                        c.Result = assignUser;
                        return c;
                    }
                    else
                    {
                        c.Status = Status.Fail.ToString();
                        c.Result = "User Already assigned another customer profile.";
                        return c;
                    }
                }
                else
                {
                    c.Status = Status.BadRequest.ToString();
                    c.Result = "Provide account no and user no OR Access denied";
                    return c;
                }
            }
            catch (Exception ex)
            {
                c.Status = Status.InternalServerError.ToString();
                c.Result = ex.InnerException;
                return c;
            }
        }

        //code for ForgotPassword service
        [HttpPost]
        [Route("ForgotPassword")]
        //[ValidateAntiForgeryToken]
        public async Task<StatusResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            StatusResult c = new Models.StatusResult();
            if (ModelState.IsValid)
            {
                Customer cus = new Customer();
                var CompanyId = cus.IsCustomerActive(model.AccountNo);
                if (CompanyId != null)
                {
                    c.Status = Status.Fail.ToString();
                    c.Result = "User Not assigned any customer";
                    return c;
                }
                ApplicationUser user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !user.IsActive || user.AccountNumber != model.AccountNo.ToLower())
                {
                    c.Status = Status.Fail.ToString();
                    c.Result = "User is Inactive Or Account not valid";
                    return c;
                }

                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = ConfigurationManager.AppSettings["portalURL"].ToString() + "/Account/ResetPassword?code=" + code;
                EmailService email = new EmailService();
                IdentityMessage msg = new IdentityMessage();
                msg.Destination = user.Email;
                msg.Body = "Hi your password reset link : <a href =\"" + callbackUrl + "\">here</a>";
                msg.Subject = "New User Registration";
                await email.SendAsync(msg);
                c.Status = Status.Success.ToString();
                c.Result = "password link sent ot registred email id";
                return c;
            }
            c.Status = Status.BadRequest.ToString();
            c.Result = BadRequest();
            return c;
        }
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<StatusResult> ResetPassword(ResetPasswordViewModel model)
        {
            StatusResult c = new Models.StatusResult();
            if (!ModelState.IsValid)
            {
                c.Status = Status.BadRequest.ToString();
                c.Result = BadRequest();
                return c;
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                c.Status = Status.Fail.ToString();
                c.Result = "User is Inactive Or Account not valid";
                return c;
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                c.Status = Status.Success.ToString();
                c.Result = ConfigurationManager.AppSettings["portalURL"].ToString() + "/Account/ResetPasswordConfirmation";
                return c;
            }
            c.Status = Status.Fail.ToString();
            c.Result = "User Not assigned any customer";
            return c;
        }
        #endregion

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
