using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL_LapTrinhTrucQuan;
namespace BTL_LapTrinhTrucQuan
{
    public partial class dangnhap : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        // ⭐ Tạo đối tượng kết nối
        KETNOISQL kn = new KETNOISQL();

        public dangnhap()
        {
            InitializeComponent();
            SendMessage(txtTenDN.Handle, EM_SETCUEBANNER, 0, "User name");
            SendMessage(txtPassWord.Handle, EM_SETCUEBANNER, 0, "Pass word");
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string tenDN = txtTenDN.Text.Trim();
            string password = txtPassWord.Text.Trim();

            if (tenDN == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!");
                return;
            }

            // CẬP NHẬT QUERY - THÊM TRUY VẤN CỘT HOTEN
            string query = $@"
        SELECT 
            tk.ID_TAIKHOAN, 
            tk.TENDANGNHAP,
            tk.HOTEN, -- THÊM CỘT HỌ TÊN
            tk.EMAIL,
            tk.GIOITINH,
            tk.NGAYSINH,
            tk.SDT,
            q.VAITRO
        FROM TAIKHOAN tk
        JOIN QUYEN_TRUYCAP q ON tk.ID_TAIKHOAN = q.ID_TAIKHOAN
        WHERE tk.TENDANGNHAP = N'{tenDN}' AND tk.MATKHAU = N'{password}'";

            DataTable dtResult = kn.GetData(query);

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                DataRow row = dtResult.Rows[0];

                // LƯU THÔNG TIN VÀO USER SESSION - PHÂN BIỆT TENDANGNHAP VÀ HOTEN
                TaiKhoan.ID = row["ID_TAIKHOAN"].ToString();
                TaiKhoan.Quyen = row["VAITRO"].ToString();
                TaiKhoan.TenDangNhap = row["TENDANGNHAP"].ToString(); // Tên đăng nhập
                TaiKhoan.HoTen = row["HOTEN"] != DBNull.Value ? row["HOTEN"].ToString() : ""; // Họ tên thật
                TaiKhoan.Email = row["EMAIL"] != DBNull.Value ? row["EMAIL"].ToString() : "";

                // THÔNG TIN CÁ NHÂN
                TaiKhoan.GioiTinh = row["GIOITINH"] != DBNull.Value ? row["GIOITINH"].ToString() : "";

                // Xử lý ngày sinh
                if (row["NGAYSINH"] != DBNull.Value)
                {
                    DateTime ngaySinh = Convert.ToDateTime(row["NGAYSINH"]);
                    TaiKhoan.NgaySinh = ngaySinh.ToString("yyyy-MM-dd");
                }
                else
                {
                    TaiKhoan.NgaySinh = "";
                }

                TaiKhoan.SoDienThoai = row["SDT"] != DBNull.Value ? row["SDT"].ToString() : "";

                // Đăng nhập thành công
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }
        private void btnThoat_DN_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
