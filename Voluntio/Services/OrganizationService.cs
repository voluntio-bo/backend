using AutoMapper;
using Voluntio.Data.Entity;
using Voluntio.Data.Repository;
using Voluntio.Exceptions;
using Voluntio.Models;

namespace Voluntio.Services
{
    public class OrganizationService : IOrganizationService
    {
        private IVoluntioRepository _voluntioRepository;
        private IMapper _mapper;
        public OrganizationService(IVoluntioRepository voluntioRepository, IMapper mapper)
        {
            _voluntioRepository = voluntioRepository;
            _mapper = mapper;
        }
        public async Task<OrganizationModel> CreateOrganizationAsync(OrganizationModel organization)
        {
            var organizationEntity = _mapper.Map<OrganizationEntity>(organization);
            _voluntioRepository.CreateOrganization(organizationEntity);
            var result = await _voluntioRepository.SaveChangesAsync();
            if (result)
            {
                return _mapper.Map<OrganizationModel>(organizationEntity);
            }
            throw new Exception("Database Error.");
        }

        public async Task<OrganizationModel> GetOrganizationAsync(int organizationId)
        {
            var organization = await _voluntioRepository.GetOrganization(organizationId);
            if(organization == null)
            {
                throw new NotFoundElementException($"The organization with Id {organizationId} does not exist");
            }
            return _mapper.Map<OrganizationModel>(organization);
        }

        public async Task<IEnumerable<OrganizationModel>> GetOrganizationsAsync()
        {
       
            var organizationsEntity = await _voluntioRepository.GetOrganizationsAsync();
            return _mapper.Map<IEnumerable<OrganizationModel>>(organizationsEntity);

        }
    }
}
