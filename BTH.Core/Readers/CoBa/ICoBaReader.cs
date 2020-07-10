using BHT.Core.Entities;
using System.Threading.Tasks;

namespace BHT.Core.Readers.CoBa
{
    public interface ICoBaReader
    {
        Task<CoBaTransaction[]> ParseCsvFileAsync(string csvFilePath);
    }
}
