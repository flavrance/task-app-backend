namespace TaskApp.Application.Repositories{
    

    public interface ITaskWriteOnlyRepository
    {
        System.Threading.Tasks.Task Add(Domain.Tasks.Task task);
        System.Threading.Tasks.Task Update(Domain.Tasks.Task task);
        System.Threading.Tasks.Task Delete(Domain.Tasks.Task task);
    }
}
