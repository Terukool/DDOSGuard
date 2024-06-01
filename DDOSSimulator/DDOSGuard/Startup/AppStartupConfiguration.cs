using DDOSGuardService.Startup.Extensions;
using DDOSGuardService.Startup.Middleware;

namespace DDOSGuardService.Startup
{
    public class AppStartupConfiguration
    {
        #region Public Methods

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.ConfigureDependencyInjection();
            services.ConfigureSettings(configuration);
            services.ConfigureSwagger();
        }

        public void ConfigureApp(WebApplication app)
        {
            app.ConfigureSwagger();
            app.UseMiddleware<HttpExceptionMiddleware>();
            app.UseMiddleware<RequestRateLimiterMiddleware>();
            app.MapControllers();
        }

        #endregion
    }
}