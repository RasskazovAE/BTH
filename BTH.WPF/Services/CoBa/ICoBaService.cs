using BankTransactionHistory.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankTransactionHistory.Services.CoBa
{
    public interface ICoBaService
    {
        Task<CoBaTransaction[]> Get();

        void AddOnlyNewAsync(IEnumerable<CoBaTransaction> coBaTransactions);
    }
}
