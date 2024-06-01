using DDOSGuardService.Startup.Extensions;
using DDOSGuardService.Startup.Middleware;

namespace DDOSGuardService.Startup
{
    public class AppStartupConfiguration
    {
        const string SETTINGS_FILE = "appsettings.json";

        #region Public Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ConfigureDependencyInjection();
            services.ConfigureSettings(SETTINGS_FILE);
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