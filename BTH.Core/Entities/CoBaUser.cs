using BHT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BTH.Core.Entities
{
    /// <summary>
    /// User
    /// </summary>
    [Index(nameof(IBAN), IsUnique = true)]
    public class CoBaUser : EntityBase
    {
        /// <summary>
        /// Name of user account
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Client account
        /// </summary>
        public string ClientAccount { get; set; }

        /// <summary>
        /// BIC of client account
        /// </summary>
        public string BIC { get; set; }

        /// <summary>
        /// IBAN of client account
        /// </summary>
        public string IBAN { get; set; }

        /// <summary>
        /// Transactions for commerzbank
        /// </summary>
        public ICollection<CoBaTransaction> CoBaTransactions { get; set; }
    }
}
