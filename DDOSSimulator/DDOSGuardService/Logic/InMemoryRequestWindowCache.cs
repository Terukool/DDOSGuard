using DDOSGuardService.Models;
using DDOSGuardService.Properties;
using Microsoft.Extensions.Caching.Memory;

namespace DDOSGuardService.Logic
{
    public class InMemoryRequestWindowCache(RateLimiterSettings rateLimiterSettings) : RequestWindowCache(rateLimiterSettings.TimeFrameInSeconds)
    {
        #region Constants

        private const int CACHE_ITEM_SIZE = 1;
        private const CacheItemPriority CACHE_ITEM_PRIORITY = CacheItemPriority.High;

        #endregion

        #region Fields

        private readonly MemoryCache _cache = new(new MemoryCacheOptions
        {
            SizeLimit = rateLimiterSettings.CacheSize
        });

        #endregion

        #region Properties

        public override bool DoesExpire => true;

        public override RequestWindowState this[string id]
        {
            get
            {
                _cache.TryGetValue(id, out RequestWindowState? requestState);

                return requestState ?? new RequestWindowState(_timeFrameInSeconds);
            }
            protected set 
            {
                var cacheEntryOptions = GetMemoryCacheEntryOptions(value);

                _cache.Set(id, value, cacheEntryOptions);
            }
        }

        #endregion

        #region Private Methods

        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions(RequestWindowState currentState)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                Size = CACHE_ITEM_SIZE,
                Priority = CACHE_ITEM_PRIORITY,
                AbsoluteExpiration = currentState.ExpireTimestamp
            };

            return cacheEntryOptions;

        }

        #endregion
    }
}