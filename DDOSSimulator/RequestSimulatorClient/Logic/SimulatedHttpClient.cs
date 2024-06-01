using RequestSimulatorClient.Logic.Interfaces;
using System.Net;
using System.Web;

namespace RequestSimulatorClient.Logic
{
    public class SimulatedHttpClient(IInputOutput io, HttpClient httpClient, Random random, Tuple<int, int> waitRangeMS, string serverUrl, string id) : ISimulation
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
        private readonly string _id = id;

        #endregion

        #region Public Methods

        public void Simulate(CancellationToken token)
        {
            _io.Write($"Client {_id} started simulation");

            while (!token.IsCancellationRequested)
            {
                SimulateRequest().Wait(token);
                WaitRandomTime();
            }
        }
        #endregion

        #region Private Methods

        private async Task SimulateRequest()
        {
            try
            {
                var responseStatusCode = await SendRequest();

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

        private async Task<HttpStatusCode> SendRequest()
        {
            var url = ConstructServerUrl();
            var response = await _httpClient.GetAsync(url);
            var status = response.StatusCode;

            return status;
        }

        private string ConstructServerUrl()
        {
            var uriBuilder = new UriBuilder(_serverUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[ID_QUERY_KEY] = _id;
            uriBuilder.Query = query.ToString();

            return uriBuilder.ToString();
        }

        #endregion
    }
}