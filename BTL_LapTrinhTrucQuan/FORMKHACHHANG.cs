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
            LoadDataHoaDon();
            //Muave
            HienThiTongChiTieu();
            GanSuKienChoGhe();
            btnChonPhim.Click += btnChonPhim_Click;
            btnXacNhan_KH.Click += btnXacNhan_KH_Click;
            btnXoa_KH.Click += btnXoa_KH_Click;
            btnMua_KH.Click += btnMua_KH_Click;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            //CaNhan
            button40.Click += btnCapNhat_Click;
            LoadThongTinCaNhan();
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
        
        private void LoadDataHoaDon() // Đổi tên từ LoadDataVe
        {
            string userID = TaiKhoan.ID;
            if (string.IsNullOrEmpty(userID))
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng. Vui lòng đăng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            KETNOISQL ketNoi = new KETNOISQL();
            // Thay đổi truy vấn để lấy dữ liệu từ bảng HOADON
            string query = $@"
        SELECT 
        ID_HOADON, 
        NGAYLAP, 
        TONGTIEN,
        TRANGTHAI 
        FROM HOADON
        WHERE ID_TAIKHOAN = N'{userID}'
        ORDER BY NGAYLAP DESC"; // Sắp xếp theo ngày mới nhất

            try
            {
                DataTable dtHoaDon = ketNoi.GetData(query);
                dgvLichSu.DataSource = dtHoaDon;

                // Cấu hình DataGridView (giữ nguyên)
                dgvLichSu.RowHeadersVisible = false;
                dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvLichSu.ReadOnly = true;
                dgvLichSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvLichSu.AllowUserToAddRows = false;
                dgvLichSu.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvLichSu.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                if (dtHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Bạn chưa có hóa đơn nào.", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu hóa đơn: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        // Biến toàn cục
        private string selectedPhimId = "";
        private string selectedCaChieuId = "";
        private string selectedLoaiPhongId = "";
        private List<string> danhSachGheDaChon = new List<string>();
        private List<int> danhSachGheIdDaChon = new List<int>();
        private decimal tongTien = 0;

        // Phương thức lấy danh sách ca chiếu theo phim - BỎ LỌC NGÀY
        private void LoadCaChieuTheoPhim(string phimId)
        {
            KETNOISQL ketNoi = new KETNOISQL();

            string query = $@"
        SELECT cc.ID_CACCHIEU, cc.NGAYCHIEU, cc.GIOBATDAU, cc.GIOKETTHUC, 
               lp.TENLOAIPHONG, cc.GIA, cc.TRANGTHAI
        FROM CACCHIEU cc
        JOIN LOAIPHONG lp ON cc.ID_LOAIPHONG = lp.ID_LOAIPHONG
        WHERE cc.ID_PHIM = {phimId} 
        AND cc.TRANGTHAI = 1
        ORDER BY cc.NGAYCHIEU, cc.GIOBATDAU";

            try
            {
                DataTable dtCaChieu = ketNoi.GetData(query);

                // Clear combo box trước
                comboBox1.Items.Clear();

                if (dtCaChieu.Rows.Count > 0)
                {
                    foreach (DataRow row in dtCaChieu.Rows)
                    {
                        string ngayChieu = Convert.ToDateTime(row["NGAYCHIEU"]).ToString("dd/MM/yyyy");
                        string displayText = $"{ngayChieu} - {row["GIOBATDAU"]} - {row["TENLOAIPHONG"]}";
                        string value = row["ID_CACCHIEU"].ToString();
                        comboBox1.Items.Add(new { Text = displayText, Value = value });
                    }

                    comboBox1.DisplayMember = "Text";
                    comboBox1.ValueMember = "Value";

                    if (comboBox1.Items.Count > 0)
                        comboBox1.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Không có ca chiếu nào cho phim này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải ca chiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức lấy thông tin ghế theo phòng
        private void LoadGheTheoPhong(string loaiPhongId)
        {
            KETNOISQL ketNoi = new KETNOISQL();
            string query = $@"
        SELECT g.ID_GHE, g.HANG, g.COT, lg.TENLOAI, lg.GIAGHE,
               CASE WHEN v.TRANGTHAI = 'SOLD' THEN 1 ELSE 0 END AS DADAT
        FROM GHE g
        JOIN LOAIGHE lg ON g.ID_LOAIGHE = lg.ID_LOAIGHE
        LEFT JOIN VE v ON g.ID_GHE = v.ID_GHE 
                      AND v.ID_CACCHIEU = {selectedCaChieuId}
        WHERE g.ID_LOAIPHONG = {loaiPhongId}
        ORDER BY g.HANG, g.COT";

            try
            {
                DataTable dtGhe = ketNoi.GetData(query);
                HienThiGheLenPanel(dtGhe);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách ghế: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức hiển thị ghế lên panel
        private void HienThiGheLenPanel(DataTable dtGhe)
        {
            // Reset panel ghế
            foreach (Control control in panelGhe.Controls)
            {
                if (control is Button btn)
                {
                    string hang = btn.Text.Substring(0, 1);
                    string cot = btn.Text.Substring(1);

                    DataRow[] gheRows = dtGhe.Select($"HANG = '{hang}' AND COT = {cot}");
                    if (gheRows.Length > 0)
                    {
                        bool daDat = Convert.ToBoolean(gheRows[0]["DADAT"]);
                        string loaiGhe = gheRows[0]["TENLOAI"].ToString();

                        if (daDat)
                        {
                            btn.BackColor = Color.Red; // Ghế đã đặt
                            btn.Enabled = false;
                        }
                        else
                        {
                            if (loaiGhe == "VIP")
                            {
                                btn.BackColor = Color.Gold;
                            }
                            else
                            {
                                btn.BackColor = Color.LightYellow;
                            }
                            btn.Enabled = true;
                        }
                    }
                }
            }

            // Reset danh sách ghế đã chọn
            danhSachGheDaChon.Clear();
            danhSachGheIdDaChon.Clear();
            CapNhatGheDaChon();
            TinhTongTien();
        }
        // Phương thức xử lý khi click vào ghế
        private void XuLyChonGhe(Button btnGhe, string tenGhe)
        {
            if (!btnGhe.Enabled) return; // Ghế đã đặt thì không cho chọn

            // Lấy ID ghế từ database
            string hang = tenGhe.Substring(0, 1);
            string cot = tenGhe.Substring(1);

            KETNOISQL ketNoi = new KETNOISQL();
            string query = $@"
        SELECT g.ID_GHE, lg.TENLOAI, lg.GIAGHE
        FROM GHE g
        JOIN LOAIGHE lg ON g.ID_LOAIGHE = lg.ID_LOAIGHE
        WHERE g.HANG = '{hang}' AND g.COT = {cot} 
        AND g.ID_LOAIPHONG = {selectedLoaiPhongId}";

            try
            {
                DataTable dtGhe = ketNoi.GetData(query);
                if (dtGhe.Rows.Count > 0)
                {
                    int gheId = Convert.ToInt32(dtGhe.Rows[0]["ID_GHE"]);

                    // Kiểm tra ghế đã được chọn chưa
                    if (danhSachGheDaChon.Contains(tenGhe))
                    {
                        // Bỏ chọn ghế
                        danhSachGheDaChon.Remove(tenGhe);
                        danhSachGheIdDaChon.Remove(gheId);

                        // Đổi màu về trạng thái ban đầu
                        string loaiGhe = dtGhe.Rows[0]["TENLOAI"].ToString();
                        btnGhe.BackColor = (loaiGhe == "VIP") ? Color.Gold : Color.LightYellow;
                    }
                    else
                    {
                        // Chọn ghế
                        danhSachGheDaChon.Add(tenGhe);
                        danhSachGheIdDaChon.Add(gheId);
                        btnGhe.BackColor = Color.LightBlue; // Màu khi được chọn
                    }

                    CapNhatGheDaChon();
                    TinhTongTien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn ghế: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức tính tổng tiền
        private void TinhTongTien()
        {
            tongTien = 0;

            if (string.IsNullOrEmpty(selectedCaChieuId)) return;

            KETNOISQL ketNoi = new KETNOISQL();

            foreach (int gheId in danhSachGheIdDaChon)
            {
                string query = $@"
            SELECT (cc.GIA + lg.GIAGHE) as TONGTIEN
            FROM CACCHIEU cc
            JOIN GHE g ON g.ID_LOAIPHONG = cc.ID_LOAIPHONG
            JOIN LOAIGHE lg ON g.ID_LOAIGHE = lg.ID_LOAIGHE
            WHERE cc.ID_CACCHIEU = {selectedCaChieuId}
            AND g.ID_GHE = {gheId}";

                try
                {
                    object result = ketNoi.ExecuteScalar(query);
                    if (result != null && decimal.TryParse(result.ToString(), out decimal giaVe))
                    {
                        tongTien += giaVe;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tính tiền ghế: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            txtSoTien_KH.Text = tongTien.ToString("N0") + " VNĐ";
        }
        // Sự kiện khi chọn phim
        private void btnChonPhim_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPhim_BDK.Text))
            {
                MessageBox.Show("Vui lòng chọn một phim trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selectedPhimId = txtMaPhim_BDK.Text;

            // Load ca chiếu cho phim đã chọn
            LoadCaChieuTheoPhim(selectedPhimId);

            // Chuyển sang tab mua vé
            tabControl1.SelectedIndex = 3;

            MessageBox.Show($"Đã chọn phim: {txtTenPhim_BDK.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Sự kiện khi chọn ca chiếu
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                dynamic selectedItem = comboBox1.SelectedItem;
                selectedCaChieuId = selectedItem.Value.ToString();

                // Lấy loại phòng từ ca chiếu
                KETNOISQL ketNoi = new KETNOISQL();
                string query = $"SELECT ID_LOAIPHONG FROM CACCHIEU WHERE ID_CACCHIEU = {selectedCaChieuId}";

                try
                {
                    object result = ketNoi.ExecuteScalar(query);
                    if (result != null)
                    {
                        selectedLoaiPhongId = result.ToString();

                        // Check radio button tương ứng
                        switch (selectedLoaiPhongId)
                        {
                            case "1": rdbPhong2D_KH.Checked = true; break;
                            case "2": rdbPhong3D_KH.Checked = true; break;
                            case "3": rdbPhongIMAX_KH.Checked = true; break;
                        }

                        // Load ghế theo phòng
                        LoadGheTheoPhong(selectedLoaiPhongId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy thông tin ca chiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Sự kiện khi xác nhận phòng
        private void btnXacNhan_KH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCaChieuId))
            {
                MessageBox.Show("Vui lòng chọn ca chiếu trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Đã xác nhận phòng chiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Sự kiện khi mua vé
        // Sự kiện khi mua vé
        private void btnMua_KH_Click(object sender, EventArgs e)
        {
            // Kiểm tra validation
            if (danhSachGheIdDaChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một ghế!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!rdbTienMat_KH.Checked && !rdbDienTu_KH.Checked)
            {
                MessageBox.Show("Vui lòng chọn phương thức thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(selectedCaChieuId))
            {
                MessageBox.Show("Vui lòng chọn ca chiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị thông tin xác nhận
            string phuongThucTT = rdbTienMat_KH.Checked ? "Tiền mặt" : "Điện tử";
            string thongBao = $"Thông tin đặt vé:\n" +
                             $"Phim: {txtTenPhim_BDK.Text}\n" +
                             $"Ghế: {txtGheDaChon.Text}\n" +
                             $"Tổng tiền: {txtSoTien_KH.Text}\n" +
                             $"Phương thức thanh toán: {phuongThucTT}";

            DialogResult result = MessageBox.Show(thongBao, "Xác nhận đặt vé", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                if (rdbTienMat_KH.Checked)
                {
                    // Thanh toán tiền mặt - xử lý trực tiếp
                    LuuHoaDonVaVe();
                }
                else if (rdbDienTu_KH.Checked)
                {
                    // Thanh toán điện tử - mở form ThanhToan
                    MoFormThanhToan();
                }
            }
        }

        // Phương thức mở form thanh toán điện tử
        private void MoFormThanhToan()
        {
            try
            {
                // Lấy tổng tiền từ chuỗi (loại bỏ " VNĐ")
                string tienStr = txtSoTien_KH.Text.Replace(" VNĐ", "").Replace(",", "").Replace(".", "");
                if (long.TryParse(tienStr, out long soTien))
                {
                    using (ThanhToan formThanhToan = new ThanhToan())
                    {
                        formThanhToan.LoadThongTin(soTien);

                        // Hiển thị form ThanhToan dưới dạng dialog
                        DialogResult result = formThanhToan.ShowDialog();

                        // Khi form ThanhToan đóng (dù bằng nút nào), tiếp tục xử lý
                        if (result == DialogResult.OK || result == DialogResult.Cancel)
                        {
                            // Sau khi thanh toán điện tử thành công hoặc quay về, lưu hóa đơn
                            LuuHoaDonVaVe();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi chuyển đổi số tiền!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức lưu hóa đơn và vé
        // Phương thức lưu hóa đơn và vé
        private void LuuHoaDonVaVe()
        {
            KETNOISQL ketNoi = new KETNOISQL();

            try
            {
                // 1. Tạo hóa đơn mới
                string userId = TaiKhoan.ID;
                string insertHoaDonQuery = $@"
            INSERT INTO HOADON (ID_TAIKHOAN, TONGTIEN, TRANGTHAI)
            VALUES ('{userId}', {tongTien}, 'UNPAID');
            SELECT SCOPE_IDENTITY();";

                object hoadonIdObj = ketNoi.ExecuteScalar(insertHoaDonQuery);

                if (hoadonIdObj != null)
                {
                    int hoadonId = Convert.ToInt32(hoadonIdObj);

                    // 2. Cập nhật các vé đã chọn
                    foreach (int gheId in danhSachGheIdDaChon)
                    {
                        string updateVeQuery = $@"
                    UPDATE VE 
                    SET ID_HOADON = {hoadonId}, 
                        TRANGTHAI = 'SOLD'
                    WHERE ID_CACCHIEU = {selectedCaChieuId} 
                    AND ID_GHE = {gheId}";

                        ketNoi.ExecuteNonQuery(updateVeQuery);
                    }

                    MessageBox.Show("Đặt vé thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reset form
                    XoaTatCaGheDaChon();
                    LoadGheTheoPhong(selectedLoaiPhongId); // Refresh trạng thái ghế

                    // Cập nhật lịch sử
                    LoadDataHoaDon();
                    HienThiTongChiTieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Phương thức xóa tất cả ghế đã chọn
        private void XoaTatCaGheDaChon()
        {
            danhSachGheDaChon.Clear();
            danhSachGheIdDaChon.Clear();
            CapNhatGheDaChon();
            TinhTongTien();
        }

        // Phương thức cập nhật textbox ghế đã chọn
        private void CapNhatGheDaChon()
        {
            txtGheDaChon.Text = string.Join(", ", danhSachGheDaChon);
        }
        // Phương thức gán sự kiện cho các button ghế
        private void GanSuKienChoGhe()
        {
            // Gán sự kiện Click cho tất cả các button ghế trong panelGhe
            foreach (Control control in panelGhe.Controls)
            {
                if (control is Button btn && !string.IsNullOrEmpty(btn.Text))
                {
                    string tenGhe = btn.Text;
                    btn.Click += (sender, e) => XuLyChonGhe(btn, tenGhe);
                }
            }
        }

        private void btnXoa_KH_Click(object sender, EventArgs e)
        {
            XoaTatCaGheDaChon();
        }


        //CẬP NHẬT THÔNG TIN CÁ NHÂN
        private void LoadThongTinCaNhan()
        {
            try
            {
                // Hiển thị thông tin từ session
                textBox3.Text = TaiKhoan.Name ?? ""; // Name vừa là tên đăng nhập vừa là họ tên
                textBox7.Text = TaiKhoan.GioiTinh ?? "";
                textBox6.Text = TaiKhoan.SoDienThoai ?? "";
                textBox4.Text = TaiKhoan.NgaySinh ?? "";
                textBox5.Text = TaiKhoan.Email ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin cá nhân: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện khi click nút Cập nhật
        // Sự kiện khi click nút Cập nhật
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các textbox
                string name = textBox3.Text.Trim(); // Name vừa là tên đăng nhập vừa là họ tên
                string gioiTinh = textBox7.Text.Trim();
                string soDienThoai = textBox6.Text.Trim();
                string ngaySinh = textBox4.Text.Trim();
                string email = textBox5.Text.Trim();

                // Validation cơ bản
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Vui lòng nhập họ và tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox3.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox5.Focus();
                    return;
                }

                // Xử lý ký tự đặc biệt để tránh SQL Injection
                name = name.Replace("'", "''");
                gioiTinh = gioiTinh.Replace("'", "''");
                soDienThoai = soDienThoai.Replace("'", "''");
                email = email.Replace("'", "''");

                // Xử lý ngày sinh
                string ngaySinhSQL = "NULL";
                if (!string.IsNullOrEmpty(ngaySinh))
                {
                    if (DateTime.TryParse(ngaySinh, out DateTime parsedDate))
                    {
                        ngaySinhSQL = $"'{parsedDate:yyyy-MM-dd}'";
                    }
                    else
                    {
                        MessageBox.Show("Định dạng ngày sinh không hợp lệ! Vui lòng nhập theo định dạng yyyy-MM-dd", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox4.Focus();
                        return;
                    }
                }

                // Cập nhật vào database - CẬP NHẬT TENDANGNHAP (Name)
                KETNOISQL ketNoi = new KETNOISQL();

                // KIỂM TRA ID TÀI KHOẢN TRƯỚC KHI UPDATE
                if (string.IsNullOrEmpty(TaiKhoan.ID))
                {
                    MessageBox.Show("Lỗi: Không tìm thấy ID tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = $@"
            UPDATE TAIKHOAN 
            SET TENDANGNHAP = N'{name}',
                GIOITINH = N'{gioiTinh}',
                SDT = N'{soDienThoai}',
                NGAYSINH = {ngaySinhSQL},
                EMAIL = N'{email}'
            WHERE ID_TAIKHOAN = '{TaiKhoan.ID}'"; // THÊM DẤU NHÁY QUANH ID

                int result = ketNoi.ExecuteNonQuery(query);

                if (result > 0)
                {
                    // Cập nhật thông tin trong session
                    TaiKhoan.Name = name;
                    TaiKhoan.GioiTinh = gioiTinh;
                    TaiKhoan.SoDienThoai = soDienThoai;
                    TaiKhoan.NgaySinh = ngaySinh;
                    TaiKhoan.Email = email;

                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại thông tin ở tab Lịch sử
                    HienThiTenNguoiDung();
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại! Có thể ID tài khoản không tồn tại hoặc có lỗi xảy ra.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cập nhật phương thức HienThiTenNguoiDung để hiển thị tên thật thay vì tên đăng nhập
        private void HienThiTenNguoiDung()
        {
            string tenHienThi = TaiKhoan.Name ?? "";
            string email = TaiKhoan.Email ?? "";

            if (!string.IsNullOrEmpty(tenHienThi))
            {
                txtTenKH_LS.Text = tenHienThi;
                txtEmail_LS.Text = email;
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
    }


}



