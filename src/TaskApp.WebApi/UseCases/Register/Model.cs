namespace TaskApp.WebApi.UseCases.Register
{
    using TaskApp.WebApi.Model;
    using System;
    using System.Collections.Generic;

    internal sealed class Model
    {        
        public List<TaskDetailsModel> Tasks { get; set; }

        public Model(List<TaskDetailsModel> tasks)
        {            
            Tasks = tasks;
        }
    }
}
