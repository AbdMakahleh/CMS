

using System.Net;

namespace Infrastructure.ApiResponse
{
    public class ResponseResult<T> : ResponseResult
    {
        public T Data { get; set; }
    }
}
