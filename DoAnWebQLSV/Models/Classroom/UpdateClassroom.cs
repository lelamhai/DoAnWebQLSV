namespace DoAnWebQLSV.Models.Classroom
{
    public class UpdateClassroom
    {
        public string MaLop { get; set; } = string.Empty;

        public string TenLop { get; set; } = string.Empty;

        public string KhoaHoc { get; set; } = string.Empty;

        public string MaKhoa { get; set; } = string.Empty;

        public string TenNhanVien { get; set; } = "NV00000001";

        public string NgayMoLop { get; set; } = string.Empty;

        public int TrangThai { get; set; }
    }
}
