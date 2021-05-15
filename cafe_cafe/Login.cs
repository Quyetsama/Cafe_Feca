using cafe_cafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cafe_cafe
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (CheckLogin(edtUserName.Text, edtPassWord.Text))
            {
                this.Hide();
                Main fMain = new Main();
                fMain.Show();
            }
            else
            {
                MessageBox.Show("Sai thông tin đăng nhập");
            }
            
        }

        public bool CheckLogin(string username, string password)
        {
            string query = "exec USP_Login @userName , @passWord"; //split ' '
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {username, password});
            return result.Rows.Count > 0;
        }
    }
}
