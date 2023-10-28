﻿using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
        bool UserExists(string userId);
        void AddUser(User user);

        public List<User> GetUserss();

        public User GetUser(int id);

        public void UpdateUser(User user);
        
        public void DeleteUser(User user);
    }
}
