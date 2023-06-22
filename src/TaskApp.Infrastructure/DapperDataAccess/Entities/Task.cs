namespace TaskApp.Infrastructure.DapperDataAccess.Entities
{
    using System;

    public class Task
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public DateTime? InsertedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
