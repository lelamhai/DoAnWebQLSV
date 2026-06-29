using System.Diagnostics;
using DoAnWebQLSV.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAnWebQLSV.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
