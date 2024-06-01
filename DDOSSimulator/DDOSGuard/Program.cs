using DDOSGuardService.Startup;

ThreadPool.SetMinThreads(Environment.ProcessorCount, Environment.ProcessorCount);
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
var startupConfiguration = new AppStartupConfiguration();

startupConfiguration.ConfigureServices(builder.Services);




var app = builder.Build();

startupConfiguration.ConfigureApp(app);

await app.StartAsync();

Console.WriteLine("Press Enter to stop the application...");
Console.ReadLine();

await app.StopAsync();