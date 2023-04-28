using Microsoft.EntityFrameworkCore;
using Voluntio.Data.Entity;
using Voluntio.Models;

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
                new EventEntity() { Id=1,EventDate=DateTime.Now,StartTime="8:00AM",EndTime="10:00AM", Description="descripcion",Name="name",EventType="type",ImagePath="https://th.bing.com/th/id/R.bae01f61b1db58c36b51cd85c6e2d941?rik=TaUKK1GhA7eoZA&riu=http%3a%2f%2fwww.eventaris.es%2fimages%2fslide-4.jpg&ehk=pbsePoNHobQDSerNyVzsX60Qi1dlSpuRxSaK85i81KE%3d&risl=&pid=ImgRaw&r=0"},
                new EventEntity() { Id=2,EventDate=DateTime.Now,StartTime="8:00AM",EndTime="10:00AM", Description="descripcion",Name="name",EventType="type",ImagePath="https://th.bing.com/th/id/R.bae01f61b1db58c36b51cd85c6e2d941?rik=TaUKK1GhA7eoZA&riu=http%3a%2f%2fwww.eventaris.es%2fimages%2fslide-4.jpg&ehk=pbsePoNHobQDSerNyVzsX60Qi1dlSpuRxSaK85i81KE%3d&risl=&pid=ImgRaw&r=0"}
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
        public void CreateEvent(EventEntity eventEntity)
        {
            _dbContext.Events.Add(eventEntity);
     
        }
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var result = await _dbContext.SaveChangesAsync();
                return result > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
