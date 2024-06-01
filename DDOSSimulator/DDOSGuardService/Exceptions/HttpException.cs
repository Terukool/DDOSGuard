using System.Net;

namespace DDOSGuardService.Exceptions
{
    public class HttpException(HttpStatusCode statusCode, string message) : Exception(message)
    {
        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}