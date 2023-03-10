using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace VTask.Services
{
    public class PasswordService : IPasswordService
    {
        public (byte[] passwordHash, byte[] PasswordSalt) CreatePasswordHashSalt(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                byte[] passwordSalt = hmac.Key;
                return (passwordHash, passwordSalt);
            }
        }

        public bool IsValidPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
