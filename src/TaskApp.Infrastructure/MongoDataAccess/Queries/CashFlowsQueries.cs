﻿namespace TaskApp.Infrastructure.MongoDataAccess.Queries
{
    using MongoDB.Driver;
    using System;
    using System.Threading.Tasks;
    using TaskApp.Application.Queries;
    using TaskApp.Application.Results;
    using TaskApp.Infrastructure.MongoDataAccess.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;

    public class TasksQueries : ITasksQueries
    {
        private readonly Context context;

        public TasksQueries(Context context)
        {
            this.context = context;
        }

        public async Task<TaskResult> GetCashFlow(Guid cashFlowId)
        {
            CashFlow data = await context
                .Tasks
                .Find(e => e.Id == cashFlowId)
                .SingleOrDefaultAsync();

            if (data == null)
                throw new CashFlowNotFoundException($"The cashFlow {cashFlowId} does not exists or is not processed yet.");

            List<Credit> credits = await context
                .Credits
                .Find(e => e.CashFlowId == cashFlowId)
                .ToListAsync();

            List<Debit> debits = await context
                .Debits
                .Find(e => e.CashFlowId == cashFlowId)
                .ToListAsync();

            double credit = credits.Sum(c => c.Amount);
            double debit = debits.Sum(d => d.Amount);
            

            List<EntryResult> entryResults = new List<EntryResult>();

            List<EntryResult> reportResults = new List<EntryResult>();

            foreach (Credit entry in credits)
            {
                EntryResult entryResult = new EntryResult(
                    entry.Description, entry.Amount, entry.EntryDate);
                entryResults.Add(entryResult);
                reportResults.Add(entryResult);
            }

            foreach (Debit entry in debits)
            {
                EntryResult entryResult = new EntryResult(
                    entry.Description, entry.Amount, entry.EntryDate);
                entryResults.Add(entryResult);

                EntryResult reportResult = new EntryResult(
                    String.Empty, -entry.Amount, entry.EntryDate);
                reportResults.Add(reportResult);
            }

            List<EntryResult> orderedEntries = entryResults.OrderBy(e => e.EntryDate).ToList();
            List<EntryResult> reportEntries = reportResults.GroupBy(e => new { EntryDate = e.EntryDate }).Select(s => new EntryResult(String.Empty, s.Sum(x => x.Amount), s.Key.EntryDate)).ToList();

            TaskResult cashFlowResult = new TaskResult(
                data.Id,
                data.Year,
                credit - debit,
                orderedEntries,
                reportEntries);

            return cashFlowResult;
        }
    }
}
