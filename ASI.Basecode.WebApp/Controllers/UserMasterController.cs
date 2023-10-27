using ASI.Basecode.WebApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASI.Basecode.WebApp.Controllers
{
    public interface IGetAllUsersService
    {
        public ValueTask<IEnumerable<UserMasterModel>> GetAllUsersAsync();
    }
    public class UserMasterController : Controller
    {
        [Inject]
        public IGetAllUsersService GetAllUsersService { get; } // public property with implicit getter

        public async Task<IActionResult> UserMaster()
        {
            var users = await GetAllUsersService.GetAllUsersAsync();

            return View(users);
        }
    }
}
