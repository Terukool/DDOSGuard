using DDOSGuardService.Models;
using DDOSGuardService.Properties;
using System.Collections.Concurrent;

namespace DDOSGuardService.Logic
{
    public class InMemoryRecentRequestCache(RateLimiterSettings rateLimiterSettings) : RecentRequestsCache(rateLimiterSettings.MaxRequestsPerTimeFrame)
    {
        #region Fields

        private readonly ConcurrentDictionary<string, RequestState> _cache = new();

        #endregion

        #region Properties

        public override RequestState this[string id]
        {
            get => _cache.GetOrAdd(id, _ => new RequestState());
            protected set => _cache[id] = value;
        }

        #endregion
    }
}