﻿namespace TaskApp.Infrastructure.EntityFrameworkDataAccess.Queries
{
    using System;
    using System.Threading.Tasks;
    using TaskApp.Application.Queries;
    using TaskApp.Application.Results;
    using System.Collections.Generic;
    using System.Linq;
    using TaskApp.Domain.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class TasksQueries : ITasksQueries
    {
        private readonly Context _context;

        public TasksQueries(Context context)
        {
            _context = context;
        }

        public async Task<TaskResult> GetCashFlow(Guid cashFlowId)
        {
            Entities.CashFlow cashFlow = await _context
                .Tasks
                .FindAsync(cashFlowId);

            List<Entities.Credit> credits = await _context
                .Credits
                .Where(e => e.CashFlowId == cashFlowId)
                .ToListAsync();

            List<Entities.Debit> debits = await _context
                .Debits
                .Where(e => e.CashFlowId == cashFlowId)
                .ToListAsync();

            List<IEntry> entries = new List<IEntry>();

            foreach (Entities.Credit entryData in credits)
            {
                Credit entry = Credit.Load(
                    entryData.Id,
                    entryData.CashFlowId,
                    entryData.Amount,
                    entryData.EntryDate);

                entries.Add(entry);
            }

            foreach (Entities.Debit entryData in debits)
            {
                Debit entry = Debit.Load(
                    entryData.Id,
                    entryData.CashFlowId,
                    entryData.Amount,
                    entryData.EntryDate);

                entries.Add(entry);
            }

            var orderedTransactions = entries.OrderBy(o => o.EntryDate).ToList();

            TaskCollection entryCollection = new TaskCollection();
            entryCollection.Add(orderedTransactions);

            CashFlow result = CashFlow.Load(
                cashFlow.Id,                
                cashFlow.Year,
                entryCollection);

            TaskResult re = new CashFlowResult(result);
            return re;
        }
    }
}
