namespace TaskApp.Application
{
    public sealed class TaskNotFoundException : ApplicationException
    {        
        public TaskNotFoundException() : base() { }
        public TaskNotFoundException(string message)
            : base(message)
        { }
    }
}
