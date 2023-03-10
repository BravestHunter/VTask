using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VTask.Model;
using VTask.Model.DTO.User;
using VTask.Repositories;
using VTask.Services;

namespace VTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiAuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public ApiAuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
        {
            var response = await _authService.Login(request);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponseDto>> Register(RegisterRequestDto request)
        {
            var response = await _authService.Register(request);

            return Ok(response);
        }
    }
}
