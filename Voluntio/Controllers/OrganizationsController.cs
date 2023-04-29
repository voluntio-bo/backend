using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using Voluntio.Exceptions;
using Voluntio.Models;
using Voluntio.Services;
namespace Voluntio.Controllers
{
    [Route("api/organizations")]
    public class OrganizationsController:Controller
    {
        private IOrganizationService organizationService;
        private IFileService fileService;
        public OrganizationsController(IOrganizationService organizationService, IFileService fileService)
        {
            this.organizationService = organizationService;
            this.fileService = fileService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationModel>>> GetOrganizations()
        {
            try
            {
                var Organizations = await organizationService.GetOrganizationsAsync();
                return Ok(Organizations);
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
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrganizationModel>> GetOrganizationAsync(int id)
        {
            try
            {
                var organization = await organizationService.GetOrganizationAsync(id);
                return Ok(organization);
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
        [HttpPost]
        public async Task<ActionResult<OrganizationModel>> PostOrganizationAsync([FromBody] OrganizationModel organization)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var newOrganization = await organizationService.CreateOrganizationAsync(organization);
                return Created($"api/organizations/{newOrganization.Id}", newOrganization);
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

        [HttpPost("Form")]
        public async Task<ActionResult<OrganizationModel>> CreateOrganiztionFormAsync([FromForm] OrganizationFormModel newOrganization)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                string imagePathProfile = null;
                var file = newOrganization.ProfileImage;
                if (file != null)
                {
                    imagePathProfile = await fileService.UploadFile(file);
                }

                string imagePathCover = null;
                var fileC = newOrganization.CoverImage;
                if (fileC != null)
                {
                    imagePathCover = await fileService.UploadFile(fileC);
                }

                newOrganization.CoverImagePath = imagePathCover;
                newOrganization.ProfileImagePath = imagePathProfile;

                var organizationCreated = await organizationService.CreateOrganizationAsync(newOrganization);
                var id = organizationCreated.Id;
                return Created($"/api/[controller]/{id}", organizationCreated);
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
