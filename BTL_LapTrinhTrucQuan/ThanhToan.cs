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

            // Nếu bạn muốn tạo mã QR (như trong ảnh 2), bạn sẽ dùng soTien để tạo mã QR tại đây
            // (Đây là logic phức tạp hơn, cần thư viện như QRCoder)
            // string dataQRCode = $"TongTien={soTien}";
            // ... (Code tạo và hiển thị mã QR)
        }

        // Xử lý nút "Quay về" (Đóng Form này)
        private void btnQuayVe_ThanhToan_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ThanhToan_Load(object sender, EventArgs e)
        {

        }

      
    }
}
