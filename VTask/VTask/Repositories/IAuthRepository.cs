using System.Threading.Tasks;
using VTask.Model;
using VTask.Services;

namespace VTask.Repositories
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string name, string password);
        Task<bool> UserExists(string name);
    }
}
