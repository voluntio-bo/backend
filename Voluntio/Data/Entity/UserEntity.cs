using System.ComponentModel.DataAnnotations;

namespace Voluntio.Data.Entity
{
    public class UserEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? CoverImagePath { get; set; }
        public int? Points { get; set; }
        public string? AcademicFormation { get; set; }
        public string? Abilities { get; set; }
        public string? Interests { get; set; }
        public DateTime? BirthDate { get; set; }
        public ICollection<UserEventEntity> UserEvents { get; set; }
    }
}
