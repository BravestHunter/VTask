using System.Collections.Generic;
using System.Threading.Tasks;
using VTask.Model;
using VTask.Model.DTO;

namespace VTask.Service
{
    public interface IUserTaskService
    {
        Task<ServiceResponse<GetUserTaskResponseDto>> Get(int id);
        Task<ServiceResponse<IEnumerable<GetUserTaskResponseDto>>> GetAll();
        Task<ServiceResponse<AddUserTaskResponseDto>> Add(AddUserTaskRequestDto addTaskDto);
        Task<ServiceResponse<UpdateUserTaskResponseDto>> Update(UpdateUserTaskRequestDto deleteTaskDto);
        Task<ServiceResponse<DeleteUserTaskResponseDto>> Delete(int id);
    }
}
