using AutoMapper;
using Voluntio.Data.Entity;
using Voluntio.Models;

namespace Voluntio
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            this.CreateMap<OrganizationEntity, OrganizationModel>()
                .ReverseMap();
            this.CreateMap<EventEntity, EventModel>()
                .ForMember(mod => mod.OrganizationId, ent => ent.MapFrom(entSrc => entSrc.Organization.Id))
                .ReverseMap()
                .ForMember(ent => ent.Organization, mod => mod.MapFrom(modSrc => new OrganizationEntity() { Id = modSrc.OrganizationId }));
            this.CreateMap<UserEntity, UserModel>()
                .ReverseMap();
            this.CreateMap<UserEventEntity, UserEventModel>()
                .ReverseMap();
        }
    }
}
