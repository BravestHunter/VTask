using NuGet.Protocol.Core.Types;
using System.Collections.Generic;
using VTask.Data;
using VTask.Model;

namespace VTask.Repositories
{
    public class UserTaskRepository : Repository<UserTask>, IUserTaskRepository
    {
        public UserTaskRepository(DefaultDbContext dbContext) : base(dbContext)
        {
        }
    }
}
