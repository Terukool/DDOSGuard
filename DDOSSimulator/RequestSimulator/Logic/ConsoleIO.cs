using RequestSimulatorClient.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
