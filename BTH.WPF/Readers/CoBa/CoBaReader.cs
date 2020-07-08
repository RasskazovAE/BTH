using BankTransactionHistory.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BankTransactionHistory.Readers.CoBa
{
    public class CoBaReader : ICoBaReader
    {
        public async Task<CoBaTransaction[]> ParseCsvFileAsync(string csvFilePath)
        {
            throw new NotImplementedException();
        }
    }
}
