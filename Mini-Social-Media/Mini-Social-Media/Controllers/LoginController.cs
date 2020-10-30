using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_Social_Media.Models;
using Mini_Social_Media.Services;

namespace Mini_Social_Media.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _dataService;
        private readonly AuthService _authService;

        public LoginController(UserService dataService, AuthService authService)
        {
            _dataService = dataService;
            _authService = authService;
        }

        public IActionResult LoginUser()
        {
            return View("LoginUser");
        }

        [Route("/login/{login}")]
        public IActionResult Login(string login)
        {
            if (_dataService.CheckIfIdExists(login))
            {
                HttpContext.Session.SetString("login", login);

                if (_authService.isAdminLogged())
                {
                    return Redirect("/user/list");
                } else if (_authService.isUserLogged())
                {
                    return Redirect("/friends");
                }
            }
            
            return View("../Home/Index");
        }
        
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("login");
            
            return View("../Home/Index");
        }
    }
}