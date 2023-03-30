using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TaskApp.WorkerService.Core.Extensions 
{
    public static class MassTransitExtension
    {
        public static void AddMassTransitExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddDelayedMessageScheduler();
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("RabbitMq"));

                    cfg.UseDelayedMessageScheduler();
                    cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
                    cfg.UseMessageRetry(retry => { retry.Interval(3, TimeSpan.FromSeconds(5)); });
                });
            });
        }
        public static void AddMassTransitPublisher(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(bus =>
            {
                bus.UsingRabbitMq((ctx, busConfigurator) =>
                {
                    busConfigurator.Host(configuration.GetConnectionString("RabbitMq"));
                });
            });

            services.Configure<MassTransitHostOptions>(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromSeconds(30);
                options.StopTimeout = TimeSpan.FromMinutes(1);
            });
            
        }
    }
}


