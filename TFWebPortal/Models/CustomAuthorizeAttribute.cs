using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransForce.Web.Portal.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public UserModel currentUserRoles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            
            this.allowedroles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            currentUserRoles = System.Web.HttpContext.Current.Session["UserData"] != null ? (UserModel)System.Web.HttpContext.Current.Session["UserData"] : null;
            if (currentUserRoles != null && allowedroles != null)
            {
                foreach (var role in allowedroles)
                {
                    foreach (var currentUserRole in currentUserRoles.Roles)
                    {
                        if (role.ToString().Trim() == currentUserRole.Replace(" ", ""))
                        {
                            authorize = true;
                        }
                    }
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}