namespace TaskApp.WebApi.UseCases.Credit
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using TaskApp.Application.Commands.Credit;

    [Route("api/[controller]")]
    public sealed class TasksController : Controller
    {
        private readonly ITaskUseCase creditService;

        public TasksController(
            ITaskUseCase creditService)
        {
            this.creditService = creditService;
        }

        /// <summary>
        /// Credit from an cash flow
        /// </summary>
        [HttpPatch("Credit")]
        public async Task<IActionResult> Credit([FromBody]CreditRequest request)
        {
            TaskResult creditResult = await creditService.Execute(
                request.CashFlowId,
                request.Amount);

            if (creditResult == null)
            {
                return new NoContentResult();
            }

            Model model = new Model(
                creditResult.Entry.Amount,
                creditResult.Entry.Description,
                creditResult.Entry.EntryDate,
                creditResult.UpdatedBalance
            );

            return new ObjectResult(model);
        }
    }
}
