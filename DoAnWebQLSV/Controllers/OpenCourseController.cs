using DoAnWebQLSV.Models.Account;
using DoAnWebQLSV.Models.Classroom;
using DoAnWebQLSV.Models.Employment;
using DoAnWebQLSV.Models.Student;
using DoAnWebQLSV.Models.Teacher;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DoAnWebQLSV.Controllers
{
    public class OpenCourseController : Controller
    {
        private const string API_GETACCOUNTINFO = "https://localhost:7141/api/v1/private/Account/info-account?username=";
        private const string API_SUBJECT = "https://localhost:7141/api/ManageLTC/get-ltc";
        private const string API_GETTEACHER = "https://localhost:7141/api/v1/private/Teacher/get-teachers";
        private const string API_GETSUBJECT = "https://localhost:7141/api/ManageLTC/get-subject";
        private const string API_CREATELTC = "https://localhost:7141/api/v1/private/Classroom/create";
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
                var ltcs = await GetLTCAsync(client, page);
                var teachers = await GetTeacherAsync(client, -1);
                var subjects = await GetSubjectAsync(client, -1);   
                ViewBag.AccountInfo = accountInfo?.Data;
                ViewBag.LTCs = ltcs;
                ViewBag.Teachers = teachers;
                ViewBag.Subjects = subjects;
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

        private async Task<LTCNVData> GetLTCAsync(HttpClient client, int page)
        {
            try
            {
                var url = API_SUBJECT + $"?page={page}";
                var response = await client.GetAsync(url);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API lớp lỗi {(int)response.StatusCode}: {responseText}");
                }

                var result = JsonSerializer.Deserialize<ListLTCNVModel>(
                    responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return result?.Data ?? new LTCNVData();
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi gọi API sinh viên: {e.Message}");
            }
        }

        private async Task<TeacherData> GetTeacherAsync(HttpClient client, int page)
        {
            try
            {
                var url = API_GETTEACHER + $"?page={page}";
                var response = await client.GetAsync(url);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API lớp lỗi {(int)response.StatusCode}: {responseText}");
                }

                var result = JsonSerializer.Deserialize<ListTeacherModel>(
                    responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return result?.Data ?? new TeacherData();
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi gọi API sinh viên: {e.Message}");
            }
        }

        private async Task<SubjectData> GetSubjectAsync(HttpClient client, int page)
        {
            try
            {
                var response = await client.GetAsync(API_GETSUBJECT);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API lớp lỗi {(int)response.StatusCode}: {responseText}");
                }

                var result = JsonSerializer.Deserialize<ListSubjectModel>(
                    responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return result?.Data ?? new SubjectData();
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi gọi API sinh viên: {e.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLTC model)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                TempData["Error"] = "Vui lòng đăng nhập để tạo sinh viên.";
                return RedirectToAction("Index", "Login");
            }

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var payload = new
                {
                    nienKhoa = model.NienKhoa.Trim(),
                    hocKy = model.HocKy,
                    maMH = model.MaMH.Trim(),
                    maGV = model.MaGV.Trim(),
                    siSoToiDa = model.SiSoToiDa,
                    dayThuTrongTuan = model.DayThuTrongTuan.Trim(),
                    thoiGianBatDau = model.ThoiGianBatDau,
                    thoiGianKetThuc = model.ThoiGianKetThuc,
                    huyLop = model.HuyLop
                };

                var json = JsonSerializer.Serialize(payload);

                var content = new StringContent(
                    json,
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync(API_CREATELTC, content);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] =
                        $"API lỗi {(int)response.StatusCode} - {response.StatusCode}: {responseText}";

                    return RedirectToAction("Index");
                }

                TempData["Success"] = responseText;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra khi tạo sinh viên: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
