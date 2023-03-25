namespace TaskApp.Infrastructure.InMemoryDataAccess
{
    using TaskApp.Domain.Tasks;    
    using System.Collections.ObjectModel;

    public class Context
    {        
        public Collection<CashFlow> Tasks { get; set; }

        public Context()
        {
            Tasks = new Collection<CashFlow>();
        }
    }
}
