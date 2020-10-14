

using System;
using System.Net;

namespace Infrastructure.ApiResponse
{
    public interface IResponseResult 
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode Code { get; set; }

    }
}
