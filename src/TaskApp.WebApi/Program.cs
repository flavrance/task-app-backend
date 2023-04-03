namespace TaskApp.WebApi
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using Serilog.Events;
    using System.IO;
    using Microsoft.AspNetCore.Routing;
    using System.Collections.Generic;
    using Microsoft.Extensions.Hosting;

    internal sealed class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IHost BuildWebHost(string[] args)
        { 
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webbuilder =>
                {
                    webbuilder.UseStartup<Startup>();
                    webbuilder.ConfigureAppConfiguration((builderContext, config) =>
                    {
                        config.SetBasePath(Directory.GetCurrentDirectory());
                        IWebHostEnvironment env = (IWebHostEnvironment)builderContext.HostingEnvironment;
                        config.AddJsonFile("autofac.json");
                        config.AddEnvironmentVariables();
                    });
                })
                .UseSerilog((hostingContext, loggerConfig) =>
                            loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
                            .MinimumLevel.Debug()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                            .Enrich.FromLogContext()
                            .WriteTo.File(Path.Combine(hostingContext.HostingEnvironment.ContentRootPath, $"logs/log-{System.DateTime.UtcNow.ToString(format: "yyyyMMdd")}.log")),
                            preserveStaticLogger: false,
                            writeToProviders: true
                        )
                        //.ConfigureServices(services => services.AddAutofac())
                        .Build();
        }
    }
}
