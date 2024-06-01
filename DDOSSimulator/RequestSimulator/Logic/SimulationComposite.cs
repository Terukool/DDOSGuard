using RequestSimulatorClient.Logic.Interfaces;

namespace RequestSimulatorClient.Logic
{
    class SimulationComposite(int numberOfSimulations, Func<ISimulation> spawnSimulation) : ISimulation
    {
        #region Fields

        private readonly Func<ISimulation> _spawnSimulation = spawnSimulation;
        private readonly int _numberOfSimulations = numberOfSimulations;

        #endregion

        #region Public Methods

        public void Simulate(CancellationToken token)
        {
            var clients = Enumerable.Range(0, _numberOfSimulations).Select(_ => _spawnSimulation());
            var threads = clients.Select(client => new Thread(() => client.Simulate(token)));

            threads.ToList().ForEach(thread => thread.Start());
        }

        #endregion
    }
}