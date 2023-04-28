namespace Voluntio.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string? EventType { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public DateTime? EventDate { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? ImagePath { get; set; }

    }
}
