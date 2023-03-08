using System.Collections.Generic;
using System.Threading.Tasks;
using VTask.Model;
using VTask.Model.DTO;

namespace VTask.Repositories
{
    public interface IUserTaskRepositoryAlter
    {
        Task<UserTask> Get(int id);
        Task<IEnumerable<UserTask>> GetLast(int count);
        Task<UserTask> Add(UserTask task);
        Task<UserTask> Update(UserTask task);
        Task Delete(UserTask task);
    }
}
