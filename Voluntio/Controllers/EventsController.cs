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
        private IFileService _fileService;
        public EventsController(IEventService eventService, IFileService fileService)
        {
            _eventService = eventService;
            _fileService = fileService;
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

        [HttpPost("Form")]
        public async Task<ActionResult<EventModel>> CreateEventFormAsync([FromForm] EventFormModel eventT)
        {
            try
            {
                var d = DateTime.UtcNow;
                //qr.DonationPostId = donationPostId;
                //[TO DO] Find a way to validate a model FromForm
                //if (!ModelState.IsValid)
                //return BadRequest(ModelState);
                // var qrModel = qr;
                string imagePath = null;
                var file = eventT.Image;
                if (file != null)
                {
                    imagePath = await _fileService.UploadFile(file);
                }


                eventT.ImagePath = imagePath;

                var eventCreated = await _eventService.CreateEventAsync(eventT);
                var id = eventCreated.Id;
                return Created($"/api/[controller]/{id}", eventT);//new BookModel() { Id = newBook.Id, Title = newBook.Title, Author = newBook.Author, Genre = newBook.Genre, ImagePath = newBook.ImagePath });
                //return a;
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidImageException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happened.");
            }
        }
    }
}
