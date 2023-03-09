using System.Threading.Tasks;
using VTask.Model;

namespace VTask.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
        (byte[] passwordHash, byte[] PasswordSalt) CreatePasswordHashSalt(string password);
        bool IsValidPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
