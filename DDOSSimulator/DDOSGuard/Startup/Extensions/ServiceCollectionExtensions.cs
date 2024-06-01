using DDOSGuardService.Logic.Interfaces;
using DDOSGuardService.Logic;
using DDOSGuardService.Properties;
using Microsoft.Extensions.Options;
using DDOSGuardService.Startup.Middleware;

namespace DDOSGuardService.Startup.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<RequestRateLimiterMiddleware>();
            services.AddSingleton<HttpExceptionMiddleware>();
            services.AddSingleton<RecentRequestsCache, InMemoryRecentRequestCache>();

            services.AddTransient<IRateLimiter, RequestRateLimiter>();
        }

        public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RateLimiterSettings>(configuration.GetSection(RateLimiterSettings.Key));

            services.AddSingleton(_ => _.GetRequiredService<IOptions<RateLimiterSettings>>().Value);
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}