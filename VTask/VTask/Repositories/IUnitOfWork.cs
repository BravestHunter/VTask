using System.Threading.Tasks;

namespace VTask.Repositories
{
    public interface IUnitOfWork
    {
        IUserTaskRepository UserTaskRepository { get; }

        Task SaveChanges();
    }
}
