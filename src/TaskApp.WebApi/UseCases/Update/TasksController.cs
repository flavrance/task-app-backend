namespace TaskApp.WebApi.UseCases.Update
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using TaskApp.Application.Commands.Task;
    using TaskApp.Application.Results;

    [Route("api/[controller]")]
    public sealed class TasksController : Controller
    {
        private readonly ITaskUseCase taskService;

        public TasksController(
            ITaskUseCase taskService)
        {
            this.taskService = taskService;
        }

        /// <summary>
        /// Update an task
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateRequest request)
        {
            TaskResult taskResult = await taskService.Execute(
                request.TaskId,
                request.Description,
                request.Date,
                (Domain.Tasks.TaskStatusEnum)request.Status);

            if (taskResult == null)
            {
                return new NoContentResult();
            }

            Model model = new Model(
                taskResult.TaskId,
                taskResult.Description,
                taskResult.Date,
                (int)taskResult.Status
            );

            return new ObjectResult(model);
        }
    }
}
