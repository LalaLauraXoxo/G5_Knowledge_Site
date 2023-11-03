using Microsoft.AspNetCore.Mvc;

namespace ASI.Basecode.KnowledgeApp.Controllers
{
    public class TrainingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RateTraining()
        {
            return View();
        }
    }
}
