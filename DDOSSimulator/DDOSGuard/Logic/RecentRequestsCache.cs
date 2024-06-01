using DDOSGuardService.Logic.Interfaces;
using DDOSGuardService.Models;

namespace DDOSGuardService.Logic
{
    public abstract class RecentRequestsCache(int maxRequestsPerClient) : ICache<RequestState>
    {
        #region Fields

        private readonly int _maxRequestsPerClient = maxRequestsPerClient;

        #endregion

        #region Properties

        public abstract RequestState this[string id] { get; protected set; }

        #endregion

        #region Public Methods

        public void Add(string clientId, DateTime timestamp)
        {
            var updatedTimestamps = GetRecentRequestTimestamps(this[clientId].Timestamps, timestamp);

            this[clientId] = new RequestState(updatedTimestamps);
        }

        #endregion

        #region Private Methods

        private IEnumerable<DateTime> GetRecentRequestTimestamps(IEnumerable<DateTime> recentRequests, DateTime timestamp)
        {
            return recentRequests.Prepend(timestamp).Take(_maxRequestsPerClient);
        }

        #endregion
    }
}