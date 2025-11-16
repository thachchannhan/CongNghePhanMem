using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_LapTrinhTrucQuan
{
    public partial class ThanhToan : Form
    {
        public ThanhToan()
        {
            InitializeComponent();
        }

        public void LoadThongTin(long soTien)
        {
            // Hiển thị tổng tiền lên TextBox trong Form ThanhToan
            txtTongTien_ThanhToan.Text = soTien.ToString("N0") + " VNĐ";
        }

        // Xử lý nút "Thanh toán" - Giả lập thanh toán thành công
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn ngân hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Giả lập thanh toán thành công
            MessageBox.Show($"Thanh toán qua {comboBox1.Text} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Đóng form và trả về kết quả OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Xử lý nút "Quay về" - Đóng form và quay về
        private void btnQuayVe_ThanhToan_Click(object sender, EventArgs e)
        {
            // Đóng form và trả về kết quả Cancel
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            // Có thể thêm các thiết lập khởi tạo khác nếu cần
        }
    }
}