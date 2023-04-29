using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voluntio.Data.Entity
{
    public class EventEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? EventType { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public DateTime? EventDate { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? ImagePath { get; set; }
        public virtual OrganizationEntity Organization { get; set;}
    }
}
