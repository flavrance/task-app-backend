namespace TaskApp.Domain.Tasks
{
    using TaskApp.Domain.ValueObjects;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public sealed class TaskCollection
    {
        private readonly IList<ITask> _tasks;

        public TaskCollection()
        {
            _tasks = new List<ITask>();
        }

        public IReadOnlyCollection<ITask> GetTasks()
        {
            IReadOnlyCollection<ITask> tasks = new ReadOnlyCollection<ITask>(_tasks);
            return tasks;
        }

        public ITask GetLastTask()
        {
            ITask task = _tasks[_tasks.Count - 1];
            return task;
        }

        public void Add(ITask task)
        {
            _tasks.Add(task);
        }

        public void Add(IEnumerable<ITask> tasks)
        {
            foreach (var task in tasks)
            {
                Add(task);
            }
        }               
    }
}
