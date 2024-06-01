using RequestSimulatorClient.Logic;
using RequestSimulatorClient.Logic.Interfaces;

const string SERVER_URL = "http://localhost:4293";
const int MIN_WAIT_MS = 100;
const int MAX_WAIT_MS = 1500;

var waitRangeMs = new Tuple<int, int>(MIN_WAIT_MS, MAX_WAIT_MS);
using var cancellationTokenSource = new CancellationTokenSource();
var httpClient = new HttpClient();
var random = new Random();
var io = new ConsoleIO();

io.Write("Enter the amount of HTTP clients to simulate:");
string numberOfClientsInput = io.Read();

var didParseSucceed = int.TryParse(numberOfClientsInput, out int numberOfClients);

if (!didParseSucceed || numberOfClients <= 0)
{
    io.Write("The number of clients must be greater than 0.");

    return;
}

var spawnClient = new Func<string, ISimulation>((id) => new SimulatedHttpClient(io, httpClient, random, waitRangeMs, SERVER_URL, id));
var simulation = new SimulationComposite(numberOfClients, spawnClient);

simulation.Simulate(cancellationTokenSource.Token);

io.Write("Press Enter to stop the application...");
io.Read();

cancellationTokenSource.Cancel();