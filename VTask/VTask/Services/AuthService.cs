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

        public async Task<string?> Login(string name, string password)
        {
            ServiceResponse<string> response = new();

            var user = await _unitOfWork.UserRepository.GetFirstOrDefault(u => u.Name.ToLower() == name.ToLower());

            if (user == null || !_jwtTokenService.IsValidPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            string tokenStr = _jwtTokenService.GenerateToken(user);

            return tokenStr;
        }

        public async Task Register(User user, string password)
        {
            if (await UserExists(user.Name))
            {
                return;
            }

            (byte[] passwordHash, byte[] PasswordSalt) = _jwtTokenService.CreatePasswordHashSalt(password);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = PasswordSalt;

            _unitOfWork.UserRepository.Add(user);
        }

        public async Task<bool> UserExists(string name)
        {
            return await _unitOfWork.UserRepository.GetFirstOrDefault(u => u.Name.ToLower() == name.ToLower()) != null;
        }
    }
}
