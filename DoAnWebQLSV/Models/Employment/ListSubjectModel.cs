using DoAnWebQLSV.Models.Classroom;
using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Employment
{
    public class ListSubjectModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public SubjectData Data { get; set; } = new SubjectData();
    }

    public class SubjectData
    {
        [JsonPropertyName("items")]
        public List<SubjectItem> Items { get; set; } = new List<SubjectItem>();
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; } = new Pagination();
    }

    public class SubjectItem
    {
        public string MaMh { get; set; } = string.Empty;

        public string TenMh { get; set; } = string.Empty;

        public int SoTietLt { get; set; }

        public int SoTietTh { get; set; }

        public int SoTinChi { get; set; }

        public string MaKhoa { get; set; } = string.Empty;
    }
}
