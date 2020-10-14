using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Exceptions
{
    public class BadRequestException : Exception
    {
        public string ErrorCode { get; set; }

        public BadRequestException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
