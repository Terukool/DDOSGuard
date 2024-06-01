using DDOSGuardService.Logic.Interfaces;
using DDOSGuardService.Properties;

namespace DDOSGuardService.Logic
{
    public class RequestRateLimiter(RequestWindowCache cache, RateLimiterSettings settings) : IRateLimiter
    {
        #region Fields

        private readonly RequestWindowCache _cache = cache;
        private readonly int _maxRequestsPerTimeFrame = settings.MaxRequestsPerTimeFrame;
        private readonly double _timeFrameInSeconds = settings.TimeFrameInSeconds;

        #endregion

        #region Public Methods

        public bool ShouldBlock(string id)
        {
            var requestTimestamp = DateTime.UtcNow;
            var shouldBlock = DoesRequestExceedLimit(id);

            if (!shouldBlock)
            {
                UpdateWindowCache(id, requestTimestamp);
            }

            return shouldBlock;
        }

        #endregion

        #region Private Methods

        private bool DoesRequestExceedLimit(string windowId)
        {
            var windowState = _cache[windowId];

            return windowState.Count >= _maxRequestsPerTimeFrame;
        }

        private void UpdateWindowCache(string windowId, DateTime newRequestTimestamp)
        {
            if (ShouldStartNewWindow(windowId, newRequestTimestamp))
            {
                _cache.ResetWindow(windowId, newRequestTimestamp);
            }

            _cache.CacheRequest(windowId);
        }

        private bool ShouldStartNewWindow(string windowId, DateTime newRequestTimestamp)
        {
            if (_cache.DoesExpire)
            {
                return false;
            }

            var windowState = _cache[windowId];

            return newRequestTimestamp - windowState.StartTimestamp >= TimeSpan.FromSeconds(_timeFrameInSeconds);
        }

        #endregion
    }
}