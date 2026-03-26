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
    public partial class GopY : Form
    {
        public GopY()
        {
            InitializeComponent();
        }

        private void btnGUI_GY_Click(object sender, EventArgs e)
        {
            string phanGopY = txtphangopy.Text.Trim();
            string noiDungGopY = txtnd.Text.Trim();
            if (string.IsNullOrEmpty(phanGopY))
            {
                MessageBox.Show("Vui lòng nhập phần bạn muốn góp ý.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtphangopy.Focus();
                return;
            }

            if (string.IsNullOrEmpty(noiDungGopY))
            {
                MessageBox.Show("Vui lòng nhập nội dung góp ý của bạn.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnd.Focus();
                return;
            }

            // 3. Kiểm tra xem người dùng đã chọn Mức độ trải nghiệm chưa (ít nhất 1 RadioButton được chọn)
            if (!rdbhl.Checked && !rdbhl1.Checked && !rdbkhl.Checked)
            {
                MessageBox.Show("Vui lòng chọn mức độ trải nghiệm của bạn.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Chúng tôi cảm ơn sự góp ý và phản hồi của bạn!", "Gửi góp ý thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }

}
//em yeu thay