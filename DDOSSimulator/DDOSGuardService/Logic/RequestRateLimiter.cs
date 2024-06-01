using DDOSGuardService.Logic.Interfaces;
using DDOSGuardService.Models;
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
            var requestTimestamp = DateTime.Now;
            var shouldStartNewWindow = ShouldStartNewWindow(id, requestTimestamp);

            if (shouldStartNewWindow)
            {
                _cache.ResetWindow(id, requestTimestamp);
            }

            var shouldBlock = DoesRequestExceedLimit(id);

            if (!shouldBlock)
            {
                _cache.CacheRequest(id);
            }

            return shouldBlock;
        }

        #endregion

        #region Private Methods

        private bool ShouldStartNewWindow(string windowId, DateTime newRequestTimestamp)
        {
            if (_cache.DoesExpire)
            {
                return false;
            }

            var windowState = _cache[windowId];
            var hasWindowExpired = HasTimeframePassed(newRequestTimestamp, windowState);

            return hasWindowExpired;
        }

        private bool DoesRequestExceedLimit(string windowId)
        {
            var windowState = _cache[windowId];

            return windowState.Count >= _maxRequestsPerTimeFrame;
        }

        private bool HasTimeframePassed(DateTime newRequestTimestamp, RequestWindowState windowState)
        {
            var timeSinceStart = newRequestTimestamp - windowState.StartTimestamp;
            var timeframe = TimeSpan.FromSeconds(_timeFrameInSeconds);

            return timeSinceStart >= timeframe;
        }

        #endregion
    }
}