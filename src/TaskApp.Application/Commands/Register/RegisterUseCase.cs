namespace TaskApp.Application.Commands.Register
{
    using System;
    using System.Threading.Tasks;    
    using TaskApp.Application.Repositories;
    using TaskApp.Domain.Tasks;
    using TaskApp.Domain.ValueObjects;

    public sealed class RegisterUseCase : IRegisterUseCase    {
        
        private readonly ITaskWriteOnlyRepository taskWriteOnlyRepository;

        public RegisterUseCase(            
            ITaskWriteOnlyRepository taskWriteOnlyRepository)        {
            
            this.taskWriteOnlyRepository = taskWriteOnlyRepository;
        }

        public async Task<RegisterResult> Execute(Description description, Date date, TaskStatusEnum status)
        {
            Domain.Tasks.Task task = Domain.Tasks.Task.Load(description, date, status);            
            await taskWriteOnlyRepository.Add(task);
            RegisterResult result = new RegisterResult(task);
            return result;
        }
    }
}
