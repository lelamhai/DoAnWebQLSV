using System.Diagnostics;
using DoAnWebQLSV.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAnWebQLSV.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}