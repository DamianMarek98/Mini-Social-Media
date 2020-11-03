using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mini_Social_Media.Controllers;
using Mini_Social_Media.Models;

namespace Mini_Social_Media.Services
{
    public class UserService
    {
        public List<UserModel> Records { get; set; }

        public UserService()
        {
            //Records = new List<UserModel>(); todo uncommnet and the end
            InitData();
        }

        public void InitData()
        {
            var user1 = new UserModel("admin", DateTime.Now);
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
        
        public void Add(UserModel record)
        {
            if (record != null)
            {
                Records.Add(record);
            }
        }

        public void Remove(UserModel record)
        {
            if (record != null)
            {
                Records.Remove(record);
            }
        }

        public UserModel Find(string id)
        {
            return Records.FirstOrDefault(u => u.Login.ToLower().Equals(id.ToLower()));
        }
        
        public bool CheckIfIdIsNotTaken(string id)
        {
            return Find(id) == null;
        }

        public bool CheckIfIdExists(string id)
        {
            return Find(id) != null;
        }

        public bool removeFriend(string userLogin, string friendLogin)
        {
            UserModel user = Find(userLogin);
            UserModel friend = Find(friendLogin);
            if (userLogin != null && friend != null)
            {
                user.Friends.Remove(friend);
                return true;
            }

            return false;
        }
        
        public void writeFriendsToFile(string login)
        {
            string[] lines = Find(login).Friends.Select(u => u.Login).ToArray();
            System.IO.File.WriteAllLines(Path.GetFullPath(@"Resources") + @"\Friends.txt", lines);
        }

        public void loadFriendsFromFile(string login)
        {
            UserModel user = Find(login);
            if (user != null)
            {
                foreach (string line in File.ReadLines(
                    Path.GetFullPath(@"Resources") + @"\NewFriends.txt"))
                {
                    UserModel friend = Find(line);
                    if (friend != null && !user.Friends.Contains(friend))
                    {
                        user.addFriend(friend);
                    }
                }
            }
        }
    }
}