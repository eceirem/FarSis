using Microsoft.AspNetCore.Mvc;

namespace FarSis.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}