using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Account
{
    public class AccountInfoViewModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public StudentInfoData? Data { get; set; }
    }

    public class StudentInfoData
    {
        [JsonPropertyName("masv")]
        public string MaSV { get; set; } = string.Empty;

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; } = string.Empty;

        [JsonPropertyName("ho")]
        public string Ho { get; set; } = string.Empty;

        [JsonPropertyName("ten")]
        public string Ten { get; set; } = string.Empty;
    }
}
