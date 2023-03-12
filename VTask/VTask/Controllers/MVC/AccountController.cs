using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Security.Claims;
using System.Threading.Tasks;
using VTask.Model.DTO.User;
using VTask.Model.MVC;
using VTask.Services;

namespace VTask.Controllers.MVC
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IEmailSenderService _emailSenderService;

        public AccountController(IUserService userService, IMapper mapper, IEmailSenderService emailSenderService)
        {
            _userService = userService;
            _mapper = mapper;
            _emailSenderService = emailSenderService;
        }

        [HttpGet]
        public async Task<IActionResult> Info()
        {
            var response = await GetAuthenticatedUser();
            var model = _mapper.Map<UserModel>(response);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var response = await GetAuthenticatedUser();
            var model = _mapper.Map<UserModel>(response);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = _mapper.Map<UserUpdateRequestDto>(model);
            var response = await _userService.Update(request);

            TempData[Constants.Notification.SuccessMessageTempBagKey] = "Account data was updated successfully";

            return RedirectToAction("Info");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeUsername()
        {
            var response = await GetAuthenticatedUser();
            var model = _mapper.Map<UsernameChangeModel>(response);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUsername(UsernameChangeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = _mapper.Map<UserChangeUsernameRequestDto>(model);
            var response = await _userService.ChangeUsername(request);

            TempData[Constants.Notification.SuccessMessageTempBagKey] = "Username was changed successfully";

            return RedirectToAction("Logout", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var response = await GetAuthenticatedUser();
            var model = _mapper.Map<PasswordChangeModel>(response);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordChangeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = _mapper.Map<UserChangePasswordRequestDto>(model);
            var response = await _userService.ChangePassword(request);

            TempData[Constants.Notification.SuccessMessageTempBagKey] = "Password was changed successfully";

            return RedirectToAction("Logout", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> VerifyEmailMessageSend()
        {
            var getUserResponse = await GetAuthenticatedUser();

            var request = _mapper.Map<UserGetEmailVerificationTokenRequestDto>(getUserResponse);
            var response = await _userService.GetEmailVerificationToken(request);

            var confirmationLink = Url.Action("VerifyEmail", "Account", new { response.Token }, Request.Scheme);

            await _emailSenderService.SendEmailAsync(getUserResponse.Email!, "VTask: Confirmation email link", confirmationLink!);

            var model = _mapper.Map<UserModel>(getUserResponse);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            UserVerifyEmailRequestDto request = new()
            {
                Token = token
            };

            await _userService.VerifyEmail(request);

            TempData[Constants.Notification.SuccessMessageTempBagKey] = "Email was verified successfully";

            return RedirectToAction("Index", "Home");
        }

        private async Task<UserGetResponseDto> GetAuthenticatedUser()
        {
            int userId = GetAuthenticatedUserId();
            UserGetRequestDto request = new()
            {
                Id = userId
            };

            return await _userService.Get(request);
        }

        private int GetAuthenticatedUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
    }
}
