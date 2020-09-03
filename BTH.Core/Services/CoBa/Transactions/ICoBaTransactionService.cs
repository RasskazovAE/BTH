﻿using BHT.Core.Entities;
using BTH.Core.CsvData;
using BTH.Core.Dto;
using BTH.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BHT.Core.Services.CoBa.Transactions
{
    public interface ICoBaTransactionService
    {
        Task<IList<CoBaTransaction>> GroupTransactions(IList<CoBaTransactionCsv> coBaTransactionsCsv);

        Task<CoBaTransaction[]> Get(CoBaUser user, Filter filter);

        Task AddNewAsync(IEnumerable<CoBaTransaction> coBaTransactions);
    }
}
