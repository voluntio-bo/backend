using Voluntio.Data.Entity;

namespace Voluntio.Data.Repository
{
    public interface IVoluntioRepository
    {
        Task<IEnumerable<EventEntity>> GetEventsAsync();
        Task<EventEntity> GetEventAsync(int eventId);
        void CreateEvent(EventEntity entity);
        Task<bool> SaveChangesAsync();
    }
}
