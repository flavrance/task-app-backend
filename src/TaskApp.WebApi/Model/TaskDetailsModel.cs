namespace TaskApp.WebApi.Model
{
    using System;
    using System.Collections.Generic;

    public sealed class TaskDetailsModel
    {
        public Guid TaskId { get; }
        public string Description { get; }
        public string Date { get; }
        public int Status { get; }        

        public TaskDetailsModel(Guid taskId, string description, string date, int status)
        {
            TaskId = taskId;
            Description = description;
            Date = date;
            Status = status;
        }
    }
}
