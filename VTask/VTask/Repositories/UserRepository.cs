using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Linq;
using System.Threading.Tasks;
using VTask.Data;
using VTask.Model.DAO;

namespace VTask.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DefaultDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> Get(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }
    }
}
