using CarHist;
using CarHist.Service;
using Elders.Cronus;
using Log;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Serilog;
using Serilog.Extensions.Logging;

Environment.SetEnvironmentVariable("log_name", App.LogName, EnvironmentVariableTarget.Process);

Start.WithStartupDiagnostics(App.Name, () =>
{
    IHost host = Host.CreateDefaultBuilder(args)
        .UseCronusStartupLogger(new SerilogLoggerProvider().CreateLogger(nameof(Program)))
        .ConfigureAppConfiguration(x => x.AddEnvironmentVariables())
        .ConfigureServices((hostContext, services) =>
        {
            services.AddOptions();
            services.AddLogging();
            services.AddHostedService<Worker>();
            services.AddCronus(hostContext.Configuration);
            services.AddSignalR();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        })
        .UseSerilog(SerilogConfiguration.Configure)
        .Build();

    host.Run();
});
