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
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        String connectionString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLYRAPCHIEUPHIMLAST;Integrated Security=True";
        public Form1()
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
            String sql = "select CACCHIEU.ID_CACCHIEU,PHIM.TENPHIM,LOAIPHONG.TENLOAIPHONG,PHONGCHIEU.TENPHONG,CACCHIEU.NGAYCHIEU ,CACCHIEU.GIOBATDAU,CACCHIEU.GIOKETTHUC, CACCHIEU.GIA  from CACCHIEU " +
                            "inner join PHONGCHIEU on PHONGCHIEU.ID_PHONGCHIEU = CACCHIEU.ID_PHONGCHIEU " +
                            "inner join LOAIPHONG on LOAIPHONG.ID_LOAIPHONG = PHONGCHIEU.ID_LOAIPHONG " +
                            "inner join PHIM on PHIM.ID_PHIM = CACCHIEU.ID_PHIM";
            cmd = new SqlCommand(sql, con);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dtgridviewSuatChieu.DataSource = dt;
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

        private void btnXoaHetPhim_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM PHIM";
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand(sql, con);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Xóa hết phim thành công!");
                loadDataPhim();
            }
            else
            {
                MessageBox.Show("Xóa hết phim thất bại!");
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
        }

        private void btn_qlysuatchieu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabSuatChieu;
            loadDataCaChieu();
            LoadComboBox(cmboxMaPhimSuatChieu, "SELECT ID_PHIM , TENPHIM FROM PHIM", "TENPHIM", "ID_PHIM");
            LoadComboBox(cmboxPhongSuatChieu, "SELECT ID_PHONGCHIEU , TENPHONG FROM PHONGCHIEU", "TENPHONG", "ID_PHONGCHIEU");
        }

        private void btnXoaSuatChieu_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(connectionString))
            {
                string maphim = cmboxMaPhimSuatChieu.SelectedValue.ToString();
                string sql = "DELETE FROM CACCHIEU WHERE ID_PHIM = @maphim";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@maphim", maphim);
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
                int giave = int.Parse(txtBoxGiaVeSuatChieu.Text);
                string maphim = cmboxMaPhimSuatChieu.SelectedValue.ToString();
                string maphong = cmboxPhongSuatChieu.SelectedValue.ToString();
                string giobatdau = ThoigianbatdauSuatChieu.Value.ToString("HH:mm:ss");
                string gioketthuc = ThoigianketthucSuatChieu.Value.ToString("HH:mm:ss");
                string NgayChieu = ngayChieuSuatChieu.Value.ToString("yyyy-MM-dd");
                string sql = "UPDATE CACCHIEU SET ID_PHONGCHIEU = @maphong, " +
                             "GIOBATDAU = @giobatdau, GIOKETTHUC = @gioketthuc, GIA =@giave, NGAYCHIEU =@ngaychieu   WHERE ID_PHIM = @maphim";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@maphong", maphong);
                cmd.Parameters.AddWithValue("@giobatdau", giobatdau);
                cmd.Parameters.AddWithValue("@gioketthuc", gioketthuc);
                cmd.Parameters.AddWithValue("@giave", giave);
                cmd.Parameters.AddWithValue("@ngaychieu", NgayChieu);
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
                string gioketthuc = ThoigianketthucSuatChieu.Value.ToString("HH:mm:ss");
                string NgayChieu = ngayChieuSuatChieu.Value.ToString("yyyy-MM-dd");
                int giave = int.Parse(txtBoxGiaVeSuatChieu.Text);
                string sql = "INSERT INTO CACCHIEU (ID_PHIM, ID_PHONGCHIEU, GIOBATDAU, GIOKETTHUC, GIA,NGAYCHIEU) " +
                             "VALUES (@maphim, @maphong, @giobatdau, @gioketthuc, @giave,@ngaychieu)";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@maphim", maphim);
                cmd.Parameters.AddWithValue("@maphong", maphong);
                cmd.Parameters.AddWithValue("@giobatdau", giobatdau);
                cmd.Parameters.AddWithValue("@gioketthuc", gioketthuc);
                cmd.Parameters.AddWithValue("@giave", giave);
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
    }
}