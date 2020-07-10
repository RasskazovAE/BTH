using BHT.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BHT.Core.Services.CoBa
{
    public interface ICoBaService
    {
        Task<CoBaTransaction[]> Get();

        void AddOnlyNewAsync(IEnumerable<CoBaTransaction> coBaTransactions);
    }
}
