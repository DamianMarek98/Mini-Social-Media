using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mini_Social_Media.Models
{
    public class UserModel
    {
        [Display(Name = "Login")]
        [StringLength(25, MinimumLength=4, ErrorMessage="len=3..25")]
        public string Login { get; set; }
        
        [Display(Name = "Creation time")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}")]
        public DateTime CreationTime { get; set; }
        public List<UserModel> Friends { get; set; }

        public UserModel(string login, DateTime creationTime)
        {
            Login = login;
            CreationTime = creationTime;
            Friends = new List<UserModel>();
        }

        public void addFriend(UserModel userModel)
        {
            Friends.Add(userModel);
        }
    }
}