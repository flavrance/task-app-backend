namespace TaskApp.WebApi.UseCases.Delete
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using TaskApp.Application.Commands.Task;
    using TaskApp.Application.Results;

    [Route("api/[controller]")]
    public sealed class TasksController : Controller
    {
        private readonly ITaskUseCase taskService;

        public TasksController(ITaskUseCase taskService)
        {
            this.taskService = taskService;
        }

        /// <summary>
        /// Debit from an cash flow
        /// </summary>
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> Delete(Guid taskId)
        {
            TaskResult taskResult = await taskService.Execute(taskId);

            if (taskResult == null)
            {
                return new NoContentResult();
            }
            

            return Ok();
        }
    }
}
