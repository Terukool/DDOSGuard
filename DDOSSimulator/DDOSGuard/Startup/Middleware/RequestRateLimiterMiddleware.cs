using DDOSGuardService.Exceptions;
using DDOSGuardService.Logic.Interfaces;
using DDOSGuardService.Properties;

namespace DDOSGuardService.Startup.Middleware
{
    public class RequestRateLimiterMiddleware(IRateLimiter rateLimiter, RateLimiterSettings settings, ILogger<RequestRateLimiterMiddleware> logger) : IMiddleware
    {
        #region Fields

        private readonly IRateLimiter _rateLimiter = rateLimiter;
        private readonly string _clientIdKey = settings.ClientQueryKey;
        private readonly ILogger<RequestRateLimiterMiddleware> _logger = logger;

        #endregion

        #region Public Methods

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string? clientId = context.Request.Query[_clientIdKey];

            if (string.IsNullOrEmpty(clientId))
            {
                throw new ClientIdRequiredException();
            }

            ProcessRequest(clientId);

            return next(context);
        }

        public void ProcessRequest(string id)
        {
            if (!_rateLimiter.ShouldBlock(id))
            {
                return;
            }

            _logger.LogInformation($"Request from {id} was blocked due to rate limit exceedance");

            throw new RateLimitExceededException();
        }

        #endregion
    }
}