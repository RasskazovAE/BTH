using BHT.WPF.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BHT.WPF.Services.CoBa
{
    public interface ICoBaService
    {
        Task<CoBaTransaction[]> Get();

        void AddOnlyNewAsync(IEnumerable<CoBaTransaction> coBaTransactions);
    }
}
