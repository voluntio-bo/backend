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
        [HttpGet("{UserId:int}")]
        public async Task<ActionResult<UserModel>> GetUserAsync(int UserId)
        {
            try
            {
                var User = await _userService.GetUserAsync(UserId);
                return Ok(User);
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
    }
}
