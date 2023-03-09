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
        private readonly IMapper _mapper;

        public ApiAuthController(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponseDto>> Register(RegisterRequestDto requestDto)
        {
            var user = _mapper.Map<User>(requestDto);
            await _authService.Register(user, requestDto.Password);

            await _unitOfWork.SaveChanges();

            var response = _mapper.Map<RegisterResponseDto>(user);

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(LoginRequestDto requestDto)
        {
            string? token = await _authService.Login(requestDto.Name, requestDto.Password);

            LoginResponseDto response = new() { Token = token };

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
