namespace TaskApp.Application.Commands.Debit
{
    using TaskApp.Application.Results;
    using TaskApp.Domain.Tasks;
    using TaskApp.Domain.ValueObjects;

    public sealed class DebitResult
    {
        public EntryResult Entry { get; }
        public double UpdatedBalance { get; }

        public DebitResult(Debit entry, Amount updatedBalance)
        {
            Entry = new EntryResult(
                entry.Description,
                entry.Amount,
                entry.EntryDate);

            UpdatedBalance = updatedBalance;
        }
    }
}
