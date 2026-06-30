using DoAnWebQLSV.Models.Classroom;
using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Student
{
    public class ListRegisterSubject
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public RegisterSubjectData Data { get; set; } = new RegisterSubjectData();
    }

    public class RegisterSubjectData
    {
        [JsonPropertyName("items")]
        public List<RegisterSubjectItem> Items { get; set; } = new List<RegisterSubjectItem>();

        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; } = new Pagination();
    }

    public class RegisterSubjectItem
    {
        public int MaLtc { get; set; }

        public string MaMh { get; set; } = string.Empty;

        public string TenMh { get; set; } = string.Empty;

        public int SoTinChi { get; set; }

        public string? MaGv { get; set; }

        public string? TenGiangVien { get; set; }

        public string NienKhoa { get; set; } = string.Empty;

        public int HocKy { get; set; }

        public string DayThuTrongTuan { get; set; } = string.Empty;

        public string LichHoc { get; set; } = string.Empty;

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }

        public string ThoiGianHoc { get; set; } = string.Empty;
    }
}
