using RequestSimulatorClient.Logic;

const string SERVER_URL = "http://localhost:4293";
const int MIN_WAIT_MS = 100;
const int MAX_WAIT_MS = 1500;

var waitRangeMs = new Tuple<int, int>(MIN_WAIT_MS, MAX_WAIT_MS);
var cancellationTokenSource = new CancellationTokenSource();
var io = new ConsoleIO();

io.Write("Enter the amount of HTTP clients to simulate:");
string numOfClientsInput = io.Read();
int.TryParse(numOfClientsInput, out int numOfClients);

if (numOfClients <= 0)
{
    io.Write("The number of clients must be greater than 0.");
    return;
}

var simulation = new Simulation(io, numOfClients, waitRangeMs, SERVER_URL);
simulation.Simulate(cancellationTokenSource.Token);

io.Write("Press Enter to stop the application...");
io.Read();

cancellationTokenSource.Cancel();