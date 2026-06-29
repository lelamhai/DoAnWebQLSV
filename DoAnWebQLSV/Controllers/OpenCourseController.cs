using Microsoft.AspNetCore.Mvc;

namespace DoAnWebQLSV.Controllers
{
    public class OpenCourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
