using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VTask.Model.DAO;

namespace VTask.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> Get(string username);
    }
}
