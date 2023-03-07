using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using VTask.Data;
using VTask.Model;
using VTask.Model.DTO;
using VTask.Services;

namespace VTask.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DefaultDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthRepository(DefaultDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(string name, string password)
        {
            ServiceResponse<string> response = new();

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower());

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return response;
            }

            response.Data = CreateToken(user);
            response.Success = true;

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new();

            if (await UserExists(user.Name))
            {
                return response;
            }

            (byte[] passwordHash, byte[] PasswordSalt) = CreatePasswordHashSalt(password);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = PasswordSalt;

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();


            response.Data = user.Id;
            response.Success = true;

            return response;
        }

        public async Task<bool> UserExists(string name)
        {
            return await _dbContext.Users.AnyAsync(u => u.Name.ToLower() == name.ToLower());
        }


        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var strKey = _configuration.GetSection("AuthSettings:Token").Value;
            if (strKey == null)
            {
                throw new KeyNotFoundException("Auth key wasn't found in app settings");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(strKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static (byte[] passwordHash, byte[] PasswordSalt) CreatePasswordHashSalt(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                byte[] passwordSalt = hmac.Key;
                return (passwordHash, passwordSalt);
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
