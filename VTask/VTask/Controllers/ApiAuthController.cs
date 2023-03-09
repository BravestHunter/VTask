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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public ApiAuthController(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponseDto>> Register(RegisterRequestDto requestDto)
        {
            RegisterResponseDto response = new();

            var serviceResponse = await _authService.Register(requestDto.Name, requestDto.Password);
            if (!serviceResponse.Success)
            {
                return BadRequest(response);
            }

            await _unitOfWork.SaveChanges();

            response.Id = serviceResponse.Data;

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(LoginRequestDto requestDto)
        {
            var serviceResponse = await _authService.Login(requestDto.Name, requestDto.Password);

            LoginResponseDto response = new() { Token = serviceResponse.Data };

            if (serviceResponse.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
