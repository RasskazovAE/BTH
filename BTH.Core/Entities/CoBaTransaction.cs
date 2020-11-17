using BTH.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BHT.Core.Entities
{
    /// <summary>
    /// Transaction for commerzbank
    /// </summary>
    [Index(nameof(BookingText), IsUnique = true)]
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
        /// Category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// User account key property
        /// </summary>
        [ForeignKey(nameof(UserAccount))]
        public long UserAccountId { get; set; }

        /// <summary>
        /// User account
        /// </summary>
        public CoBaUser UserAccount { get; set; }
    }
}
