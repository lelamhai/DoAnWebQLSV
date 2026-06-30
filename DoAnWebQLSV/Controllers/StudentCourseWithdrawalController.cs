using DoAnWebQLSV.Models.Account;
using DoAnWebQLSV.Models.Student;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DoAnWebQLSV.Controllers
{
    public class StudentCourseWithdrawalController : Controller
    {
        private const string API_GETACCOUNTINFO = "https://localhost:7141/api/v1/private/Account/info-account?username=";
        private const string API_GETRESIGTERSUBJECT = "https://localhost:7141/api/RegisterLTC/get-register-ltc";
        public async Task<IActionResult> Index(int page)
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
                var subjectData = await GetLTCAsync(client, username, page);
                ViewBag.AccountInfo = accountInfo?.Data;
                ViewBag.SubjectData = subjectData;
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

        private async Task<RegisterSubjectData> GetLTCAsync(HttpClient client, string?username, int page)
        {
            try
            {
                var url = $"{API_GETRESIGTERSUBJECT}?maSv={username}&page={page}";
                var response = await client.GetAsync(url);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API lớp lỗi {(int)response.StatusCode}: {responseText}");
                }

                var result = JsonSerializer.Deserialize<ListRegisterSubject>(
                    responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return result?.Data ?? new RegisterSubjectData();
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi gọi API sinh viên: {e.Message}");
            }
        }
    }
}
