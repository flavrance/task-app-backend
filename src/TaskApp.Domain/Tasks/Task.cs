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
        public IReadOnlyCollection<ITask> GetTasks()
        {
            IReadOnlyCollection<ITask> readOnly = _tasks.GetTasks();
            return readOnly;
        }

        private TaskCollection _tasks;        
        
        public Task(TaskStatusEnum status)
        {
            Id = Guid.NewGuid();
            Status = status;
            _tasks = new TaskCollection();            
        }       
        

        public ITask GetLastTask()
        {
            ITask task = _tasks.GetLastTask();
            return task;
        }

        public IReadOnlyCollection<ITask> GetTasksByDescription()
        {
            IReadOnlyCollection<ITask> readOnly = _tasks.GetTasks();
            return readOnly;
        }

        

        public static Task Load(Guid id, Description description, Date date, TaskStatusEnum status, TaskCollection tasks)
        {
            Task task = new Task(status);
            task.Id = id;
            task._tasks = tasks;
            task.Description = description;
            task.Date = date;            
            return task;
        }
    }
}
