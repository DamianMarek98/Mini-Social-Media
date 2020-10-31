using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mini_Social_Media.Models;
using Mini_Social_Media.Services;

namespace Mini_Social_Media.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _dataService;
        
        public HomeController(ILogger<HomeController> logger, UserService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Init")]
        public IActionResult Init()
        {
            _dataService.InitData();
            return View("Index");
        }
        
        public IActionResult Status(int code) { 
            ViewBag.code = code; 
            ErrorViewModel errorViewModel = new ErrorViewModel();
            errorViewModel.RequestId = code.ToString();
            return View("Error", errorViewModel); 
        }
    }
}