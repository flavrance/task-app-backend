namespace TaskApp.Application.Commands.Task
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskApp.Application.Repositories;
    using TaskApp.Application.Results;
    using TaskApp.Domain.Tasks;
    using TaskApp.Domain.ValueObjects;

    public sealed class TaskUseCase : ITaskUseCase
    {
        private readonly ITaskReadOnlyRepository taskReadOnlyRepository;
        private readonly ITaskWriteOnlyRepository taskWriteOnlyRepository;

        public TaskUseCase(
            ITaskReadOnlyRepository taskReadOnlyRepository,
            ITaskWriteOnlyRepository taskWriteOnlyRepository)
        {
            this.taskReadOnlyRepository = taskReadOnlyRepository;
            this.taskWriteOnlyRepository = taskWriteOnlyRepository;
        }


        public async Task<TaskResult> Execute(Guid taskId)
        {            
            Domain.Tasks.Task task = await taskReadOnlyRepository.Get(taskId);
            if (task == null)
                throw new TaskNotFoundException($"The task {taskId} does not exists.");

            await taskWriteOnlyRepository.Delete(task);
            TaskResult result = new TaskResult(task);
            return result;
        }
        public async Task<TaskResult> Execute(Guid taskId, Description description, Date date, TaskStatusEnum status)
        {
            Domain.Tasks.Task task = await taskReadOnlyRepository.Get(taskId);
            if (task == null)
                throw new TaskNotFoundException($"The task {taskId} does not exists.");

            task = Domain.Tasks.Task.Load(task.Id, description, date, status);            

            await taskWriteOnlyRepository.Update(task);
            TaskResult result = new TaskResult(task);
            return result;
        }        
    }
}
