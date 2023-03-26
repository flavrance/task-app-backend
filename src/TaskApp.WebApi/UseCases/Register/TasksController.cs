namespace TaskApp.WebApi.UseCases.Register
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using TaskApp.Application.Commands.Register;
    using TaskApp.WebApi.Model;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    public sealed class TasksController : Controller
    {
        private readonly IRegisterUseCase registerService;

        public TasksController(IRegisterUseCase registerService)
        {
            this.registerService = registerService;
        }

        /// <summary>
        /// Register a new Task
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterRequest request)
        {
            RegisterResult result = await registerService.Execute(
                request.Description, request.Date, (Domain.Tasks.TaskStatusEnum)request.Status);

            TaskDetailsModel task = new TaskDetailsModel(
                result.Task.TaskId,
                result.Task.Description,
                result.Task.Date,
                (int)result.Task.Status);

            return CreatedAtRoute("GetTask", new { taskId = task.TaskId }, task);
        }
    }
}
