using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_LapTrinhTrucQuan
{
    internal class KETNOISQL
    {
        private readonly string connectionString;

        // 🧱 Hàm khởi tạo - thay chuỗi kết nối của bạn vào đây
        public KETNOISQL()
        {
            connectionString = "Data Source=DESKTOP-V7DI0T1;Initial Catalog=P;Integrated Security=True;";
        }

        // 📥 Lấy dữ liệu (SELECT)
        public DataTable GetData(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // 🧾 Thực thi lệnh (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        // ⚡ Thực thi câu lệnh trả về 1 giá trị (VD: SELECT COUNT(*))
        public object ExecuteScalar(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
        // Đặt hàm này trong lớp KETNOISQL
        public DataTable GetAvailablePhongForPhim(string idPhim)
        {
            // Truy vấn DISTINCT các ID_LOAIPHONG và TENLOAIPHONG 
            // có suất chiếu cho ID_PHIM này
            string query = $@"
        SELECT DISTINCT LP.ID_LOAIPHONG, LP.TENLOAIPHONG
        FROM CACCHIEU CC
        JOIN LOAIPHONG LP ON CC.ID_LOAIPHONG = LP.ID_LOAIPHONG
        WHERE 
            CC.ID_PHIM = {idPhim} 
            AND CONVERT(DATETIME, CC.NGAYCHIEU) + CONVERT(DATETIME, CC.GIOKETTHUC) > GETDATE() -- Suất chiếu chưa kết thúc
            AND CC.TRANGTHAI = 1
        ORDER BY LP.ID_LOAIPHONG";

            // Giả sử GetData(string query) là phương thức thực thi SQL trong KETNOISQL
            return GetData(query);
        }
    }
}

