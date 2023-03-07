using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VTask.Controllers.MVC
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }
    }
}
