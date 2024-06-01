using System.Net;

namespace DDOSGuardService.Exceptions
{
    public class RateLimitExceededException() : HttpException(HttpStatusCode.ServiceUnavailable, "Rate limit exceeded");
}