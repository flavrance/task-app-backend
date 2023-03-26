namespace TaskApp.Infrastructure
{
    public class TaskNotFoundException : InfrastructureException
    {
        internal TaskNotFoundException(string message)
            : base(message)
        { }
    }
}
