using DoAnWebQLSV.Models.Account;
using DoAnWebQLSV.Models.Student;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DoAnWebQLSV.Controllers
{
    public class StudentCourseRegistrationController : Controller
    {
        private const string API_GETACCOUNTINFO = "https://localhost:7141/api/v1/private/Account/info-account?username=";
        private const string API_GETLTC = "https://localhost:7141/api/Employment/get-ltc?username=";
        public async Task<IActionResult> Index(int page = 1)
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
                var ltcData = await GetLTCAsync(client, 1);
                ViewBag.AccountInfo = accountInfo?.Data;
                ViewBag.LTCData = ltcData;
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

        private async Task<LTCData> GetLTCAsync(HttpClient client, int page)
        {
            try
            {
                var url = API_GETLTC + $"?page={page}";
                var response = await client.GetAsync(url);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API lớp lỗi {(int)response.StatusCode}: {responseText}");
                }

                var result = JsonSerializer.Deserialize<ListLTCModel>(
                    responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return result?.Data ?? new LTCData();
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi gọi API sinh viên: {e.Message}");
            }
        }

    }
}
