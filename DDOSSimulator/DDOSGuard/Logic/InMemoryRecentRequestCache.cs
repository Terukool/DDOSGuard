using DDOSGuardService.Models;
using DDOSGuardService.Properties;
using Microsoft.Extensions.Caching.Memory;

namespace DDOSGuardService.Logic
{
    public class InMemoryRecentRequestCache(RateLimiterSettings rateLimiterSettings) : RecentRequestsCache(rateLimiterSettings.MaxRequestsPerTimeFrame)
    {
        #region Fields

        private readonly MemoryCache _cache = new(new MemoryCacheOptions
        {
            SizeLimit = 100000
        });
        private readonly MemoryCacheEntryOptions _cacheEntryOptions = new ()
        {
            SlidingExpiration = TimeSpan.FromSeconds(rateLimiterSettings.TimeFrameInSeconds),
            Size = 1,
            Priority = CacheItemPriority.High
        };

        #endregion

        #region Properties

        public override RequestState this[string id]
        {
            get
            {
                _cache.TryGetValue(id, out RequestState? requestState);

                return requestState ?? new RequestState();
            }
            protected set => _cache.Set(id, value, _cacheEntryOptions);
        }

        #endregion
    }
}