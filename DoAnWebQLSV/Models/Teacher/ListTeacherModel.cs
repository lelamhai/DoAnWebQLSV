using DoAnWebQLSV.Models.Classroom;
using DoAnWebQLSV.Models.Student;
using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Teacher
{
    public class ListTeacherModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public TeacherData Data { get; set; } = new TeacherData();
    }

    public class TeacherData
    {
        [JsonPropertyName("items")]
        public List<TeacherItem> Items { get; set; } = new List<TeacherItem>();

        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; } = new Pagination();
    }


    public class TeacherItem
    {
        public string MaKhoa { get; set; } = string.Empty;
        public string TenKhoa { get; set; } = string.Empty;
        public string MaGV { get; set; } = string.Empty;
        public string Ho { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public string? Phai { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? NgaySinh { get; set; }
        public string? Email { get; set; }
        public string? HocVi { get; set; }
        public string? HocHam { get; set; }
        public string? ChuyenMon { get; set; }
        public int Id { get; set; }
        public string TenTrangThai { get; set; } = string.Empty;
    }
}
