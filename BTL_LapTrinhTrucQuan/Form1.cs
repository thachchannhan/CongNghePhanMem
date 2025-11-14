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
            GanSuKienChoTatCaGhe();
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

        private List<string> danhSachGheDaChon = new List<string>();
        private const int GIA_GHE_MAC_DINH = 60000;
        private void GanSuKienChoTatCaGhe()
        {
            TabPage tabGhe = tabControl1.TabPages[3];
            foreach (Control control in tabGhe.Controls)
            {
                // Kiểm tra xem control đó có phải là Button không
                if (control is Button btnGhe)
                {
                    // Kiểm tra xem Button này có phải là nút ghế thực sự (có tên ghế, vd: A1, B6)
                    if (btnGhe.Text.Length == 2 && Char.IsLetter(btnGhe.Text[0]) && Char.IsDigit(btnGhe.Text[1]))
                    {
                        // Gán sự kiện Click cho nút ghế đó vào hàm xử lý chung
                        btnGhe.Click += new EventHandler(XuLyClickGhe);
                    }
                }
            }
        }
        private static readonly Color MAU_GHE_DA_CHON = Color.LightBlue;
        private static readonly Color MAU_GHE_CHUA_CHON = Color.LightYellow;
        private void XuLyClickGhe(object sender, EventArgs e)
        {
            // Biến sender chính là nút (Button) vừa được click
            {
                Button btnGhe = (Button)sender;
                string tenGhe = btnGhe.Text;

                // Logic chọn/hủy chọn ghế
                if (danhSachGheDaChon.Contains(tenGhe))
                {
                    // 1. Nếu ghế đã chọn -> Hủy chọn
                    // ĐẶT LẠI MÀU VÀNG BAN ĐẦU
                    btnGhe.BackColor = MAU_GHE_CHUA_CHON;
                    danhSachGheDaChon.Remove(tenGhe);
                }
                else
                {
                    // 2. Nếu ghế chưa chọn -> Chọn ghế
                    // ĐẶT MÀU XANH KHI ĐƯỢC CHỌN
                    btnGhe.BackColor = MAU_GHE_DA_CHON;
                    danhSachGheDaChon.Add(tenGhe);
                }

                CapNhatThongTinThanhToan();
            }
        }
        private void CapNhatThongTinThanhToan()
        {
            txtGheDaChon.Text = string.Join(", ", danhSachGheDaChon);

            int tongSoGhe = danhSachGheDaChon.Count;
            long tongTien = (long)tongSoGhe * GIA_GHE_MAC_DINH;


            txtGheDaChon.Text = string.Join(", ", danhSachGheDaChon);
            txtGheDaChon.ReadOnly = true; // <--- VÔ HIỆU HÓA CHỈNH SỬA

            // 3. Cập nhật và vô hiệu hóa chỉnh sửa cho txtTongTienThanhToan
            txtSoTien_KH.Text = tongTien.ToString("N0") + " VNĐ";
            txtSoTien_KH.ReadOnly = true; // <--- VÔ HIỆU HÓA CHỈNH SỬA
                                          }

        private void btnXacNhan_KH_Click(object sender, EventArgs e)
        {
            if (rdbPhong2D_KH.Checked || rdbPhong3D_KH.Checked || rdbPhongIMAX_KH.Checked)
            {
                MessageBox.Show("Đã xác nhận phòng.", "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Sau khi xác nhận, bạn có thể vô hiệu hóa các lựa chọn phòng để tránh thay đổi
                // rdbPhong2D.Enabled = false;
                // ...
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại phòng trước khi xác nhận.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void rdbDienTu_KH_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        private void btnXoa_KH_Click(object sender, EventArgs e)
        {
            danhSachGheDaChon.Clear();
            CapNhatThongTinThanhToan();

            rdbPhong2D_KH.Checked = false;
            rdbPhong3D_KH.Checked = false;
            rdbPhongIMAX_KH.Checked = false;
            // ... và các RadioButton thanh toán ...
            rdbTienMat_KH.Checked = false;
            rdbDienTu_KH.Checked = false;
            TabPage tabGhe = tabControl1.TabPages[3];
            foreach (Control control in tabGhe.Controls)
            {
                if (control is Button btnGhe)
                {
                    // Kiểm tra xem Button này có phải là nút ghế thực sự (có tên ghế, vd: A1, B6)
                    if (btnGhe.Text.Length == 2 && Char.IsLetter(btnGhe.Text[0]) && Char.IsDigit(btnGhe.Text[1]))
                    {
                        // Đặt lại màu nền về màu mặc định (ví dụ: Control hoặc LightGray)
                        btnGhe.BackColor = MAU_GHE_CHUA_CHON; // <--- SỬ DỤNG MÀU VÀNG                    }
                    }
                }
            }
        }

        private void btnMua_KH_Click(object sender, EventArgs e)
        {
            if (rdbDienTu_KH.Checked)
            {
                string tongTienText = txtSoTien_KH.Text.Replace(" VNĐ", "").Replace(",", "");
                long tongTien = 0;
                long.TryParse(tongTienText, out tongTien);
                ThanhToan formThanhToan = new ThanhToan();
                // *Quan trọng*: Chuyển dữ liệu Tổng tiền sang Form ThanhToan (nếu Form ThanhToan có phương thức/thuộc tính để nhận)
                // Đây là ví dụ về cách truyền dữ liệu qua constructor hoặc thuộc tính:

                // Giả sử Form ThanhToan có một hàm tên là LoadThongTin(long soTien)
                formThanhToan.LoadThongTin(tongTien);
                formThanhToan.ShowDialog();
            }
            else if (rdbTienMat_KH.Checked)
            {
                // 2. Logic cho Thanh toán tiền mặt (Thường là lưu vé ngay lập tức)
                // Lưu thông tin vé vào CSDL
                // ... (Gọi hàm Lưu vé) ...

                MessageBox.Show("Thanh toán tiền mặt hoàn tất. Vé đã được lưu.");

                // Xóa dữ liệu trên Form hiện tại
                // ... (Gọi hàm Xóa / Reset Form) ...
            }
            else
            {
                // 3. Trường hợp chưa chọn phương thức thanh toán
                MessageBox.Show("Vui lòng chọn phương thức thanh toán (Tiền mặt hoặc Điện tử) trước khi mua vé.");
            }
        }

     
    }
} //tao hamj ronaldo

