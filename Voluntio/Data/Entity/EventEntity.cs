using System.ComponentModel.DataAnnotations;

namespace Voluntio.Data.Entity
{
    public class EventEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
