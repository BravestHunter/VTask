using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using VTask.Model.DAO;

namespace VTask.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private static readonly TimeSpan ExpirationTimeSpan = TimeSpan.FromDays(1);

        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string token, DateTime expirationDate) GenerateAuthToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var strKey = _configuration.GetSection("AuthSettings:Token").Value;
            if (strKey == null)
            {
                throw new KeyNotFoundException("Auth key wasn't found in app settings");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(strKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            DateTime expirationTime = DateTime.Now + ExpirationTimeSpan;

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationTime,
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenStr = tokenHandler.WriteToken(token);

            return (tokenStr, expirationTime);
        }
    }
}
