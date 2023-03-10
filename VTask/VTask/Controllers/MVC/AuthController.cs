using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VTask.Model.DTO.User;
using VTask.Model.MVC;
using VTask.Services;

namespace VTask.Controllers.MVC
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginRequest = _mapper.Map<LoginRequestDto>(model);
            await Login(loginRequest);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("JWT");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registerRequest = _mapper.Map<RegisterRequestDto>(model);
            var registerResponse = await _authService.Register(registerRequest);

            var loginRequest = _mapper.Map<LoginRequestDto>(registerRequest);
            await Login(loginRequest);

            return RedirectToAction("Index", "Home");
        }

        private async Task Login(LoginRequestDto request)
        {
            var response = await _authService.Login(request);

            CookieOptions cookieOptions = new CookieOptions()
            {
                Expires = response.ExpirationDate
            };
            HttpContext.Response.Cookies.Append("JWT", response.Token, cookieOptions);
        }
    }
}
