using System.Threading.Tasks;
using VTask.Model;

namespace VTask.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(string name, string password);
        Task<ServiceResponse<int>> Register(string name, string password);
    }
}
