using Elders.Cronus;
using Elders.Cronus.Api;
using Elders.Cronus.Discoveries;
using Microsoft.AspNetCore.Hosting;

namespace CarHist.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    //private readonly ICronusApiAccessor accessor;
    private readonly ICronusHost _cronusHost;
    private IHost _cronusDashboard;
    //private IHost signalRHost;

    public Worker(IServiceProvider provider, ICronusHost cronusHost, ILogger<Worker> logger /*ICronusApiAccessor accessor*/)
    {
        _cronusHost = cronusHost;
        _logger = logger;
        //this.accessor = accessor;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting service...");

        _cronusHost.Start();
        _cronusDashboard = CronusApi.GetHost(); // CronusApi SignalR won't be working 
        await _cronusDashboard.StartAsync(stoppingToken);

        //signalRHost = SignalRApiStartup.GetHost();
        //await signalRHost.StartAsync(stoppingToken);
        //accessor.Provider = signalRHost.Services;


        _logger.LogInformation("Service started!");
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping service...");

        _cronusHost.Stop();
        _cronusDashboard.StopAsync(TimeSpan.FromSeconds(1));

        //signalRHost.StopAsync(TimeSpan.FromSeconds(1));
        //signalRHost?.Dispose();

        _logger.LogInformation("Service stopped!");
        return Task.CompletedTask;
    }
}
