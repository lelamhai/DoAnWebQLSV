using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace DoAnWebQLSV.Controllers
{
    public class LoginController : Controller
    {
        private const string LoginApiUrl = "https://localhost:7141/api/v1/public/User/login";

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Vui lòng nhập username và mật khẩu.";
                return View();
            }

            using var client = new HttpClient();

            var requestBody = new
            {
                username,
                password
            };

            HttpResponseMessage response;
            string responseText = string.Empty;

            try
            {
                response = await client.PostAsJsonAsync(LoginApiUrl, requestBody);
                responseText = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

            bool isSuccess = response.IsSuccessStatusCode;
            string? message = null;
            string? accessToken = null;
            string? refreshToken = null;
            string? expiredToken = null;
            string? role = null;

            if (!string.IsNullOrWhiteSpace(responseText))
            {
                try
                {
                    using var doc = JsonDocument.Parse(responseText);
                    var root = doc.RootElement;

                    if (root.TryGetProperty("message", out var messageProp))
                    {
                        message = messageProp.GetString();
                    }

                    if (root.TryGetProperty("data", out var dataProp) && dataProp.ValueKind == JsonValueKind.Object)
                    {
                        if (dataProp.TryGetProperty("accessToken", out var accessTokenProp) && accessTokenProp.ValueKind == JsonValueKind.String)
                        {
                            accessToken = accessTokenProp.GetString();
                        }

                        if (dataProp.TryGetProperty("refreshToken", out var refreshTokenProp) && refreshTokenProp.ValueKind == JsonValueKind.String)
                        {
                            refreshToken = refreshTokenProp.GetString();
                        }

                        if (dataProp.TryGetProperty("expiredToken", out var expiredTokenProp) && expiredTokenProp.ValueKind == JsonValueKind.String)
                        {
                            expiredToken = expiredTokenProp.GetString();
                        }

                        if(dataProp.TryGetProperty("role", out var roleProp) && roleProp.ValueKind == JsonValueKind.String)
                        {
                            role = roleProp.GetString();
                        }
                    }

                    if (root.TryGetProperty("success", out var successProp) && successProp.ValueKind == JsonValueKind.True)
                    {
                        isSuccess = true;
                    }

                    if (!string.IsNullOrWhiteSpace(accessToken))
                    {
                        isSuccess = true;
                    }
                }
                catch
                {
                    // Không cần xử lý nếu API không trả về JSON.
                }
            }

            if (isSuccess)
            {
                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    HttpContext.Session.SetString("AccessToken", accessToken);
                }

                if (!string.IsNullOrWhiteSpace(refreshToken))
                {
                    HttpContext.Session.SetString("RefreshToken", refreshToken);
                }

                if (!string.IsNullOrWhiteSpace(expiredToken))
                {
                    HttpContext.Session.SetString("ExpiredToken", expiredToken);
                }

                if (!string.IsNullOrWhiteSpace(expiredToken))
                {
                    HttpContext.Session.SetString("ExpiredToken", expiredToken);
                }

                if (!string.IsNullOrWhiteSpace(role))
                {
                    HttpContext.Session.SetString("Role", role);
                }

                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("LoginMessage", message ?? "Đăng nhập thành công.");

                return RedirectToAction("index", "Student");
            }

            ViewBag.Error = message ?? "Đăng nhập thất bại. Vui lòng kiểm tra lại tài khoản hoặc mật khẩu.";
            return View();
        }
    }
}