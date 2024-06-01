using RequestSimulatorClient.Logic.Interfaces;

namespace RequestSimulatorClient.Logic
{
    class SimulationComposite(int numberOfSimulations, Func<string, ISimulation> spawnSimulation) : ISimulation
    {
        #region Fields

        private readonly Func<string, ISimulation> _spawnSimulation = spawnSimulation;
        private readonly int _numberOfSimulations = numberOfSimulations;

        #endregion

        #region Public Methods

        public void Simulate(CancellationToken token)
        {
            var clients = Enumerable.Range(0, _numberOfSimulations).Select(id => _spawnSimulation(id.ToString()));
            var threads = clients.Select(client => SpawnSimulationThread(client, token));

            threads.ToList().ForEach(thread => thread.Start());
        }

        #endregion

        #region Private Methods

        private Thread SpawnSimulationThread(ISimulation client, CancellationToken token)
        {
            return new Thread(() => client.Simulate(token));
        }

        #endregion
    }
}