namespace TaskApp.Application.Queries
{
    using TaskApp.Application.Results;
    using System;
    using System.Threading.Tasks;

    public interface ITasksQueries
    {
        Task<TaskResult> GetCashFlow(Guid cashFlowId);
    }
}
