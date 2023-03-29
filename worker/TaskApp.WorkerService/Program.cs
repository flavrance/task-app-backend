namespace TaskApp.WorkerService
{
    using MassTransit;
    using Microsoft.Extensions.Hosting;
    using TaskApp.WorkerService.Core.Extensions;    
    using Serilog;
    using Microsoft.Extensions.Configuration;
    using TaskApp.WorkerService.Workers;
    using Microsoft.Extensions.DependencyInjection;
    using TaskApp.Application.Commands.Register;

    internal sealed class Program 
    {
        public static async Task<int> BuildHost(string[] args)
        {
            try
            {
                Log.Information("Starting host");

                var host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddTransient<IRegisterUseCase, RegisterUseCase>();
                        services.AddMassTransit(x =>
                        {
                            x.AddConsumer<QueueRegisterSaved>();                            

                            x.UsingRabbitMq((context, cfg) =>
                            {
                                cfg.Host(hostContext.Configuration.GetConnectionString("RabbitMq"));
                                cfg.ConfigureEndpoints(context);
                            });
                        });

                    })
                    .UseSerilog()                    
                    .Build();

                await host.RunAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");

                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
        public static void Main(string[] args)
        {
            SerilogExtension.AddSerilog();

            _ = BuildHost(args);
        }
    }
}
