namespace TaskApp.Infrastructure.DapperDataAccess.Queries
{
    using Dapper;
    using TaskApp.Domain.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using TaskApp.Application.Queries;
    using TaskApp.Application.Results;
    using TaskApp.Domain.ValueObjects;
    using System.Diagnostics;
    using SharpCompress.Common;

    public class TaskQueries : ITaskQueries
    {
        private readonly string _connectionString;

        public TaskQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<TaskResult> GetTask(Guid taskId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string tasktSQL = @"SELECT * FROM Task WHERE Id = @taskId;";
                Entities.Task data = await db
                    .QueryFirstOrDefaultAsync<Entities.Task>(tasktSQL, new { taskId });

                if (data == null)
                    return null;


                TaskResult taskResult = new TaskResult(
                data.Id,
                data.Description,
                data.Date,
                (Domain.Tasks.TaskStatusEnum)data.Status);

                return taskResult;
            }
        }

        public async Task<TaskCollectionResult> GetTasks()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string tasktSQL = @"SELECT * FROM Task;";
                TaskCollectionResult result = new TaskCollectionResult();
                using (var reader = await db.ExecuteReaderAsync(tasktSQL))
                {
                    var parser = reader.GetRowParser<Entities.Task>();                    

                    while (reader.Read())
                    {
                        Entities.Task data = parser(reader);
                        TaskResult taskResult = new TaskResult(data.Id, data.Description, data.Date, (Domain.Tasks.TaskStatusEnum)data.Status);
                        result.Add(taskResult);
                    }
                }

                return result;
            }
        }

        public async Task<TaskCollectionResult> GetTasksByDescription(string description)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string tasktSQL = @"SELECT * FROM Task where Description = @description;";
                TaskCollectionResult result = new TaskCollectionResult();
                using (var reader = await db.ExecuteReaderAsync(tasktSQL, new { description }))
                {
                    var parser = reader.GetRowParser<Entities.Task>();

                    while (reader.Read())
                    {
                        Entities.Task data = parser(reader);
                        TaskResult taskResult = new TaskResult(data.Id, data.Description, data.Date, (Domain.Tasks.TaskStatusEnum)data.Status);
                        result.Add(taskResult);
                    }
                }

                return result;
            }
        }
    }
}
