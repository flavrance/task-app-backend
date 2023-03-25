namespace TaskApp.Application.Commands.Task
{
    using System;
    using System.Threading.Tasks;
    using TaskApp.Application.Repositories;
    using TaskApp.Application.Results;
    using TaskApp.Domain.Tasks;
    using TaskApp.Domain.ValueObjects;

    public sealed class TaskUseCase : ITaskUseCase
    {
        private readonly ICashFlowReadOnlyRepository cashFlowReadOnlyRepository;
        private readonly ICashFlowWriteOnlyRepository cashFlowWriteOnlyRepository;

        public TaskUseCase(
            ICashFlowReadOnlyRepository cashFlowReadOnlyRepository,
            ICashFlowWriteOnlyRepository cashFlowWriteOnlyRepository)
        {
            this.cashFlowReadOnlyRepository = cashFlowReadOnlyRepository;
            this.cashFlowWriteOnlyRepository = cashFlowWriteOnlyRepository;
        }


        public async Task<TaskResult> Execute(Guid taskId, Description description, Date date, TaskStatusEnum status)
        {
            CashFlow cashFlow = await cashFlowReadOnlyRepository.Get(cashFlowId);
            if (cashFlow == null)
                throw new CashFlowNotFoundException($"The cashFlow {cashFlowId} does not exists.");

            cashFlow.Credit(amount);
            Credit credit = (Credit)cashFlow.GetLastEntry();

            await cashFlowWriteOnlyRepository.Update(
                cashFlow,
                credit);

            TaskResult result = new CreditResult(
                credit,
                cashFlow.GetCurrentBalance());
            return result;
        }
    }
}
