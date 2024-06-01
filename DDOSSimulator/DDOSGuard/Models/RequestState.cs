namespace DDOSGuardService.Models
{
    public class RequestState()
    {
        #region Properties

        public IEnumerable<DateTime> Timestamps { get; private set; } = [];

        #endregion

        #region C'tor

        public RequestState(IEnumerable<DateTime> timestamps) : this()
        {
            Timestamps = timestamps;
        }

        #endregion
    }
}