using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Classroom
{
    public class FacultyModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public List<ItemFaculty> Data { get; set; } = new List<ItemFaculty>();
    }

    public class ItemFaculty
    {
        [JsonPropertyName("maKhoa")]
        public string MaKhoa { get; set; }

        [JsonPropertyName("tenKhoa")]
        public string TenKhoa { get; set; } = string.Empty;
    }
}
