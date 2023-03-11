using AutoMapper;
using Azure;
using System.Threading.Tasks;
using VTask.Exceptions;
using VTask.Model.DTO.User;
using VTask.Model.MVC;
using VTask.Repositories;

namespace VTask.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserGetResponseDto> Get(UserGetRequestDto request)
        {
            var user = await _userRepository.Get(request.Id);
            if (user == null)
            {
                throw new DbEntryNotFoundException($"User with id '{request.Id}' was not found");
            }

            var response = _mapper.Map<UserGetResponseDto>(user);

            return response;
        }

        public async Task<UserUpdateResponseDto> Update(UserUpdateRequestDto request)
        {
            var user = await _userRepository.Get(request.Id);
            if (user == null)
            {
                throw new DbEntryNotFoundException($"User with id '{request.Id}' was not found");
            }

            user.Email = request.Email;
            user.Nickname = request.NickName;

            user = _userRepository.Update(user);
            await _userRepository.SaveChanges();

            var response = _mapper.Map<UserUpdateResponseDto>(user);

            return response;
        }
    }
}
