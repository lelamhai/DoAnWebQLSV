using DoAnWebQLSV.Models.Classroom;
using DoAnWebQLSV.Models.Teacher;
using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Student
{
    public class ListPointStudent
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public PointStudentData Data { get; set; } = new PointStudentData();
    }

    public class PointStudentData
    {
        [JsonPropertyName("items")]
        public List<PointStudentItem> Items { get; set; } = new List<PointStudentItem>();

        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; } = new Pagination();
    }

    public class PointStudentItem
    {
        public string? MaMH { get; set; }

        public string? TenMH { get; set; }

        public int SoTinChi { get; set; }

        public string? NienKhoa { get; set; }

        public int HocKy { get; set; }

        public decimal? DiemCC { get; set; }

        public decimal? DiemGK { get; set; }

        public decimal? DiemCK { get; set; }

        public decimal? DiemTong { get; set; }
    }
}
