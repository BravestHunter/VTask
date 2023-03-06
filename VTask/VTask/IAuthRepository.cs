using System.Threading.Tasks;
using VTask.Model;
using VTask.Service;

namespace VTask
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string name, string password);
        Task<bool> UserExists(string name);
    }
}
