using System;
using System.Collections.Generic;
using System.Text;

namespace Brag.Domain.Exceptions
{
    public class ApiGenericException : Exception
    { 
        public string ErrorCode { get; set; }

        public ApiGenericException(string message) : base(message)
        { }

        public ApiGenericException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
