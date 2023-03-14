using System.Threading.Tasks;
using VTask.Model.DAO;
using VTask.Model.DTO.Auth;
using VTask.Model.MVC;

namespace VTask.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto request);
        Task<RegisterResponseDto> Register(RegisterRequestDto request);
        Task<PasswordResetGetTokenResponseDto> GetPasswordResetToken(PasswordResetGetTokenRequestDto request);
        Task<PasswordResetResponseDto> ResetPassword(PasswordResetRequestDto request);
    }
}
