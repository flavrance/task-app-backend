namespace TaskApp.Application.Repositories
{
    using TaskApp.Domain.Tasks;
    using System;
    using System.Threading.Tasks;

    public interface ICashFlowReadOnlyRepository
    {
        Task<CashFlow> Get(Guid id);        
    }
}
