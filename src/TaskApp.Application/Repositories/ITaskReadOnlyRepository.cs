namespace TaskApp.Application.Repositories
{    
    using System;
    using System.Collections.Generic;

    public interface ITaskReadOnlyRepository
    {
        System.Threading.Tasks.Task<Domain.Tasks.Task> Get(Guid id);
        System.Threading.Tasks.Task<IList<Domain.Tasks.Task>> GetTasks();
        System.Threading.Tasks.Task<IList<Domain.Tasks.Task>> GetTasksByDescription(string description);
        
    }
}
