using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using VTask.Data;
using VTask.Model;
using VTask.Model.DTO;

namespace VTask.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly DefaultDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserTaskService(DefaultDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<GetUserTaskResponseDto>> Get(int id)
        {
            int userId = GetUserId();
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
            int userId = GetUserId();
            var tasks = (await _dbContext.Users.Include(u => u.Tasks).FirstOrDefaultAsync(u => u.Id == userId))!.Tasks;

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

            int userId = GetUserId();
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId)!;
            task.User = user;

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


        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
