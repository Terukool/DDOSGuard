namespace RequestSimulatorClient.Logic.Interfaces
{
    public interface IInputOutput
    {
        void Write(string message);
        string Read();
    }
}
