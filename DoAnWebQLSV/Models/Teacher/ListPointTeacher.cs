using DoAnWebQLSV.Models.Classroom;
using DoAnWebQLSV.Models.Student;
using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Teacher
{
    public class ListPointTeacher
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public PointTeacherData Data { get; set; } = new PointTeacherData();
    }

    public class PointTeacherData
    {
        [JsonPropertyName("items")]
        public List<PointTeacherItem> Items { get; set; } = new List<PointTeacherItem>();

        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; } = new Pagination();
    }

    public class PointTeacherItem
    {
        public long Stt { get; set; }

        public int MaLtc { get; set; }

        public string MaSv { get; set; } = string.Empty;

        public string MaMh { get; set; } = string.Empty;

        public string TenMh { get; set; } = string.Empty;

        public int SoTinChi { get; set; }

        public int HocKy { get; set; }

        public string NienKhoa { get; set; } = string.Empty;

        public decimal? DiemCc { get; set; }

        public decimal? DiemGk { get; set; }

        public decimal? DiemCk { get; set; }

        public decimal? DiemTong { get; set; }

        public string XepLoai { get; set; } = string.Empty;
    }
}
