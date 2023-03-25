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
        public List<TaskResult> Tasks { get; }

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
        public TaskResult(
            Guid taskId,
            string description,
            DateTime date,
            TaskStatusEnum status,
            List<TaskResult> tasks)
        {
            TaskId = taskId;
            Description = description;
            Date = date;
            Status = status;
            Tasks = tasks;            
        }

        public TaskResult(Task task)
        {
            TaskId = task.Id;
            Description = task.Description;
            Date = task.Date;
            Status = task.Status;           

            List<TaskResult> taskResults = new List<TaskResult>();
            foreach (ITask _task in task.GetTasks())
            {
                TaskResult taskResult = new TaskResult(
                    _task.Id, _task.Description, _task.Date, _task.Status);
                taskResults.Add(taskResult);
            }

            Tasks = taskResults;
        }
    }
}
