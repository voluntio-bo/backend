using Voluntio.Models;

namespace Voluntio.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUserAsync(int userId);
        Task<UserEventModel> InscribeToEventAsync(int userId,int eventId);
        Task<UserModel> CreateUserAsync(UserModel user);
    }
}
