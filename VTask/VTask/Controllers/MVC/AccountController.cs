using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using VTask.Model.MVC;
using VTask.Repositories;

namespace VTask.Controllers.MVC
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Info()
        {
            var strUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            int userId = int.Parse(strUserId);

            var user = await _userRepository.Get(userId);
            var model = _mapper.Map<UserModel>(user);

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit() 
        {
            return View();
        }
    }
}
