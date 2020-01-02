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
    public partial class frmTotalEveningGown : Form
    {
        public frmTotalEveningGown()
        {
            InitializeComponent();
        }

        private void frmTotalEveningGown_Load(object sender, EventArgs e)
        {
            FillDatagridviewTotalEveningGown();
        }

        TotalEveningGownDAL eveningGownDatagridviewTotal = new TotalEveningGownDAL();
        public void FillDatagridviewTotalEveningGown()
        {
            //Refreshing Data Grid View
            DataTable dt = eveningGownDatagridviewTotal.Select();
            bunifuDataGridTotalEveningGown.DataSource = dt;

            bunifuDataGridTotalEveningGown.Columns[1].Width = 90;
            bunifuDataGridTotalEveningGown.Columns[3].Width = 100;
            bunifuDataGridTotalEveningGown.Columns[4].Width = 50;

            bunifuDataGridTotalEveningGown.Columns[0].Visible = false;

            foreach (DataGridViewColumn column in bunifuDataGridTotalEveningGown.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            bunifuDataGridTotalEveningGown.Columns[1].HeaderText = "Candidate #";
            bunifuDataGridTotalEveningGown.Columns[2].HeaderText = "Candidate Name";
            bunifuDataGridTotalEveningGown.Columns[3].HeaderText = "Total Score";

            DataView dv = dt.DefaultView;
            dv.Sort = "Total DESC";
            DataTable sortedDT = dv.ToTable();
        }

        private void bunifuDataGridTotalEveningGown_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void btnPrintTotalEveningGown_Click(object sender, EventArgs e)
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
            Bitmap objBmp = new Bitmap(this.bunifuDataGridTotalEveningGown.Width, this.bunifuDataGridTotalEveningGown.Height);
            bunifuDataGridTotalEveningGown.DrawToBitmap(objBmp, new Rectangle(0, 0, this.bunifuDataGridTotalEveningGown.Width, this.bunifuDataGridTotalEveningGown.Height));

            e.Graphics.DrawImage(objBmp, 50, 100);

            e.Graphics.DrawString(lblHead.Text, new Font("Segoe Print", 35, FontStyle.Regular), Brushes.HotPink, new Point(130, 30));

            e.Graphics.DrawString(lblSignatures.Text, new Font("Segoe Print", 11, FontStyle.Regular), Brushes.Black, new Point(50, 650));
            e.Graphics.DrawString(lblSignatureJudge1.Text, new Font("Segoe Print", 11, FontStyle.Regular), Brushes.Black, new Point(150, 700));
            e.Graphics.DrawString(lblSignatureJudge2.Text, new Font("Segoe Print", 11, FontStyle.Regular), Brushes.Black, new Point(330, 700));
            e.Graphics.DrawString(lblSignatureJudge3.Text, new Font("Segoe Print", 11, FontStyle.Regular), Brushes.Black, new Point(510, 700));
            e.Graphics.DrawString(lblSignatureJudge4.Text, new Font("Segoe Print", 11, FontStyle.Regular), Brushes.Black, new Point(690, 700));
            e.Graphics.DrawString(lblSignatureJudge5.Text, new Font("Segoe Print", 11, FontStyle.Regular), Brushes.Black, new Point(870, 700));

            e.Graphics.DrawString(lblRank1.Text, new Font("Arial", 17, FontStyle.Bold), Brushes.Black, new Point(852, 155));
            e.Graphics.DrawString(lblRank2.Text, new Font("Arial", 17, FontStyle.Bold), Brushes.Blue, new Point(852, 185));
            e.Graphics.DrawString(lblRank3.Text, new Font("Arial", 17, FontStyle.Bold), Brushes.Green, new Point(852, 215));
            e.Graphics.DrawString(lblRank4.Text, new Font("Arial", 17, FontStyle.Bold), Brushes.Yellow, new Point(852, 247));
            e.Graphics.DrawString(lblRank5.Text, new Font("Arial", 17, FontStyle.Bold), Brushes.DarkViolet, new Point(852, 278));
        }
    }
}
