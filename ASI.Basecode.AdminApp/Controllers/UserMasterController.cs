using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using ASI.Basecode.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using ASI.Basecode.Services.Manager;
using ASI.Basecode.AdminApp.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ASI.Basecode.AdminApp.Controllers
{
    public class UserMasterController : ControllerBase<UserMasterController>
    {
        private readonly IUserService _UserService;

        public UserMasterController(IUserService userService,
                                    IHttpContextAccessor httpContextAccessor,
                                    ILoggerFactory loggerFactory,
                                    IConfiguration configuration,
                                    IMapper mapper = null) : base(httpContextAccessor, loggerFactory, configuration, mapper
                                   )
        {
            _UserService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult UserMaster()
        {
            var users = _UserService.GetUsersDisplay();
            return View(users);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateUser(UserViewModel model)
        {
            try
            {
                _UserService.AddUser(model, this.UserName);
                return RedirectToAction("UserMaster", "UserMaster");
            }
            catch (InvalidDataException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = Resources.Messages.Errors.ServerError;
            }
            return View();
        }

        [HttpGet]
        public IActionResult ViewUser(int id)
        {
            var user = _UserService.GetUser(id);
            if (user != null)
            {
                UserViewModel model = new()
                {
                    Id = id,
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.Username,
                };
                return View(model);
            }
            return NotFound();
        }

        //
        public IActionResult Delete(UserViewModel model)
        {
            bool isDeleted = _UserService.DeleteUser(model);
            if (isDeleted)
            {
                return RedirectToAction("UserMaster");
            }
            return NotFound();
        }

        //

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = _UserService.GetUser(id);
            if (user != null)
            {
                UserViewModel model = new()
                {
                    Id = id,
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = this.UserName,
                    Password = PasswordManager.DecryptPassword(user.Password),

                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult EditUser(UserViewModel model)
        {
            bool isUpdated = _UserService.UpdateUser(model, this.UserName);
            if (isUpdated)
            {
                return RedirectToAction("UserMaster", "UserMaster");
            }
            return NotFound();
        }

    }
}
