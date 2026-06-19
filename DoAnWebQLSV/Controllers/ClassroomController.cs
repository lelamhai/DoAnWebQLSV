using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using DoAnWebQLSV.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAnWebQLSV.Controllers
{
    public class ClassroomController : Controller
    {
        private const string ClassroomApiUrl = "https://localhost:7141/api/v1/private/Classroom/get-classrooms";

        public async Task<IActionResult> Index()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                TempData["LoginRequired"] = "Vui lòng đăng nhập để xem dữ liệu.";
                return RedirectToAction("Index", "Login");
            }

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            LopData? model = null;

            try
            {
                var response = await client.GetAsync(ClassroomApiUrl);
                var responseText = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(responseText))
                {
                    try
                    {
                        using var doc = JsonDocument.Parse(responseText);
                        var root = doc.RootElement;
                        var result = JsonSerializer.Deserialize<ClassroomViewModel>(root);
                        model = result.Data;
                    }
                    catch
                    {
                        ViewBag.ApiError = $"API returned {(int)response.StatusCode} {response.ReasonPhrase}: {responseText}";
                    }
                }
                else
                {
                    ViewBag.ApiError = $"API returned {(int)response.StatusCode} {response.ReasonPhrase}: {responseText}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ApiError = ex.Message;
            }

            return View(model);
        }
    }
}