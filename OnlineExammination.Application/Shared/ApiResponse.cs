using System.Net;

namespace OnlineExammination.Application.Shared
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; }
        public T? Result { get; set; }
    }
}
