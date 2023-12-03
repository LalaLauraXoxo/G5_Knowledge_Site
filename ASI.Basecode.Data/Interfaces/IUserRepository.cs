using ASI.Basecode.Data.Models;
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

        List<User> GetUsersDisplay();

        User GetUser(int id);

        void DeleteUser(User user);

        void UpdateUser(User user);
        Task<User> GetUserByEmail(string email);
    }
}
