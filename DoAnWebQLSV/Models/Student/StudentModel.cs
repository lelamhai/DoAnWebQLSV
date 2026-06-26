using DoAnWebQLSV.Models.Classroom;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Student
{
    public class StudentModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public StudentData Data { get; set; } = new StudentData();
    }

    public class StudentData
    {
        [JsonPropertyName("items")]
        public List<StudentItem> Items { get; set; } = new List<StudentItem>();

        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; } = new Pagination();
    }

    public class StudentItem
    {
        [JsonPropertyName("tenlop")]
        public string TenLop { get; set; }

        [JsonPropertyName("masv")]
        public string MaSV { get; set; } = string.Empty;

        [JsonPropertyName("ho")]
        public string Ho { get; set; } = string.Empty;

        [JsonPropertyName("ten")]
        public string Ten { get; set; } = string.Empty;

        [JsonPropertyName("phai")]
        public string Phai { get; set; }

        [JsonPropertyName("diachi")]
        public string DiaChi { get; set; }

        [JsonPropertyName("sodienthoai")]
        public string SoDienThoai { get; set; }

        [JsonPropertyName("ngaysinh")]
        public string NgaySinh { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("trangthai")]
        public string TenTrangThai { get; set; }
    }
}
