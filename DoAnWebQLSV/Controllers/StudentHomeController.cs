using Microsoft.AspNetCore.Mvc;

namespace DoAnWebQLSV.Controllers
{
    public class StudentHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
