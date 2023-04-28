using Voluntio.Models;

namespace Voluntio.Services
{
    public interface IEventService
    {
        Task<IEnumerable<EventModel>> GetEventsAsync();
        Task<EventModel> GetEventAsync(int EventId);
    }
}
