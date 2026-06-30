using DoAnWebQLSV.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DoAnWebQLSV.Controllers
{
    public class OpenCourseController : Controller
    {
        private const string API_GETACCOUNTINFO = "https://localhost:7141/api/v1/private/Account/info-account?username=";
        public async Task<IActionResult> IndexAsync()
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
                var accountInfo = await GetAccountInfoAsync(client, username);
                ViewBag.AccountInfo = accountInfo?.Data;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi lấy thông tin tài khoản: {ex.Message}";
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        private async Task<AccountInfoViewModel?> GetAccountInfoAsync(HttpClient client, string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }


            var url = API_GETACCOUNTINFO + username;

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
