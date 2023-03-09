using System.Threading.Tasks;
using VTask.Model;

namespace VTask.Services
{
    public interface IAuthService
    {
        Task<string?> Login(string name, string password);
        Task Register(User user, string password);
    }
}
