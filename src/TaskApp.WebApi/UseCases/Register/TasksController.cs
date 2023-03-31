namespace TaskApp.WebApi.UseCases.Register
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using TaskApp.Application.Commands.Register;
    using TaskApp.WebApi.Model;
    using System.Collections.Generic;
    using MassTransit;
    using TaskApp.WorkerService.Core.Events;

    [Route("api/[controller]")]
    public sealed class TasksController : Controller
    {
        private readonly IRegisterUseCase registerService;
        private readonly IPublishEndpoint _publishEndpoint;

        public TasksController(IRegisterUseCase registerService, IPublishEndpoint publishEndpoint)
        {
            this.registerService = registerService;
            this._publishEndpoint = publishEndpoint;
        }

        /// <summary>
        /// Register a new Task
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterRequest request)
        {
            await _publishEndpoint.Publish<RegisterSavedEvent>(new RegisterSavedEvent{ Date = request.Date, 
             Description = request.Description, 
            Status = request.Status});

            return Ok();
            /*
            RegisterResult result = await registerService.Execute(
                request.Description, request.Date, (Domain.Tasks.TaskStatusEnum)request.Status);

            TaskDetailsModel task = new TaskDetailsModel(
                result.Task.TaskId,
                result.Task.Description,
                result.Task.Date,
                (int)result.Task.Status);

            return CreatedAtRoute("GetTask", new { taskId = task.TaskId }, task);*/
        }
    }
}
