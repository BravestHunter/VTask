using AutoMapper;
using VTask.Model;
using VTask.Model.DTO;

namespace VTask
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<UserTask, GetUserTaskResponseDto>();
            CreateMap<AddUserTaskRequestDto, UserTask>();
            CreateMap<UserTask, AddUserTaskResponseDto>();
            CreateMap<UpdateUserTaskRequestDto, UserTask>();
            CreateMap<UserTask, UpdateUserTaskResponseDto>();
            CreateMap<UserTask, DeleteUserTaskResponseDto>();
        }
    }
}
