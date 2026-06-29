using Microsoft.AspNetCore.Mvc;

namespace DoAnWebQLSV.Controllers
{
    public class TeacherHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
