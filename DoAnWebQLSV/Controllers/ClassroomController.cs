using DoAnWebQLSV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DoAnWebQLSV.Controllers
{
    public class ClassroomController : Controller
    {
        private const string API_GETACCOUNTINFO = "https://localhost:7141/api/v1/private/Account/info-account";
        private const string API_GETCLASSROOMS = "https://localhost:7141/api/v1/private/Classroom/get-classrooms";


        public async Task<IActionResult> Index()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                TempData["LoginRequired"] = "Vui lòng đăng nhập để xem dữ liệu.";
                return RedirectToAction("Index", "Login");
            }

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var classrooms = await GetClassroomsAsync(client);
                var accountInfo = await GetAccountInfoAsync(client, username);

                ViewBag.Classrooms = classrooms;
                ViewBag.AccountInfo = accountInfo?.Data;
            }
            catch (Exception ex)
            {
                ViewBag.ApiError = ex.Message;
            }

            return View();
        }

        private async Task<LopData?> GetClassroomsAsync(HttpClient client)
        {
            var response = await client.GetAsync(API_GETCLASSROOMS);
            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API lớp lỗi {(int)response.StatusCode}: {responseText}");
            }

            var result = JsonSerializer.Deserialize<ClassroomViewModel>(
                responseText,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            return result?.Data;
        }

        private async Task<AccountInfoViewModel?> GetAccountInfoAsync(HttpClient client, string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            var url = QueryHelpers.AddQueryString(
                API_GETACCOUNTINFO,
                "username",
                username
            );

            var response = await client.GetAsync(url);
            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API tài khoản lỗi {(int)response.StatusCode}: {responseText}");
            }

            if (string.IsNullOrWhiteSpace(responseText))
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<AccountInfoViewModel>(
                responseText,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            return result;
        }
    }
}