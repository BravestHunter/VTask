using System;
using System.Threading.Tasks;
using VTask.Repositories;
using VTask.Exceptions;
using VTask.Model.DAO;
using AutoMapper;
using VTask.Model.DTO.Auth;

namespace VTask.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AuthService(IJwtTokenService jwtTokenService, IPasswordService passwordService, IMapper mapper, IUserRepository userRepository)
        {
            _jwtTokenService = jwtTokenService;
            _passwordService = passwordService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            var user = await _userRepository.Get(request.Username);
            if (user == null)
            {
                throw new DbEntryNotFoundException($"User with username '{request.Username}' was not found");
            }

            if (!_passwordService.IsValidPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new PasswordNotValidException();
            }

            (string token, DateTime expirationDate) = _jwtTokenService.GenerateAuthToken(user);

            LoginResponseDto response = new()
            {
                Token = token,
                ExpirationDate = expirationDate
            };

            return response;
        }

        public async Task<RegisterResponseDto> Register(RegisterRequestDto request)
        {
            var existingUser = await _userRepository.Get(request.Username);
            if (existingUser != null)
            {
                throw new DbEntryAlreadyExists($"User with username {request.Username} already exists");
            }

            (byte[] passwordHash, byte[] passwordSalt) = _passwordService.CreatePasswordHashSalt(request.Password);

            User user = new()
            {
                Username = request.Username,
                Nickname = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _userRepository.Add(user);

            await _userRepository.SaveChanges();

            var response = _mapper.Map<RegisterResponseDto>(user);

            return response;
        }
    }
}
