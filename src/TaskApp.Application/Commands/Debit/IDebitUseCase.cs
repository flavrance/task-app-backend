namespace TaskApp.Application.Commands.Debit
{
    using TaskApp.Domain.ValueObjects;
    using System;
    using System.Threading.Tasks;

    public interface IDebitUseCase
    {
        Task<DebitResult> Execute(Guid cashFlowId, Amount amount);
    }
}
