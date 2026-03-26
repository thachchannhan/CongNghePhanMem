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
using ClosedXML.Excel;
using Word = Microsoft.Office.Interop.Word;
namespace BTL_LapTrinhTrucQuan
{
    public partial class FormAdmin : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        String connectionString ="Data Source=DESKTOP-V7DI0T1;Initial Catalog=P;Integrated Security=True;";
        public FormAdmin()
        {
            InitializeComponent();
        }

        void loadDataPhim()
        {
            con = new SqlConnection(connectionString);
            con.Open();
            String sql = "SELECT * FROM PHIM";
            cmd = new SqlCommand(sql, con);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dtgridviewPhim.DataSource = dt;
            con.Close();
        }
        void loadDataCaChieu()
        {
            con = new SqlConnection(connectionString);
            con.Open();
            String sql = "select CACCHIEU.ID_CACCHIEU,PHIM.TENPHIM,LOAIPHONG.TENLOAIPHONG,CACCHIEU.NGAYCHIEU ,CACCHIEU.GIOBATDAU,CACCHIEU.GIOKETTHUC  from CACCHIEU " +
                            "inner join LOAIPHONG on LOAIPHONG.ID_LOAIPHONG = CACCHIEU.ID_LOAIPHONG " +
                            "inner join PHIM on PHIM.ID_PHIM = CACCHIEU.ID_PHIM";
            cmd = new SqlCommand(sql, con);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dtgridviewSuatChieu.DataSource = dt;
            con.Close();
        }
        void loadDataHoaDon()
        {
            con = new SqlConnection (connectionString);
            con.Open();
            string sql = $@"
                        SELECT 
                            T.ID_TAIKHOAN AS [Mã KH],
                            T.hoten as [Họ tên KH],
                            T.SDT, 
                            T.EMAIL,
                            H.ID_HOADON AS [Mã HĐ],
                            H.NGAYLAP AS [Ngày Lập HĐ],
                            H.TONGTIEN AS [Tổng Tiền HĐ],
                            H.TRANGTHAI AS [Trạng thái HĐ]
                        FROM 
                            HOADON H
                        JOIN 
                            TAIKHOAN T ON H.ID_TAIKHOAN = T.ID_TAIKHOAN
                        where T.ID_TAiKHOAN <> N'Admin01'  
                        ORDER BY 
                            H.NGAYLAP DESC, H.ID_HOADON";
            cmd = new SqlCommand(sql, con);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dtgridviewKH.DataSource = dt;
            con.Close();
        }
        void LoadComboBox(ComboBox cbo, string query, string display, string value)
        {
            using (con = new SqlConnection(connectionString))
            {
                da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbo.DataSource = dt;
                cbo.DisplayMember = display;
                cbo.ValueMember = value;
            }
        }
        public void ExportDataGridViewToExcel(DataGridView dgv, string filePath)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            for (int col = 0; col < dgv.Columns.Count; col++)
            {
                worksheet.Cell(1, col + 1).Value = dgv.Columns[col].HeaderText;
            }

            for (int row = 0; row < dgv.Rows.Count; row++)
            {
                for (int col = 0; col < dgv.Columns.Count; col++)
                {
                    worksheet.Cell(row + 2, col + 1).Value =
                        dgv.Rows[row].Cells[col].Value?.ToString() ?? "";
                }
            }

            worksheet.Columns().AdjustToContents();
            workbook.SaveAs(filePath);
        }
        public void ExportToWord(DataGridView dgv, string filePath)
        {
            Word.Application wordApp = new Word.Application();
            Word.Document doc = wordApp.Documents.Add();

            Word.Range range = doc.Range();
            Word.Table table = doc.Tables.Add(range, dgv.Rows.Count + 1, dgv.Columns.Count);
            table.Borders.Enable = 1;

            // Header
            for (int col = 0; col < dgv.Columns.Count; col++)
            {
                table.Cell(1, col + 1).Range.Text = dgv.Columns[col].HeaderText;
                table.Cell(1, col + 1).Range.Bold = 1;
            }

            // Data
            for (int row = 0; row < dgv.Rows.Count; row++)
            {
                for (int col = 0; col < dgv.Columns.Count; col++)
                {
                    table.Cell(row + 2, col + 1).Range.Text =
                        dgv.Rows[row].Cells[col].Value?.ToString() ?? "";
                }
            }

            doc.SaveAs2(filePath);
            doc.Close();
            wordApp.Quit();
        }
        private void Menu_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tabPhim_Click(object sender, EventArgs e)
        {

        }

        private void btnthemPhim_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            string tenPhim = txtboxTenPhim.Text;
            //string theLoai = lstboxTheLoaiPhim.SelectedItem.ToString();
            string theLoai = cmboxTheLoaiPhim.SelectedItem.ToString();
            int ThoiLuong = int.Parse(txtThoiLuong.Text);
            //string ThoiLuong = txtThoiLuong.Text;
            string daoDien = txtBoxDaoDien.Text;
            string ngonngu = txtBoxNgonNguPhim.Text;
            string noidung = txtboxNoiDungPhim.Text;
            string dateTimeNgaycongchieu = datetimeNgayCongChieuPhim.Value.ToString("yyyy-MM-dd");
            string sql = "INSERT INTO Phim (TENPHIM, THELOAI, THOILUONG, DAODIEN, NGONNGU, NOIDUNG, NGAYKHOICHIEU) " +
                         "VALUES (@tenPhim, @theLoai, @ThoiLuong, @daoDien, @ngonngu, @noidung, @dateTimeNgaycongchieu)";
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@tenPhim", tenPhim);
            cmd.Parameters.AddWithValue("@theLoai", theLoai);
            cmd.Parameters.AddWithValue("@ThoiLuong", ThoiLuong);
            cmd.Parameters.AddWithValue("@daoDien", daoDien);
            cmd.Parameters.AddWithValue("@ngonngu", ngonngu);
            cmd.Parameters.AddWithValue("@noidung", noidung);
            cmd.Parameters.AddWithValue("@dateTimeNgaycongchieu", dateTimeNgaycongchieu);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Thêm phim thành công!");
                loadDataPhim();
            }
            else
            {
                MessageBox.Show("Thêm phim thất bại!");
            }
        }

        private void btnXoaPhim_Click(object sender, EventArgs e)
        {
            string idPhim = txtboxMaPhim.Text;
            string sql = "DELETE FROM PHIM WHERE ID_PHIM = @idPhim";
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@idPhim", idPhim);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Xóa phim thành công!");
                loadDataPhim();
            }
            else
            {
                MessageBox.Show("Xóa phim thất bại!");
            }
        }

        private void btnCapNhatPhim_Click(object sender, EventArgs e)
        {
            string tenPhim = txtboxTenPhim.Text;
            string theLoai = cmboxTheLoaiPhim.SelectedItem.ToString();
            int ThoiLuong = int.Parse(txtThoiLuong.Text);
            string daoDien = txtBoxDaoDien.Text;
            string ngonngu = txtBoxNgonNguPhim.Text;
            string noidung = txtboxNoiDungPhim.Text;
            string dateTimeNgaycongchieu = datetimeNgayCongChieuPhim.Value.ToString("yyyy-MM-dd");
            string idPhim = txtboxMaPhim.Text;
            string sql = "UPDATE PHIM SET TENPHIM = @tenPhim, THELOAI = @theLoai, THOILUONG = @ThoiLuong, " +
                         "DAODIEN = @daoDien, NGONNGU = @ngonngu, NOIDUNG = @noidung, NGAYKHOICHIEU = @dateTimeNgaycongchieu " +
                         "WHERE ID_PHIM = @idPhim";
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@tenPhim", tenPhim);
            cmd.Parameters.AddWithValue("@theLoai", theLoai);
            cmd.Parameters.AddWithValue("@ThoiLuong", ThoiLuong);
            cmd.Parameters.AddWithValue("@daoDien", daoDien);
            cmd.Parameters.AddWithValue("@ngonngu", ngonngu);
            cmd.Parameters.AddWithValue("@noidung", noidung);
            cmd.Parameters.AddWithValue("@dateTimeNgaycongchieu", dateTimeNgaycongchieu);
            cmd.Parameters.AddWithValue("@idPhim", idPhim);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Cập nhật phim thành công!");
                loadDataPhim();
            }
            else
            {
                MessageBox.Show("Cập nhật phim thất bại!");
            }
        }

        private void lblTheLoaiPhim_Click(object sender, EventArgs e)
        {

        }

        private void lblTenPhim_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_qlyphim_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPhim;
            loadDataPhim();
            cmboxTheLoaiPhim.SelectedIndex = 0;
        }

        private void btn_qlysuatchieu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabSuatChieu;
           
            loadDataCaChieu();
            LoadComboBox(cmboxMaPhimSuatChieu, "SELECT ID_PHIM , TENPHIM FROM PHIM", "TENPHIM", "ID_PHIM");
            LoadComboBox(cmboxPhongSuatChieu, "SELECT ID_LOAIPHONG , TENLOAIPHONG FROM LOAIPHONG", "TENLOAIPHONG", "ID_LOAIPHONG");
        }

        private void btnXoaSuatChieu_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(connectionString))
            {
                string macachieu =txtboxMaCaChieu_SuatChieu.Text;
                string sql = "DELETE FROM CACCHIEU WHERE ID_CACCHIEU = @macachieu";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@macachieu", macachieu);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa suất chiếu thành công!");
                    loadDataCaChieu();
                }
                else
                {
                    MessageBox.Show("Xóa suất chiếu thất bại!");
                }
            }
        }

        private void btnXoaHetSuatChieu_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM CACCHIEU";
                cmd = new SqlCommand(sql, con);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa hết suất chiếu thành công!");
                    loadDataCaChieu();
                }
                else
                {
                    MessageBox.Show("Xóa hết suất chiếu thất bại!");
                }
            }
        }

        private void btnCapNhatSuatChieu_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(connectionString))
            {
                string maCaChieu = txtboxMaCaChieu_SuatChieu.Text;
                string maphim = cmboxMaPhimSuatChieu.SelectedValue.ToString();
                string maphong = cmboxPhongSuatChieu.SelectedValue.ToString();
                string giobatdau = ThoigianbatdauSuatChieu.Value.ToString("HH:mm:ss");
                string NgayChieu = ngayChieuSuatChieu.Value.ToString("yyyy-MM-dd");
                string sql = "UPDATE CACCHIEU SET ID_LOAIPHONG = @maphong, " +
                             "GIOBATDAU = @giobatdau, GIOKETTHUC = DATEADD(Minute,PHIM.THOILUONG + 15,GIOBATDAU), " +
                             " NGAYCHIEU =@ngaychieu , ID_PHIM= @maphim" +
                             " from PHIM  WHERE CACCHIEU.ID_CACCHIEU = @macachieu";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@maphong", maphong);
                cmd.Parameters.AddWithValue("@giobatdau", giobatdau);
                cmd.Parameters.AddWithValue("@ngaychieu", NgayChieu);
                cmd.Parameters.AddWithValue("@macachieu", maCaChieu);
                cmd.Parameters.AddWithValue("@maphim", maphim);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật suất chiếu thành công!");
                    loadDataCaChieu();
                }
                else
                {
                    MessageBox.Show("Cập nhật suất chiếu thất bại!");
                }
            }
        }

        private void btnThemSuatChieu_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(connectionString))
            {
                string maphim = cmboxMaPhimSuatChieu.SelectedValue.ToString();
                string maphong = cmboxPhongSuatChieu.SelectedValue.ToString();
                string giobatdau = ThoigianbatdauSuatChieu.Value.ToString("HH:mm:ss");
                string NgayChieu = ngayChieuSuatChieu.Value.ToString("yyyy-MM-dd");
                string sql = "INSERT INTO CACCHIEU (ID_PHIM, ID_LOAIPHONG, GIOBATDAU,NGAYCHIEU) " +
                             "VALUES (@maphim, @maphong, @giobatdau,@ngaychieu)";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@maphim", maphim);
                cmd.Parameters.AddWithValue("@maphong", maphong);
                cmd.Parameters.AddWithValue("@giobatdau", giobatdau);
                cmd.Parameters.AddWithValue("@ngaychieu", NgayChieu);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm suất chiếu thành công!");
                    loadDataCaChieu();
                }
                else
                {
                    MessageBox.Show("Thêm suất chiếu thất bại!");
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_baocao_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = lbltenPhim_BaoCao;
            LoadComboBox(cmboxTenPhim_BaoCao,"SELECT ID_PHIM , TENPHIM FROM PHIM", "TENPHIM", "ID_PHIM");
            Time_BaoCaoDoanhThu.Enabled = false;

        }

        private void btnXemBaoCao_BaoCaoDoanhThu_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(connectionString))
            {
                con.Open();
                if (rdioNam_BaoCaoDoanhThu.Checked)
                {
                    string sql = "select SUM(TONGTIEN) as N'Số tiền kiếm được' ,year(NGAYLAP) as N'Năm' from HOADON " +
                                    " WHERE TRANGTHAI = 'PAID' " + 
                                    " group by year(NGAYLAP) "; 
                    cmd = new SqlCommand(sql, con);
                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dtgridviewBaoCao.DataSource = dt;
                }
                else if (rdioThang_BaoCaoDoanhThu.Checked)
                {
                    string sql = "select SUM(TONGTIEN) as N'Số tiền kiếm được' ,year(NGAYLAP) as N'Năm' ,month(NGAYLAP) as N'Tháng' from HOADON " +
                                    " WHERE TRANGTHAI = 'PAID' " + 
                                    " group by year(NGAYLAP) ,month(NGAYLAP)";
                    cmd = new SqlCommand(sql, con);
                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dtgridviewBaoCao.DataSource = dt;
                }
                else
                {
                    Time_BaoCaoDoanhThu.Enabled = true;
                    string ngaycuthe = Time_BaoCaoDoanhThu.Value.ToString("yyyy-MM-dd");
                    string sql = "select SUM(TONGTIEN) as N'Số tiền kiếm được' from HOADON " +
                        "WHERE CAST(NGAYLAP AS DATE) = @ngaycuthe AND TRANGTHAI = 'PAID'";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ngaycuthe", ngaycuthe);
                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dtgridviewBaoCao.DataSource = dt;
                }
            }
        }

        private void rdioNgay_BaoCaoDoanhTHu_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_XemDoanhThuTHeoPhim_Click(object sender, EventArgs e)
        {
            using(con = new SqlConnection(connectionString))
            {
                con.Open();
                string maphim = cmboxTenPhim_BaoCao.SelectedValue.ToString();
                string sql = "select SUM(HOADON.TONGTIEN) as N'Tổng doanh thu' , PHIM.TENPHIM as N'Tên phim' from HOADON " +
                                "inner join VE on VE.ID_HOADON = HOADON.ID_HOADON " +
                                "inner join CACCHIEU on CACCHIEU.ID_CACCHIEU = VE.ID_VE " +
                                "inner join PHIM on PHIM.ID_PHIM = CACCHIEU.ID_PHIM " +
                                "WHERE PHIM.ID_PHIM = @maphim AND HOADON.TRANGTHAI = 'PAID' " +
                                "group by PHIM.TENPHIM";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@maphim", maphim);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtgridviewBaoCao.DataSource = dt;
            }
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btn_timkiem_QLKH_Click(object sender, EventArgs e)
        {
            string matk = txtMAKH.Text;
            string hotenkh = txtHOTEN.Text;
            string SDT = txtSDT.Text;
            string email = txtEMAIL.Text;
            string sql = $@"
                        SELECT 
                            T.ID_TAIKHOAN AS [Mã KH],
                            T.hoten as [Họ tên KH],
                            T.SDT, 
                            T.EMAIL,
                            H.ID_HOADON AS [Mã HĐ],
                            H.NGAYLAP AS [Ngày Lập HĐ],
                            H.TONGTIEN AS [Tổng Tiền HĐ],
                            H.TRANGTHAI AS [Trạng thái HĐ]
                        FROM 
                            HOADON H
                        JOIN 
                            TAIKHOAN T ON H.ID_TAIKHOAN = T.ID_TAIKHOAN
                        where T.ID_TAiKHOAN <> N'Admin01' and  (T.ID_TAIKHOAN =@matk or T.hoten =@ht or T.sdt=@sdt or T.EMAIL = @email)
                        ORDER BY 
                            H.NGAYLAP DESC, H.ID_HOADON";
            using (con = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@matk", matk);
                cmd.Parameters.AddWithValue("@ht", hotenkh);
                cmd.Parameters.AddWithValue("@sdt", SDT);
                cmd.Parameters.AddWithValue("@email", email);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtgridviewKH.DataSource = dt;
            }
        }

        private void btn_qlkh_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabKhachHang;
            loadDataHoaDon(); 
        }

        private void btn_lammoi_QLKH_Click(object sender, EventArgs e)
        {
            loadDataHoaDon(); 
            txtSDT.Clear();
            txtMAKH.Clear();
            txtHOTEN.Clear();
            txtEMAIL.Clear();
        }

        private void btn_dangxuat_Click(object sender, EventArgs e)
        {

    // Clear session trước
    TaiKhoan.ClearSession();
    
    // Đóng form khách hàng
    this.DialogResult = DialogResult.Abort; // Hoặc DialogResult.Cancel
    this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_XuatDoanhThu_Click(object sender, EventArgs e)
        {
            if (rdio_Excel.Checked)
            {
                saveFileDialog1.Filter ="Excel Files|*.xlsx";
                saveFileDialog1.FileName = "doanhthu.xlsx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog1.FileName;
                    ExportDataGridViewToExcel(dtgridviewBaoCao, filePath);
                    MessageBox.Show("Xuất báo cáo thành công!");
                }
            }
            else
            {
                saveFileDialog1.Filter = "Word Files|*.docx";
                saveFileDialog1.FileName = "doanhthu.docx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog1.FileName;
                    ExportToWord(dtgridviewBaoCao, filePath);
                    MessageBox.Show("Xuất báo cáo thành công!");
                }
            }
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }


    }
}
