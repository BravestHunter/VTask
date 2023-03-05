using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly MainDatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public UserTaskService(MainDatabaseContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetUserTaskResponseDto>> Get(int id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);

            ServiceResponse<GetUserTaskResponseDto> response = new()
            {
                Data = _mapper.Map<GetUserTaskResponseDto>(task),
                Success = task != null
            };

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<GetUserTaskResponseDto>>> GetAll()
        {
            var tasks = await _dbContext.Tasks.ToArrayAsync();

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
            _dbContext.Tasks.Add(task);

            await _dbContext.SaveChangesAsync();

            ServiceResponse<AddUserTaskResponseDto> response = new()
            {
                Data = _mapper.Map<AddUserTaskResponseDto>(task),
                Success = true
            };

            return response;
        }

        public async Task<ServiceResponse<UpdateUserTaskResponseDto>> Update(UpdateUserTaskRequestDto updateTaskDto)
        {
            var task = await _dbContext.Tasks.FindAsync(updateTaskDto.Id);
            if (task != null)
            {
                task.Title = updateTaskDto.Title;
                task.Description = updateTaskDto.Description;

                await _dbContext.SaveChangesAsync();
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
            var task = await _dbContext.Tasks.FindAsync(id);
            if (task != null)
            {
                _dbContext.Tasks.Remove(task);

                await _dbContext.SaveChangesAsync();
            }

            ServiceResponse<DeleteUserTaskResponseDto> response = new()
            {
                Data = _mapper.Map<DeleteUserTaskResponseDto>(task),
                Success = task != null
            };

            return response;
        }
    }
}
