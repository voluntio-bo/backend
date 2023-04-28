using Microsoft.EntityFrameworkCore;
using Voluntio.Data.Entity;

namespace Voluntio.Data.Repository
{
    public class VoluntioRepository:IVoluntioRepository
    {
        private Voluntio_DBContext _dbContext;

        public async Task<IEnumerable<EventEntity>> GetEventsAsync()
        {
            /*IQueryable<EventEntity> query = _dbContext.Events;
            query = query.AsNoTracking();            
            var result = await query.ToListAsync();*/
            var result = new List<EventEntity>(){
                new EventEntity() { Id=1},
                new EventEntity() { Id=2}
            };
            return result;
        }

        public async Task<EventEntity> GetEventAsync(int eventId)
        {
            IQueryable<EventEntity> query = _dbContext.Events;
            query = query.AsNoTracking();
            var eventT = await query.FirstOrDefaultAsync(g => (g.Id == eventId));
            return eventT;
        }

    }
}
