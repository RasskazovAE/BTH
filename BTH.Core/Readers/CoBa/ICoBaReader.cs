using BTH.Core.CsvData;
using System.Threading.Tasks;

namespace BHT.Core.Readers.CoBa
{
    public interface ICoBaReader
    {
        Task<CoBaTransactionCsv[]> ParseCsvFileAsync(string csvFilePath);
    }
}
