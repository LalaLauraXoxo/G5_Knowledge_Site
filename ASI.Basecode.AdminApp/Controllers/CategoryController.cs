using Microsoft.AspNetCore.Mvc;

namespace ASI.Basecode.AdminApp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult TrainingCategories()
        {
            return View();
        }
    }
}
