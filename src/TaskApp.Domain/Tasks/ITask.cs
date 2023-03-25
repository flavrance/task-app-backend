namespace TaskApp.Domain.Tasks
{
    using TaskApp.Domain.ValueObjects;
    using System;
    using TaskApp.Domain.Tasks;

    public interface ITask
    {
        Guid Id { get; }
        Description Description { get; }
        Date Date { get; }
        TaskStatusEnum Status { get; }
    }
}
