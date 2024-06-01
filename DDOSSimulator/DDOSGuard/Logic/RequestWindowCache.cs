using DDOSGuardService.Logic.Interfaces;
using DDOSGuardService.Models;

namespace DDOSGuardService.Logic
{
    public abstract class RequestWindowCache() : ICache<RequestWindowState>
    {
        #region Properties

        public virtual bool DoesExpire => false;

        public abstract RequestWindowState this[string id] { get; protected set; }

        #endregion

        #region Public Methods

        public void CacheRequest(string clientId)
        {
            var currentWindow = this[clientId];
            this[clientId] = new RequestWindowState(currentWindow.StartTimestamp, currentWindow.Count + 1);
        }

        public void ResetWindow(string clientId, DateTime timestamp)
        {
            this[clientId] = new RequestWindowState(timestamp);
        }

        #endregion
    }
}