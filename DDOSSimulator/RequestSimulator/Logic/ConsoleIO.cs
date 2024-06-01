using RequestSimulatorClient.Logic.Interfaces;

namespace RequestSimulatorClient.Logic
{
    public class ConsoleIO : IInputOutput
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public string Read()
        {
            var input = Console.ReadLine();

            ArgumentNullException.ThrowIfNull(input, "Input cannot be null!");

            return input;
        }
    }
}