using BHT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task AddOnlyNewAsync(IEnumerable<CoBaTransaction> coBaTransactions)
        {
            _ctx.CoBaTransactions.AddRange(coBaTransactions);
            await _ctx.SaveChangesAsync();
        }

        public async Task<CoBaTransaction[]> Get()
        {
            return await _ctx.CoBaTransactions.ToArrayAsync();
        }
    }
}
