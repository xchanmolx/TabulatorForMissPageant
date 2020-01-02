using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabulator.BLL;
using Tabulator.DAL;

namespace Tabulator.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        UserBLL user = new UserBLL();
        UserDAL dal = new UserDAL();

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            user.Username = txtUsername.Text.Trim();
            user.Password = txtPassword.Text.Trim();            

            DataTable dt = await dal.Login(user);

            if (dt.Rows.Count == 1)
            {
                if (user.Username == "xchanmolx")
                {
                    frmMain main = new frmMain();
                    this.Hide();
                    main.Show();
                }
                else if (user.Username == "judge1")
                {
                    frmJudge1 judge1 = new frmJudge1();
                    this.Hide();
                    judge1.Show();
                }
                else if (user.Username == "judge2")
                {
                    frmJudge2 judge2 = new frmJudge2();
                    this.Hide();
                    judge2.Show();
                }
                else if (user.Username == "judge3")
                {
                    frmJudge3 judge3 = new frmJudge3();
                    this.Hide();
                    judge3.Show();
                }
                else if (user.Username == "judge4")
                {
                    frmJudge4 judge4 = new frmJudge4();
                    this.Hide();
                    judge4.Show();
                }
                else if (user.Username == "judge5")
                {
                    frmJudge5 judge5 = new frmJudge5();
                    this.Hide();
                    judge5.Show();
                }
            }
            else
            {
                MessageBox.Show("Check your Username and Password!", "Login Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
        }

        private void timerShutdown_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0.0)
            {
                this.Opacity -= 0.3;
            }
            else
            {
                timerShutdown.Stop();
                Application.Exit();
            }
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPassword frm = new frmPassword();
            frm.Show();
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            timerShutdown.Start();
        }
    }
}
