using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TransForce.Web.Portal.Models;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using TransForce.Web.Portal.TFWebExceptions;

namespace TransForce.Web.Portal.Controllers
{
    [CustomAuthorize]
    public class AccountController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HttpClient client;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly string _url = ConfigurationManager.AppSettings["webapibaseurl"];
        private readonly UserModel userData;
        private string errorMessage;

        public AccountController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            userData = System.Web.HttpContext.Current.Session["UserData"] != null ? (UserModel)System.Web.HttpContext.Current.Session["UserData"] : null;
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #region validate captch
        /// <summary>
        /// Validate Captcha and return status of captcha
        /// </summary>
        /// <returns></returns>
        private CaptchaResponse ValidateCaptha()
        {
            var response = Request["g-recaptcha-response"];
            //secretkey that was generated in key value pair
            string secret = ConfigurationManager.AppSettings["Captchasecret"].ToString();

            var webClient = new System.Net.WebClient();
            var reply =
                webClient.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                secret, response));

            return JsonConvert.DeserializeObject<CaptchaResponse>(reply);
        }
        #endregion

        #region user Login
        /// <summary>
        /// User Login Get Method
        /// </summary>
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                #region captch validate
                //var captchaResponse = ValidateCaptha();
                //if (!captchaResponse.Success)
                //{
                //    if (captchaResponse.ErrorCodes.Count <= 0) return View();

                //    var error = captchaResponse.ErrorCodes[0].ToLower();
                //    switch (error)
                //    {
                //        case ("missing-input-secret"):
                //            ViewBag.CaptchaErrorMessage = "The secret parameter is missing.";
                //            break;

                //        case ("invalid-input-secret"):
                //            ViewBag.CaptchaErrorMessage = "The secret parameter is invalid or malformed.";
                //            break;

                //        case ("missing-input-response"):
                //            ViewBag.CaptchaErrorMessage = "Please select the captcha";
                //            break;
                //        case ("invalid-input-response"):
                //            ViewBag.CaptchaErrorMessage = "The response parameter is invalid or malformed.";
                //            break;

                //        default:
                //            ViewBag.CaptchaErrorMessage = "Error occured. Please try again";
                //            break;
                //    }
                //}
                //else
                //{
                #endregion
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Account/Login", model);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    StatusResult statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                    if (statusResult.Status == Status.Success.ToString())
                    {
                        Log.Info(Logfornet.LogMessage(model.Email, "Login", ErrorMessages.Success, ""));
                        UserModel obj = JsonConvert.DeserializeObject<UserModel>(statusResult.Result.ToString());
                        Session["UserData"] = obj;
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        Log.Info(Logfornet.LogMessage(model.Email, "Login", statusResult.Status, ""));
                        model.ErrorMessage = "Wrong username or password";
                        return View(model);
                    }
                }
                else
                {
                    Log.Info(Logfornet.LogMessage(model.Email, "Login", responseMessage.StatusCode, ""));
                    model.ErrorMessage = responseMessage.StatusCode.ToString();
                    return View(model);
                }
                //}
                //return View(model);
            }
            catch (Exception ex)
            {
                Log.Info(Logfornet.LogMessage(model.Email, "Login", ex.Message, ""));
                model.ErrorMessage = ex.Message;
                return View(model);
            }
            #region default code
            //// This doesn't count login failures towards account lockout
            //// To enable password failures to trigger account lockout, change to shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return View(model);
            //}
            #endregion
        }
        #endregion

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        #region BindData to DropDown
        /// <summary>
        /// Bind Roles to Dropdown
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> BindDropDownRoles()
        {
            var constants = (from fieldInfo in typeof(Roles).GetFields()
                             where (fieldInfo.Attributes & FieldAttributes.Literal) != 0
                             select fieldInfo.Name).ToList();
            List<SelectListItem> roleList = constants.Select(r => new SelectListItem
            {
                Text = r.Replace("A", " A"),
                Value = r
            }).ToList();

            if (userData.Roles.Contains(Roles.SuperAdmin))
            {
                roleList.Remove(roleList.Where(r => r.Value == Roles.SuperAdmin).Single());
            }
            else if (userData.Roles.Contains(Roles.PortalAdmin))
            {
                roleList.Remove(roleList.Where(r => r.Value == Roles.SuperAdmin).Single());
                roleList.Remove(roleList.Where(r => r.Value == Roles.PortalAdmin).Single());
            }
            return roleList;
        }
        #endregion


        /// <summary>
        /// User Registration PartialView
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public PartialViewResult RegisterUser()
        {
            ViewBag.RoleList = BindDropDownRoles();
            var model = new RegisterBindingModel();
            model.UserStaus = "CreateUser";
            return PartialView("_RegisterUserPartial", model);
        }


        /// <summary>
        /// Edit User By UserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> EditUser(string UserId, string RoleName)
        {
            RegisterBindingModel rbModel = new RegisterBindingModel();
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Account/EditUser/" + UserId, "");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    StatusResult statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                    if (statusResult.Status.Equals(Status.Success.ToString()))
                    {
                        rbModel = JsonConvert.DeserializeObject<RegisterBindingModel>(statusResult.Result.ToString());
                        ViewBag.RoleList = BindDropDownRoles();
                        Log.Info(Logfornet.LogMessage(UserId, "Login", ErrorMessages.Success, ""));
                        rbModel.RoleName = RoleName;
                        return PartialView("_RegisterUserPartial", rbModel);
                    }
                    else
                    {
                        Log.Info(Logfornet.LogMessage(UserId, "EditUser", statusResult.Status, ""));
                        rbModel.ErrorMessage = statusResult.Status;
                    }
                }
                else
                {
                    Log.Info(Logfornet.LogMessage(UserId, "EditUser", responseMessage.StatusCode, ""));
                    rbModel.ErrorMessage = responseMessage.StatusCode.ToString();
                }
                return PartialView("_RegisterUserPartial", rbModel);
            }
            catch (Exception ex)
            {
                Log.Info(Logfornet.LogMessage(UserId, "EditUser", ex.Message, ""));
                rbModel.ErrorMessage = ex.Message;
                return PartialView("_RegisterUserPartial", rbModel);
            }
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="customerVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateUser")]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> UpdateUser(UpdateRegisterBindingModel userVM)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Account/UpdateUser", userVM);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                    if (statusResult.Status.Equals(Status.Success.ToString()))
                    {
                        Log.Info(Logfornet.LogMessage(userVM.FirstName, "UpdateUser", ErrorMessages.Success, ""));
                        return Json(statusResult.Status, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Log.Info(Logfornet.LogMessage(userVM.FirstName, "UpdateUser", statusResult.Status, ""));
                        errorMessage = statusResult.Status;
                    }
                }
                else
                {
                    Log.Info(Logfornet.LogMessage(userVM.FirstName, "UpdateUser", responseMessage.StatusCode, ""));
                    errorMessage = responseMessage.StatusCode.ToString();
                }
                return Json(errorMessage, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Info(Logfornet.LogMessage(userVM.FirstName, "UpdateUser", ex.Message, ""));
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="customerVM"></param>
        /// <returns></returns>
        [Route("DeleteUser")]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> DeleteUser(string UserID)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Account/DeleteUser/" + UserID, "");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    var statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                    if (statusResult.Status.Equals(Status.Success.ToString()))
                    {
                        Log.Info(Logfornet.LogMessage(UserID, "DeleteUser", ErrorMessages.Success, ""));
                        return Json(statusResult.Status, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Log.Info(Logfornet.LogMessage(UserID, "DeleteUser", statusResult.Status, ""));
                        errorMessage = statusResult.Status;
                    }
                }
                else
                {
                    Log.Info(Logfornet.LogMessage(UserID, "DeleteUser", responseMessage.StatusCode, ""));
                    errorMessage = responseMessage.StatusCode.ToString();
                }
                return Json(errorMessage, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Info(Logfornet.LogMessage(UserID, "DeleteUser", ex.Message, ""));
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin)]
        public async Task<ActionResult> SaveRegisterUser(RegisterBindingModel model)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Account/Register", model);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    StatusResult statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                    if (statusResult.Status.Equals(Status.Success.ToString()))
                    {
                        return Json(statusResult.Status, JsonRequestBehavior.AllowGet);
                    }
                    else if (statusResult.Status.Equals(Status.Fail.ToString()))
                    {
                        return Json(statusResult.Result.ToString().Replace(",", "</br>"), JsonRequestBehavior.AllowGet);
                    }
                }
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Info(Logfornet.LogMessage(model.FirstName, "SaveRegisterUser", ex.Message, ""));
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            #region Default Code
            //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            //var result = await UserManager.CreateAsync(user, model.Password);
            //if (result.Succeeded)
            //{
            //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

            //    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            //    // Send an email with this link
            //    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            //    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            //    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            //    return RedirectToAction("Index", "Home");
            //}
            //AddErrors(result);
            #endregion
        }

        /// <summary>
        /// Get Users by Account Number
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> GetCustomerUsers(string accountNo)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Account/GetUsersByAccountNo/" + accountNo, "");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                StatusResult statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    List<UpdateRegisterBindingModel> usersData = JsonConvert.DeserializeObject<List<UpdateRegisterBindingModel>>(statusResult.Result.ToString());
                    var jsonResult = Json(new { data = usersData }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                return Json(new { data = new UpdateRegisterBindingModel() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = "Error" }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            ResetToken token = new Models.ResetToken();
            token.UserId = userId;
            token.Code = code;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Account/ConfirmEmail", token);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                StatusResult statusResult = JsonConvert.DeserializeObject<StatusResult>(result);
                if (statusResult.Status.Equals(Status.Success.ToString()))
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error");
            }
            return View("Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        //[ValidateAntiForgeryToken]
        [HttpGet]
        [AllowAnonymous]
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin, Roles.ReportAdmin)]
        public async Task<ActionResult> LogOff()
        {

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.AccessToken);
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(_url + "Account/Logout", "");
            if (responseMessage.IsSuccessStatusCode)
            {
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }



        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}