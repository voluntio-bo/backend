using Voluntio.Data.Entity;

namespace Voluntio.Data.Repository
{
    public interface IVoluntioRepository
    {
        //Events
        Task<IEnumerable<EventEntity>> GetEventsAsync();
        Task<EventEntity> GetEventAsync(int eventId);
        void CreateEvent(EventEntity entity);
        Task<bool> SaveChangesAsync();
        //Organizations
        Task<IEnumerable<OrganizationEntity>> GetOrganizationsAsync();
        Task<OrganizationEntity> GetOrganization(int organizationId);
        void CreateOrganization(OrganizationEntity organization);

        //USERS
        Task<UserEntity> GetUserAsync(int userId);
    }
}
