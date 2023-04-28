using AutoMapper;
using Voluntio.Data.Entity;
using Voluntio.Models;

namespace Voluntio
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            this.CreateMap<EventEntity, EventModel>()
                .ReverseMap();                
        }
    }
}
