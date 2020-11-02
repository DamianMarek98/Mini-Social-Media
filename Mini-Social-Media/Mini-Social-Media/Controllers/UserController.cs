using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_Social_Media.Models;
using Mini_Social_Media.Services;
using Newtonsoft.Json;

namespace Mini_Social_Media.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;

        public UserController(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public IActionResult Add()
        {
            if (_authService.isAdminLogged())
            {
                var userAddModel = new UserAddModel();
                return View("AddUser");
            }
            else
            {
                return View("../Home/Index");
            }
        }

        [HttpPost]
        public IActionResult AddUser(UserAddModel userAddModel)
        {
            if (_authService.isAdminLogged())
            {
                if (ModelState.IsValid)
                {
                    var userModel = new UserModel(userAddModel.Login, DateTime.Now);
                    if (_userService.CheckIfIdIsNotTaken(userAddModel.Login))
                    {
                        _userService.Add(userModel);
                    }
                }

                return View("UserList", _userService.Records);
            }
            else
            {
                return View("../Home/Index");
            }
        }

        public IActionResult List()
        {
            if (_authService.isAdminLogged())
            {
                return View("UserList", _userService.Records);
            }
            else
            {
                return View("../Home/Index");
            }
        }

        [Route("/user/del/{login}")]
        public IActionResult Del(string login)
        {
            if (_authService.isAdminLogged())
            {
                _userService.Remove(_userService.Find(login));
                return View("UserList", _userService.Records);
            }
            else
            {
                return View("../Home/Index");
            }
        }

        public IActionResult Init()
        {
            _userService.InitData();
            return View("UserList", _userService.Records);
        }
        
        [Route("/friends")]
        public IActionResult Friends()
        {
            if (_authService.isUserLogged())
            {
                return View("UserFriends", _userService.Find(_authService.getUserLoggedLogin()).Friends);
            }
            else
            {
                return View("../Home/Index");
            }
        }
        
        [Route("/friends/add/{login}")]
        public IActionResult AddFriend(string login)
        {
            if (_authService.isUserLogged() && _userService.Find(login) != null)
            {
                var loggedUserLogin = _authService.getUserLoggedLogin();
                if (!loggedUserLogin.ToLower().Equals(login.ToLower()))
                {
                    _userService.Find(loggedUserLogin).addFriend(_userService.Find(login));
                    return Json(true);
                } 
            }

            return Json(false);
        }
        
        public IActionResult AddFriendForm(string login)
        {
            return Redirect("/friends/add/" + login);
        }
        
        public IActionResult DelUserFromRow(string login)
        {
            return Redirect("/friends/del/" + login);
        }
        
        [Route("/friends/del/{login}")]
        public IActionResult RemoveFriend(string login)
        {
            if (_authService.isUserLogged() && _userService.Find(login) != null)
            {
                var loggedUserLogin = _authService.getUserLoggedLogin();
                if (!loggedUserLogin.ToLower().Equals(login.ToLower()))
                {
                    return Json(_userService.removeFriend(loggedUserLogin, login));
                } 
            }

            return Json(false);
        }
        
        [Route("/friends/List")]
        public IActionResult FriendsList()
        {
            if (_authService.isUserLogged())
            {
                UserModel user = _userService.Find(_authService.getUserLoggedLogin());
                return Json(JsonConvert.SerializeObject(user.Friends));
            }

            return View("../Home/Index");
        }

        [Route("/friends/export")]
        public IActionResult friendsExport()
        {
            if (_authService.isUserLogged())
            {
                _userService.writeFriendsToFile(_authService.getUserLoggedLogin());
                byte[] fileBytes = System.IO.File.ReadAllBytes(
                    @"C:\Users\zmddd\Desktop\Studia\Semestr VII\RAI\Mini-Social-Media\Mini-Social-Media\Mini-Social-Media\Resources\Friends.txt");
                string fileName = _authService.getUserLoggedLogin() + "_Friends.txt";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }

            return View("../Home/Index");
        }
        
        [Route("/friends/import")]
        public IActionResult friendsImport()
        {
            if (_authService.isUserLogged())
            {
                _userService.loadFriendsFromFile(_authService.getUserLoggedLogin());
                return Redirect("/friends");
            }

            return View("../Home/Index");
        }
        
    }
}