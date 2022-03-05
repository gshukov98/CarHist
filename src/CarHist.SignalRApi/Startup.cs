using Elders.Cronus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarHist.SignalRApi.Hubs;

namespace CarHist.SignalRApi;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc(o =>
        {
            var noContentFormatter = o.OutputFormatters.OfType<HttpNoContentOutputFormatter>().FirstOrDefault();
            if (noContentFormatter != null)
            {
                noContentFormatter.TreatNullValueAsNoContent = false;
            }
        });

        services.AddCronus(configuration);

        services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowCredentials();
            }));

        services.AddSignalR();
        services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { "application/octet-stream" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseCors("CorsPolicy");

        app.UseResponseCompression();


        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<CarsHub>("/hub/cars");
        });
    }
}
