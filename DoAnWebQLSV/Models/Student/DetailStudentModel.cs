using DoAnWebQLSV.Models.Classroom;
using System.Text.Json.Serialization;

namespace DoAnWebQLSV.Models.Student
{
    public class DetailStudentModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public List<ItemStudent> Data { get; set; } = new List<ItemStudent>();
    }

    public class ItemStudent
    {
        public string Masv { get; set; } = null!;

        public string Ho { get; set; } = null!;

        public string Ten { get; set; } = null!;

        public string Phai { get; set; }

        public string Diachi { get; set; }

        public string Sodienthoai { get; set; }

        public string Ngaysinh { get; set; }

        public string Email { get; set; }

        public string Malop { get; set; }

        public string Tenlop { get; set; } = null!;

        public int Id { get; set; }

        public string Trangthai { get; set; }
    }
}
