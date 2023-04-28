using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Voluntio.Data.Entity;
using Voluntio.Data.Repository;
using Voluntio.Exceptions;
using Voluntio.Models;

namespace Voluntio.Services
{
    public class EventService : IEventService
    {
        private IVoluntioRepository _EventRepository;
        private IMapper _mapper;
        public EventService(IVoluntioRepository eventRepository, IMapper mapper)
        {
            _EventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EventModel>> GetEventsAsync()
        {
            var eventEntityList = await _EventRepository.GetEventsAsync();

            if (eventEntityList == null || !eventEntityList.Any())
                throw new NotFoundElementException($"La lista de eventos no existe o está vacía.");

            var eventEnumerable = _mapper.Map<IEnumerable<EventModel>>(eventEntityList);
            return eventEnumerable;
        }

        public async Task<EventModel> GetEventAsync(int eventId)
        {
            var eventT = await _EventRepository.GetEventAsync(eventId);
            if (eventT == null)
                throw new NotFoundElementException($"El evento con Id:{eventId} no existe.");

            var eventEnumerable = _mapper.Map<EventModel>(eventT);
            return eventEnumerable;
        }

        public async Task<EventModel> CreateEventAsync(EventModel model)
        {
            var eventEntity = _mapper.Map<EventEntity>(model);
            _EventRepository.CreateEvent(eventEntity);
            var result = await _EventRepository.SaveChangesAsync();
            if (result)
            {
                return _mapper.Map<EventModel>(model);
            }
            throw new Exception("Database Error");
        }
        

    }
}
