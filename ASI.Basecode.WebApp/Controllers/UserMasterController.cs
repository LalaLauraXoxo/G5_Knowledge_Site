using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using Microsoft.AspNetCore.Mvc;

namespace ASI.Basecode.WebApp.Controllers
{
    public class UserMasterController : Controller
    {
        private readonly IUserService _UserService;

        //constructor to call service

        public UserMasterController(IUserService userService)
        {
            _UserService = userService;
        }
        public IActionResult UserMaster()
        {
            var users = _UserService.GetUserss();
            return View(users);
        }
        public IActionResult Create()
        {
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
                    Name = user.Name,
                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult ViewUser(UserViewModel model)
        {
            bool isUpdated = _UserService.UpdateUser(model);
            if (isUpdated)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int id)
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
                    Name = user.Name,
                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(UserViewModel model) 
        {
            bool isUpdated = _UserService.UpdateUser(model);
            if (isUpdated)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        public IActionResult Delete(UserViewModel model) 
        {
            bool isDeleted = _UserService.DeleteUser(model);
            if(!isDeleted)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

    }
}