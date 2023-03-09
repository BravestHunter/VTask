using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System;
using VTask.Model;
using System.Linq;
using Microsoft.Extensions.Configuration;
using VTask.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTask.Repositories;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Identity;

namespace VTask.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ServiceResponse<string>> Login(string name, string password)
        {
            ServiceResponse<string> response = new();

            var user = await GetUserByName(name);

            if (user == null || !_jwtTokenService.IsValidPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return response;
            }

            string tokenStr = _jwtTokenService.GenerateToken(user);
            if (string.IsNullOrEmpty(tokenStr))
            {
                return response;
            }

            response.Data = tokenStr;
            response.Success = true;

            return response;
        }

        public async Task<ServiceResponse<int>> Register(string name, string password)
        {
            ServiceResponse<int> response = new();

            var existingUser = await GetUserByName(name);
            if (existingUser != null)
            {
                return response;
            }

            (byte[] passwordHash, byte[] passwordSalt) = _jwtTokenService.CreatePasswordHashSalt(password);

            User user = new()
            {
                Name = name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _unitOfWork.UserRepository.Add(user);

            response.Data = user.Id;
            response.Success = true;

            return response;
        }

        public async Task<User?> GetUserByName(string name)
        {
            return await _unitOfWork.UserRepository.GetFirstOrDefault(u => u.Name.ToLower() == name.ToLower());
        }
    }
}
