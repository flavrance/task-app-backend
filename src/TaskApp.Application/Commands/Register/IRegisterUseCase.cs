namespace TaskApp.Application.Commands.Register
{
    using System;
    using System.Threading.Tasks;
    using TaskApp.Domain.Tasks;
    using TaskApp.Domain.ValueObjects;

    public interface IRegisterUseCase
    {
        Task<RegisterResult> Execute(Description description, Date date, TaskStatusEnum status);
    }
}
