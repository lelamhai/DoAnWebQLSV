using System.Diagnostics;
using DoAnWebQLSV.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAnWebQLSV.Controllers
{
    public class ClassroomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}