using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabulator.BLL;
using Tabulator.DAL;

namespace Tabulator.UI
{
    public partial class frmPassword : Form
    {
        public frmPassword()
        {
            InitializeComponent();
        }

        UserBLL user = new UserBLL();
        UserDAL dal = new UserDAL();

        private void btnVerifyPassword_Click(object sender, EventArgs e)
        {
            user.Password = txtPassword.Text.Trim();

            DataTable dt = dal.PasswordVerify(user);

            if (dt.Rows.Count == 1)
            {
                frmCreateAccount frm = new frmCreateAccount();
                this.Hide();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Check your Password!", "Password Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
