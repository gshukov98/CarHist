using System.Net;
using Elders.Cronus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarHist.SignalRApi
{
    public static class SignalRApiStartup
    {
        private static readonly ILogger logger = CronusLogger.CreateLogger(typeof(SignalRApiStartup));

        public static IHost GetHost()
        {
            // change it
            logger.Info(() => $"Starting Cronus API.{Environment.NewLine}If you are not able to access it using DNS or public IP make sure that you have firewall rule and urlacl setup on the hosting machine.{Environment.NewLine}Example firewall: netsh advfirewall firewall add rule name=\"Cronus\" dir=in action=allow localport=7477 protocol=tcp{Environment.NewLine}Example urlacl: netsh http add urlacl url=http://[::]:7477 user=Everyone listen=yes");

            var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddCronus(context.Configuration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel((context, options) =>
                    {
                        options.Listen(IPAddress.Any, 17677, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                        });
                    });

                    webBuilder.UseStartup<Startup>();
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                    options.ValidateOnBuild = false;
                })

                .Build();

            return host;
        }
    }
}
