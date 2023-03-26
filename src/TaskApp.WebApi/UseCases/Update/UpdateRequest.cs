namespace TaskApp.WebApi.UseCases.Update
{
    using System;
    public sealed class UpdateRequest
    {
        public Guid TaskId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
    }
}
