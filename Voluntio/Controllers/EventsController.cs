using Microsoft.AspNetCore.Mvc;
using Voluntio.Exceptions;
using Voluntio.Models;
using Voluntio.Services;

namespace Voluntio.Controllers
{
    [Route("api/events")]

    public class EventsController : Controller
    {
        private IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventModel>>> GetEventsAsync()
        {
            try
            {
                var Events = await _eventService.GetEventsAsync();
                return Ok(Events);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lo sentimos, algo sucedió.");
            }
        }

        [HttpGet("{EventId:int}")]
        public async Task<ActionResult<EventModel>> GetEventAsync(int EventId)
        {
            try
            {
                var Event = await _eventService.GetEventAsync(EventId);
                return Ok(Event);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lo sentimos, algo sucedió.");
            }
        }
        [HttpPost]
        public async Task<ActionResult<EventModel>> PostEvent([FromBody] EventModel eventModel)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                var newEvent = await _eventService.CreateEventAsync(eventModel);
                return Created($"api/events/{newEvent.Id}", newEvent);

            }
            catch(NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
