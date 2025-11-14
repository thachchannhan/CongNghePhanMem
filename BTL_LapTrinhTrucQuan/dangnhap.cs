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

namespace BTL_LapTrinhTrucQuan
{
    public partial class dangnhap : Form
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        public dangnhap()
        {
            InitializeComponent();
            SendMessage(txtTenDN.Handle, EM_SETCUEBANNER, 0, "User name");
            SendMessage(txtPassword.Handle, EM_SETCUEBANNER, 0, "Pass word");
        }

        private void dangnhap_Load(object sender, EventArgs e)
        {

        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string tenDN = txtTenDN.Text.Trim();
            string password = txtPassword.Text.Trim();

            string connectionString = @"Data Source=DESKTOP-L7HKKMB;Initial Catalog=QLPhimPho;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @" SELECT q.VAITRO 
        FROM TAIKHOAN tk
        JOIN QUYEN_TRUYCAP q ON tk.ID_TAIKHOAN = q.ID_TAIKHOAN
        WHERE tk.TENDANGNHAP = @user AND tk.MATKHAU = @pass";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", tenDN);
                cmd.Parameters.AddWithValue("@pass", password);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string vaiTro = result.ToString().Trim();

                    this.Hide();

                    if (vaiTro == "Quản Trị Viên")
                    {
                        Form1 f = new Form1();
                        f.ShowDialog();
                    }

                    else if (vaiTro == "Khách Hàng")
                    {
                        Form1 f = new Form1();
                        f.ShowDialog();
                    }

                    this.Show();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }
            }
        }

        private void btnThoat_DN_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
