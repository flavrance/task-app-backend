namespace TaskApp.WebApi.UseCases.GetTasks
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using TaskApp.Application.Queries;
    using TaskApp.WebApi.Model;
    using System.Collections.Generic;
    using TaskApp.Application.Results;

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
        public async Task<IActionResult> Get([FromQuery] string description)
        {
            TaskCollectionResult taskCollection = null;
            if(String.IsNullOrEmpty(description) && String.IsNullOrWhiteSpace(description))            
                taskCollection = await TasksQueries.GetTasks();
            else if(!String.IsNullOrEmpty(description) && !String.IsNullOrWhiteSpace(description))
                taskCollection = await TasksQueries.GetTasksByDescription(description);

            IList<TaskDetailsModel> result = new List<TaskDetailsModel>();
            foreach(var task in taskCollection.GetTasks()) {

                result.Add(new TaskDetailsModel(
                task.TaskId,
                task.Description,
                task.Date.ToString("yyyy-MM-dd"),
                (int)task.Status));

            }
            
            return new ObjectResult(result);
        }        
    }
}
