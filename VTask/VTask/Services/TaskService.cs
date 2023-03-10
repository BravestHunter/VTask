using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTask.Exceptions;
using VTask.Model.DTO;
using VTask.Model.DTO.Task;
using VTask.Repositories;

namespace VTask.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public TaskService(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<GetTaskResponseDto> Get(GetTaskRequestDto request)
        {
            var task = await _taskRepository.Get(request.Id);
            if (task == null)
            {
                throw new DbEntryNotFoundException($"Task with id '{request.Id}' was not found");
            }

            var response = _mapper.Map<GetTaskResponseDto>(task);

            return response;
        }

        public async Task<IEnumerable<GetTaskResponseDto>> GetAll()
        {
            var tasks = await _taskRepository.GetAll();

            var response = tasks.Select(t => _mapper.Map<GetTaskResponseDto>(t));

            return response;
        }

        public async Task<AddTaskResponseDto> Add(AddTaskRequestDto request)
        {
            var task = _mapper.Map<VTask.Model.DAO.Task>(request);
            _taskRepository.Add(task);

            await _taskRepository.SaveChanges();

            var response = _mapper.Map<AddTaskResponseDto>(task);

            return response;
        }

        public async Task<UpdateTaskResponseDto> Update(UpdateTaskRequestDto request)
        {
            var task = await _taskRepository.Get(request.Id);
            if (task == null)
            {
                throw new DbEntryNotFoundException($"Task with id '{request.Id}' was not found");
            }

            task.Title = request.Title;
            task.Description = request.Description;
            task.State = request.State;

            await _taskRepository.SaveChanges();

            var response = _mapper.Map<UpdateTaskResponseDto>(task);

            return response;
        }

        public async Task<RemoveTaskResponseDto> Remove(RemoveTaskRequestDto request)
        {
            var task = await _taskRepository.Get(request.Id);
            if (task == null)
            {
                throw new DbEntryNotFoundException($"Task with id '{request.Id}' was not found");
            }

            _taskRepository.Remove(task);

            await _taskRepository.SaveChanges();

            var response = _mapper.Map<RemoveTaskResponseDto>(task);

            return response;
        }
    }
}
