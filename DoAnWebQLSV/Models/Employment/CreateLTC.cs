using System.ComponentModel.DataAnnotations;

namespace DoAnWebQLSV.Models.Employment
{
    public class CreateLTC
    {
        public string NienKhoa { get; set; } = string.Empty;
        public int HocKy { get; set; }
        public string MaMH { get; set; } = string.Empty;
        public string MaGV { get; set; } = string.Empty;
        public int SiSoToiDa { get; set; }
        public string DayThuTrongTuan { get; set; } = string.Empty;
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }

        public bool HuyLop { get; set; } = false;
    }
}
