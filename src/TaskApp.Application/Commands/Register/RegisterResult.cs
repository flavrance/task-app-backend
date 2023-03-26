namespace TaskApp.Application.Commands.Register
{
    using TaskApp.Application.Results;
    using TaskApp.Domain.Tasks;    
    using System.Collections.Generic;

    public sealed class RegisterResult
    {
        
        public TaskResult Task { get; }

        public RegisterResult(Task task)
        {
            Task = new TaskResult(task.Id, task.Description, task.Date, task.Status);            
        }
    }
}
