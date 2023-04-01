namespace TaskApp.WebApi.UseCases.GetTaskDetails
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using TaskApp.Application.Queries;
    using TaskApp.WebApi.Model;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    public sealed class TasksController : Controller
    {
        private readonly ITaskQueries TasksQueries;

        public TasksController(
            ITaskQueries TasksQueries)
        {
            this.TasksQueries = TasksQueries;
        }

        /// <summary>
        /// Get an task
        /// </summary>
        [HttpGet("{taskId}", Name = "GetTask")]
        public async Task<IActionResult> Get(Guid taskId)
        {
            var task = await TasksQueries.GetTask(taskId);            

            return new ObjectResult(new TaskDetailsModel(
                task.TaskId,
                task.Description,
                task.Date.ToString("yyyy-MM-dd"),
                (int)task.Status));
        }
    }
}
