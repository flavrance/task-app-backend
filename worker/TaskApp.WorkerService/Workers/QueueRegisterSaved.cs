using MassTransit;
using MassTransit.Metadata;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TaskApp.Application.Commands.Register;
using TaskApp.WorkerService.Core.Events;

namespace TaskApp.WorkerService.Workers
{
    public class QueueRegisterSavedConsumer : IConsumer<RegisterSavedEvent>
    {
        private readonly ILogger<QueueRegisterSavedConsumer> _logger;
        private readonly IRegisterUseCase registerService;

        public QueueRegisterSavedConsumer(ILogger<QueueRegisterSavedConsumer> logger, IRegisterUseCase registerService)
        {
            _logger = logger;
            this.registerService = registerService;
        }

        public async Task Consume(ConsumeContext<RegisterSavedEvent> context)
        {
            var timer = Stopwatch.StartNew();

            try
            {
                var description = context.Message.Description;
                var date = context.Message.Date;
                var status = context.Message.Status;

                registerService.Execute(description, date, (Domain.Tasks.TaskStatusEnum)status);                    

                _logger.LogInformation($"Receive Register: {description} - {date} - {status}");
                await context.NotifyConsumed(timer.Elapsed, TypeMetadataCache<RegisterSavedEvent>.ShortName);
            }
            catch (Exception ex)
            {
                await context.NotifyFaulted(timer.Elapsed, TypeMetadataCache<RegisterSavedEvent>.ShortName, ex);
            }
        }
    }

    public class QueueRegisterConsumerDefinition : ConsumerDefinition<QueueRegisterSavedConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<QueueRegisterSavedConsumer> consumerConfigurator)
        {
            consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
        }
    }
}
