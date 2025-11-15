using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_LapTrinhTrucQuan
{
    public static  class TaiKhoan
    {
        public static string ID { get; set; }

        // Lưu tên người dùng/khách hàng
        public static string Name { get; set; }

        // Lưu vai trò (VaiTro) để phân quyền
        public static string Quyen { get; set; }
        public static string Email { get; set; }
        public static void ClearSession()
        {
            ID = null;
            Name = null;
            Quyen = null;
            Email = null;
        }
    }
}
