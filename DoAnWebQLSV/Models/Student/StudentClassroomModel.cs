using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Student
{
    public class StudentClassroomModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public List<ItemClassroom> Data { get; set; } = new List<ItemClassroom>();
    }

    public class ItemClassroom
    {
        public string MaLop { get; set; } = string.Empty;

        public string TenLop { get; set; } = string.Empty;
    }
}
