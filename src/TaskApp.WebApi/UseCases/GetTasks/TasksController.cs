namespace TaskApp.WebApi.UseCases.GetTasks
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
        /// Get tasks
        /// </summary>
        [HttpGet(Name = "GetTasks")]
        public async Task<IActionResult> Get()
        {
            var taskCollection = await TasksQueries.GetTasks();
            IList<TaskDetailsModel> result = new List<TaskDetailsModel>();
            foreach(var task in taskCollection.GetTasks()) {

                result.Add(new TaskDetailsModel(
                task.TaskId,
                task.Description,
                task.Date,
                (int)task.Status));

            }
            
            return new ObjectResult(result);
        }

        /// <summary>
        /// Get tasks by description
        /// </summary>
        [HttpGet("{description}", Name = "GetTasksByDescription")]
        public async Task<IActionResult> Get(string desription)
        {
            var taskCollection = await TasksQueries.GetTasksByDescription(desription);
            IList<TaskDetailsModel> result = new List<TaskDetailsModel>();
            foreach (var task in taskCollection.GetTasks())
            {

                result.Add(new TaskDetailsModel(
                task.TaskId,
                task.Description,
                task.Date,
                (int)task.Status));

            }

            return new ObjectResult(result);
        }
    }
}
