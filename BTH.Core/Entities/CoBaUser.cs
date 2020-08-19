using BHT.Core.Entities;
using BTH.Core.DbSupport;
using System.Collections.Generic;

namespace BTH.Core.Entities
{
    /// <summary>
    /// User
    /// </summary>
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
        [Index(IsUnique = true)]
        public string IBAN { get; set; }

        /// <summary>
        /// Transactions for commerzbank
        /// </summary>
        public ICollection<CoBaTransaction> CoBaTransactions { get; set; }
    }
}
