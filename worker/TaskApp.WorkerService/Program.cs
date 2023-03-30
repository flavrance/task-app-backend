using System;
using System.Threading;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TaskApp.Application.Commands.Register;
using TaskApp.Application.Repositories;
using TaskApp.Infrastructure.MongoDataAccess;
using TaskApp.Infrastructure.MongoDataAccess.Repositories;
using TaskApp.WorkerService.Core.Extensions;
using TaskApp.WorkerService.Workers;

try
{
    var builder = WebApplication.CreateBuilder(args);

    IConfiguration Configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

    //builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());


    Log.Information("Starting Worker");

    var host = Host.CreateDefaultBuilder(args)
        .UseSerilog(Log.Logger)
        .ConfigureServices((context, collection) =>
        {
            collection.AddSingleton<Context>(new Context(Configuration.GetConnectionString("MongoDb"), Configuration.GetSection("MongoDB").GetValue<string>("DatabaseName")));
            collection.AddScoped<ITaskWriteOnlyRepository, TaskRepository>();
            collection.AddScoped<IRegisterUseCase, RegisterUseCase>();

            collection.AddMassTransit(x =>
            {
                x.AddDelayedMessageScheduler();
                x.AddConsumer<QueueRegisterSaved>();
                

                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(context.Configuration.GetConnectionString("RabbitMq"));
                    cfg.UseDelayedMessageScheduler();
                    //cfg.ConnectReceiveObserver(new ReceiveObserverExtensions());
                    cfg.ServiceInstance(instance =>
                    {
                        instance.ConfigureJobServiceEndpoints();
                        instance.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
                    });
                });
            });
        }).Build();

    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}