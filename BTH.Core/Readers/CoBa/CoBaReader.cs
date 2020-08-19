using BTH.Core.CsvData;
using BTH.Core.Environment;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BHT.Core.Readers.CoBa
{
    public class CoBaReader : ICoBaReader
    {
        public async Task<CoBaTransactionCsv[]> ParseCsvFileAsync(string csvFilePath)
        {
            var fileLines = await File.ReadAllLinesAsync(csvFilePath);

            return fileLines.Skip(1).Select(line =>
            {
                try
                {
                    var values = line.Split(';');
                    return new CoBaTransactionCsv()
                    {
                        BookingDate = Convert.ToDateTime(values[0], BTHCulture.CultureInfo),
                        ValueDate = Convert.ToDateTime(values[1], BTHCulture.CultureInfo),
                        TurnoverType = values[2],
                        BookingText = values[3],
                        Amount = decimal.Parse(values[4], BTHCulture.CultureInfo),
                        Currency = values[5],
                        ClientAccount = values[6],
                        BIC = values[7],
                        IBAN = values[8],
                        Category = values[9]
                    };
                }
                catch (Exception)
                {
                    // TODO: return statistic
                    return null;
                }

            }).Where(t => t != null).ToArray();
        }
    }
}
