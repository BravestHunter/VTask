using System;
using System.Threading.Tasks;
using VTask.Model.DAO;

namespace VTask.Services
{
    public interface IJwtTokenService
    {
        (string token, DateTime expirationDate) GenerateAuthToken(User user);
    }
}
