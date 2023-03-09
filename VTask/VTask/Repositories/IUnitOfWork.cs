using System.Threading.Tasks;

namespace VTask.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IUserTaskRepository UserTaskRepository { get; }

        Task SaveChanges();
    }
}
