using System.ComponentModel.DataAnnotations;

namespace BankTransactionHistory.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public long Id { get; set; }
    }
}
