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
using ASI.Basecode.Data.Models;

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
        public IActionResult UserMaster()
        {
            var users = _UserService.GetUsersDisplay();
            return View(users);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserViewModel model)
        {
            try
            {
                _UserService.AddUser(model, this.UserName);
                return RedirectToAction("UserMaster", "UserMaster");
            }
            catch (InvalidOperationException ex)
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
                return View(_UserService.GetUserViewModel(user, id));
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = _UserService.GetUser(id);
            if (user != null)
            {
                return View(_UserService.GetEditUserViewModel(user, id));
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditUser(UserViewModel model)
        {
            try
            {
                bool isUpdated = _UserService.UpdateUser(model, this.UserName);
                if (isUpdated)
                {
                    return RedirectToAction("UserMaster", "UserMaster");
                }
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = Resources.Messages.Errors.ServerError;
                return View(model);
            }
            return View(model);
        }
        public IActionResult Delete(UserViewModel model)
        {
            bool isDeleted = _UserService.DeleteUser(model);
            if (isDeleted)
            {
                return RedirectToAction("UserMaster");
            }
            return NotFound();
        }

    }
}
