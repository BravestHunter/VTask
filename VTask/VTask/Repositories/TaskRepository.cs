using VTask.Data;
using VTask.Model.DAO;

namespace VTask.Repositories
{
    public class TaskRepository : BaseRepository<Task>, ITaskRepository
    {
        public TaskRepository(DefaultDbContext dbContext) : base(dbContext)
        {
        }
    }
}
