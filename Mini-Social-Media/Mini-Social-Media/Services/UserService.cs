using System;
using System.Collections.Generic;
using System.Linq;
using Mini_Social_Media.Controllers;
using Mini_Social_Media.Models;

namespace Mini_Social_Media.Services
{
    public class UserService : IDataService<UserModel>
    {
        public List<UserModel> Records { get; set; }

        public UserService()
        {
            InitData();
        }

        public void InitData()
        {
            var user1 = new UserModel("admian", DateTime.Now);
            var user2 = new UserModel("Damian", DateTime.Now);
            var user3 = new UserModel("Camilo", DateTime.Now);
            var user4 = new UserModel("Maciej", DateTime.Now);
            var user5 = new UserModel("Krzychu", DateTime.Now);
            var user6 = new UserModel("Jasiu", DateTime.Now);

            user2.addFriend(user3);
            user2.addFriend(user6);

            user3.addFriend(user2);
            user3.addFriend(user5);
            user3.addFriend(user6);

            user4.addFriend(user3);

            user5.addFriend(user4);
            user5.addFriend(user2);

            user6.addFriend(user2);
            user6.addFriend(user5);

            Records = new List<UserModel>() {user1, user2, user3, user4, user5, user6};
        }

        public UserModel Find(string id)
        {
            return Records.FirstOrDefault(u => u.Login.ToLower().Equals(id.ToLower()));
        }
    }
}