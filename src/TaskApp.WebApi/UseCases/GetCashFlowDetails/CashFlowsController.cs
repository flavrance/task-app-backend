namespace TaskApp.WebApi.UseCases.GetCashFlowDetails
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using TaskApp.Application.Queries;
    using TaskApp.WebApi.Model;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    public sealed class TasksController : Controller
    {
        private readonly ITasksQueries TasksQueries;

        public TasksController(
            ITasksQueries TasksQueries)
        {
            this.TasksQueries = TasksQueries;
        }

        /// <summary>
        /// Get an cash flow balance
        /// </summary>
        [HttpGet("{cashFlowId}", Name = "GetCashFlow")]
        public async Task<IActionResult> Get(Guid cashFlowId)
        {
            var cashFlow = await TasksQueries.GetCashFlow(cashFlowId);

            List<EntryModel> entries = new List<EntryModel>();

            foreach (var item in cashFlow.Entries)
            {
                var entry = new EntryModel(
                    item.Amount,
                    item.Description,
                    item.EntryDate);

                entries.Add(entry);
            }

            return new ObjectResult(new CashFlowDetailsModel(
                cashFlow.CashFlowId,
                cashFlow.Year,
                cashFlow.CurrentBalance,
                entries));
        }
    }
}
