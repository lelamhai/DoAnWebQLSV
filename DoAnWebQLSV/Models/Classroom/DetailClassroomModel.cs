using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Classroom
{
    public class DetailClassroomModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public List<ItemDetail> Data { get; set; } = new List<ItemDetail>();
    }

    public class ItemDetail
    {
        [JsonPropertyName("malop")]
        public string MaLop { get; set; } = string.Empty;

        [JsonPropertyName("tenlop")]
        public string TenLop { get; set; } = string.Empty;

        [JsonPropertyName("khoahoc")]
        public string KhoaHoc { get; set; } = string.Empty;

        [JsonPropertyName("makhoa")]
        public string MaKhoa { get; set; } = string.Empty;

        [JsonPropertyName("tennv")]
        public string TenNhanVien { get; set; } = string.Empty;

        [JsonPropertyName("ngaymolop")]
        public string NgayMoLop { get; set; } = string.Empty;

        [JsonPropertyName("trangthai")]
        public int TrangThai { get; set; }
    }
}
