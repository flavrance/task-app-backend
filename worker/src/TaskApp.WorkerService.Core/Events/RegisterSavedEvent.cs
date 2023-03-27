using System;

namespace TaskApp.WorkerService.Core.Events 
{
    public class RegisterSavedEvent
    {
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? Status { get; set; }
    }
}


