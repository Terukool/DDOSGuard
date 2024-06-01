using RequestSimulatorClient.Logic;

const string SERVER_URL = "http://localhost:4293";
Tuple<int, int> waitRangeMs = new(100, 1500);

var cancellationTokenSource = new CancellationTokenSource();
ConsoleIO io = new();

io.Write("Enter the amount of HTTP clients to simulate:");
string numOfClientsInput = io.Read();
int numOfClients = int.Parse(numOfClientsInput);

var simulation = new Simulation(io, numOfClients, waitRangeMs, SERVER_URL);
simulation.Simulate(cancellationTokenSource.Token);

io.Write("Press Enter to stop the application...");
io.Read();

cancellationTokenSource.Cancel();