namespace TaskApp.Application.Commands.Task
{
    using TaskApp.Domain.ValueObjects;
    using System;
    using System.Threading.Tasks;
    using TaskApp.Application.Results;
    using TaskApp.Domain.Tasks;

    public interface ITaskUseCase
    {
        Task<TaskResult> Execute(Guid taskId);
        Task<TaskResult> Execute(Guid taskId, Description description, Date date, TaskStatusEnum status);
    }
}
