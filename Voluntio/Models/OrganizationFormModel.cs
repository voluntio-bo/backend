namespace Voluntio.Models
{
    public class OrganizationFormModel:OrganizationModel

    {
        public IFormFile ProfileImage { get; set; }
        public IFormFile CoverImage { get; set; }

    }
}
