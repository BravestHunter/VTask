using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using VTask.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTask.Repositories;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Identity;
using VTask.Exceptions;
using VTask.Model.DAO;
using VTask.Model.DTO.User;
using AutoMapper;

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

            (string token, DateTime expirationDate) = _jwtTokenService.GenerateToken(user);

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
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreationTime = DateTime.Now
            };

            _userRepository.Add(user);

            await _userRepository.SaveChanges();

            var response = _mapper.Map<RegisterResponseDto>(user);

            return response;
        }
    }
}
