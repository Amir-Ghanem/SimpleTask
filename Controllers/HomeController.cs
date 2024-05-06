using Microsoft.AspNetCore.Mvc;
using SimpleTask.Models;
using SimpleTask.Models.ViewModels;
using SimpleTask.Repository.User;
using System.Diagnostics;

namespace SimpleTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View(new UserLoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            
            var user = await _userRepository.GetUserByEmailAsync(model.Email);
            if (user != null && user.Password == model.Password)
            {
                CurrentUser.Name = user.Name;
                CurrentUser.Id = user.ID;
                return RedirectToAction("Index", "Employees");
            }
            else
            {
               
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
                CurrentUser.Name = null;
                CurrentUser.Id = null;
                return RedirectToAction("Login", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
