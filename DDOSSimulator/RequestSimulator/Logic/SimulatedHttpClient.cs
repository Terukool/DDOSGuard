using RequestSimulatorClient.Logic.Interfaces;
using System.Net;

namespace RequestSimulatorClient.Logic
{
    public class SimulatedHttpClient(IInputOutput io, HttpClient httpClient, Random random, Tuple<int, int> waitRangeMS, string serverUrl)
    {
        #region Constants

        private const string ID_QUERY_KEY = "clientId";

        #endregion

        #region Fields


        private readonly IInputOutput _io = io;
        private readonly HttpClient _httpClient = httpClient;
        private readonly Random _random = random;
        private readonly Tuple<int, int> waitRangeMS = waitRangeMS;
        private readonly string _serverUrl = serverUrl;

        private readonly string _id = Guid.NewGuid().ToString();

        #endregion

        #region Public Methods

        public Task Simulate()
        {
            _io.Write($"Client {_id} started simulation\n");

            while (true)
            {
                SimulateRequest();
                WaitRandomTime();
            }
        }
        #endregion

        #region Private Methods

        private void SimulateRequest()
        {
            try
            {
                var responseStatusCode = SendRequest();

                _io.Write($"Client {_id} received response code: {responseStatusCode}");
            }
            catch (Exception ex)
            {
                _io.Write($"Client {_id} failed to send request: {ex.Message}");
            }
        }

        private void WaitRandomTime()
        {
            var (minTime, maxTime) = waitRangeMS;
            var delayMS = _random.Next(minTime, maxTime);

            Thread.Sleep(delayMS);
        }

        private HttpStatusCode SendRequest()
        {
            var url = ConstructServerUrl();
            var response = _httpClient.GetAsync(url).Result;
            var status = response.StatusCode;

            return status;
        }

        private string ConstructServerUrl()
        {
            return $"{_serverUrl}?{ID_QUERY_KEY}={_id}";
        }

        #endregion
    }
}
