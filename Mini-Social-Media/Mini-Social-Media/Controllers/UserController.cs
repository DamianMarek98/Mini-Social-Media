using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mini_Social_Media.Models;
using Mini_Social_Media.Services;

namespace Mini_Social_Media.Controllers
{
    public class UserController : Controller
    {
        private readonly IDataService<UserModel> _dataService;

        public UserController(IDataService<UserModel> dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Add()
        {
            var userAddModel = new UserAddModel();
            return View("AddUser");
        }

        [HttpPost]
        public IActionResult AddUser(UserAddModel userAddModel)
        {
            if (ModelState.IsValid)
            {
                var userModel = new UserModel(userAddModel.Login, DateTime.Now);
                if (_dataService.CheckIfIdIsNotTaken(userAddModel.Login))
                {
                    _dataService.Add(userModel);
                } 
            }

            return View("UserList", _dataService.Records);
        }

        public IActionResult List()
        {
            return View("UserList", _dataService.Records);
        }

        public IActionResult Del(string login)
        {
            _dataService.Remove(_dataService.Find(login));
            return View("UserList", _dataService.Records);
        }
        
        public IActionResult Init()
        {
            _dataService.InitData();
            return View("UserList", _dataService.Records);
        }
    }
}