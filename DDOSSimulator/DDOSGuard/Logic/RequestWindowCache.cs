using DDOSGuardService.Logic.Interfaces;
using DDOSGuardService.Models;

namespace DDOSGuardService.Logic
{
    public abstract class RequestWindowCache(double timeFrameInSeconds) : ICache<RequestWindowState>
    {
        #region Fields

        protected readonly double _timeFrameInSeconds = timeFrameInSeconds;

        #endregion

        #region Properties

        public virtual bool DoesExpire => false;

        public abstract RequestWindowState this[string id] { get; protected set; }

        #endregion

        #region Public Methods

        public void CacheRequest(string clientId)
        {
            var (start, expire, count) = this[clientId];
            this[clientId] = new RequestWindowState(start, expire, count + 1);
        }

        public void ResetWindow(string clientId, DateTime timestamp)
        {
            this[clientId] = new RequestWindowState(timestamp, _timeFrameInSeconds);
        }

        #endregion
    }
}