using System.Net;

namespace DDOSGuardService.Exceptions
{
    public class ClientIdRequiredException(): HttpException(HttpStatusCode.BadRequest, "User ID is required");
}