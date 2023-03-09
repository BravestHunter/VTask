using NuGet.Protocol.Core.Types;
using VTask.Data;
using VTask.Model;

namespace VTask.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DefaultDbContext dbContext) : base(dbContext)
        {
        }
    }
}
