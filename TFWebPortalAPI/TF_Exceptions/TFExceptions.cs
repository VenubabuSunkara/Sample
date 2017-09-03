using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TransForce.API.TF_Exceptions
{
    public class TFExceptions : Exception
    {
        public enum ExceptionStatus
        {
            SqlException = 1
        }
        public TFExceptions()
        { }

        public TFExceptions(string message)
        : base(message)
        { }

        public TFExceptions(string message, Exception innerException)
        : base(message, innerException)
        { }
    }
}