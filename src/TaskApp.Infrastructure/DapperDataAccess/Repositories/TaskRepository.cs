namespace TaskApp.Infrastructure.DapperDataAccess.Repositories
{
    using Dapper;
    using TaskApp.Application.Repositories;
    using TaskApp.Domain.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text;
    using TaskApp.Application.Results;

    public class TaskRepository : ITaskReadOnlyRepository, ITaskWriteOnlyRepository
    {
        private readonly string _connectionString;

        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async System.Threading.Tasks.Task Add(Domain.Tasks.Task task)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string insertTaskSQL = "INSERT INTO Task (Id, Description, Date, Status, InsertedAt) VALUES (@Id, @Description, @Date, @Status, @InsertedAt)";

                DynamicParameters taskParameters = new DynamicParameters();
                taskParameters.Add("@id", task.Id);
                taskParameters.Add("@description", task.Description);
                taskParameters.Add("@date", task.Date);
                taskParameters.Add("@status", ((int)task.Status));
                taskParameters.Add("@insertedAt", DateTime.UtcNow);

                int taskRows = await db.ExecuteAsync(insertTaskSQL, taskParameters);                
                
            }
        }

        public async System.Threading.Tasks.Task Delete(Domain.Tasks.Task task)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string deleteSQL =
                    @"DELETE FROM Task WHERE Id = @Id;";
                int rowsAffected = await db.ExecuteAsync(deleteSQL, task);
            }
        }

        public async System.Threading.Tasks.Task<Domain.Tasks.Task> Get(Guid id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string taskSQL = @"SELECT * FROM Task WHERE Id = @Id;";
                Entities.Task task = await db
                    .QueryFirstOrDefaultAsync<Entities.Task>(taskSQL, new { id });

                if (task == null)
                    return null;

                Domain.Tasks.Task result = Domain.Tasks.Task.Load(
                task.Id,
                task.Description,
                task.Date,
                (TaskStatusEnum)task.Status);
                return result;
            }
        }

        public async Task<IList<Domain.Tasks.Task>> GetTasks()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string tasktSQL = @"SELECT * FROM Task;";
                IList<Domain.Tasks.Task> tasks = new List<Domain.Tasks.Task>();
                using (var reader = await db.ExecuteReaderAsync(tasktSQL))
                {
                    var parser = reader.GetRowParser<Entities.Task>();
                    while (reader.Read())
                    {
                        Entities.Task data = parser(reader);
                        tasks.Add(Domain.Tasks.Task.Load(data.Id, data.Description, data.Date, (TaskStatusEnum)data.Status));                        
                    }
                }
                
                return tasks;
            }
        }

        public async Task<IList<Domain.Tasks.Task>> GetTasksByDescription(string description)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string tasktSQL = @"SELECT * FROM Task where Description = @description;";
                IList<Domain.Tasks.Task> tasks = new List<Domain.Tasks.Task>();
                using (var reader = await db.ExecuteReaderAsync(tasktSQL, new { description }))
                {
                    var parser = reader.GetRowParser<Entities.Task>();
                    while (reader.Read())
                    {
                        Entities.Task data = parser(reader);
                        tasks.Add(Domain.Tasks.Task.Load(data.Id, data.Description, data.Date, (TaskStatusEnum)data.Status));
                    }
                }

                return tasks;
            }
        }

        public async System.Threading.Tasks.Task Update(Domain.Tasks.Task task)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                StringBuilder updateTaskSQL = new StringBuilder();
                updateTaskSQL.AppendLine("UPDATE Task SET ");
                updateTaskSQL.AppendLine("Description = @Description,");
                updateTaskSQL.AppendLine("Date = @Date,");
                updateTaskSQL.AppendLine("Status = @Status,");
                updateTaskSQL.AppendLine("UpdatedAt = @UpdatedAt");
                updateTaskSQL.AppendLine("WHERE Id = @Id");

                DynamicParameters taskParameters = new DynamicParameters();                
                taskParameters.Add("@description", task.Description);
                taskParameters.Add("@date", task.Date);
                taskParameters.Add("@status", ((int)task.Status));
                taskParameters.Add("@updatedAt", DateTime.UtcNow);
                taskParameters.Add("@id", task.Id);               

                int creditRows = await db.ExecuteAsync(updateTaskSQL.ToString(), taskParameters);
            }
        }       
    }
}
