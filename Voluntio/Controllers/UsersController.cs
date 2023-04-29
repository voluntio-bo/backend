using Microsoft.AspNetCore.Mvc;
using Voluntio.Exceptions;
using Voluntio.Models;
using Voluntio.Services;

namespace Voluntio.Controllers
{
    [Route("api/users")]
    public class UsersController:Controller
    {        
        private IUserService _userService;
        private IFileService _fileService;
        public UsersController(IUserService userService, IFileService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }
        [HttpGet("{userId:int}")]
        public async Task<ActionResult<UserModel>> GetUserAsync(int userId)
        {
            try
            {
                var user = await _userService.GetUserAsync(userId);
                return Ok(user);
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

        [HttpGet("{userId:int}/inscribe/{eventId:int}")]
        public async Task<ActionResult<UserModel>> InscribeToEventAsync(int userId, int eventId)
        {
            try
            {
                var userEvent = await _userService.InscribeToEventAsync(userId, eventId);
                return Ok(userEvent);
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
        [HttpPost("Form")]
        public async Task<ActionResult<UserModel>> CreateUserFormAsync([FromForm] UserFormModel newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                string imagePathProfile = null;
                var file = newUser.ProfileImage;
                if (file != null)
                {
                    imagePathProfile = await _fileService.UploadFile(file);
                }

                string imagePathCover = null;
                var fileC = newUser.CoverImage;
                if (fileC != null)
                {
                    imagePathCover = await _fileService.UploadFile(fileC);
                }

                newUser.CoverImagePath = imagePathCover;
                newUser.ProfileImagePath = imagePathProfile;

                var userCreated = await _userService.CreateUserAsync(newUser);
                var id = userCreated.Id;
                return Created($"/api/[controller]/{id}", userCreated);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry, something unexpected happened");
            }

        }
    }    

}
