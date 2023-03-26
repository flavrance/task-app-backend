namespace TaskApp.Application.Results
{
    using TaskApp.Domain.Tasks;
    using System;
    using System.Collections.Generic;

    public sealed class TaskCollectionResult
    {
        private readonly IList<TaskResult> _tasks;

        public TaskCollectionResult()
        {
            _tasks = new List<TaskResult>();
        }


        public TaskCollectionResult(
            IList<TaskResult> tasks)
        {
            _tasks = tasks;
        }

        public void Add(TaskResult task)
        {
            _tasks.Add(task);
        }

        public void Add(IEnumerable<TaskResult> tasks)
        {
            foreach (var task in tasks)
            {
                Add(task);
            }
        }

        public IList<TaskResult > GetTasks()
        {
            return _tasks;
        }
    }
}
