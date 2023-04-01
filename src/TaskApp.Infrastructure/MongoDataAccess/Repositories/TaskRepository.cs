namespace TaskApp.Infrastructure.MongoDataAccess.Repositories
{
    using TaskApp.Domain.Tasks;
    using TaskApp.Application.Repositories;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TaskApp.Application.Results;
    using System.Security.Claims;

    public class TaskRepository : ITaskReadOnlyRepository, ITaskWriteOnlyRepository
    {
        private readonly Context _context;

        public TaskRepository(Context context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task Add(Domain.Tasks.Task task)
        {
            Entities.Task taskEntity = new Entities.Task()
            {                
                Id = task.Id,
                Description = task.Description,
                Date = task.Date,
                Status = (int)task.Status
            };            

            await _context.Tasks.InsertOneAsync(taskEntity);            
        }

        public async System.Threading.Tasks.Task Delete(Domain.Tasks.Task task)
        {            
            await _context.Tasks.DeleteOneAsync(e => e.Id == task.Id);
        }

        public async System.Threading.Tasks.Task<Domain.Tasks.Task> Get(Guid id)
        {
            Entities.Task task = await _context
                .Tasks
                .Find(e => e.Id == id)
                .SingleOrDefaultAsync();


            Domain.Tasks.Task result = Domain.Tasks.Task.Load(
                task.Id,
                task.Description,
                task.Date,
                (TaskStatusEnum)task.Status);

            return result;
        }

        public async Task<IList<Domain.Tasks.Task>> GetTasks()
        {
            List<Entities.Task> tasks = await _context
                .Tasks
                .Find(_ => true)
                .ToListAsync();

            IList<Domain.Tasks.Task> result = new List<Domain.Tasks.Task>();

            foreach (Entities.Task taskData in tasks)
            {
                Domain.Tasks.Task task = Domain.Tasks.Task.Load(
                    taskData.Id,
                    taskData.Description,
                    taskData.Date,
                    (TaskStatusEnum)taskData.Status);

                result.Add(task);
            }


            return result;
        }

        public async Task<IList<Domain.Tasks.Task>> GetTasksByDescription(string description)
        {
            List<Entities.Task> tasks = await _context
                .Tasks
                .Find(e => e.Description.Contains(description))
                .ToListAsync();

            IList<Domain.Tasks.Task> result = new List<Domain.Tasks.Task>();

            foreach (Entities.Task taskData in tasks)
            {
                Domain.Tasks.Task task = Domain.Tasks.Task.Load(
                    taskData.Id,
                    taskData.Description,
                    taskData.Date,
                    (TaskStatusEnum)taskData.Status);

                result.Add(task);                
            }
            

            return result;
        }

        public async System.Threading.Tasks.Task Update(Domain.Tasks.Task task)
        {
            Entities.Task taskEntity = new Entities.Task()
            {
                Id = task.Id,
                Description = task.Description,
                Date = task.Date,
                Status = (int)task.Status
            };
            var filter = Builders<Entities.Task>.Filter.Eq("Id", taskEntity.Id);
            var update = Builders<Entities.Task>.Update.Set("Description", taskEntity.Description)
                .Set("Date", taskEntity.Date)
                .Set("Status", taskEntity.Status);

            await _context.Tasks.UpdateOneAsync(filter, update);
        }        
    }
}
