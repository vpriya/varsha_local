using Microsoft.AspNetCore.Mvc;

namespace vpWebApplication.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
