namespace TaskApp.Application.Commands.Register
{
    using System.Threading.Tasks;    
    using TaskApp.Application.Repositories;
    using TaskApp.Domain.Tasks;

    public sealed class RegisterUseCase : IRegisterUseCase    {
        
        private readonly ICashFlowWriteOnlyRepository cashFlowWriteOnlyRepository;

        public RegisterUseCase(            
            ICashFlowWriteOnlyRepository cashFlowWriteOnlyRepository)        {
            
            this.cashFlowWriteOnlyRepository = cashFlowWriteOnlyRepository;
        }

        public async Task<RegisterResult> Execute(int year, double initialAmount){
            

            CashFlow cashFlow = new CashFlow(year);
            cashFlow.Credit(initialAmount);
            Credit credit = (Credit)cashFlow.GetLastEntry();            

            
            await cashFlowWriteOnlyRepository.Add(cashFlow, credit);

            RegisterResult result = new RegisterResult(cashFlow);
            return result;
        }
    }
}
