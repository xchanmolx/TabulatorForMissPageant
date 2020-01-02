using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabulator.DAL;

namespace Tabulator.UI
{
    public partial class frmAverageTalent : Form
    {
        public frmAverageTalent()
        {
            InitializeComponent();            
        }

        private void frmAverageTalent_Load(object sender, EventArgs e)
        {
            FillDatagridviewAverageTalent();
        }

        AverageTalentDAL talentDatagridviewAverage = new AverageTalentDAL();
        public void FillDatagridviewAverageTalent()
        {
            //Refreshing Data Grid View
            DataTable dt = talentDatagridviewAverage.Select();
            bunifuDataGridAverageTalent.DataSource = dt;

            bunifuDataGridAverageTalent.Columns[1].Width = 90;
            bunifuDataGridAverageTalent.Columns[3].Width = 100;
            bunifuDataGridAverageTalent.Columns[4].Width = 50;

            bunifuDataGridAverageTalent.Columns[0].Visible = false;

            foreach (DataGridViewColumn column in bunifuDataGridAverageTalent.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            bunifuDataGridAverageTalent.Columns[1].HeaderText = "Candidate #";
            bunifuDataGridAverageTalent.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Average DESC";
            DataTable sortedDT = dv.ToTable();
        }

        private void bunifuDataGridAverageTalent_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void btnPrintAverageTalent_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = new PaperSize("Letter", 1100, 850);
                pd.DefaultPageSettings.Landscape = true;
                pd.PrintPage += new PrintPageEventHandler(this.printDocument_PrintPage);

                printPreviewDialog.Document = pd;

                //printPreviewDialog.ShowDialog();

                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while printing", ex.ToString());
            }
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap objBmp = new Bitmap(this.bunifuDataGridAverageTalent.Width, this.bunifuDataGridAverageTalent.Height);
            bunifuDataGridAverageTalent.DrawToBitmap(objBmp, new Rectangle(0, 0, this.bunifuDataGridAverageTalent.Width, this.bunifuDataGridAverageTalent.Height));

            e.Graphics.DrawImage(objBmp, 50, 100);

            e.Graphics.DrawString(lblHead.Text, new Font("Segoe Print", 35, FontStyle.Regular), Brushes.HotPink, new Point(230, 30));

            e.Graphics.DrawString(lblSignatures.Text, new Font("Segoe Print", 11, FontStyle.Regular), Brushes.Black, new Point(50, 650));
            e.Graphics.DrawString(lblSignatureJudge1.Text, new Font("Segoe Print", 11, FontStyle.Regular), Brushes.Black, new Point(150, 700));
            e.Graphics.DrawString(lblSignatureJudge2.Text, new Font("Segoe Print", 11, FontStyle.Regular), Brushes.Black, new Point(400, 700));
            e.Graphics.DrawString(lblSignatureJudge3.Text, new Font("Segoe Print", 11, FontStyle.Regular), Brushes.Black, new Point(650, 700));

            e.Graphics.DrawString(lblRank1.Text, new Font("Arial", 17, FontStyle.Bold), Brushes.Black, new Point(852, 155));
            e.Graphics.DrawString(lblRank2.Text, new Font("Arial", 17, FontStyle.Bold), Brushes.Blue, new Point(852, 185));
            e.Graphics.DrawString(lblRank3.Text, new Font("Arial", 17, FontStyle.Bold), Brushes.Green, new Point(852, 215));
        }        
    }
}
