using BHT.Core.Entities;
using BTH.Core.Context;
using BTH.Core.CsvData;
using BTH.Core.Dto;
using BTH.Core.Entities;
using BTH.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHT.Core.Services.CoBa.Transactions
{
    public class CoBaTransactionService : ICoBaTransactionService
    {
        private readonly DataContext _ctx;

        public CoBaTransactionService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IList<CoBaTransaction>> GroupTransactions(IList<CoBaTransactionCsv> coBaTransactionsCsv)
        {
            var transactions = new List<CoBaTransaction>();
            var transGroup = coBaTransactionsCsv.GroupBy(e => new { e.IBAN, e.BIC, e.ClientAccount }).ToList();

            foreach (var gr in transGroup)
            {
                var user = await _ctx.CoBaUsers.FirstOrDefaultAsync(e => e.IBAN.ToLower() == gr.Key.IBAN.ToLower()); ;
                if (user == null)
                {
                    user = new CoBaUser
                    {
                        ClientAccount = gr.Key.ClientAccount,
                        BIC = gr.Key.BIC,
                        IBAN = gr.Key.IBAN
                    };
                    await _ctx.CoBaUsers.AddAsync(user);
                }
                else
                {
                    user.ClientAccount = gr.Key.ClientAccount;
                    user.BIC = gr.Key.BIC;
                    _ctx.CoBaUsers.Update(user);
                }
                await _ctx.SaveChangesAsync();
                transactions.AddRange(gr.Select(e => new CoBaTransaction
                {
                    Amount = e.Amount,
                    BookingDate = e.BookingDate,
                    BookingText = e.BookingText,
                    Category = e.Category,
                    Currency = e.Currency,
                    TurnoverType = e.TurnoverType,
                    ValueDate = e.ValueDate,
                    UserAccountId = user.Id,
                    UserAccount = user
                }));
            }
            return transactions;
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

        public async Task<CoBaTransaction[]> Get(CoBaUser user, Filter filter)
        {
            return await _ctx.CoBaTransactions
                .Where(e => e.UserAccountId == user.Id)
                .ApplyFilter(filter)
                .OrderByDescending(e => e.BookingDate)
                .ToArrayAsync();
        }
    }
}
