namespace TaskApp.WebApi.Model
{
    using System;
    using System.Collections.Generic;

    public sealed class TaskDetailsModel
    {
        public Guid TaskId { get; }
        public string Description { get; }
        public DateTime Date { get; }
        public int Status { get; }        

        public TaskDetailsModel(Guid taskId, string description, DateTime date, int status)
        {
            TaskId = taskId;
            Description = description;
            Date = date;
            Status = status;
        }
    }
}
