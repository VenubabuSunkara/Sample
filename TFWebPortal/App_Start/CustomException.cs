using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransForce.Web.Portal.App_Start
{
    public class CustomException
    {

    }
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        //Create an instance for Logger class.
        /// <summary>
        /// Checks if the request is AJAX request or not
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        public override void OnException(ExceptionContext filterContext)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(filterContext.Exception.StackTrace, EventLogEntryType.Error);
            }

            if (filterContext != null && filterContext.Exception != null)
            {
            }
            HttpContext.Current.Response.Redirect("/Account/Login");
        }
    }
}