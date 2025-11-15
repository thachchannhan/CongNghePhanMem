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
            SendMessage(txtPassword.Handle, EM_SETCUEBANNER, 0, "Pass word");
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string tenDN = txtTenDN.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (tenDN == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!");
                return;
            }

            // ⭐ TRUY VẤN MỚI để lấy nhiều cột
            string query = $@"
        SELECT 
            tk.ID_TAIKHOAN, 
            tk.TENDANGNHAP,
            tk.EMAIL,
            q.VAITRO
        FROM TAIKHOAN tk
        JOIN QUYEN_TRUYCAP q ON tk.ID_TAIKHOAN = q.ID_TAIKHOAN
        WHERE tk.TENDANGNHAP = N'{tenDN}' AND tk.MATKHAU = N'{password}'";

            DataTable dtResult = kn.GetData(query);

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                DataRow row = dtResult.Rows[0];

                // ⭐ LƯU THÔNG TIN VÀO USER SESSION
                TaiKhoan.ID = row["ID_TAIKHOAN"].ToString();
                TaiKhoan.Quyen = row["VAITRO"].ToString();
                TaiKhoan.Name = row["TENDANGNHAP"].ToString(); // Dùng TENDANGNHAP làm Tên hiển thị
                TaiKhoan.Email = row["EMAIL"].ToString();

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
