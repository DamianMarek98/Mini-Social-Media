using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_Social_Media.Models;
using Mini_Social_Media.Services;

namespace Mini_Social_Media.Controllers
{
    public class LoginController : Controller
    {
        private readonly IDataService<UserModel> _dataService;

        public LoginController(IDataService<UserModel> dataService)
        {
            _dataService = dataService;
        }

        public IActionResult LoginUser()
        {
            return View("LoginUser");
        }

        [Route("/login/{login}")]
        public IActionResult Login(string login)
        {
            HttpContext.Session.SetString("login", login);
            
            return View("../Home/Index");
        }
    }
}