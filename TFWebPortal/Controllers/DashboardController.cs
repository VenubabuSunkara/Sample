using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransForce.Web.Portal.Models;

namespace TransForce.Web.Portal.Controllers
{
    [CustomAuthorize]
    public class DashboardController : Controller
    {
        // GET: Index
        [CustomAuthorize(Roles.SuperAdmin, Roles.PortalAdmin, Roles.AccountAdmin,Roles.ReportAdmin)]
        public ActionResult Index()
        {

            return View();
        }

        public PartialViewResult Sidebar()
        {
            return PartialView("_Sidebar");
        }
    }
}