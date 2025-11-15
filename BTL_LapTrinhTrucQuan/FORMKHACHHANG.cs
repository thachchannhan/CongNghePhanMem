using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_LapTrinhTrucQuan
{
    public partial class FORMKHACHHANG : Form

    {
        public FORMKHACHHANG()
        {
            InitializeComponent();
            btnXoa_BDK.Enabled = false;
            btnChonPhim.Enabled = false;
            //BDK
            LoadDataPHIM();
            //Lichsu
            HienThiTenNguoiDung();
            LoadDataVe();
            HienThiTongChiTieu();
        }
        private void btnBDK_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
        private void btnMua_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }
        private void btnDX_Click(object sender, EventArgs e)
        {

            this.Hide();
            using (dangnhap dnForm = new dangnhap())
            {
                if (dnForm.ShowDialog() == DialogResult.OK)
                {

                    this.Show();
                }
                else
                {

                    Application.Exit();
                }
            }

        }
        //TABPAGE BANGDIEUKHIEN
        private void LoadDataPHIM()
        {
            KETNOISQL ketNoi = new KETNOISQL();
            string query = "SELECT * FROM PHIM";
            try
            {

                DataTable dtPhim = ketNoi.GetData(query);
                if (dgvDSPhim != null)
                {
                    dgvDSPhim.DataSource = dtPhim;
                    dgvDSPhim.RowHeadersVisible = false;
                    dgvDSPhim.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvDSPhim.ReadOnly = true;
                    dgvDSPhim.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvDSPhim.AllowUserToAddRows = false;
                    dgvDSPhim.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dgvDSPhim.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu phim: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDSPhim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDSPhim.Rows[e.RowIndex];
                try
                {

                    txtMaPhim_BDK.Text = row.Cells["ID_PHIM"].Value.ToString();
                    txtTenPhim_BDK.Text = row.Cells["TENPHIM"].Value.ToString();
                    txtTheLoai_BDK.Text = row.Cells["THELOAI"].Value.ToString();
                    txtThoiLuong_BDK.Text = row.Cells["THOILUONG"].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lấy dữ liệu từ DataGridView: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnXoa_BDK.Enabled = true;
                btnChonPhim.Enabled = true;
            }
        }

        private void btnXoa_BDK_Click(object sender, EventArgs e)
        {
            txtMaPhim_BDK.Text = "";
            txtTenPhim_BDK.Text = "";
            txtTheLoai_BDK.Text = "";
            txtThoiLuong_BDK.Text = "";
            txtMaPhim_BDK.Focus();
            LoadDataPHIM();
        }

        private void btnTKPHIM_BDK_Click(object sender, EventArgs e)
        {
            KETNOISQL ketNoi = new KETNOISQL();
            string maPhim = txtMaPhim_BDK.Text.Trim();
            string tenPhim = txtTenPhim_BDK.Text.Trim();

            string query = "SELECT * FROM PHIM WHERE 1 = 1";

            if (!string.IsNullOrEmpty(maPhim))
            {
                query += $" AND ID_PHIM LIKE N'%{maPhim}%'";
            }
            if (!string.IsNullOrEmpty(tenPhim))
            {
                query += $" AND TENPHIM LIKE N'%{tenPhim}%'";
            }


            if (string.IsNullOrEmpty(maPhim) && string.IsNullOrEmpty(tenPhim))
            {
                MessageBox.Show("Vui lòng nhập Mã phim hoặc Tên phim để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                DataTable dtPhim = ketNoi.GetData(query);
                dgvDSPhim.DataSource = dtPhim;

                if (dtPhim.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy phim nào thỏa mãn điều kiện.", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình tìm kiếm: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //TABPAGE LICHSU
        private void HienThiTenNguoiDung()
        {
            string tenHienThi = TaiKhoan.Name;
            string Email = TaiKhoan.Email;

            if (!string.IsNullOrEmpty(tenHienThi))
            {
                txtTenKH_LS.Text = tenHienThi;
                txtEmail_LS.Text = Email;

            }
            else
            {
                txtTenKH_LS.Text = "Lỗi: Không tìm thấy thông tin người dùng.";
                txtEmail_LS.Text = "Lỗi: Không tìm thấy thông tin người dùng.";
            }
            txtTenKH_LS.SelectionStart = 0;
            txtTenKH_LS.SelectionLength = 0;

            txtEmail_LS.SelectionStart = 0;
            txtEmail_LS.SelectionLength = 0;
        }
        private void LoadDataVe()
        {
            string userID = TaiKhoan.ID;
            if (string.IsNullOrEmpty(userID))
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng. Vui lòng đăng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            KETNOISQL ketNoi = new KETNOISQL();
            string query = $@"
            SELECT 
            v.ID_VE, 
            v.ID_CACCHIEU, 
            v.ID_GHE, 
            v.GIA,
            h.NGAYLAP,
            v.TRANGTHAI
            FROM VE v
            JOIN HOADON h ON v.ID_HOADON = h.ID_HOADON
            WHERE h.ID_TAIKHOAN = N'{userID}'";
            try
            {
                DataTable dtVe = ketNoi.GetData(query);
                dgvLichSu.DataSource = dtVe;
                dgvLichSu.RowHeadersVisible = false;
                dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvLichSu.ReadOnly = true;
                dgvLichSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvLichSu.AllowUserToAddRows = false;
                dgvLichSu.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvLichSu.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                if (dtVe.Rows.Count == 0)
                {
                    MessageBox.Show("Bạn chưa mua vé nào.", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu vé: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiTongChiTieu()
        {
            string userID = TaiKhoan.ID;

            if (string.IsNullOrEmpty(userID))
            {
                txttongchitieu.Text = "0 VNĐ";
                return;
            }
            KETNOISQL ketNoi = new KETNOISQL();
            string query = $@"
                SELECT ISNULL(SUM(v.GIA), 0) -- Dùng ISNULL để đảm bảo trả về 0 nếu không có vé nào
                FROM VE v
                JOIN HOADON h ON v.ID_HOADON = h.ID_HOADON
                WHERE h.ID_TAIKHOAN = N'{userID}'";

            try
            {
                object result = ketNoi.ExecuteScalar(query);

                decimal tongTien = 0;
                if (result != null && decimal.TryParse(result.ToString(), out tongTien))
                {
                    txttongchitieu.Text = tongTien.ToString("N0") + " VNĐ";
                }
                else
                {
                    txttongchitieu.Text = "0 VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính tổng chi tiêu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txttongchitieu.Text = "Lỗi";
            }
        }
        // GOP Y
        private void btnGopY_Click(object sender, EventArgs e)
        {
            GopY frmGopY = new GopY();
            frmGopY.Show();
        }
        //DAT VE
        public decimal TinhTongTienVeCoDinh(string loaiPhongDuocChon, List<string> danhSachGheDaChon)
        {
            decimal tongTien = 0;
            decimal giaCoBanPhong = 0;

            // --- BƯỚC 1: Xác định Giá Cơ Bản của Phòng ---
            switch (loaiPhongDuocChon.ToUpper())
            {
                case "PHÒNG 2D":
                    giaCoBanPhong = 90000;
                    break;
                case "PHÒNG 3D":
                    giaCoBanPhong = 120000;
                    break;
                case "PHÒNG IMAX":
                    giaCoBanPhong = 150000;
                    break;
                default:
                    // Xử lý trường hợp không xác định được loại phòng
                    MessageBox.Show("Lỗi: Không xác định được loại phòng đã chọn.");
                    return 0;
            }

            // --- BƯỚC 2: Tính Tổng Tiền theo từng Ghế ---
            foreach (string tenGhe in danhSachGheDaChon)
            {
                decimal phuPhiGhe = 0;

                // **Giả định logic xác định Ghế VIP:**
                // Ghế VIP là các hàng A, B, C (như trong Form của bạn, 3 hàng trên cùng).
                // Ghế thường là các hàng D, E, F.

                // Lấy ký tự đầu tiên (Hàng ghế)
                char hangGhe = tenGhe.FirstOrDefault();

                if (hangGhe == 'A' || hangGhe == 'B' || hangGhe == 'C')
                {
                    // Ghế VIP: Cộng thêm 15,000
                    phuPhiGhe = 15000;
                }
                else
                {
                    // Ghế Thường: Phụ phí 0
                    phuPhiGhe = 0;
                }

                decimal giaVeDon = giaCoBanPhong + phuPhiGhe;
                tongTien += giaVeDon;
            }

            return tongTien;
        }

}

}

