﻿using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {

        }

        public IQueryable<User> GetUsers()
        {
            return this.GetDbSet<User>();
        }

        public bool UserExists(string userId)
        {
            return this.GetDbSet<User>().Any(x => x.UserId == userId);
        }

        public bool EmailExists(string email)
        {
            return this.GetDbSet<User>().Any(x => x.Email == email);
        }

        public void AddUser(User user)
        {
            this.GetDbSet<User>().Add(user);
            UnitOfWork.SaveChanges();
        }

        public List<User> GetUsersDisplay()
        {
            var users = GetDbSet<User>().ToList();
            return users;
        }
        public User GetUser(int id)
        {
            var user = this.GetDbSet<User>().FirstOrDefault(x => x.Id == id);
            return user;
        }

        public void DeleteUser(User user)
        {
            this.GetDbSet<User>().Remove(user);
            UnitOfWork.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            this.GetDbSet<User>().Update(user);
            UnitOfWork.SaveChanges();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await this.GetDbSet<User>().FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
