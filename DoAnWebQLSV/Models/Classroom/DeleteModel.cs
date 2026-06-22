using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Classroom
{
    public class DeleteModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public bool Data { get; set; } 
    }
}
