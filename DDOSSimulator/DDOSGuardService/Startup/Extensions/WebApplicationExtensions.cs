using DDOSGuardService.Startup.Middleware;

namespace DDOSGuardService.Startup.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void ConfigureSwagger(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                return;
            }

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        public static void UseHttpExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<HttpExceptionMiddleware>();
        }

        public static void UseRateLimiting(this WebApplication app)
        {
            app.UseMiddleware<RequestRateLimiterMiddleware>();
        }
    }
}