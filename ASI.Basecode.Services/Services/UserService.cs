using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Manager;
using ASI.Basecode.Services.ServiceModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using static ASI.Basecode.Resources.Constants.Enums;

namespace ASI.Basecode.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public LoginResult AuthenticateUser(string userId, string password, ref User user)
        {
            user = new User();
            var passwordKey = PasswordManager.EncryptPassword(password);
            user = _repository.GetUsers().Where(x => x.UserId == userId &&
                                                     x.Password == passwordKey).FirstOrDefault();

            return user != null ? LoginResult.Success : LoginResult.Failed;
        }

        public void AddUser(UserViewModel model, string username)
        {
            var user = new User();
            if (!_repository.UserExists(model.UserId))
            {
                _mapper.Map(model, user);
                user.Password = PasswordManager.EncryptPassword(model.Password);
                user.CreatedTime = DateTime.Now;
                user.UpdatedTime = DateTime.Now;
                user.CreatedBy = username;
                user.UpdatedBy = username;

                _repository.AddUser(user);
            }
            else
            {
                throw new InvalidDataException(Resources.Messages.Errors.UserExists);
            }
        }

        public List<User> GetUsersDisplay()
        {
            var users = _repository.GetUsersDisplay();
            return users;
        }

        public User GetUser(int id)
        {
            var user = _repository.GetUser(id);
            return user;
        }

        public UserViewModel GetUserViewModel(User user, int id)
        {
            var model = new UserViewModel();

            model = new()
            {
                Id = id,
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
            };

            return model;
        }

        public UserViewModel GetEditUserViewModel(User user, int id)
        {
            var model = new UserViewModel();

            model = new()
            {
                Id = id,
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Password = PasswordManager.DecryptPassword(user.Password),
            };

            return model;
        }

        public bool UpdateUser(UserViewModel model, string username)
        {
            User user = _repository.GetUser(model.Id);

           
                user.UserId = model.UserId;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Username = model.Username;
                user.Password = PasswordManager.EncryptPassword(model.Password);

                user.UpdatedBy = username;
                user.CreatedTime = DateTime.Now;

                _repository.UpdateUser(user);
                return true;
        }

        public bool DeleteUser(UserViewModel model)
        {
            User user = _repository.GetUser(model.Id);
            if (user != null)
            {
                _repository.DeleteUser(user);
                return true;
            }
            return false;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _repository.GetUserByEmail(email);
            return user;
        }

        public async Task<bool> UpdateUserPasswordByEmail(string email, string newPassword)
        {

            var user = await _repository.GetUserByEmail(email);
            if (user != null)
            {
                user.Password = PasswordManager.EncryptPassword(newPassword);
                user.UpdatedBy = "admin"; 
                user.UpdatedTime = DateTime.Now;

                _repository.UpdateUser(user);
                return true;
            }
            return false;
        }


    }
}
