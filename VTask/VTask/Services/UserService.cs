using AutoMapper;
using System;
using System.Threading.Tasks;
using VTask.Exceptions;
using VTask.Model.DAO;
using VTask.Model.DTO.User;
using VTask.Repositories;

namespace VTask.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<UserGetResponseDto> Get(UserGetRequestDto request)
        {
            var user = await GetUser(request.Id);
            var response = _mapper.Map<UserGetResponseDto>(user);

            return response;
        }

        public async Task<UserUpdateResponseDto> Update(UserUpdateRequestDto request)
        {
            var user = await GetUser(request.Id);

            user.Email = request.Email;
            user.Nickname = request.NickName;

            user = _userRepository.Update(user);
            await _userRepository.SaveChanges();

            var response = _mapper.Map<UserUpdateResponseDto>(user);

            return response;
        }

        public async Task<UserChangeUsernameResponseDto> ChangeUsername(UserChangeUsernameRequestDto request)
        {
            var existingUser = await _userRepository.Get(request.UserName);
            if (existingUser != null)
            {
                throw new DbEntryAlreadyExists($"User with username '{request.UserName}' already exists");
            }

            var user = await GetUser(request.Id);

            user.Username = request.UserName;

            _userRepository.Update(user);
            await _userRepository.SaveChanges();

            var response = _mapper.Map<UserChangeUsernameResponseDto>(user);

            return response;
        }

        public async Task<UserChangePasswordResponseDto> ChangePassword(UserChangePasswordRequestDto request)
        {
            var user = await GetUser(request.Id);

            if (!_passwordService.IsValidPassword(request.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                throw new PasswordNotValidException();
            }

            (byte[] passwordHash, byte[] passwordSalt) = _passwordService.CreatePasswordHashSalt(request.NewPassword);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.Update(user);
            await _userRepository.SaveChanges();

            var response = _mapper.Map<UserChangePasswordResponseDto>(user);

            return response;
        }

        private async Task<User> GetUser(int id)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
            {
                throw new DbEntryNotFoundException($"User with id '{id}' was not found");
            }

            return user;
        }
    }
}
