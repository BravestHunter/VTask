using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VTask.Model;
using VTask.Model.DTO.User;
using VTask.Repositories;
using VTask.Services;

namespace VTask.Controllers.MVC
{
    public class AuthController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return View(requestDto);
            }

            var user = _mapper.Map<User>(requestDto);
            await _authService.Register(user, requestDto.Password);

            await _unitOfWork.SaveChanges();

            string? token = await _authService.Login(requestDto.Name, requestDto.Password);
            if (string.IsNullOrEmpty(token))
            {
                return View(requestDto);
            }


            HttpContext.Response.Cookies.Append("JWT", token);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return View(requestDto);
            }

            string? token = await _authService.Login(requestDto.Name, requestDto.Password);
            if (string.IsNullOrEmpty(token))
            {
                return View(requestDto);
            }

            HttpContext.Response.Cookies.Append("JWT", token);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("JWT");

            return RedirectToAction("Index", "Home");
        }
    }
}
