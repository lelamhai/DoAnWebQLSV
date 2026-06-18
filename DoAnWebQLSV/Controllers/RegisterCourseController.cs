using System.Diagnostics;
using DoAnWebQLSV.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAnWebQLSV.Controllers
{
    public class RegisterCourseController : Controller
    {
        public IActionResult index()
        {
            return View();
        }
    }
}