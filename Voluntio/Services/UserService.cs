using AutoMapper;
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
        public async Task<UserModel> GetUserAsync(int UserId)
        {
            var userT = await _voluntioRepository.GetUserAsync(userId);
            if (userT == null)
                throw new NotFoundElementException($"El usario con Id:{userId} no existe.");

            var userEnumerable = _mapper.Map<UserModel>(userT);
            return userEnumerable;
        }
    }
}
