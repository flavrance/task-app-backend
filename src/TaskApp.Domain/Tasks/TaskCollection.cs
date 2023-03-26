namespace TaskApp.Domain.Tasks
{
    using TaskApp.Domain.ValueObjects;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public sealed class TaskCollection
    {
        private readonly IList<Task> _tasks;

        public TaskCollection()
        {
            _tasks = new List<Task>();
        }

        public IReadOnlyCollection<Task> GetTasks()
        {
            IReadOnlyCollection<Task> tasks = new ReadOnlyCollection<Task>(_tasks);
            return tasks;
        }
        public Task GetLastTask()
        {
            Task task = _tasks[_tasks.Count - 1];
            return task;
        }

        public void Add(Task task)
        {
            _tasks.Add(task);
        }

        public void Add(IEnumerable<Task> tasks)
        {
            foreach (var task in tasks)
            {
                Add(task);
            }
        }               
    }
}
