namespace DDOSGuardService.Models
{
    public class RequestWindowState(DateTime timestamp, int count = 0)
    {
        #region Properties

        public DateTime StartTimestamp { get; private set; } = timestamp;
        public int Count { get; protected set; } = count;

        #endregion

        #region C'tor

        public RequestWindowState() : this(DateTime.Now) { }

        #endregion
    }
}