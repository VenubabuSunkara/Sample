using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransForce.API.TF_Exceptions
{
    public static class Logfornet
    {
        public static string LogMessage(object Account, object Method, object Message, object StackTrace)
        {
            return "TFWebAPI: An error occurred with message " + Message + " and stack trace " + StackTrace + " for " + Method + " " + Account;
        }
    }
    public static class ErrorMessages
    {
        public const string NoAccessDenied = "No authorised access";
        public const string AlreadyExists = "Record already exist";
        public const string Success = "Record inserted successfully";
        public const string NoResults = "No results Found";
    }
}