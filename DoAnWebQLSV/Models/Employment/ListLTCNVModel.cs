using DoAnWebQLSV.Models.Classroom;
using DoAnWebQLSV.Models.Teacher;
using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Employment
{
    public class ListLTCNVModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public LTCNVData Data { get; set; } = new LTCNVData();
    }

    public class LTCNVData
    {
        [JsonPropertyName("items")]
        public List<LTCNVItem> Items { get; set; } = new List<LTCNVItem>();

        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; } = new Pagination();
    }

    public class LTCNVItem
    {
        // Mã lớp tín chỉ trong database
        public int MaLtc { get; set; }

        // Mã lớp học phần hiển thị: INT1001_1, CNTT-01...
        public string MaLopHp { get; set; } = string.Empty;

        // Thông tin môn học
        public string MaMh { get; set; } = string.Empty;

        public string TenMh { get; set; } = string.Empty;

        public int SoTinChi { get; set; }

        public int SoTietLt { get; set; }

        public int SoTietTh { get; set; }

        // Thông tin giảng viên
        public string? MaGv { get; set; }

        public string? TenGiangVien { get; set; }

        // Học kỳ và niên khóa
        public int HocKy { get; set; }

        public string NienKhoa { get; set; } = string.Empty;

        // Sĩ số
        public int SiSoHienTai { get; set; }

        public int SiSoToiDa { get; set; }

        // Dạng hiển thị: 25/50
        public string SiSo { get; set; } = string.Empty;

        // Lịch học
        public string DayThuTrongTuan { get; set; } = string.Empty;

        // Dạng hiển thị: Thứ: 2, 4, 5
        public string LichHoc { get; set; } = string.Empty;

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }

        // Dạng hiển thị: 01/03/2026 - 30/05/2026
        public string ThoiGianHoc { get; set; } = string.Empty;

        public bool HuyLop { get; set; }
    }
}
