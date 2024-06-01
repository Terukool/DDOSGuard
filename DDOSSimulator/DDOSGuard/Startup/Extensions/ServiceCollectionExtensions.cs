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
            services.AddSingleton<RecentRequestsCache, InMemoryRecentRequestCache>();

            services.AddTransient<IRateLimiter, RequestRateLimiter>();
            services.AddTransient<RequestRateLimiterMiddleware>();
            services.AddTransient<HttpExceptionMiddleware>();
        }

        public static void ConfigureSettings(this IServiceCollection services, string settingsPath)
        {
            var Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(settingsPath, optional: false, reloadOnChange: true)
                .Build();

            services.Configure<RateLimiterSettings>(Configuration.GetSection(RateLimiterSettings.Key));

            services.AddSingleton(_ => _.GetRequiredService<IOptions<RateLimiterSettings>>().Value);
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}