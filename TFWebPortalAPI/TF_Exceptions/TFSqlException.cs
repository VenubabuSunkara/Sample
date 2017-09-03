using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransForce.API.TF_Exceptions
{
    [Serializable]
    public class TFSqlException : TFExceptions
    {
        public TFSqlException()
        { }

        public TFSqlException(string message)
            : base(message)
        { }

        public TFSqlException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}