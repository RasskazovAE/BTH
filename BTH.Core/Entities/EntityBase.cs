using System.ComponentModel.DataAnnotations;

namespace BHT.Core.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public long Id { get; set; }
    }
}
