using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VTask.Model.DTO;
using VTask.Repositories;
using VTask.Services;

namespace VTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiAuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public ApiAuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto requestDto)
        {
            var response = await _authRepository.Register(new Model.User() { Name = requestDto.Name }, requestDto.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto requestDto)
        {
            var response = await _authRepository.Login(requestDto.Name, requestDto.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
