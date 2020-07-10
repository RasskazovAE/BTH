using System;

namespace BHT.Core.Entities
{
    /// <summary>
    /// Transaction for commerzbank
    /// </summary>
    public class CoBaTransaction : EntityBase
    {
        /// <summary>
        /// Booking date
        /// </summary>
        public DateTime BookingDate { get; set; }

        /// <summary>
        /// Value date
        /// </summary>
        public DateTime ValueDate { get; set; }

        /// <summary>
        /// Type of turnover
        /// </summary>
        public string TurnoverType { get; set; }

        /// <summary>
        /// Booking text
        /// </summary>
        public string BookingText { get; set; }

        /// <summary>
        /// Amount of transaction
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Currency of transaction
        /// </summary>
        public string Currency { get; set; }

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
        /// Category
        /// </summary>
        public string Category { get; set; }
    }
}
