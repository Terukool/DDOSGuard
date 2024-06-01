namespace RequestSimulatorClient.Logic.Interfaces
{
    public interface ISimulation
    {
        void Simulate(CancellationToken token);
    }
}