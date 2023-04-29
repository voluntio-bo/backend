namespace Voluntio.Services
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
