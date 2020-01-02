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
    public partial class frmCreateAccount : Form
    {
        public frmCreateAccount()
        {
            InitializeComponent();
        }

        UserBLL user = new UserBLL();
        UserDAL dal = new UserDAL();

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                // Getting data from UI
                user.Name = txtName.Text;
                user.Username = txtUsername.Text;
                user.Password = txtPassword.Text;

                // Inserting data into Database
                bool success = dal.Insert(user);

                // If the data is successfully inserted then the value of success will be true else it will be false
                if (success == true)
                {
                    // Data successfully inserted
                    MessageBox.Show("User successfully created.", "User Created Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();

                    this.Hide();
                    if (success == true)
                    {
                        frmLogin frm = new frmLogin();
                        frm.Close();
                    }
                    else
                    {
                        frmLogin frm = new frmLogin();
                        frm.Show();
                    }
                }
                else
                {
                    // Failed to insert data
                    MessageBox.Show("Failed to add new user.", "User Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Refreshing Data Grid View
                //DataTable dt = dal.Select();
                //dgvUsers.DataSource = dt;
            }
        }

        private bool ValidateForm()
        {
            bool bStatus = true;

            if (txtName.Text == "")
            {
                errorProviderCreateAccount.SetError(txtName, "Required Name");
                bStatus = false;
            }
            else if (txtUsername.Text == "")
            {
                errorProviderCreateAccount.SetError(txtUsername, "Required Username");
                bStatus = false;
            }
            else if (txtPassword.Text == "")
            {
                errorProviderCreateAccount.SetError(txtPassword, "Required Password");
                bStatus = false;
            }
            else
            {
                errorProviderCreateAccount.Clear();
            }

            return bStatus;
        }

        private void Clear()
        {
            txtName.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
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
                this.Close();
            }
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            timerShutdown.Start();
        }
    }
}
