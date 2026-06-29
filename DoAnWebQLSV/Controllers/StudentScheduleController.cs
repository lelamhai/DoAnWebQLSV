using Microsoft.AspNetCore.Mvc;

namespace DoAnWebQLSV.Controllers
{
    public class StudentScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
