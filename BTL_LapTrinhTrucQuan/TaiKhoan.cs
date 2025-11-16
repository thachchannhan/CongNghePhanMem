using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_LapTrinhTrucQuan
{
    public static class TaiKhoan
    {
        public static string ID { get; set; }
        public static string Quyen { get; set; }
        public static string Name { get; set; } // Vừa là tên đăng nhập, vừa là họ tên hiển thị
        public static string Email { get; set; }

        // THÔNG TIN CÁ NHÂN
        public static string GioiTinh { get; set; }
        public static string NgaySinh { get; set; }
        public static string SoDienThoai { get; set; }

        public static void ClearSession()
        {
            ID = null;
            Name = null;
            Quyen = null;
            Email = null;
            GioiTinh = null;
            NgaySinh = null;
            SoDienThoai = null;
        }
    }
}