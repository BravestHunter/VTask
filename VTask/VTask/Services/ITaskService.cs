using System.Collections.Generic;
using System.Threading.Tasks;
using VTask.Model;
using VTask.Model.DTO;
using VTask.Model.DTO.Task;

namespace VTask.Services
{
    public interface ITaskService
    {
        Task<GetTaskResponseDto> Get(GetTaskRequestDto request);
        Task<IEnumerable<GetTaskResponseDto>> GetAll();
        Task<AddTaskResponseDto> Add(AddTaskRequestDto request);
        Task<UpdateTaskResponseDto> Update(UpdateTaskRequestDto request);
        Task<RemoveTaskResponseDto> Remove(RemoveTaskRequestDto request);
    }
}
