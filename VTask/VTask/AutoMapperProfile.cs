using AutoMapper;
using VTask.Model.DAO;
using VTask.Model.DTO.Auth;
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
            CreateMap<User, PasswordResetResponseDto>();
            CreateMap<RegisterRequestDto, LoginRequestDto>();
            CreateMap<RegisterModel, RegisterRequestDto>();
            CreateMap<RegisterModel, LoginRequestDto>();
            CreateMap<PasswordResetSendMessageModel, PasswordResetGetTokenRequestDto>();
            CreateMap<PasswordResetModel, PasswordResetRequestDto>();
            CreateMap<User, UserModel>();
            CreateMap<User, UserGetResponseDto>();
            CreateMap<User, UserUpdateResponseDto>();
            CreateMap<User, UserChangeUsernameResponseDto>();
            CreateMap<User, UserChangePasswordResponseDto>();
            CreateMap<User, UserVerifyEmailResponseDto>();
            CreateMap<UserGetResponseDto, UserModel>();
            CreateMap<UserGetResponseDto, UsernameChangeModel>();
            CreateMap<UserGetResponseDto, PasswordChangeModel>();
            CreateMap<UserGetResponseDto, UserGetEmailVerificationTokenRequestDto>();
            CreateMap<UserModel, UserUpdateRequestDto>();
            CreateMap<UsernameChangeModel, UserChangeUsernameRequestDto>();
            CreateMap<PasswordChangeModel, UserChangePasswordRequestDto>();

            // Task
            CreateMap<TaskAddRequestDto, Task>();
            CreateMap<Task, TaskGetResponseDto>();
            CreateMap<Task, TaskAddResponseDto>();
            CreateMap<Task, TaskUpdateResponseDto>();
            CreateMap<Task, TaskRemoveResponseDto>();

            CreateMap<TaskGetResponseDto, TaskModel>();
            CreateMap<TaskGetResponseDto, DeleteTaskModel>();
            CreateMap<TaskModel, TaskAddRequestDto>();
            CreateMap<TaskModel, TaskUpdateRequestDto>();
            CreateMap<DeleteTaskModel, TaskRemoveTRequestDto>();
        }
    }
}
