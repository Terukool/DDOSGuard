using RequestSimulatorClient.Logic.Interfaces;

namespace RequestSimulatorClient.Logic
{
    class Simulation(IInputOutput io, int numOfClients, Tuple<int, int> waitRangeMS, string serverUrl) : ISimulation
    {
        #region Fields

        private readonly IInputOutput _io = io;
        private readonly int _numOfClients = numOfClients;
        private readonly HttpClient _httpClient = new();
        private readonly Random _random = new();
        private readonly Tuple<int, int> waitRangeMS = waitRangeMS;
        private readonly string _serverUrl = serverUrl;

        #endregion

        #region Public Methods

        public void Simulate(CancellationToken token)
        {
            var clients = Enumerable.Range(0, _numOfClients).Select(_ => new SimulatedHttpClient(_io, _httpClient, _random, waitRangeMS, _serverUrl));
            var threads = clients.Select(client => new Thread(_ => client.Simulate(token)));

            threads.ToList().ForEach(thread => thread.Start());
        }

        #endregion
    }
}
