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
                //.ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserEvents.Select(ec => ec.User).ToList()))                
                .ForMember(mod => mod.OrganizationId, ent => ent.MapFrom(entSrc => entSrc.Organization.Id))
                .ReverseMap()
                .ForMember(ent => ent.Organization, mod => mod.MapFrom(modSrc => new OrganizationEntity() { Id = modSrc.OrganizationId }))
                .ReverseMap();
            this.CreateMap<UserEntity, UserModel>()
                .ForMember(dest => dest.EventsT, opt => opt.MapFrom(src => src.UserEvents.Select(ec => ec.EventT).ToList()))
                .ReverseMap();
            this.CreateMap<UserEventEntity, UserEventModel>()
                .ReverseMap();
        }
    }
}
