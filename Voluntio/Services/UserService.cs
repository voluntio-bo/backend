using AutoMapper;
using Voluntio.Data.Entity;
using Voluntio.Data.Repository;
using Voluntio.Exceptions;
using Voluntio.Models;

namespace Voluntio.Services
{
    public class UserService : IUserService
    {
        private IVoluntioRepository _voluntioRepository;
        private IMapper _mapper;
        public UserService(IVoluntioRepository voluntioRepository, IMapper mapper)
        {
            _voluntioRepository = voluntioRepository;
            _mapper = mapper;
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            _voluntioRepository.CreateUser(userEntity);
            var result = await _voluntioRepository.SaveChangesAsync();
            if (result)
            {
                return _mapper.Map<UserModel>(userEntity);
            }
            throw new Exception("Database Error.");
        }

        public async Task<UserModel> GetUserAsync(int userId)
        {
            var userT = await _voluntioRepository.GetUserAsync(userId);
            if (userT == null)
                throw new NotFoundElementException($"El usario con Id:{userId} no existe.");

            var userEnumerable = _mapper.Map<UserModel>(userT);
            return userEnumerable;
        }

        public async Task<UserEventModel> InscribeToEventAsync(int userId,int eventId)
        {
            var newUserEvent = new UserEventModel{
                UserId = userId,
                EventId = eventId
            };
            var newUserEventEntity = _mapper.Map<UserEventEntity>(newUserEvent);
            _voluntioRepository.InscribeToEventAsync(newUserEventEntity);
            var result = await _voluntioRepository.SaveChangesAsync();
            return newUserEvent;
        }
    }
}
