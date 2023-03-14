using System;
using System.Threading.Tasks;
using VTask.Repositories;
using VTask.Exceptions;
using VTask.Model.DAO;
using AutoMapper;
using VTask.Model.DTO.Auth;
using System.IO;

namespace VTask.Services
{
    public class AuthService : IAuthService
    {
        private static readonly TimeSpan PasswordResetTokenLifetime = TimeSpan.FromHours(1);

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

        public async Task<PasswordResetGetTokenResponseDto> GetPasswordResetToken(PasswordResetGetTokenRequestDto request)
        {
            var user = await _userRepository.Get(request.Username);
            if (user == null)
            {
                throw new DbEntryNotFoundException($"User with username '{request.Username}' was not found");
            }

            if (!user.EmailConfirmed)
            {
                throw new EmailNotConfirmedException();
            }

            string token = GeneratePasswordResetToken(user.Id);

            PasswordResetGetTokenResponseDto response = new()
            {
                Id = user.Id,
                Token = token,
                Email = user.Email!
            };

            return response;
        }

        public async Task<PasswordResetResponseDto> ResetPassword(PasswordResetRequestDto request)
        {
            int userId = ProcessPasswordResetToken(request.Token);

            var user = await GetUser(userId);

            (byte[] passwordHash, byte[] passwordSalt) = _passwordService.CreatePasswordHashSalt(request.NewPassword);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.Update(user);

            await _userRepository.SaveChanges();

            var response = _mapper.Map<PasswordResetResponseDto>(user);

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

        private string GeneratePasswordResetToken(int userId)
        {
            DateTime expirationDate = DateTime.Now + PasswordResetTokenLifetime;

            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(userId);
                writer.Write(expirationDate.ToBinary());

                return Convert.ToBase64String(stream.ToArray());
            }
        }

        private int ProcessPasswordResetToken(string token)
        {
            using (var stream = new MemoryStream(Convert.FromBase64String(token)))
            using (var reader = new BinaryReader(stream))
            {
                int userId = reader.ReadInt32();
                DateTime expirationDate = DateTime.FromBinary(reader.ReadInt64());

                if (expirationDate < DateTime.Now)
                {
                    throw new TokenExpiredException();
                }

                return userId;
            }
        }
    }
}
