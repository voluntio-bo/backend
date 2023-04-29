namespace Voluntio.Models
{
    public class UserFormModel:UserModel
    {
        public IFormFile ProfileImage { get; set; }
        public IFormFile CoverImage { get; set; }
    }
}
