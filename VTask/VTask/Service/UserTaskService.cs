using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VTask.Model;
using VTask.Model.DTO;

namespace VTask.Service
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IMapper _mapper;
        private List<UserTask> _tasks;

        public UserTaskService(IMapper mapper) 
        {
            _mapper = mapper;

            this._tasks = new List<UserTask>()
            {
                new() { Id = 0, Title = "Title 0", Description = "Description 0", State = TaskState.New },
                new() { Id = 1, Title = "Title 1", Description = "Description 1", State = TaskState.New },
                new() { Id = 2, Title = "Title 2", Description = "Description 2", State = TaskState.Active },
                new() { Id = 3, Title = "Title 3", Description = "Description 3", State = TaskState.Active },
                new() { Id = 4, Title = "Title 4", Description = "Description 4", State = TaskState.Active },
                new() { Id = 5, Title = "Title 5", Description = "Description 5", State = TaskState.Active },
                new() { Id = 6, Title = "Title 6", Description = "Description 6", State = TaskState.Closed },
                new() { Id = 7, Title = "Title 7", Description = "Description 7", State = TaskState.Closed },
                new() { Id = 8, Title = "Title 8", Description = "Description 8", State = TaskState.Closed }
            };
        }

        public async Task<ServiceResponse<GetUserTaskResponseDto>> Get(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);

            ServiceResponse<GetUserTaskResponseDto> response = new()
            {
                Data = _mapper.Map<GetUserTaskResponseDto>(task),
                Success = task != null
            };

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<GetUserTaskResponseDto>>> GetAll()
        {
            IEnumerable<UserTask> tasks = _tasks;

            ServiceResponse<IEnumerable<GetUserTaskResponseDto>> response = new()
            {
                Data = tasks.Select(t => _mapper.Map<GetUserTaskResponseDto>(t)),
                Success = tasks != null && tasks.Any()
            };

            return response;
        }

        public async Task<ServiceResponse<AddUserTaskResponseDto>> Add(AddUserTaskRequestDto addTaskDto)
        {
            var task = _mapper.Map<UserTask>(addTaskDto);
            task.Id = _tasks.Max(t => t.Id) + 1;
            _tasks.Add(task);

            ServiceResponse<AddUserTaskResponseDto> response = new()
            {
                Data = _mapper.Map<AddUserTaskResponseDto>(task),
                Success = true
            };

            return response;
        }

        public async Task<ServiceResponse<UpdateUserTaskResponseDto>> Update(UpdateUserTaskRequestDto updateTaskDto)
        {
            var task = _tasks.Find(t => t.Id == updateTaskDto.Id);
            if (task != null)
            {
                task.Title = updateTaskDto.Title;
                task.Description = updateTaskDto.Description;
            }

            ServiceResponse<UpdateUserTaskResponseDto> response = new()
            {
                Data = _mapper.Map<UpdateUserTaskResponseDto>(task),
                Success = task != null
            };

            return response;
        }

        public async Task<ServiceResponse<DeleteUserTaskResponseDto>> Delete(int id)
        {
            var task = _tasks.Find(t => t.Id == id);
            _tasks.Remove(task);

            ServiceResponse<DeleteUserTaskResponseDto> response = new()
            {
                Data = _mapper.Map<DeleteUserTaskResponseDto>(task),
                Success = task != null
            };

            return response;
        }
    }
}
