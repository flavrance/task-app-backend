namespace TaskApp.Application.Results
{
    using TaskApp.Domain.Tasks;
    using System;
    using System.Collections.Generic;

    public sealed class TaskResult
    {
        public Guid TaskId { get; }
        public string Description { get; }
        public DateTime Date { get; }
        public TaskStatusEnum Status { get; }
        public IList<TaskResult> Tasks { get; }

        public TaskResult(
            Guid taskId,
            string description,
            DateTime date,
            TaskStatusEnum status)
        {   
            TaskId = taskId;
            Description = description;
            Date = date;
            Status = status;            
        }       

        public TaskResult(Task task)
        {
            TaskId = task.Id;
            Description = task.Description;
            Date = task.Date;
            Status = task.Status;
        }       
    }
}
