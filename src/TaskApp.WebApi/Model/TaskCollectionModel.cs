namespace TaskApp.WebApi.Model
{
    using System;
    using System.Collections.Generic;

    public sealed class TaskCollectionModel
    {
        public List<TaskDetailsModel> Tasks { get; }
        
        public TaskCollectionModel(List<TaskDetailsModel> tasks)
        {
            Tasks = tasks;
        }
    }
}
