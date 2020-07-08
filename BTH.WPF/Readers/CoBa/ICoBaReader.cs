using BankTransactionHistory.Entities;
using System.Threading.Tasks;

namespace BankTransactionHistory.Readers.CoBa
{
    public interface ICoBaReader
    {
        Task<CoBaTransaction[]> ParseCsvFileAsync(string csvFilePath);
    }
}
