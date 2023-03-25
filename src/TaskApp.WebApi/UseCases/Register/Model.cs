namespace TaskApp.WebApi.UseCases.Register
{
    using TaskApp.WebApi.Model;
    using System;
    using System.Collections.Generic;

    internal sealed class Model
    {        
        public List<CashFlowDetailsModel> Tasks { get; set; }

        public Model(List<CashFlowDetailsModel> Tasks)
        {            
            Tasks = Tasks;
        }
    }
}
