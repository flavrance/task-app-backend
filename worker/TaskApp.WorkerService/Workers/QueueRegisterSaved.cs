using MassTransit;
using Microsoft.Extensions.Logging;
using TaskApp.Application.Commands.Register;
using TaskApp.WorkerService.Core.Events;

namespace TaskApp.WorkerService.Workers
{
    public class QueueRegisterSaved : IConsumer<RegisterSavedEvent>
    {
        private readonly ILogger<QueueRegisterSaved> _logger;
        private readonly IRegisterUseCase registerService;

        public QueueRegisterSaved(ILogger<QueueRegisterSaved> logger, IRegisterUseCase registerService)
        {
            _logger = logger;
            this.registerService = registerService;
        }

        public Task Consume(ConsumeContext<RegisterSavedEvent> context)
        {
            _logger.LogInformation($"Received Register: {context.Message.Description}");

            registerService.Execute(
                context.Message.Description, context.Message.Date, (Domain.Tasks.TaskStatusEnum)context.Message.Status);
            
            return Task.CompletedTask;
        }
    }
}
