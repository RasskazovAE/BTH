using BHT.Core.Entities;
using BTH.Core.Context;
using BTH.Core.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHT.Core.Services.CoBa
{
    public class CoBaService : ICoBaService
    {
        private readonly DataContext _ctx;

        public CoBaService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddNewAsync(IEnumerable<CoBaTransaction> coBaTransactions)
        {
            //todo: add statistic, how many duplicated transactions were in the input data
            coBaTransactions = coBaTransactions.GroupBy(e => e.BookingText).Select(g => g.First());
            var transactions = await _ctx.CoBaTransactions.ToListAsync();
            //todo: add statistic, how many transactions already exist in Db
            var newTransactions = coBaTransactions.Where(e => transactions.All(a => a.BookingText.Equals(e.BookingText, StringComparison.InvariantCultureIgnoreCase)));

            _ctx.CoBaTransactions.AddRange(newTransactions);
            await _ctx.SaveChangesAsync();
        }

        public async Task<CoBaTransaction[]> Get(Filter filter)
        {
            return await _ctx.CoBaTransactions.Where(e => 
                e.BookingDate > filter.StartDate && 
                e.BookingDate < filter.EndDate && 
                (string.IsNullOrEmpty(filter.SearchText) || e.BookingText.Contains(filter.SearchText)))
                .ToArrayAsync();
        }
    }
}
