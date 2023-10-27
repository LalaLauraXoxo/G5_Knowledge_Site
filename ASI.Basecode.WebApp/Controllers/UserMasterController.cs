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
    }
}