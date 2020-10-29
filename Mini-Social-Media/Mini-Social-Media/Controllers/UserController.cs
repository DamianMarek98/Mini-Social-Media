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

        public IActionResult UserAdd()
        {
            var userAddModel = new UserAddModel();
            return View("User/AddUser");
        }

        [HttpPost]
        public IActionResult Add(UserAddModel userAddModel)
        {
            if (ModelState.IsValid)
            {
                var userModel = new UserModel(userAddModel.Login, DateTime.Now);
                if (_dataService.CheckIfIdIsNotTaken(userAddModel.Login))
                {
                    _dataService.Add(userModel);
                } //todo komunikat
            }

            return View("User/UserList", _dataService.Records);
        }

        public IActionResult List()
        {
            return View("User/UserList", _dataService.Records);
        }

        public IActionResult Delete(string login)
        {
            _dataService.Remove(_dataService.Find(login));
            return View("User/UserList", _dataService.Records);
        }
    }
}