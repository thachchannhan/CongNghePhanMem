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
        private void LoadStatusFromDatabase()
        {
            // Ví dụ: truy vấn DB và cập nhật UI cho 36 ghế
            // 1. Lấy dữ liệu từ bảng Ghe
            // 2. Lọc trangThai != 0
            // 3. Cập nhật danhSachGheObject và btn.BackColor
        }

        private void LoadDataPhim()
        {
            // ... (Khởi tạo conn, dtPhim, connectionString như cũ)



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
        
        private void dgvDSPhim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDSPhim.Rows[e.RowIndex];


                txtMaPhim_BDK.Text = row.Cells["ID_PHIM"].Value.ToString();
                txtTenPhim_BDK.Text = row.Cells["TENPHIM"].Value.ToString();
                txtTheLoai_BDK.Text = row.Cells["THELOAI"].Value.ToString();

                object thoiLuongValue = row.Cells["THOILUONG"].Value;
                txtThoiLuong_BDK.Text = (thoiLuongValue == DBNull.Value) ? string.Empty : thoiLuongValue.ToString();
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

       

        
        private void btnXoa_BDK_Click(object sender, EventArgs e)
        {
            txtMaPhim_BDK.Text = string.Empty;
            txtTenPhim_BDK.Text = string.Empty;
            txtTheLoai_BDK.Text = string.Empty;
            txtThoiLuong_BDK.Text = string.Empty;
        }

        public class Ghe
        {
            public int GheId { get; set; }      // Khóa chính trong DB
            public string TenGhe { get; set; } // tham khảo
            public string Hang { get; set; }
            public string Cot { get; set; }
            public int TrangThai { get; set; }  // 1: Trong, 2: DaDat, 0: BoQua
            public int LoaiGhe { get; set; }    // 1: Thuong, 2: VIP
            public bool DaChon { get; set; }
            public bool DaDat { get; set; }
            public Button Nut { get; set; }      // tham chiếu nút trên UI
        }
        Dictionary<string, Ghe> danhSachGheObject;

        void AddGhe()
        {
            // Reset để tránh cộng dồn (nếu AddGhe gọi nhiều lần)
            danhSachGheObject = new Dictionary<string, Ghe>();

            foreach (Control ctrl in panelGhe.Controls)
            {
                if (ctrl is Button btn)
                {
                    string tenGhe = btn.Text;

                    // Tạo hoặc cập nhật đối tượng Ghe
                    var ghe = new Ghe
                    {
                        TenGhe = tenGhe,
                        Hang = GetHangFromTen(tenGhe),
                        Cot = GetCotFromTen(tenGhe),
                        TrangThai = 1, // mặc định trống; sẽ cập nhật từ DB sau
                        LoaiGhe = GetLoaiGheFromTen(tenGhe),
                        DaChon = false,
                        DaDat = false,
                        Nut = btn
                    };

                    danhSachGheObject[tenGhe] = ghe;

                    // Đảm bảo mỗi nút chỉ có một handler
                    btn.Click -= Ghe_Click;
                    btn.Click += Ghe_Click;

                    // Lưu tham chiếu Ghe vào Button.Tag (tùy chọn)
                    btn.Tag = ghe;
                }
            }

            // Nạp trạng thái từ DB (nếu có) và cập nhật UI
            LoadStatusFromDatabase();
        }
        
        private string GetHangFromTen(string ten)
        {
            // Ví dụ: ten = "F2" => Hang = "2"
            // Tuỳ quy ước của bạn có thể điều chỉnh:
            // Lấy chuỗi chữ số ở cuối
            var digits = new string(ten.SkipWhile(c => !char.IsDigit(c)).ToArray());
            return digits;
        }

        private string GetCotFromTen(string ten)
        {
            // Ví dụ: ten = "F2" => Cot = "F" (nút ở cột F)
            // Lấy phần chữ cái ở đầu
            var letters = new string(ten.TakeWhile(c => Char.IsLetter(c)).ToArray());
            return letters;
        }

        private int GetLoaiGheFromTen(string ten)
        {
            // Ví dụ: nếu quy ước TenGhe có chữ cái hoặc số để phân loại
            // Ở đây trả về 1 (Thường) làm mặc định
            // Bạn có thể điều chỉnh dựa trên quy ước của bạn
            return 1;
        }
        private async void Ghe_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                // Lấy Ghe từ Tag (nếu có)
                if (btn.Tag is Ghe ghe)
                {
                    if (ghe.TrangThai == 1) // ghế còn trống
                    {
                        // Toggle chọn
                        ghe.DaChon = !ghe.DaChon;
                        ghe.TrangThai = ghe.DaChon ? 2 : 1;

                        // Cập nhật UI
                        btn.BackColor = ghe.DaChon ? Color.LightBlue : Color.LightYellow;

                        // Lưu DB (nếu có logic lưu)
                        await LuuGheVaoDatabase(ghe);
                    }
                    else
                    {
                        // Ghế đã đặt
                        MessageBox.Show("Ghế đã đặt!");
                    }
                }
            }
        }
        private async Task LuuGheVaoDatabase(Ghe ghe)
        {
            // Cập nhật trạng thái theo GheId
            string query = "UPDATE Ghe SET TrangThai = @TrangThai WHERE GheId = @GheId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TrangThai", ghe.TrangThai);
                cmd.Parameters.AddWithValue("@GheId", ghe.GheId);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
        private void btnXacNhan_KH_Click(object sender, EventArgs e)
        {
            if (rdbPhong2D_KH.Checked || rdbPhong3D_KH.Checked || rdbPhongIMAX_KH.Checked)
            {
                MessageBox.Show("Đã xác nhận phòng.", "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);

             
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

        private void btnDX_Click(object sender, EventArgs e)
        {
            this.Hide();
            dangnhap loginForm = new dangnhap();
            loginForm.ShowDialog();
            this.Show();

        }


    }
}

