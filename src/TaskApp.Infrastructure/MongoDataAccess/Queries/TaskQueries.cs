namespace TaskApp.Infrastructure.MongoDataAccess.Queries
{
    using MongoDB.Driver;
    using System;
    using System.Threading.Tasks;
    using TaskApp.Application.Queries;
    using TaskApp.Application.Results;
    using TaskApp.Infrastructure.MongoDataAccess.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using TaskApp.Domain.Tasks;
    using MongoDB.Bson;

    public class TaskQueries : ITaskQueries
    {
        private readonly Context context;

        public TaskQueries(Context context)
        {
            this.context = context;
        }

        public async Task<TaskResult> GetTask(Guid taskId)
        {
            Entities.Task data = await context
                .Tasks
                .Find(e => e.Id == taskId)
                .SingleOrDefaultAsync();

            if (data == null)
                throw new TaskNotFoundException($"The task {taskId} does not exists or is not processed yet.");            
            

            TaskResult taskResult = new TaskResult(
                data.Id,
                data.Description,
                data.Date,
                (Domain.Tasks.TaskStatusEnum)data.Status);

            return taskResult;
        }
        public async Task<TaskCollectionResult> GetTasks()
        {
            IList<Entities.Task> data = await context
            .Tasks.Find(new BsonDocument())
            .ToListAsync();

            TaskCollectionResult result = new TaskCollectionResult();

            foreach (Entities.Task taskData in data)
            {
                Domain.Tasks.Task task = Domain.Tasks.Task.Load(
                    taskData.Id,
                    taskData.Description,
                    taskData.Date,
                    (TaskStatusEnum)taskData.Status);

                TaskResult resultTask = new TaskResult(task);

                result.Add(resultTask);
            }

            return result;
        }
        public async Task<TaskCollectionResult> GetTasksByDescription(string description)
        {
            IList<Entities.Task> data = await context
            .Tasks
               .Find(e => e.Description.Contains(description))
               .ToListAsync();

            TaskCollectionResult result = new TaskCollectionResult();

            foreach (Entities.Task taskData in data)
            {
                Domain.Tasks.Task task = Domain.Tasks.Task.Load(
                    taskData.Id,
                    taskData.Description,
                    taskData.Date,
                    (TaskStatusEnum)taskData.Status);

                TaskResult resultTask = new TaskResult(task);

                result.Add(resultTask);
            }

            return result;
        }
    }
}
