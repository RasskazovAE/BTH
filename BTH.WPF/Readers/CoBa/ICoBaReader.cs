using BHT.WPF.Entities;
using System.Threading.Tasks;

namespace BHT.WPF.Readers.CoBa
{
    public interface ICoBaReader
    {
        Task<CoBaTransaction[]> ParseCsvFileAsync(string csvFilePath);
    }
}
