

using System.Net;

namespace Infrastructure.ApiResponse
{
    public class ResponseResult : IResponseResult
    {
        public HttpStatusCode Status { get  ; set  ; }
        public string Message { get  ; set  ; }
        public string ErrorMessage { get  ; set  ; }

      
    }
}
