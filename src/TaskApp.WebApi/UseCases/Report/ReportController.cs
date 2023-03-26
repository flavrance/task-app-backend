namespace TaskApp.WebApi.UseCases.Report
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using TaskApp.Application.Queries;
    using TaskApp.WebApi.Model;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    public sealed class ReportController : Controller
    {
        private readonly ITaskQueries TasksQueries;

        public ReportController(
            ITaskQueries TasksQueries)
        {
            this.TasksQueries = TasksQueries;
        }

        /// <summary>
        /// Get an cash flow balance by date
        /// </summary>
        [HttpGet("{cashFlowId}", Name = "GeBalancedEntriesByDate")]
        public async Task<IActionResult> Get(Guid cashFlowId)
        {
            var cashFlow = await TasksQueries.GetCashFlow(cashFlowId);

            List<TaskCollectionModel> entries = new List<TaskCollectionModel>();

            foreach (var item in cashFlow.Report)
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
