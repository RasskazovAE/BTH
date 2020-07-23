using BHT.Core.Entities;
using BTH.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BHT.Core.Services.CoBa
{
    public interface ICoBaService
    {
        Task<CoBaTransaction[]> Get(Filter filter);

        Task AddNewAsync(IEnumerable<CoBaTransaction> coBaTransactions);
    }
}
