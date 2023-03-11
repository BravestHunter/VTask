using System.Collections.Generic;
using System.Threading.Tasks;
using VTask.Model;
using VTask.Model.DTO;
using VTask.Model.DTO.Task;

namespace VTask.Services
{
    public interface ITaskService
    {
        Task<TaskGetResponseDto> Get(TaskGetRequestDto request);
        Task<IEnumerable<TaskGetResponseDto>> GetAll();
        Task<TaskAddResponseDto> Add(TaskAddRequestDto request);
        Task<TaskUpdateResponseDto> Update(TaskUpdateRequestDto request);
        Task<TaskRemoveResponseDto> Remove(TaskRemoveTRequestDto request);
    }
}
