﻿using DDOSGuardService.Logic.Interfaces;
using DDOSGuardService.Properties;

namespace DDOSGuardService.Logic
{
    public class RequestRateLimiter(RecentRequestsCache cache, RateLimiterSettings settings) : IRateLimiter
    {
        #region Fields

        private readonly RecentRequestsCache _cache = cache;
        private readonly int _maxRequestsPerTimeFrame = settings.MaxRequestsPerTimeFrame;
        private readonly double _timeFrameInSeconds = settings.TimeFrameInSeconds;


        #endregion

        #region Public Methods

        public bool ShouldBlock(string id)
        {
            var requestTimestamp = DateTime.UtcNow;
            var recentRequests = _cache[id].Timestamps.ToList();
            var shouldBlock = DoRequestsExceedLimit(recentRequests, requestTimestamp);

            if (!shouldBlock)
            {
                _cache.Add(id, requestTimestamp);
            }

            return shouldBlock;
        }

        #endregion

        #region Private Methods

        public bool DoRequestsExceedLimit(IList<DateTime> timestamps, DateTime newTimestamp)
        {
            if (timestamps.Count < _maxRequestsPerTimeFrame)
            {
                return false;
            }

            var timeFrameStart = newTimestamp.AddSeconds(-_timeFrameInSeconds);
            var requestsInTimeFrame = timestamps.Where(timestamp => timestamp >= timeFrameStart);

            return requestsInTimeFrame.Count() >= _maxRequestsPerTimeFrame;
        }

        #endregion
    }
}