namespace Voluntio.Data.Entity
{
    public class UserEventEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public int EventId { get; set; }
        public EventEntity EventT { get; set; }
    }
}
