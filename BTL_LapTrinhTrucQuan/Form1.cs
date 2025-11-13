using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BTL_LapTrinhTrucQuan
{
    public partial class Form1 : Form

    {
        private SqlConnection conn;
        private SqlDataAdapter da;
        private DataTable dtPhim;
        private const string connectionString = "Data Source=DESKTOP-L7HKKMB;Initial Catalog=QLPhimPho;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            LoadDataPhim(); // Gọi hàm tải dữ liệu khi khởi tạo form
        }
        private void LoadDataPhim()
        {
            // ... (Khởi tạo conn, dtPhim, connectionString như cũ)

            try
            {
                conn = new SqlConnection(connectionString);
                // 1. Chuỗi truy vấn cập nhật (chỉ lấy các cột quan trọng)
                string query = "SELECT ID_PHIM, TENPHIM, THELOAI, THOILUONG, NGONNGU, DAODIEN, DIENVIEN,TRANGTHAI FROM dbo.PHIM";

                conn.Open();
                da = new SqlDataAdapter(query, conn);
                dtPhim = new DataTable();

                // 2. Đổ dữ liệu và gán nguồn
                da.Fill(dtPhim);
                dgvDSPhim.DataSource = dtPhim;
                dgvDSPhim.AllowUserToAddRows = false;

                // 3. Đổi tên tiêu đề cột hiển thị trên DataGridView
                dgvDSPhim.Columns["ID_PHIM"].HeaderText = "Mã phim";
                dgvDSPhim.Columns["TENPHIM"].HeaderText = "Tên phim";
                dgvDSPhim.Columns["THELOAI"].HeaderText = "Thể loại";
                dgvDSPhim.Columns["THOILUONG"].HeaderText = "Thời lượng";
                dgvDSPhim.Columns["NGONNGU"].HeaderText = "Ngôn ngữ";
                dgvDSPhim.Columns["DAODIEN"].HeaderText = "Đạo diễn";
                dgvDSPhim.Columns["DIENVIEN"].HeaderText = "Diễn viên";
                dgvDSPhim.Columns["TRANGTHAI"].HeaderText = "Trạng thái";
            }


            finally
            {
                // Đảm bảo đóng kết nối sau khi hoàn thành
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        private void dgvDSPhim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDSPhim.Rows[e.RowIndex];

                
                    txtMaPhim.Text = row.Cells["ID_PHIM"].Value.ToString();
                    txtTenPhim.Text = row.Cells["TENPHIM"].Value.ToString();
                    txtTheLoai.Text = row.Cells["THELOAI"].Value.ToString();

                    object thoiLuongValue = row.Cells["THOILUONG"].Value;
                    txtThoiLuong.Text = (thoiLuongValue == DBNull.Value) ? string.Empty : thoiLuongValue.ToString();
            }
        }
        private void grbMenu_Enter(object sender, EventArgs e)
        {

        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnMua_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;

        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void btnBDK_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btnChonPhim_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtMaPhim.Text = string.Empty;
            txtTenPhim.Text = string.Empty;
            txtTheLoai.Text = string.Empty;
            txtThoiLuong.Text = string.Empty;
        }
    }
}
