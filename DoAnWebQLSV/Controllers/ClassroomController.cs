using DoAnWebQLSV.Models.Account;
using DoAnWebQLSV.Models.Classroom;
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
                var statusClassroom = await GetStatusClassroomAsync(client);
                var faculty = await GetFacultyAsync(client);

                ViewBag.Classrooms = classrooms;
                ViewBag.AccountInfo = accountInfo?.Data;
                ViewBag.StatusClassroom = statusClassroom.Data;
                ViewBag.Faculty = faculty.Data;
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

            var result = JsonSerializer.Deserialize<ClassroomModel>(
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

        private async Task<StatusClassroomModel> GetStatusClassroomAsync(HttpClient client)
        {
            var response = await client.GetAsync("https://localhost:7141/api/v1/private/Classroom/get-status-classrooms");
            var responseText = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API trạng thái lớp lỗi {(int)response.StatusCode}: {responseText}");
            }
            var result = JsonSerializer.Deserialize<StatusClassroomModel>(
                responseText,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
            return result;
        }

        private async Task<FacultyModel> GetFacultyAsync(HttpClient client)
        {
            var response = await client.GetAsync("https://localhost:7141/api/v1/private/Classroom/get-faculty");
            var responseText = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API trạng thái lớp lỗi {(int)response.StatusCode}: {responseText}");
            }
            var result = JsonSerializer.Deserialize<FacultyModel>(
                responseText,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword)
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
                var url = QueryHelpers.AddQueryString(
                    "https://localhost:7141/api/v1/private/Classroom/search",
                    "keyword",
                    keyword ?? ""
                );

                var response = await client.GetAsync(url);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API tìm kiếm lớp lỗi {(int)response.StatusCode}: {responseText}");
                }

                var result = JsonSerializer.Deserialize<ClassroomModel>(
                    responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                var accountInfo = await GetAccountInfoAsync(client, username);
                var statusClassroom = await GetStatusClassroomAsync(client);
                var faculty = await GetFacultyAsync(client);

                ViewBag.Classrooms = result?.Data;
                ViewBag.AccountInfo = accountInfo?.Data;
                ViewBag.StatusClassroom = statusClassroom.Data;
                ViewBag.Faculty = faculty.Data;
                ViewBag.Keyword = keyword;
            }
            catch (Exception ex)
            {
                ViewBag.ApiError = ex.Message;
            }

            return View("Index");
        }

        [HttpDelete]
        public async Task<IActionResult>Delete(string maLop)
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
                var url = $"https://localhost:7141/api/v1/private/Classroom/delete/{Uri.EscapeDataString(maLop)}";

                var response = await client.DeleteAsync(url);
                var responseText = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<DeleteModel>(
                        responseText,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                    );

                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                ViewBag.ApiError = ex.Message;
            }

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string maLop)
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
                var url = $"https://localhost:7141/api/v1/private/Classroom/detail/{Uri.EscapeDataString(maLop)}";

                var response = await client.GetAsync(url);
                var responseText = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest(new
                    {
                        message = "Lấy chi tiết lớp thất bại.",
                        error = responseText
                    });
                }
                var result = JsonSerializer.Deserialize<DetailModel>(
                       responseText,
                       new JsonSerializerOptions
                       {
                           PropertyNameCaseInsensitive = true
                       }
                   );
                var lop = result?.Data;

                if (lop == null)
                {
                    return NotFound(new
                    {
                        message = "Không tìm thấy lớp."
                    });
                }

                return Ok(lop);
            }
            catch (Exception ex)
            {
                ViewBag.ApiError = ex.Message;
            }

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateModel model)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                TempData["Error"] = "Vui lòng đăng nhập để cập nhật dữ liệu.";
                return RedirectToAction("Index", "Login");
            }

            if (string.IsNullOrWhiteSpace(model.MaLop))
            {
                TempData["Error"] = "Mã lớp không hợp lệ.";
                return RedirectToAction("Index");
            }

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var url = $"https://localhost:7141/api/v1/private/Classroom/update/{Uri.EscapeDataString(model.MaLop)}";

                var json = JsonSerializer.Serialize(model, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var content = new StringContent(
                    json,
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PutAsync(url, content);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Cập nhật lớp thất bại: " + responseText;
                    return RedirectToAction("Index");
                }

                TempData["Success"] = "Cập nhật lớp thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra khi cập nhật lớp: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}