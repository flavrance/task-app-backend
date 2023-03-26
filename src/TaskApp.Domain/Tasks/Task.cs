namespace TaskApp.Domain.Tasks
{
    using TaskApp.Domain.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Transactions;

    public sealed class Task : IEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }        
        public Description Description { get; private set; }
        public Date Date { get; private set; }
        public TaskStatusEnum Status { get; private set; }                
        public Task() { }
        public Task(TaskStatusEnum status)
        {
            Id = Guid.NewGuid();
            Status = status;            
        }
        public static Task Load(Description description, Date date, TaskStatusEnum status)
        {
            Task task = new Task(status);            
            task.Description = description;
            task.Date = date;
            return task;
        }
        public static Task Load(Guid id, Description description, Date date, TaskStatusEnum status)
        {
            Task task = new Task(status);
            task.Id = id;            
            task.Description = description;
            task.Date = date;
            return task;
        }
    }
}
