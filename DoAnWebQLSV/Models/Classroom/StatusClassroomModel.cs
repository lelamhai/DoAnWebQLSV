using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Classroom
{
    public class StatusClassroomModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public List<StatusItem> Data { get; set; } = new List<StatusItem>();
    }

    public class StatusItem 
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
