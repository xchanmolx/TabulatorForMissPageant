using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabulator.BLL;
using Tabulator.DAL;

namespace Tabulator
{
    public partial class Judge1BestInTalentUserControl : UserControl
    {
        public Judge1BestInTalentUserControl()
        {
            InitializeComponent();
        }

        JudgeBLL judge1BLL = new JudgeBLL();
        Judge1DAL judge1DAL = new Judge1DAL();
        int id = 1;
        private void btnSend_Click(object sender, EventArgs e)
        {
            float score1, score2, score3;
            float.TryParse(txtScore1.Text, out score1);
            float.TryParse(txtScore2.Text, out score2);
            float.TryParse(txtScore3.Text, out score3);

            // Getting data from UI
            judge1BLL.Score1 = score1;
            judge1BLL.Score2 = score2;
            judge1BLL.Score3 = score3;

            // Inserting data into Database
            bool success = judge1DAL.Insert(judge1BLL);

            // If the data is successfully inserted then the value of success will be true else it will be false
            if (success == true)
            {
                // Data successfully inserted
                MessageBox.Show("Judge1 successfully send.", "Judge1 Information Send Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Clear();
                //txtPatientNo.Focus();
            }
            else
            {
                // Failed to insert data
                MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Refreshing Data Grid View
            //DataTable dt = dal.Select();
            //dgvPCR.DataSource = dt;

            //DataView dv = dt.DefaultView;
            //dv.Sort = "PatientNumber";
            //DataTable sortedDT = dv.ToTable();
        }
    }
}
