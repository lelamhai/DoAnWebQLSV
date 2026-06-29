using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoAnWebQLSV.Controllers
{
    public class HomeStudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
