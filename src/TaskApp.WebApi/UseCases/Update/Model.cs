namespace TaskApp.WebApi.UseCases.Update
{
    using System;

    internal sealed class Model
    {
        public Guid TaskId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }

        public Model(Guid taskId, string description, DateTime date, int status)
        {
            TaskId = taskId;
            Description = description;
            Date = date;
            Status = status;
        }
    }
}
