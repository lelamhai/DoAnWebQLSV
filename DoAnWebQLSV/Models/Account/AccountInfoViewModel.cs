using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Account
{
    public class AccountInfoViewModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public UserInfoData? Data { get; set; }
    }

    public class UserInfoData
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("ho")]
        public string Ho { get; set; } = string.Empty;

        [JsonPropertyName("ten")]
        public string Ten { get; set; } = string.Empty;

        [JsonPropertyName("trangthai")]
        public int TrangThai { get; set; }
    }
}
