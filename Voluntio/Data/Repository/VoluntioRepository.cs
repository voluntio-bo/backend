using Microsoft.EntityFrameworkCore;
using Voluntio.Data.Entity;
using Voluntio.Models;

namespace Voluntio.Data.Repository
{
    public class VoluntioRepository:IVoluntioRepository
    {
        private Voluntio_DBContext _dbContext;

        public VoluntioRepository(Voluntio_DBContext voluntio_DBContext)
        {
            _dbContext = voluntio_DBContext;
        }
        public async Task<IEnumerable<EventEntity>> GetEventsAsync()
        {
            IQueryable<EventEntity> query = _dbContext.Events;
            query = query.AsNoTracking();
            query = query.Include(f => f.Organization);
            var result = await query.ToListAsync();
            //var result = new List<EventEntity>(){
            //    new EventEntity() { Id=1,EventDate=DateTime.Now,StartTime="8:00AM",EndTime="10:00AM", Description="descripcion",Name="name",EventType="type",ImagePath="https://th.bing.com/th/id/R.bae01f61b1db58c36b51cd85c6e2d941?rik=TaUKK1GhA7eoZA&riu=http%3a%2f%2fwww.eventaris.es%2fimages%2fslide-4.jpg&ehk=pbsePoNHobQDSerNyVzsX60Qi1dlSpuRxSaK85i81KE%3d&risl=&pid=ImgRaw&r=0"},
            //    new EventEntity() { Id=2,EventDate=DateTime.Now,StartTime="8:00AM",EndTime="10:00AM", Description="descripcion",Name="name",EventType="type",ImagePath="https://th.bing.com/th/id/R.bae01f61b1db58c36b51cd85c6e2d941?rik=TaUKK1GhA7eoZA&riu=http%3a%2f%2fwww.eventaris.es%2fimages%2fslide-4.jpg&ehk=pbsePoNHobQDSerNyVzsX60Qi1dlSpuRxSaK85i81KE%3d&risl=&pid=ImgRaw&r=0"}
            //};
            return result;
        }

        public async Task<EventEntity> GetEventAsync(int eventId)
        {
            IQueryable<EventEntity> query = _dbContext.Events;
            query = query.AsNoTracking();
            query = query.Include(f => f.Organization);
            query = query.Include(f => f.UserEvents).ThenInclude(ec => ec.User);
            var eventT = await query.FirstOrDefaultAsync(g => (g.Id == eventId));
            return eventT;
        }
        public void CreateEvent(EventEntity eventEntity)
        {
            _dbContext.Entry(eventEntity.Organization).State = EntityState.Unchanged;
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

        public async Task<UserEntity> GetUserAsync(int userId)
        {
            IQueryable<UserEntity> query = _dbContext.Users;
            query = query.AsNoTracking();
            query = query.Include(f => f.UserEvents).ThenInclude(ec => ec.EventT);
            var userT = await query.FirstOrDefaultAsync(g => (g.Id == userId));
            return userT;
        }
        public async Task<IEnumerable<OrganizationEntity>> GetOrganizationsAsync()
        {
            IQueryable<OrganizationEntity> query = _dbContext.Organizations;
            query = query.AsNoTracking();
            var result = await query.ToListAsync();
            return result;

        }

        public async Task<OrganizationEntity> GetOrganization(int organizationId)
        {
            IQueryable<OrganizationEntity> query = _dbContext.Organizations;
            query = query.AsNoTracking();
            query = query.Include(f => f.Events);
            var result = await query.FirstOrDefaultAsync(o=> o.Id==organizationId);
            return result;
        }

        public void CreateOrganization(OrganizationEntity organization)
        {
            _dbContext.Organizations.Add(organization);
        }

        public void InscribeToEventAsync(UserEventEntity userEvent)
        {
            _dbContext.UserEvents.Add(userEvent);
        }

        public void CreateUser(UserEntity user)
        {
            _dbContext.Users.Add(user);
        }
    }
}
