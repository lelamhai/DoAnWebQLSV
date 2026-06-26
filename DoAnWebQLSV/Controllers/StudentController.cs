using DoAnWebQLSV.Models.Classroom;
using DoAnWebQLSV.Models.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DoAnWebQLSV.Controllers
{
    public class StudentController : Controller
    {
        private const string API_GETSTUDENT = "https://localhost:7141/api/v1/private/Student/get-students";
        private const string API_GETCLASSROOMS = "https://localhost:7141/api/v1/private/Student/get-classrooms";
        private const string API_GETSTATUS = "https://localhost:7141/api/v1/private/Student/get-status-student";
        private const string API_DELETESTUDENT = "https://localhost:7141/api/v1/private/Student/delete/";
        private const string API_DETAILSTUDENT = "https://localhost:7141/api/v1/private/Student/detail/";
        private const string API_UPDATESTUDENT = "https://localhost:7141/api/v1/private/Student/update/";
        private const string API_CREATESTUDENT = "https://localhost:7141/api/v1/private/Student/create";

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
                var student = await GetStudentsAsync(client, page);
                var classrooms = await GetClassroomsAsync(client);
                var status = await GetStatusAsync(client);
                ViewBag.Student = student;
                ViewBag.Classrooms = classrooms?.Data ?? new List<ItemClassroom>();
                ViewBag.Status = status?.Data ?? new List<StatusItem>();
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        private async Task<StudentData> GetStudentsAsync(HttpClient client, int page)
        {
            try
            {
                var url = API_GETSTUDENT + $"?page={page}";
                var response = await client.GetAsync(url);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API lớp lỗi {(int)response.StatusCode}: {responseText}");
                }

                var result = JsonSerializer.Deserialize<StudentModel>(
                    responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );
                
                return result?.Data ?? new StudentData();
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi gọi API sinh viên: {e.Message}");
            }
        }

        private async Task<StudentClassroomModel?> GetClassroomsAsync(HttpClient client)
        {
            try
            {
                var response = await client.GetAsync(API_GETCLASSROOMS);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API lớp lỗi {(int)response.StatusCode}: {responseText}");
                }

                var result = JsonSerializer.Deserialize<StudentClassroomModel>(
                    responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return result;
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi gọi API lớp: {e.Message}");
            }
        }

        private async Task<StatusTableModel?> GetStatusAsync(HttpClient client)
        {
            try
            {
                var response = await client.GetAsync(API_GETSTATUS);
                var responseText = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API trạng thái lớp lỗi {(int)response.StatusCode}: {responseText}");
                }
                var result = JsonSerializer.Deserialize<StatusTableModel>(
                    responseText,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );
                return result;
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi gọi API lớp: {e.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string masv)
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
                var url = API_DELETESTUDENT + masv;
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
        public async Task<IActionResult> Detail(string masv)
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
                var url = API_DETAILSTUDENT + masv;

                var response = await client.GetAsync(url);
                var responseText = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest(new
                    {
                        message = "Lấy chi tiết sinh viên thất bại.",
                        error = responseText
                    });
                }
                var result = JsonSerializer.Deserialize<DetailStudentModel>(
                responseText,new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result == null)
                {
                    return NotFound(new
                    {
                        message = "Không tìm thấy lớp."
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                ViewBag.ApiError = ex.Message;
            }

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateStudent model)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                TempData["Error"] = "Vui lòng đăng nhập để cập nhật dữ liệu.";
                return RedirectToAction("Index", "Login");
            }

            if (string.IsNullOrWhiteSpace(model.Masv))
            {
                TempData["Error"] = "Mã lớp không hợp lệ.";
                return RedirectToAction("Index");
            }

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var url = API_UPDATESTUDENT + model.Masv;

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
                    TempData["Error"] = "Cập nhật sinh viên thất bại: " + responseText;
                    return RedirectToAction("Index");
                }

                TempData["Success"] = "Cập nhật sinh viên thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra khi cập nhật sinh viên: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UpdateStudent model)
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
                    masv = model.Masv,
                    ho = model.Ho,
                    ten = model.Ten,
                    phai = model.Phai,
                    diachi = model.Diachi,
                    sodienthoai = model.Sodienthoai,
                    ngaysinh = model.Ngaysinh,
                    email = model.Email,
                    malop = model.Malop,
                    trangthai = model.Trangthai
                };

                var json = JsonSerializer.Serialize(payload);

                var content = new StringContent(
                    json,
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync(API_CREATESTUDENT, content);
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
