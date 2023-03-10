using AutoMapper;
using VTask.Model.DAO;
using VTask.Model.DTO.Task;
using VTask.Model.DTO.User;
using VTask.Model.MVC;

namespace VTask
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            // User
            CreateMap<LoginModel, LoginRequestDto>();
            CreateMap<User, RegisterResponseDto>();

            CreateMap<RegisterRequestDto, LoginRequestDto>();
            CreateMap<RegisterModel, RegisterRequestDto>();
            CreateMap<RegisterModel, LoginRequestDto>();

            // Task
            CreateMap<AddTaskRequestDto, Task>();
            CreateMap<Task, GetTaskResponseDto>();
            CreateMap<Task, AddTaskResponseDto>();
            CreateMap<Task, UpdateTaskResponseDto>();
            CreateMap<Task, RemoveTaskResponseDto>();

            CreateMap<GetTaskResponseDto, TaskModel>();
            CreateMap<GetTaskResponseDto, DeleteTaskModel>();
            CreateMap<TaskModel, AddTaskRequestDto>();
            CreateMap<TaskModel, UpdateTaskRequestDto>();
            CreateMap<DeleteTaskModel, RemoveTaskRequestDto>();
        }
    }
}
