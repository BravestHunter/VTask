using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VTask.Exceptions;
using VTask.Model.DTO.Auth;
using VTask.Model.MVC;
using VTask.Services;

namespace VTask.Controllers.MVC
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IEmailSenderService _emailSenderService;

        public AuthController(IAuthService authService, IMapper mapper, IEmailSenderService emailSenderService)
        {
            _authService = authService;
            _mapper = mapper;
            _emailSenderService = emailSenderService;
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

            try
            {
                var loginRequest = _mapper.Map<LoginRequestDto>(model);
                await Login(loginRequest);
            }
            catch (DbEntryNotFoundException)
            {
                ModelState.AddModelError("All", "Wrong username or password");
            }
            catch (PasswordNotValidException)
            {
                ModelState.AddModelError("All", "Wrong username or password");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

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

            try
            {
                var registerRequest = _mapper.Map<RegisterRequestDto>(model);
                var registerResponse = await _authService.Register(registerRequest);

                var loginRequest = _mapper.Map<LoginRequestDto>(registerRequest);
                await Login(loginRequest);
            }
            catch (DbEntryAlreadyExistsException)
            {
                ModelState.AddModelError(nameof(model.Username), "User with such login already exists");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult PasswordResetMessageSend()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordResetMessageSend(PasswordResetSendMessageModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var request = _mapper.Map<PasswordResetGetTokenRequestDto>(model);
                var response = await _authService.GetPasswordResetToken(request);

                var passwordResetLink = Url.Action("PasswordReset", "Auth", new { response.Token }, Request.Scheme);

                await _emailSenderService.SendEmailAsync(response.Email, "VTask: Password reset link", passwordResetLink!);
            }
            catch
            {
                // Do nothing
            }

            return RedirectToAction("PasswordResetMessageWasSend");
        }

        [HttpGet]
        public IActionResult PasswordResetMessageWasSend()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PasswordReset(string token)
        {
            PasswordResetModel model = new()
            {
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PasswordReset(PasswordResetModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var request = _mapper.Map<PasswordResetRequestDto>(model);
                var response = await _authService.ResetPassword(request);
            }
            catch (TokenExpiredException)
            {
                ModelState.AddModelError("All", "Token expired");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Notify about operation success?

            return RedirectToAction("Login");
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
