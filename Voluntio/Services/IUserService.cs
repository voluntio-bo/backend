using Voluntio.Models;

namespace Voluntio.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUserAsync(int UserId);
    }
}
