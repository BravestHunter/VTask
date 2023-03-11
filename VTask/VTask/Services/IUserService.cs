using System.Threading.Tasks;
using VTask.Model.DTO.User;

namespace VTask.Services
{
    public interface IUserService
    {
        Task<UserGetResponseDto> Get(UserGetRequestDto request);
        Task<UserUpdateResponseDto> Update(UserUpdateRequestDto request);
    }
}
