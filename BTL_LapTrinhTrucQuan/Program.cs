using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_LapTrinhTrucQuan
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool shouldRestart = true;

            while (shouldRestart)
            {
                TaiKhoan.ClearSession();

                using (dangnhap login = new dangnhap())
                {
                    DialogResult loginResult = login.ShowDialog();

                  
                    if (loginResult != DialogResult.OK)
                    {
                        shouldRestart = false;
                        break;
                    }

                    if (TaiKhoan.Quyen == "ADMIN")
                    {
                        using (FormAdmin adminForm = new FormAdmin())
                        {
                            DialogResult adminResult = adminForm.ShowDialog();
                            if (adminResult == DialogResult.Abort || adminResult == DialogResult.Cancel)
                            {
                                shouldRestart = true;
                            }
                            else
                            {
                                shouldRestart = false;
                            }
                        }
                    }
                    else
                    {
                        using (FORMKHACHHANG userForm = new FORMKHACHHANG())
                        {
                            DialogResult userResult = userForm.ShowDialog();
                            if (userResult == DialogResult.Abort || userResult == DialogResult.Cancel)
                            {
                                shouldRestart = true;
                            }
                            else
                            {
                                shouldRestart = false; 
                            }
                        }
                    }
                }
            }
        }
    }
}
