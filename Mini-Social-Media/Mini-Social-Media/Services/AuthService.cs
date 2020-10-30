using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mini_Social_Media.Services
{
    public class AuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool isAdminLogged()
        {
            return isLoginNotEmpty() && _httpContextAccessor.HttpContext.Session.GetString("login").Equals("admin");
        }

        public bool isUserLogged()
        {
            return isLoginNotEmpty() && !_httpContextAccessor.HttpContext.Session.GetString("login").Equals("admin");
        }

        public string getUserLoggedLogin()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("login");
        }

        private bool isLoginNotEmpty()
        {
            return !String.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("login"));
        }
    }
}