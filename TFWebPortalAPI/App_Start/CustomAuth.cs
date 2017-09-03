using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using TransForce.API.Models;

namespace TransForce.API.App_Start
{
    public class CustomAuth : AuthorizeAttribute
    {

        private readonly string[] allowedroles;
        public CustomAuth(params string[] roles)
        {
            this.allowedroles = roles;
        }
        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //    var identity = new ClaimsIdentity()User.Identity;
        //    List<Claim> claims = identity.Claims.ToList();

        //}
    }
}