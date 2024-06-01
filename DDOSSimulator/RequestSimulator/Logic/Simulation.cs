using RequestSimulatorClient.Logic.Interfaces;

namespace RequestSimulatorClient.Logic
{
    class Simulation(IInputOutput io, int numOfClients, Tuple<int, int> waitRangeMS, string serverUrl)
    {
        #region Fields

        private readonly IInputOutput _io = io;
        private readonly int _numOfClients = numOfClients;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly Random _random = new Random();
        private readonly Tuple<int, int> waitRangeMS = waitRangeMS;
        private readonly string _serverUrl = serverUrl;

        #endregion

        #region Public Methods

        public Task Simulate()
        {
            var clients = Enumerable.Range(0, numOfClients).Select(_ => new SimulatedHttpClient(_io, _httpClient, _random, waitRangeMS, _serverUrl));
            var threads = clients.Select(client => new Thread(client.Simulate));

            threads.ToList().ForEach(thread => thread.Start());
        }
    }
}
