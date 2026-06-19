using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models
{
    public class ClassroomViewModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public LopData Data { get; set; } = new LopData();
    }

    public class LopData
    {
        [JsonPropertyName("items")]
        public List<LopItem> Items { get; set; } = new List<LopItem>();

        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; } = new Pagination();
    }

    public class LopItem
    {
        [JsonPropertyName("malop")]
        public string MaLop { get; set; } = string.Empty;

        [JsonPropertyName("tenlop")]
        public string TenLop { get; set; } = string.Empty;

        [JsonPropertyName("khoahoc")]
        public string KhoaHoc { get; set; } = string.Empty;

        [JsonPropertyName("tenkhoa")]
        public string TenKhoa { get; set; } = string.Empty;

        [JsonPropertyName("tennv")]
        public string TenNhanVien { get; set; } = string.Empty;

        [JsonPropertyName("ngaymolop")]
        public string NgayMoLop { get; set; } = string.Empty;

        [JsonPropertyName("trangthai")]
        public string TrangThai { get; set; } = string.Empty;
    }

    public class Pagination
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("hasNext")]
        public bool HasNext { get; set; }

        [JsonPropertyName("hasPrevious")]
        public bool HasPrevious { get; set; }
    }
}
