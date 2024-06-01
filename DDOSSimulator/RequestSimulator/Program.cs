using RequestSimulatorClient.Logic;
using RequestSimulatorClient.Logic.Interfaces;

const string SERVER_URL = "http://localhost:4293";
Tuple<int, int> waitRangeMs = new(100, 1500);

IInputOutput io = new ConsoleIO();
var httpClient = new HttpClient();
var random = new Random();

io.Write("Enter the amount of HTTP clients to simulate:");
string numOfClientsInput = io.Read();
int numOfClients = int.Parse(numOfClientsInput);

var clients = Enumerable.Range(0, numOfClients).Select(_ => new SimulatedHttpClient(io, httpClient, random, waitRangeMs, SERVER_URL));
var threads = clients.Select(client => new Thread(client.Simulate));

threads.ToList().ForEach(thread => thread.Start());

io.Write("Press Enter to stop the application...");
io.Read();
