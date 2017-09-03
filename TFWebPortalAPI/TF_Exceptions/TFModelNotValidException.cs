using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TransForce.API.TF_Exceptions
{
    [Serializable]
    public class TFModelNotValidException : TFExceptions
    {
        public TFModelNotValidException()
        { }

        public TFModelNotValidException(string message)
            : base(message)
        { }

        public TFModelNotValidException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}