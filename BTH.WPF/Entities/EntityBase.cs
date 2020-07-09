using System.ComponentModel.DataAnnotations;

namespace BHT.WPF.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public long Id { get; set; }
    }
}
