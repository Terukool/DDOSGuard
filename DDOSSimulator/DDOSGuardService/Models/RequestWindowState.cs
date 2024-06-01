namespace DDOSGuardService.Models
{
    public class RequestWindowState
    {
        #region Properties

        public DateTime StartTimestamp { get; private set; }
        public DateTime ExpireTimestamp { get; private set; }
        public int Count { get; private set; } = 0;

        #endregion

        #region C'tor

        public RequestWindowState(DateTime timestamp, double timeFrameInSeconds)
        {
            StartTimestamp = timestamp;
            ExpireTimestamp = timestamp.AddSeconds(timeFrameInSeconds);
        }

        public RequestWindowState(double timeFrameInSeconds) : this(DateTime.Now, timeFrameInSeconds) { }

        public RequestWindowState(DateTime timestamp, DateTime expireTimestamp, int count = 0)
        {
            StartTimestamp = timestamp;
            ExpireTimestamp = expireTimestamp;
            Count = count;
        }

        #endregion

        #region Public Methods

        public void Deconstruct(out DateTime startTimestamp, out DateTime expireTimestamp, out int count)
        {
            startTimestamp = StartTimestamp;
            expireTimestamp = ExpireTimestamp;
            count = Count;
        }

        #endregion
    }
}