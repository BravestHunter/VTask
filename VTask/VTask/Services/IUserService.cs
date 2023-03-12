using System.Threading.Tasks;
using VTask.Model.DTO.User;

namespace VTask.Services
{
    public interface IUserService
    {
        Task<UserGetResponseDto> Get(UserGetRequestDto request);
        Task<UserUpdateResponseDto> Update(UserUpdateRequestDto request);
        Task<UserChangeUsernameResponseDto> ChangeUsername(UserChangeUsernameRequestDto request);
        Task<UserChangePasswordResponseDto> ChangePassword(UserChangePasswordRequestDto request);
        Task<UserGetEmailVerificationTokenResponseDto> GetEmailVerificationToken(UserGetEmailVerificationTokenRequestDto request);
        Task<UserVerifyEmailResponseDto> VerifyEmail(UserVerifyEmailRequestDto request);
    }
}
