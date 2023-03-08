using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VTask.Model;
using VTask.Model.DTO;
using VTask.Repositories;

namespace VTask.Controllers.MVC
{
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserRequestDto requestDto)
        {
            var user = _mapper.Map<User>(requestDto);
            var response = await _authRepository.Register(user, requestDto.Password);

            if (!response.Success)
            {
                return View(requestDto);
            }

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
            var response = await _authRepository.Login(requestDto.Name, requestDto.Password);

            if (!response.Success)
            {
                return View(requestDto);
            }

            HttpContext.Response.Cookies.Append("JWT", response.Data!);

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
