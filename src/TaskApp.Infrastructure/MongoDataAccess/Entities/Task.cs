namespace TaskApp.Infrastructure.MongoDataAccess.Entities
{
    using System;

    public class Task
    {
        public Guid Id { get; set; }        
        public string Description { get; set; }
        public DateTime Date { get; set;}
        public int Status { get; set;}
    }
}
