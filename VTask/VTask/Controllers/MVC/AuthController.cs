using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
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

        public AuthController(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
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

            var registerServiceResponse = await _authService.Register(requestDto.Name, requestDto.Password);
            if (!registerServiceResponse.Success)
            {
                return View(requestDto);
            }

            await _unitOfWork.SaveChanges();

            var loginServiceResponse = await _authService.Login(requestDto.Name, requestDto.Password);
            if (!loginServiceResponse.Success)
            {
                return View(requestDto);
            }

            HttpContext.Response.Cookies.Append("JWT", loginServiceResponse.Data!);

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

            var serviceResponse = await _authService.Login(requestDto.Name, requestDto.Password);
            if (!serviceResponse.Success)
            {
                return View(requestDto);
            }

            HttpContext.Response.Cookies.Append("JWT", serviceResponse.Data!);

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
