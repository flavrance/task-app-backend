using MassTransit;
using Microsoft.AspNetCore.Builder;
using TaskApp.WorkerService.Core;
using TaskApp.WorkerService.Core.Events;
using TaskApp.WorkerService.Core.Extensions;
using TaskApp.WorkerService.Workers;
using Serilog;
using TaskApp.Application.Commands.Register;
using TaskApp.Infrastructure.MongoDataAccess;
using TaskApp.Infrastructure.MongoDataAccess.Repositories;
using TaskApp.Application.Repositories;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddSerilog("TaskApp Worker");
    Log.Information("Starting Worker");

    var host = Host.CreateDefaultBuilder(args)
        .UseSerilog(Log.Logger)
        .ConfigureServices((context, collection) =>
        {
            var appSettings = new AppSettings();
            context.Configuration.Bind(appSettings);
            collection.AddOpenTelemetry(appSettings);
            collection.AddHttpContextAccessor();
            collection.AddSingleton<Context>(mongoContext => new Context(context.Configuration.GetConnectionString("MongoDb"),
                context.Configuration.GetSection("MongoDB").GetValue<string>("DatabaseName")));
            collection.AddScoped<ITaskWriteOnlyRepository, TaskRepository>();
            collection.AddScoped<IRegisterUseCase, RegisterUseCase>();
            



            collection.AddMassTransit(x =>
            {
                x.AddDelayedMessageScheduler();                
                x.AddConsumer<QueueRegisterSavedConsumer>(typeof(QueueRegisterConsumerDefinition));                

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