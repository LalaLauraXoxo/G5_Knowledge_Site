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

        public void AddUser(UserViewModel model)
        {
            var user = new User();
            if (!_repository.UserExists(model.UserId))
            {
                _mapper.Map(model, user);
                user.Password = PasswordManager.EncryptPassword(model.Password);
                user.CreatedTime = DateTime.Now;
                user.UpdatedTime = DateTime.Now;
                user.CreatedBy = System.Environment.UserName;
                user.UpdatedBy = System.Environment.UserName;

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

        public bool UpdateUser(UserViewModel model)
        {
            User user = _repository.GetUser(model.Id);
            if (user != null)
            {
                user.UserId = model.UserId;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Username = model.Username;
                //user.Password = model.Password;
                user.Password = PasswordManager.EncryptPassword(model.Password);

                user.UpdatedBy = "Username";
                user.CreatedTime = DateTime.Now;

                _repository.UpdateUser(user);
                return true;
            }
            return false;
        }
    }
}
