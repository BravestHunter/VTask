using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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

        private async Task<UserGetResponseDto> GetAuthenticatedUser()
        {
            var strUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            UserGetRequestDto request = new()
            {
                Id = int.Parse(strUserId)
            };

            return await _userService.Get(request);
        }
    }
}
