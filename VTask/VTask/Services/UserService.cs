using AutoMapper;
using Azure.Core;
using EllipticCurve.Utils;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using VTask.Exceptions;
using VTask.Model.DAO;
using VTask.Model.DTO.User;
using VTask.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VTask.Services
{
    public class UserService : IUserService
    {
        private static readonly TimeSpan EmailVerificationLifetime = TimeSpan.FromDays(1);

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IJwtTokenService _tokenService;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordService passwordService, IJwtTokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordService = passwordService;
            _tokenService = tokenService;
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
                throw new DbEntryAlreadyExistsException($"User with username '{request.UserName}' already exists");
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

        public async Task<UserGetEmailVerificationTokenResponseDto> GetEmailVerificationToken(UserGetEmailVerificationTokenRequestDto request)
        {
            var user = await GetUser(request.Id);

            if (user.Email == null)
            {
                throw new NullReferenceException($"Email for user with id {request.Id} is not specified");
            }

            var token = GenerateEmailVerificationToken(user.Id, user.Email);

            UserGetEmailVerificationTokenResponseDto response = new()
            {
                Id = user.Id,
                Email = user.Email,
                Token = token
            };

            return response;
        }

        public async Task<UserVerifyEmailResponseDto> VerifyEmail(UserVerifyEmailRequestDto request)
        {
            (int id, string email) = ProcessEmailVerificationToken(request.Token);
            var user = await GetUser(id);

            if (user.Email != email)
            {
                throw new EmailVerificationException();
            }

            user.EmailConfirmed = true;

            _userRepository.Update(user);
            await _userRepository.SaveChanges();

            var response = _mapper.Map<UserVerifyEmailResponseDto>(user);

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

        private string GenerateEmailVerificationToken(int userId, string email)
        {
            DateTime expirationDate = DateTime.Now + EmailVerificationLifetime;

            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(userId);
                writer.Write(email);
                writer.Write(expirationDate.ToBinary());

                return Convert.ToBase64String(stream.ToArray());
            }
        }

        private (int userId, string email) ProcessEmailVerificationToken(string token)
        {
            using (var stream = new MemoryStream(Convert.FromBase64String(token)))
            using (var reader = new BinaryReader(stream))
            {
                int userId = reader.ReadInt32();
                string email = reader.ReadString();
                DateTime expirationDate = DateTime.FromBinary(reader.ReadInt64());

                if (expirationDate < DateTime.Now)
                {
                    throw new TokenExpiredException();
                }

                return (userId, email);
            }
        }
    }
}
