namespace TaskApp.Application.Queries
{
    using TaskApp.Application.Results;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface ITaskQueries
    {
        Task<TaskResult> GetTask(Guid taskId);
        Task<TaskCollectionResult> GetTasks();
        Task<TaskCollectionResult> GetTasksByDescription(string description);
    }
}
