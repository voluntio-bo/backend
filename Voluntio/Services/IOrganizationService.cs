using Voluntio.Models;

namespace Voluntio.Services
{
    public interface IOrganizationService
    {
        Task<IEnumerable<OrganizationModel>> GetOrganizationsAsync();
        Task<OrganizationModel> GetOrganizationAsync(int organizationId);
        Task<OrganizationModel> CreateOrganizationAsync(OrganizationModel organization);
    }
}
