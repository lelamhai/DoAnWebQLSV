using DoAnWebQLSV.Models.Classroom;
using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Student
{
    public class ListLTCModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public LTCData Data { get; set; } = new LTCData();
    }

    public class LTCData
    {
        [JsonPropertyName("items")]
        public List<LTCItem> Items { get; set; } = new List<LTCItem>();

        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; } = new Pagination();
    }

    public class LTCItem
    {
        public int MaLtc { get; set; }

        public string NienKhoa { get; set; } = string.Empty;

        public int HocKy { get; set; }

        public string MaMh { get; set; } = string.Empty;

        public string TenMh { get; set; } = string.Empty;

        public int SoTinChi { get; set; }

        public int SoTietLt { get; set; }

        public int SoTietTh { get; set; }

        public string? MaGv { get; set; }

        public string? TenGiangVien { get; set; }

        public int SiSoHienTai { get; set; }

        public int SiSoToiDa { get; set; }

        public string SiSo { get; set; } = string.Empty;

        public string DayThuTrongTuan { get; set; } = string.Empty;

        public string LichHoc { get; set; } = string.Empty;

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }

        public string ThoiGianHoc { get; set; } = string.Empty;

        public bool HuyLop { get; set; }
    }
}
