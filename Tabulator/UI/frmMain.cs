using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabulator.BLL;
using Tabulator.DAL;
using Tabulator.UI;

namespace Tabulator
{
    public partial class frmMain : Form
    {
        //private int changeCount = 0;
        //private const string statusMessage = "{0} realtime changes have occurred.";
        private DataSet dataToWatch = null;
        private SqlConnection connection = null;
        private SqlCommand command = null;

        public frmMain()
        {
            InitializeComponent();

            btnStartRealtime.Enabled = DoesUserHavePermission();
        }

        private bool DoesUserHavePermission()
        {
            try
            {
                SqlClientPermission clientPermission = new SqlClientPermission(PermissionState.Unrestricted);
                clientPermission.Demand();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnStartRealtime_Click(object sender, EventArgs e)
        {
            try
            {
                //changeCount = 0;
                //lblChanges.Text = String.Format(statusMessage, changeCount);

                SqlDependency.Stop(GetConnectionString());
                SqlDependency.Start(GetConnectionString());

                if (connection == null)
                {
                    connection = new SqlConnection(GetConnectionString());
                }

                if (command == null)
                {
                    command = new SqlCommand(GetSQL(), connection);
                }
                if (dataToWatch == null)
                {
                    dataToWatch = new DataSet();
                }

                GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Start Realtime Button Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public string GetConnectionString()
        {
            return UserDAL.myconnstrng;
        }

        private string GetSQL()
        {
            return "SELECT [Score] FROM dbo.tbl_Dependency";
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //MessageBox.Show("modification Occurred");
            ISynchronizeInvoke i = (ISynchronizeInvoke)this;
            if (i.InvokeRequired)
            {
                OnChangeEventHandler tempDelegate = new OnChangeEventHandler(dependency_OnChange);
                object[] args = { sender, e };
                i.BeginInvoke(tempDelegate, args);
                return;
            }

            SqlDependency dependency = (SqlDependency)sender;
            dependency.OnChange -= dependency_OnChange;
            //++changeCount;
            //lblChanges.Text = String.Format(statusMessage, changeCount);

            GetData();

            if (cmbCategories.SelectedIndex == 1)
            {
                LoadDataBestInTalent();
            }
            else if (cmbCategories.SelectedIndex == 2)
            {
                LoadDataBestInProductionNo();
            }
            else if (cmbCategories.SelectedIndex == 3)
            {
                LoadDataBestInEveningGown();
            }
            else if (cmbCategories.SelectedIndex == 4)
            {
                LoadDataBestInResortWear();
            }
            else if (cmbCategories.SelectedIndex == 5)
            {
                LoadDataOnStageQuestions();
            }
            else if (cmbCategories.SelectedIndex == 6)
            {
                LoadDataTop5();
            }
        }

        private void GetData()
        {
            //dataToWatch.Clear();
            //command.Notification = null;
            dataToWatch.Clear();
            command.Notification = null;
            SqlDependency dependency = new SqlDependency(command);
            if (connection.State != ConnectionState.Open) connection.Open();
            using (var dr = command.ExecuteReader())
            {
                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
            }
        }

        public void frmMain_Load(object sender, EventArgs e)
        {
            FillPictureBoxWelcomeForm();

            FillLabelAdminName();
            FillcmbCategories();
            FillcmbResults();

            cmbCategories.Focus();

            btnStartRealtime_Click(sender, e);
        }                

        private void FillLabelAdminName()
        {
            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT AdminName FROM tbl_JudgesAndAdmin";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();

                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        // Admin Name
                        read.Read();
                        lblAdminName.Text = (read.GetValue(0).ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        DataSet ds1, ds2;
        private void FillcmbCategories()
        {
            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT CategoryID, CategoryName FROM tbl_Categories";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                try
                {
                    conn.Open();
                    ds1 = new DataSet();
                    sda.Fill(ds1, "tbl_Categories");
                    cmbCategories.ValueMember = "CategoryID";
                    cmbCategories.DisplayMember = "CategoryName";
                    cmbCategories.DataSource = ds1.Tables["tbl_Categories"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void FillcmbResults()
        {
            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT ResultID, ResultName FROM tbl_Results";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                try
                {
                    conn.Open();
                    ds2 = new DataSet();
                    sda.Fill(ds2, "tbl_Results");
                    cmbResults.ValueMember = "ResultID";
                    cmbResults.DisplayMember = "ResultName";
                    cmbResults.DataSource = ds2.Tables["tbl_Results"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategories.SelectedIndex == 0)
            {
                panelWelcomeTabulatorServer.BringToFront();
                lblTabulatorServer.Focus();
            }
            else if (cmbCategories.SelectedIndex == 1) // Best In Talent
            {
                panelBestInTalent.BringToFront();               
                lblHeadTalent.Text = cmbCategories.Text;

                LoadDataBestInTalent();

                bunifuDataGridTalentJudge1.Focus();
            }
            else if (cmbCategories.SelectedIndex == 2) // Best In Production Number
            {
                panelProductionNumber.BringToFront();
                lblHeadProductionNo.Text = cmbCategories.Text;

                LoadDataBestInProductionNo();

                bunifuDataGridProductionNoJudge1.Focus();
            }
            else if (cmbCategories.SelectedIndex == 3) // Best In Evening Gown
            {
                panelEveningGown.BringToFront();
                lblHeadEveningGown.Text = cmbCategories.Text;

                LoadDataBestInEveningGown();

                bunifuDataGridEveningGownJudge1.Focus();
            }
            else if (cmbCategories.SelectedIndex == 4) // Best In Resort Wear
            {
                panelResortWear.BringToFront();
                lblHeadResortWear.Text = cmbCategories.Text;

                LoadDataBestInResortWear();

                bunifuDataGridResortWearJudge1.Focus();
            }
            else if (cmbCategories.SelectedIndex == 5) // On Stage Questions
            {
                panelOnStageQuestions.BringToFront();
                lblHeadOnStageQuestions.Text = cmbCategories.Text;

                LoadDataOnStageQuestions();

                bunifuDataGridOnStageQuestionsJudge1.Focus();
            }
            else if (cmbCategories.SelectedIndex == 6) // Top 5
            {
                panelTop5.BringToFront();
                lblHeadTop5.Text = cmbCategories.Text;

                LoadDataTop5();

                bunifuDataGridTop5Judge1.Focus();
            }
        }

        private void cmbResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbResults.SelectedIndex == 0)
            {
                lblTabulatorServer.Focus();
            }
            else if (cmbResults.SelectedIndex == 1) // Best In Talent
            {
                frmAverageTalent averageTalent = new frmAverageTalent();
                averageTalent.Show();
                averageTalent.lblHead.Text = cmbResults.Text;
                averageTalent.btnPrintAverageTalent.Visible = true;

                averageTalent.lblRank1.Visible = true;
                averageTalent.lblRank2.Visible = true;
                averageTalent.lblRank3.Visible = true;

                averageTalent.FormClosed += FormClosed;
            }
            else if (cmbResults.SelectedIndex == 2) // Best In Production Number
            {
                frmAverageProductionNo averageProductionNo = new frmAverageProductionNo();
                averageProductionNo.Show();
                averageProductionNo.lblHead.Text = cmbResults.Text;
                averageProductionNo.btnPrintAverageProductionNo.Visible = true;

                averageProductionNo.lblRank1.Visible = true;
                averageProductionNo.lblRank2.Visible = true;
                averageProductionNo.lblRank3.Visible = true;
                averageProductionNo.lblRank4.Visible = true;
                averageProductionNo.lblRank5.Visible = true;

                averageProductionNo.FormClosed += FormClosed;
            }
            else if (cmbResults.SelectedIndex == 3) // Best In Evening Gown
            {
                frmAverageEveningGown  averageEveningGown = new frmAverageEveningGown();
                averageEveningGown.Show();
                averageEveningGown.lblHead.Text = cmbResults.Text;
                averageEveningGown.btnPrintAverageEveningGown.Visible = true;

                averageEveningGown.lblRank1.Visible = true;
                averageEveningGown.lblRank2.Visible = true;
                averageEveningGown.lblRank3.Visible = true;
                averageEveningGown.lblRank4.Visible = true;
                averageEveningGown.lblRank5.Visible = true;

                averageEveningGown.FormClosed += FormClosed;
            }
            else if (cmbResults.SelectedIndex == 4) // Best In Resort Wear
            {
                frmAverageResortWear averageResortWear = new frmAverageResortWear();
                averageResortWear.Show();
                averageResortWear.lblHead.Text = cmbResults.Text;
                averageResortWear.btnPrintAverageResortWear.Visible = true;

                averageResortWear.lblRank1.Visible = true;
                averageResortWear.lblRank2.Visible = true;
                averageResortWear.lblRank3.Visible = true;
                averageResortWear.lblRank4.Visible = true;
                averageResortWear.lblRank5.Visible = true;

                averageResortWear.FormClosed += FormClosed;
            }
            else if (cmbResults.SelectedIndex == 5) // On Stage Questions
            {
                frmAverageOnStageQuestions averageOnStageQuestions = new frmAverageOnStageQuestions();
                averageOnStageQuestions.Show();
                averageOnStageQuestions.lblHead.Text = cmbResults.Text;
                averageOnStageQuestions.btnPrintAverageOnStageQuestions.Visible = true;

                averageOnStageQuestions.lblRank1.Visible = true;
                averageOnStageQuestions.lblRank2.Visible = true;
                averageOnStageQuestions.lblRank3.Visible = true;
                averageOnStageQuestions.lblRank4.Visible = true;
                averageOnStageQuestions.lblRank5.Visible = true;

                averageOnStageQuestions.FormClosed += FormClosed;
            }
            else if (cmbResults.SelectedIndex == 6) // Top 5
            {
                frmAverageTop5 averageTop5 = new frmAverageTop5();
                averageTop5.Show();
                averageTop5.lblHead.Text = cmbResults.Text;
                averageTop5.btnPrintAverageTop5.Visible = true;

                averageTop5.lblRank1.Visible = true;
                averageTop5.lblRank2.Visible = true;
                averageTop5.lblRank3.Visible = true;
                averageTop5.lblRank4.Visible = true;
                averageTop5.lblRank5.Visible = true;

                averageTop5.FormClosed += FormClosed;
            }
            else if (cmbResults.SelectedIndex == 7) // Miss Pageant
            {
                frmAverageMissPageant averageMissPageant = new frmAverageMissPageant();
                averageMissPageant.Show();
                averageMissPageant.lblHead.Text = cmbResults.Text;
                averageMissPageant.btnPrintAverageMissPageant.Visible = true;

                averageMissPageant.lblRank1.Visible = true;
                averageMissPageant.lblRank2.Visible = true;
                averageMissPageant.lblRank3.Visible = true;
                averageMissPageant.lblRank4.Visible = true;
                averageMissPageant.lblRank5.Visible = true;

                averageMissPageant.FormClosed += FormClosed;
            }
        }

        new void FormClosed(object sender, FormClosedEventArgs e)
        {
            lblTabulatorServer.Focus();
            cmbResults.SelectedIndex = 0;
        }

        // BEST IN TALENT
        Judge1TalentDAL talentDatagridviewJudge1DAL = new Judge1TalentDAL();
        public void FillDatagridviewBestInTalentJudge1()
        {
            //Refreshing Data Grid View
            DataTable dt = talentDatagridviewJudge1DAL.Select();
            bunifuDataGridTalentJudge1.DataSource = dt;

            bunifuDataGridTalentJudge1.Columns[0].Visible = false;

            bunifuDataGridTalentJudge1.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridTalentJudge1.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge2TalentDAL talentDatagridviewJudge2DAL = new Judge2TalentDAL();
        public void FillDatagridviewBestInTalentJudge2()
        {
            //Refreshing Data Grid View
            DataTable dt = talentDatagridviewJudge2DAL.Select();
            bunifuDataGridTalentJudge2.DataSource = dt;

            bunifuDataGridTalentJudge2.Columns[0].Visible = false;

            bunifuDataGridTalentJudge2.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridTalentJudge2.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge3TalentDAL talentDatagridviewJudge3DAL = new Judge3TalentDAL();
        public void FillDatagridviewBestInTalentJudge3()
        {
            //Refreshing Data Grid View
            DataTable dt = talentDatagridviewJudge3DAL.Select();
            bunifuDataGridTalentJudge3.DataSource = dt;

            bunifuDataGridTalentJudge3.Columns[0].Visible = false;

            bunifuDataGridTalentJudge3.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridTalentJudge3.Columns[2].HeaderText = "Candidate Name";
            
            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        //Judge4TalentDAL talentDatagridviewJudge4DAL = new Judge4TalentDAL();
        //public void FillDatagridviewBestInTalentJudge4()
        //{
        //    //Refreshing Data Grid View
        //    DataTable dt = talentDatagridviewJudge4DAL.Select();
        //    bunifuDataGridTalentJudge4.DataSource = dt;

        //    bunifuDataGridTalentJudge4.Columns[0].Visible = false;

        //    bunifuDataGridTalentJudge4.Columns[1].HeaderText = "Cand No.";
        //    bunifuDataGridTalentJudge4.Columns[2].HeaderText = "Candidate Name";
            
        //    DataView dv = dt.DefaultView;
        //    dv.Sort = "Score DESC";
        //    DataTable sortedDT = dv.ToTable();
        //}

        //Judge5TalentDAL talentDatagridviewJudge5DAL = new Judge5TalentDAL();
        //public void FillDatagridviewBestInTalentJudge5()
        //{
        //    //Refreshing Data Grid View
        //    DataTable dt = talentDatagridviewJudge5DAL.Select();
        //    bunifuDataGridTalentJudge5.DataSource = dt;

        //    bunifuDataGridTalentJudge5.Columns[0].Visible = false;

        //    bunifuDataGridTalentJudge5.Columns[1].HeaderText = "Cand No.";
        //    bunifuDataGridTalentJudge5.Columns[2].HeaderText = "Candidate Name";
            
        //    DataView dv = dt.DefaultView;
        //    dv.Sort = "Score DESC";
        //    DataTable sortedDT = dv.ToTable();
        //}

        public void LoadDataBestInTalent()
        {
            FillDatagridviewBestInTalentJudge1();
            FillDatagridviewBestInTalentJudge2();
            FillDatagridviewBestInTalentJudge3();
            //FillDatagridviewBestInTalentJudge4();
            //FillDatagridviewBestInTalentJudge5();
        }

        // BEST IN PRODUCTION NUMBER
        Judge1ProductionNoDAL productionNoDatagridviewJudge1DAL = new Judge1ProductionNoDAL();
        public void FillDatagridviewBestInProductionNoJudge1()
        {
            //Refreshing Data Grid View
            DataTable dt = productionNoDatagridviewJudge1DAL.Select();
            bunifuDataGridProductionNoJudge1.DataSource = dt;

            bunifuDataGridProductionNoJudge1.Columns[0].Visible = false;

            bunifuDataGridProductionNoJudge1.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridProductionNoJudge1.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge2ProductionNoDAL productionNoDatagridviewJudge2DAL = new Judge2ProductionNoDAL();
        public void FillDatagridviewBestInProductionNoJudge2()
        {
            //Refreshing Data Grid View
            DataTable dt = productionNoDatagridviewJudge2DAL.Select();
            bunifuDataGridProductionNoJudge2.DataSource = dt;

            bunifuDataGridProductionNoJudge2.Columns[0].Visible = false;

            bunifuDataGridProductionNoJudge2.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridProductionNoJudge2.Columns[2].HeaderText = "Candidate Name";
            
            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge3ProductionNoDAL productionNoDatagridviewJudge3DAL = new Judge3ProductionNoDAL();
        public void FillDatagridviewBestInProductionNoJudge3()
        {
            //Refreshing Data Grid View
            DataTable dt = productionNoDatagridviewJudge3DAL.Select();
            bunifuDataGridProductionNoJudge3.DataSource = dt;

            bunifuDataGridProductionNoJudge3.Columns[0].Visible = false;

            bunifuDataGridProductionNoJudge3.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridProductionNoJudge3.Columns[2].HeaderText = "Candidate Name";
            
            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge4ProductionNoDAL productionNoDatagridviewJudge4DAL = new Judge4ProductionNoDAL();
        public void FillDatagridviewBestInProductionNoJudge4()
        {
            //Refreshing Data Grid View
            DataTable dt = productionNoDatagridviewJudge4DAL.Select();
            bunifuDataGridProductionNoJudge4.DataSource = dt;

            bunifuDataGridProductionNoJudge4.Columns[0].Visible = false;

            bunifuDataGridProductionNoJudge4.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridProductionNoJudge4.Columns[2].HeaderText = "Candidate Name";
            
            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge5ProductionNoDAL productionNoDatagridviewJudge5DAL = new Judge5ProductionNoDAL();
        public void FillDatagridviewBestInProductionNoJudge5()
        {
            //Refreshing Data Grid View
            DataTable dt = productionNoDatagridviewJudge5DAL.Select();
            bunifuDataGridProductionNoJudge5.DataSource = dt;

            bunifuDataGridProductionNoJudge5.Columns[0].Visible = false;

            bunifuDataGridProductionNoJudge5.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridProductionNoJudge5.Columns[2].HeaderText = "Candidate Name";
            
            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        public void LoadDataBestInProductionNo()
        {
            FillDatagridviewBestInProductionNoJudge1();
            FillDatagridviewBestInProductionNoJudge2();
            FillDatagridviewBestInProductionNoJudge3();
            FillDatagridviewBestInProductionNoJudge4();
            FillDatagridviewBestInProductionNoJudge5();
        }

        // BEST IN EVENING GOWN
        Judge1EveningGownDAL eveningGownDatagridviewJudge1DAL = new Judge1EveningGownDAL();
        public void FillDatagridviewBestInEveningGownJudge1()
        {
            //Refreshing Data Grid View
            DataTable dt = eveningGownDatagridviewJudge1DAL.Select();
            bunifuDataGridEveningGownJudge1.DataSource = dt;

            bunifuDataGridEveningGownJudge1.Columns[0].Visible = false;

            bunifuDataGridEveningGownJudge1.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridEveningGownJudge1.Columns[2].HeaderText = "Candidate Name";
            
            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge2EveningGownDAL eveningGownDatagridviewJudge2DAL = new Judge2EveningGownDAL();
        public void FillDatagridviewBestInEveningGownJudge2()
        {
            //Refreshing Data Grid View
            DataTable dt = eveningGownDatagridviewJudge2DAL.Select();
            bunifuDataGridEveningGownJudge2.DataSource = dt;

            bunifuDataGridEveningGownJudge2.Columns[0].Visible = false;

            bunifuDataGridEveningGownJudge2.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridEveningGownJudge2.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge3EveningGownDAL eveningGownDatagridviewJudge3DAL = new Judge3EveningGownDAL();
        public void FillDatagridviewBestInEveningGownJudge3()
        {
            //Refreshing Data Grid View
            DataTable dt = eveningGownDatagridviewJudge3DAL.Select();
            bunifuDataGridEveningGownJudge3.DataSource = dt;

            bunifuDataGridEveningGownJudge3.Columns[0].Visible = false;

            bunifuDataGridEveningGownJudge3.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridEveningGownJudge3.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge4EveningGownDAL eveningGownDatagridviewJudge4DAL = new Judge4EveningGownDAL();
        public void FillDatagridviewBestInEveningGownJudge4()
        {
            //Refreshing Data Grid View
            DataTable dt = eveningGownDatagridviewJudge4DAL.Select();
            bunifuDataGridEveningGownJudge4.DataSource = dt;

            bunifuDataGridEveningGownJudge4.Columns[0].Visible = false;

            bunifuDataGridEveningGownJudge4.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridEveningGownJudge4.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge5EveningGownDAL eveningGownDatagridviewJudge5DAL = new Judge5EveningGownDAL();
        public void FillDatagridviewBestInEveningGownJudge5()
        {
            //Refreshing Data Grid View
            DataTable dt = eveningGownDatagridviewJudge5DAL.Select();
            bunifuDataGridEveningGownJudge5.DataSource = dt;

            bunifuDataGridEveningGownJudge5.Columns[0].Visible = false;

            bunifuDataGridEveningGownJudge5.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridEveningGownJudge5.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        public void LoadDataBestInEveningGown()
        {
            FillDatagridviewBestInEveningGownJudge1();
            FillDatagridviewBestInEveningGownJudge2();
            FillDatagridviewBestInEveningGownJudge3();
            FillDatagridviewBestInEveningGownJudge4();
            FillDatagridviewBestInEveningGownJudge5();
        }

        // Best In Resort Wear
        Judge1ResortWearDAL resortWearDatagridviewJudge1DAL = new Judge1ResortWearDAL();
        public void FillDatagridviewBestInResortWearJudge1()
        {
            //Refreshing Data Grid View
            DataTable dt = resortWearDatagridviewJudge1DAL.Select();
            bunifuDataGridResortWearJudge1.DataSource = dt;

            bunifuDataGridResortWearJudge1.Columns[0].Visible = false;

            bunifuDataGridResortWearJudge1.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridResortWearJudge1.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge2ResortWearDAL resortWearDatagridviewJudge2DAL = new Judge2ResortWearDAL();
        public void FillDatagridviewBestInResortWearJudge2()
        {
            //Refreshing Data Grid View
            DataTable dt = resortWearDatagridviewJudge2DAL.Select();
            bunifuDataGridResortWearJudge2.DataSource = dt;

            bunifuDataGridResortWearJudge2.Columns[0].Visible = false;

            bunifuDataGridResortWearJudge2.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridResortWearJudge2.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge3ResortWearDAL resortWearDatagridviewJudge3DAL = new Judge3ResortWearDAL();
        public void FillDatagridviewBestInResortWearJudge3()
        {
            //Refreshing Data Grid View
            DataTable dt = resortWearDatagridviewJudge3DAL.Select();
            bunifuDataGridResortWearJudge3.DataSource = dt;

            bunifuDataGridResortWearJudge3.Columns[0].Visible = false;

            bunifuDataGridResortWearJudge3.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridResortWearJudge3.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge4ResortWearDAL resortWearDatagridviewJudge4DAL = new Judge4ResortWearDAL();
        public void FillDatagridviewBestInResortWearJudge4()
        {
            //Refreshing Data Grid View
            DataTable dt = resortWearDatagridviewJudge4DAL.Select();
            bunifuDataGridResortWearJudge4.DataSource = dt;

            bunifuDataGridResortWearJudge4.Columns[0].Visible = false;

            bunifuDataGridResortWearJudge4.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridResortWearJudge4.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge5ResortWearDAL resortWearDatagridviewJudge5DAL = new Judge5ResortWearDAL();
        public void FillDatagridviewBestInResortWearJudge5()
        {
            //Refreshing Data Grid View
            DataTable dt = resortWearDatagridviewJudge5DAL.Select();
            bunifuDataGridResortWearJudge5.DataSource = dt;

            bunifuDataGridResortWearJudge5.Columns[0].Visible = false;

            bunifuDataGridResortWearJudge5.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridResortWearJudge5.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        public void LoadDataBestInResortWear()
        {
            FillDatagridviewBestInResortWearJudge1();
            FillDatagridviewBestInResortWearJudge2();
            FillDatagridviewBestInResortWearJudge3();
            FillDatagridviewBestInResortWearJudge4();
            FillDatagridviewBestInResortWearJudge5();
        }

        // On Stage Questions
        Judge1OnStageQuestionsDAL onStageQuestionsDatagridviewJudge1DAL = new Judge1OnStageQuestionsDAL();
        public void FillDatagridviewOnStageQuestionsJudge1()
        {
            //Refreshing Data Grid View
            DataTable dt = onStageQuestionsDatagridviewJudge1DAL.Select();
            bunifuDataGridOnStageQuestionsJudge1.DataSource = dt;

            bunifuDataGridOnStageQuestionsJudge1.Columns[0].Visible = false;

            bunifuDataGridOnStageQuestionsJudge1.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridOnStageQuestionsJudge1.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge2OnStageQuestionsDAL onStageQuestionsDatagridviewJudge2DAL = new Judge2OnStageQuestionsDAL();
        public void FillDatagridviewOnStageQuestionsJudge2()
        {
            //Refreshing Data Grid View
            DataTable dt = onStageQuestionsDatagridviewJudge2DAL.Select();
            bunifuDataGridOnStageQuestionsJudge2.DataSource = dt;

            bunifuDataGridOnStageQuestionsJudge2.Columns[0].Visible = false;

            bunifuDataGridOnStageQuestionsJudge2.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridOnStageQuestionsJudge2.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge3OnStageQuestionsDAL onStageQuestionsDatagridviewJudge3DAL = new Judge3OnStageQuestionsDAL();
        public void FillDatagridviewOnStageQuestionsJudge3()
        {
            //Refreshing Data Grid View
            DataTable dt = onStageQuestionsDatagridviewJudge3DAL.Select();
            bunifuDataGridOnStageQuestionsJudge3.DataSource = dt;

            bunifuDataGridOnStageQuestionsJudge3.Columns[0].Visible = false;

            bunifuDataGridOnStageQuestionsJudge3.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridOnStageQuestionsJudge3.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge4OnStageQuestionsDAL onStageQuestionsDatagridviewJudge4DAL = new Judge4OnStageQuestionsDAL();
        public void FillDatagridviewOnStageQuestionsJudge4()
        {
            //Refreshing Data Grid View
            DataTable dt = onStageQuestionsDatagridviewJudge4DAL.Select();
            bunifuDataGridOnStageQuestionsJudge4.DataSource = dt;

            bunifuDataGridOnStageQuestionsJudge4.Columns[0].Visible = false;

            bunifuDataGridOnStageQuestionsJudge4.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridOnStageQuestionsJudge4.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge5OnStageQuestionsDAL onStageQuestionsDatagridviewJudge5DAL = new Judge5OnStageQuestionsDAL();
        public void FillDatagridviewOnStageQuestionsJudge5()
        {
            //Refreshing Data Grid View
            DataTable dt = onStageQuestionsDatagridviewJudge5DAL.Select();
            bunifuDataGridOnStageQuestionsJudge5.DataSource = dt;

            bunifuDataGridOnStageQuestionsJudge5.Columns[0].Visible = false;

            bunifuDataGridOnStageQuestionsJudge5.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridOnStageQuestionsJudge5.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        public void LoadDataOnStageQuestions()
        {
            FillDatagridviewOnStageQuestionsJudge1();
            FillDatagridviewOnStageQuestionsJudge2();
            FillDatagridviewOnStageQuestionsJudge3();
            FillDatagridviewOnStageQuestionsJudge4();
            FillDatagridviewOnStageQuestionsJudge5();
        }

        // Top 5
        Judge1Top5DAL top5DatagridviewJudge1DAL = new Judge1Top5DAL();
        public void FillDatagridviewTop5Judge1()
        {
            //Refreshing Data Grid View
            DataTable dt = top5DatagridviewJudge1DAL.Select();
            bunifuDataGridTop5Judge1.DataSource = dt;

            bunifuDataGridTop5Judge1.Columns[0].Visible = false;

            bunifuDataGridTop5Judge1.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridTop5Judge1.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge2Top5DAL top5DatagridviewJudge2DAL = new Judge2Top5DAL();
        public void FillDatagridviewTop5Judge2()
        {
            //Refreshing Data Grid View
            DataTable dt = top5DatagridviewJudge2DAL.Select();
            bunifuDataGridTop5Judge2.DataSource = dt;

            bunifuDataGridTop5Judge2.Columns[0].Visible = false;

            bunifuDataGridTop5Judge2.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridTop5Judge2.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge3Top5DAL top5DatagridviewJudge3DAL = new Judge3Top5DAL();
        public void FillDatagridviewTop5Judge3()
        {
            //Refreshing Data Grid View
            DataTable dt = top5DatagridviewJudge3DAL.Select();
            bunifuDataGridTop5Judge3.DataSource = dt;

            bunifuDataGridTop5Judge3.Columns[0].Visible = false;

            bunifuDataGridTop5Judge3.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridTop5Judge3.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge4Top5DAL top5DatagridviewJudge4DAL = new Judge4Top5DAL();
        public void FillDatagridviewTop5Judge4()
        {
            //Refreshing Data Grid View
            DataTable dt = top5DatagridviewJudge4DAL.Select();
            bunifuDataGridTop5Judge4.DataSource = dt;

            bunifuDataGridTop5Judge4.Columns[0].Visible = false;

            bunifuDataGridTop5Judge4.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridTop5Judge4.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        Judge5Top5DAL top5DatagridviewJudge5DAL = new Judge5Top5DAL();
        public void FillDatagridviewTop5Judge5()
        {
            //Refreshing Data Grid View
            DataTable dt = top5DatagridviewJudge5DAL.Select();
            bunifuDataGridTop5Judge5.DataSource = dt;

            bunifuDataGridTop5Judge5.Columns[0].Visible = false;

            bunifuDataGridTop5Judge5.Columns[1].HeaderText = "Cand No.";
            bunifuDataGridTop5Judge5.Columns[2].HeaderText = "Candidate Name";

            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDT = dv.ToTable();
        }

        public void LoadDataTop5()
        {
            FillDatagridviewTop5Judge1();
            FillDatagridviewTop5Judge2();
            FillDatagridviewTop5Judge3();
            FillDatagridviewTop5Judge4();
            FillDatagridviewTop5Judge5();
        }

        JudgesAndAdminBLL judgesAndAdminBLL = new JudgesAndAdminBLL();
        JudgesAndAdminDAL judgesAndAdminDAL = new JudgesAndAdminDAL();
        private void btnUpdateJudgesAndAdminNames_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to update names?", "Update Names!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                // Getting data from UI
                judgesAndAdminBLL.Id = 1;
                judgesAndAdminBLL.Judge1Name = txtJudge1.Text;
                judgesAndAdminBLL.Judge2Name = txtJudge2.Text;
                judgesAndAdminBLL.Judge3Name = txtJudge3.Text;
                judgesAndAdminBLL.Judge4Name = txtJudge4.Text;
                judgesAndAdminBLL.Judge5Name = txtJudge5.Text;
                judgesAndAdminBLL.AdminName = txtAdmin.Text;

                // Updating data into Database
                bool success = judgesAndAdminDAL.Update(judgesAndAdminBLL);

                // Update Admin Name
                FillLabelAdminName();

                // If the data is successfully updated then the value of success will be true else it will be false
                if (success == true)
                {
                    // Data successfully updated
                    MessageBox.Show("Names successfully updated.", "Updated Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Failed to updated data
                    MessageBox.Show("Failed to update new names information.", "Update Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnJudgesAndAdminNames_Click(object sender, EventArgs e)
        {
            if (groupBoxJudgesAndAdmin.Visible == false)
            {
                groupBoxJudgesAndAdmin.BringToFront();
                groupBoxJudgesAndAdmin.Show();
                btnJudgesAndAdminNames.BackColor = Color.Red;
                txtJudge1.Focus();

                cmbCategories.Hide();
                cmbResults.Hide();
            }
            else
            {
                groupBoxJudgesAndAdmin.Hide();
                btnJudgesAndAdminNames.BackColor = Color.PeachPuff;

                cmbCategories.Show();
                cmbResults.Show();
            }
        }

        private void btnCategoriesAndResults_Click(object sender, EventArgs e)
        {
            if (groupBoxCategoriesAndResults.Visible == false)
            {
                groupBoxCategoriesAndResults.BringToFront();
                groupBoxCategoriesAndResults.Show();
                btnCategoriesAndResults.BackColor = Color.Red;
                txtCategoryNo.Focus();
            }
            else
            {
                groupBoxCategoriesAndResults.Hide();
                btnCategoriesAndResults.BackColor = Color.PeachPuff;                
            }
        }

        CategoriesAndResultsBLL catAndResBLL = new CategoriesAndResultsBLL();
        CategoriesAndResultsDAL catAndResDAL = new CategoriesAndResultsDAL();
        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            // Getting data from UI
            catAndResBLL.CategoryNo = Convert.ToInt32(txtCategoryNo.Text);
            catAndResBLL.CategoryName = txtCategoryName.Text;

            // Inserting data into Database
            bool success = catAndResDAL.InsertCategory(catAndResBLL);

            // If the data is successfully inserted then the value of success will be true else it will be false
            if (success == true)
            {
                // Data successfully inserted
                MessageBox.Show("Category successfully saved.", "Category Information Save Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Failed to insert data
                MessageBox.Show("Failed to add new category.", "Category Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to update category name?", "Update Name!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                // Getting data from UI
                catAndResBLL.CategoryNo = Convert.ToInt32(txtCategoryNo.Text);
                catAndResBLL.CategoryName = txtCategoryName.Text;

                // Updating data into Database
                bool success = catAndResDAL.UpdateCategory(catAndResBLL);

                // If the data is successfully updated then the value of success will be true else it will be false
                if (success == true)
                {
                    // Data successfully updated
                    MessageBox.Show("Category successfully updated.", "Updated Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Failed to updated data
                    MessageBox.Show("Failed to update new category information.", "Update Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete category name?", "Delete Category!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                // Getting data from UI
                catAndResBLL.CategoryNo = Convert.ToInt32(txtCategoryNo.Text);

                // Deleting data into Database
                bool success = catAndResDAL.DeleteCategory(catAndResBLL);

                // If the data is successfully deleted then the value of success will be true else it will be false
                if (success == true)
                {
                    // Data successfully deleted
                    MessageBox.Show("Category successfully deleted.", "Delete Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Failed to deleted data
                    MessageBox.Show("Failed to delete new category information.", "Delete Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSaveResult_Click(object sender, EventArgs e)
        {
            // Getting data from UI
            catAndResBLL.ResultNo = Convert.ToInt32(txtResultNo.Text);
            catAndResBLL.ResultName = txtResultName.Text;

            // Inserting data into Database
            bool success = catAndResDAL.InsertResult(catAndResBLL);

            // If the data is successfully inserted then the value of success will be true else it will be false
            if (success == true)
            {
                // Data successfully inserted
                MessageBox.Show("Result successfully saved.", "Result Information Save Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Failed to insert data
                MessageBox.Show("Failed to add new result.", "Result Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdateResult_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to update result name?", "Update Name!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                // Getting data from UI
                catAndResBLL.ResultNo = Convert.ToInt32(txtResultNo.Text);
                catAndResBLL.ResultName = txtResultName.Text;

                // Updating data into Database
                bool success = catAndResDAL.UpdateResult(catAndResBLL);

                // If the data is successfully updated then the value of success will be true else it will be false
                if (success == true)
                {
                    // Data successfully updated
                    MessageBox.Show("Result successfully updated.", "Updated Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Failed to updated data
                    MessageBox.Show("Failed to update new result information.", "Update Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnDeleteResult_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete result name?", "Delete Result!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                // Getting data from UI
                catAndResBLL.ResultNo = Convert.ToInt32(txtResultNo.Text);

                // Deleting data into Database
                bool success = catAndResDAL.DeleteResult(catAndResBLL);

                // If the data is successfully deleted then the value of success will be true else it will be false
                if (success == true)
                {
                    // Data successfully deleted
                    MessageBox.Show("Result successfully deleted.", "Delete Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Failed to deleted data
                    MessageBox.Show("Failed to delete new result information.", "Delete Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Best In Talent
        private void btnBestInTalentAverage_Click(object sender, EventArgs e)
        {
            // Static method to connect Database
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            // To hold the data from Database
            DataTable dt = new DataTable();
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to get the average BEST IN TALENT?", "Get Average BEST IN TALENT!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    // SQL Query to get data from Database
                    string sql = @"DELETE FROM tbl_AverageTalent
                    INSERT INTO tbl_AverageTalent(CandidateNo, CandidateName, Average)

                    SELECT CandidateNo, CandidateName, AVG(Score)Average
                    FROM
                    (
                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge1Talent

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge2Talent

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge3Talent
                    ) t
                    GROUP BY CandidateNo, CandidateName";

                    // For executing command
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Getting data from Database
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    // Database connection open
                    conn.Open();

                    // Fill data in our DataTable
                    sda.Fill(dt);

                    MessageBox.Show("Get Average BEST IN TALENT Successfully!", "Get Average Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Insert data average best in talent from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    // Closing connection
                    conn.Close();
                }
            }
        }        

        private bool DeleteAllBestInTalentTbl_Average()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete all data from tbl_AverageTalent?", "tbl_AverageTalent Deleting All", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    string sql = "DELETE FROM tbl_AverageTalent";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        // Query successful
                        MessageBox.Show("Deleted all data from tbl_AverageTalent successfully!", "Deleting Data Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = true;
                    }
                    else
                    {
                        // Query failed
                        MessageBox.Show("There is no data from tbl_AverageTalent!", "Nothing to delete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Delete data from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    conn.Close();
                }
            }

            return isSuccess;
        }

        private void btnDeleteAllBestInTalentAverageTable_Click(object sender, EventArgs e)
        {
            DeleteAllBestInTalentTbl_Average();
        }

        // Best In Production Number
        private void btnBestInProductionNoAverage_Click(object sender, EventArgs e)
        {
            // Static method to connect Database
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            // To hold the data from Database
            DataTable dt = new DataTable();
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to get the average BEST IN PRODUCTION NUMBER?", "Get Average BEST IN PRODUCTION NUMBER!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    // SQL Query to get data from Database
                    string sql = @"DELETE FROM tbl_AverageProductionNo
                    INSERT INTO tbl_AverageProductionNo(CandidateNo, CandidateName, Average)

                    SELECT CandidateNo, CandidateName, AVG(Score)Average
                    FROM
                    (
                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge1ProductionNo

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge2ProductionNo

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge3ProductionNo

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge4ProductionNo

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge5ProductionNo
                    ) t
                    GROUP BY CandidateNo, CandidateName";

                    // For executing command
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Getting data from Database
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    // Database connection open
                    conn.Open();

                    // Fill data in our DataTable
                    sda.Fill(dt);

                    MessageBox.Show("Get Average BEST IN PRODUCTION NUMBER Successfully!", "Get Average Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Insert data average best in production number from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    // Closing connection
                    conn.Close();
                }
            }
        }        

        private bool DeleteAllBestInProductionNoTbl_Average()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete all data from tbl_AverageProductionNo?", "tbl_AverageProductionNo Deleting All", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    string sql = "DELETE FROM tbl_AverageProductionNo";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        // Query successful
                        MessageBox.Show("Deleted all data from tbl_AverageProductionNo successfully!", "Deleting Data Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = true;
                    }
                    else
                    {
                        // Query failed
                        MessageBox.Show("There is no data from tbl_AverageProductionNo!", "Nothing to delete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Delete data from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    conn.Close();
                }
            }

            return isSuccess;
        }        

        private void btnDeleteAllBestInProductionNoAverageTable_Click(object sender, EventArgs e)
        {
            DeleteAllBestInProductionNoTbl_Average();
        }

        // Best In Evening Gown
        private void btnBestInEveningGownAverage_Click(object sender, EventArgs e)
        {
            // Static method to connect Database
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            // To hold the data from Database
            DataTable dt = new DataTable();
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to get the average BEST IN EVENING GOWN?", "Get Average BEST IN EVENING GOWN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    // SQL Query to get data from Database
                    string sql = @"DELETE FROM tbl_AverageEveningGown
                    INSERT INTO tbl_AverageEveningGown(CandidateNo, CandidateName, Average)

                    SELECT CandidateNo, CandidateName, AVG(Score)Average
                    FROM
                    (
                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge1EveningGown

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge2EveningGown

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge3EveningGown

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge4EveningGown

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge5EveningGown
                    ) t
                    GROUP BY CandidateNo, CandidateName";

                    // For executing command
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Getting data from Database
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    // Database connection open
                    conn.Open();

                    // Fill data in our DataTable
                    sda.Fill(dt);

                    MessageBox.Show("Get Average BEST IN EVENING GOWN Successfully!", "Get Average Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Insert data average best in evening gown from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    // Closing connection
                    conn.Close();
                }
            }
        }        

        private bool DeleteAllBestInEveningGownTbl_Average()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete all data from tbl_AverageEveningGown?", "tbl_AverageEveningGown Deleting All", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    string sql = "DELETE FROM tbl_AverageEveningGown";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        // Query successful
                        MessageBox.Show("Deleted all data from tbl_AverageEveningGown successfully!", "Deleting Data Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = true;
                    }
                    else
                    {
                        // Query failed
                        MessageBox.Show("There is no data from tbl_AverageEveningGown!", "Nothing to delete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Delete data from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    conn.Close();
                }
            }

            return isSuccess;
        }

        private void btnDeleteAllBestInEveningGownAverageTable_Click(object sender, EventArgs e)
        {
            DeleteAllBestInEveningGownTbl_Average();
        }

        // Best In Resort Wear
        private void btnBestInResortWearAverage_Click(object sender, EventArgs e)
        {
            // Static method to connect Database
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            // To hold the data from Database
            DataTable dt = new DataTable();
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to get the average BEST IN RESORT WEAR?", "Get Average BEST IN RESORT WEAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    // SQL Query to get data from Database
                    string sql = @"DELETE FROM tbl_AverageResortWear
                    INSERT INTO tbl_AverageResortWear(CandidateNo, CandidateName, Average)

                    SELECT CandidateNo, CandidateName, AVG(Score)Average
                    FROM
                    (
                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge1ResortWear

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge2ResortWear

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge3ResortWear

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge4ResortWear

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge5ResortWear
                    ) t
                    GROUP BY CandidateNo, CandidateName";

                    // For executing command
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Getting data from Database
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    // Database connection open
                    conn.Open();

                    // Fill data in our DataTable
                    sda.Fill(dt);

                    MessageBox.Show("Get Average BEST IN RESORT WEAR Successfully!", "Get Average Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Insert data average best in resort wear from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    // Closing connection
                    conn.Close();
                }
            }
        }        

        private bool DeleteAllBestInResortWearTbl_Average()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete all data from tbl_AverageResortWear?", "tbl_AverageResortWear Deleting All", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    string sql = "DELETE FROM tbl_AverageResortWear";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        // Query successful
                        MessageBox.Show("Deleted all data from tbl_AverageResortWear successfully!", "Deleting Data Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = true;
                    }
                    else
                    {
                        // Query failed
                        MessageBox.Show("There is no data from tbl_AverageResortWear!", "Nothing to delete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Delete data from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    conn.Close();
                }
            }

            return isSuccess;
        }

        private void btnDeleteAllBestInResortWearAverageTable_Click(object sender, EventArgs e)
        {
            DeleteAllBestInResortWearTbl_Average();
        }

        // On Stage Questions
        private void btnOnStageQuestionsAverage_Click(object sender, EventArgs e)
        {
            // Static method to connect Database
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            // To hold the data from Database
            DataTable dt = new DataTable();
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to get the average ON STAGE QUESTIONS?", "Get Average ON STAGE QUESTIONS!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    // SQL Query to get data from Database
                    string sql = @"DELETE FROM tbl_AverageOnStageQuestions
                    INSERT INTO tbl_AverageOnStageQuestions(CandidateNo, CandidateName, Average)

                    SELECT CandidateNo, CandidateName, AVG(Score)Average
                    FROM
                    (
                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge1OnStageQuestions

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge2OnStageQuestions

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge3OnStageQuestions

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge4OnStageQuestions

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge5OnStageQuestions
                    ) t
                    GROUP BY CandidateNo, CandidateName";

                    // For executing command
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Getting data from Database
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    // Database connection open
                    conn.Open();

                    // Fill data in our DataTable
                    sda.Fill(dt);

                    MessageBox.Show("Get Average ON STAGE QUESTIONS Successfully!", "Get Average Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Insert data average on stage questions from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    // Closing connection
                    conn.Close();
                }
            }
        }       

        private bool DeleteAllOnStageQuestionsTbl_Average()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete all data from tbl_AverageOnStageQuestions?", "tbl_AverageOnStageQuestions Deleting All", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    string sql = "DELETE FROM tbl_AverageOnStageQuestions";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        // Query successful
                        MessageBox.Show("Deleted all data from tbl_AverageOnStageQuestions successfully!", "Deleting Data Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = true;
                    }
                    else
                    {
                        // Query failed
                        MessageBox.Show("There is no data from tbl_AverageOnStageQuestions!", "Nothing to delete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Delete data from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    conn.Close();
                }
            }

            return isSuccess;
        }

        private void btnDeleteAllOnStageQuestionsAverageTable_Click(object sender, EventArgs e)
        {
            DeleteAllOnStageQuestionsTbl_Average();
        }

        // Top 5
        private void btnTop5_Click(object sender, EventArgs e)
        {
            // Static method to connect Database
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            // To hold the data from Database
            DataTable dt = new DataTable();
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to get the average of TOP 5?", "Get Average of TOP 5!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    // SQL Query to get data from Database
                    string sql = @"DELETE FROM tbl_AverageTop5
                    INSERT INTO tbl_AverageTop5(CandidateNo, CandidateName, Average)

                    SELECT CandidateNo, CandidateName, AVG(Average)Average
                    FROM
                    (
                    SELECT CandidateNo, CandidateName, Average

                    FROM tbl_AverageTalent

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Average

                    FROM tbl_AverageProductionNo

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Average

                    FROM tbl_AverageEveningGown

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Average

                    FROM tbl_AverageResortWear

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Average

                    FROM tbl_AverageOnStageQuestions
                    ) t
                    GROUP BY CandidateNo, CandidateName";

                    // For executing command
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Getting data from Database
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    // Database connection open
                    conn.Open();

                    // Fill data in our DataTable
                    sda.Fill(dt);

                    MessageBox.Show("Get Average of TOP 5 Successfully!", "Get Average Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Insert data average of top 5 from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    // Closing connection
                    conn.Close();
                }
            }
        }

        private bool DeleteAllTop5Tbl_Average()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete all data from tbl_AverageTop5?", "tbl_AverageTop5 Deleting All", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    string sql = "DELETE FROM tbl_AverageTop5";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        // Query successful
                        MessageBox.Show("Deleted all data from tbl_AverageTop5 successfully!", "Deleting Data Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = true;
                    }
                    else
                    {
                        // Query failed
                        MessageBox.Show("There is no data from tbl_AverageTop5!", "Nothing to delete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Delete data from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    conn.Close();
                }
            }

            return isSuccess;
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            DeleteAllTop5Tbl_Average();
        }

        // Miss Aloguinsan 2019
        private void btnMissPageantAverage_Click(object sender, EventArgs e)
        {
            // Static method to connect Database
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            // To hold the data from Database
            DataTable dt = new DataTable();
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to get the average of Miss Aloguinsan 2019?", "Get Average of Miss Aloguinsan 2019!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    // SQL Query to get data from Database
                    string sql = @"DELETE FROM tbl_AverageMissPageant
                    INSERT INTO tbl_AverageMissPageant(CandidateNo, CandidateName, Average)

                    SELECT CandidateNo, CandidateName, AVG(Score)Average
                    FROM
                    (
                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge1Top5

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge2Top5

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge3Top5

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge4Top5

                    UNION ALL

                    SELECT CandidateNo, CandidateName, Score

                    FROM tbl_Judge5Top5
                    ) t
                    GROUP BY CandidateNo, CandidateName";

                    // For executing command
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Getting data from Database
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    // Database connection open
                    conn.Open();

                    // Fill data in our DataTable
                    sda.Fill(dt);

                    MessageBox.Show("Get Average of Miss Aloguinsan 2019 Successfully!", "Get Average Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Insert data average of Miss Aloguinsan 2019 from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    // Closing connection
                    conn.Close();
                }
            }
        }

        private bool DeleteAllMissPageantTbl_Average()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete all data from tbl_AverageMissPageant?", "tbl_AverageMissPageant Deleting All", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    string sql = "DELETE FROM tbl_AverageMissPageant";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        // Query successful
                        MessageBox.Show("Deleted all data from tbl_AverageMissPageant successfully!", "Deleting Data Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = true;
                    }
                    else
                    {
                        // Query failed
                        MessageBox.Show("There is no data from tbl_AverageMissPageant!", "Nothing to delete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    // Throw message if any error occurs
                    MessageBox.Show(ex.Message, "Delete data from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    conn.Close();
                }
            }

            return isSuccess;
        }

        private void btnDeleteAllMissPageantAverageTable_Click(object sender, EventArgs e)
        {
            DeleteAllMissPageantTbl_Average();
        }

        private void FillPictureBoxWelcomeForm()
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_PicturesWelcomeTabulatorServer";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;

                try
                {
                    conn.Open();
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        if (read.HasRows)
                        {
                            read.Read();
                            pctrBoxWelcomeTabulatorServer.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        OpenFileDialog ofd = new OpenFileDialog();
        private void btnBrowseImageWelcomeForm_Click(object sender, EventArgs e)
        {
            try
            {
                //ofd.InitialDirectory = "C:\\";
                ofd.Filter = "Image Files (*.jpg)|*.jpg|All Files (*.*)|*.*";
                ofd.FilterIndex = 1;

                if (ofd.ShowDialog() == DialogResult.OK)
                {

                    if (ofd.CheckFileExists)
                    {
                        string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                        string correctFileName = Path.GetFileName(ofd.FileName);
                        File.Copy(ofd.FileName, paths + @"\Images\WelcomeTabulatorServer\" + correctFileName, true);
                        lblImageFilenameWelcomeForm.Text = correctFileName;
                        pctrBoxWelcomeTabulatorServer.Image = Image.FromFile(ofd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Browse Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        PathImageBLL pathImgBLL = new PathImageBLL();
        PathImageWelcomeTabulatorServer pathImageWelcomeTabulatorServer = new PathImageWelcomeTabulatorServer();
        private void btnSaveImageWelcomeForm_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to save the image?", "Save Welcome Form Image!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                string correctFileName = Path.GetFileName(ofd.FileName);

                // Getting data from UI
                pathImgBLL.PathImage = correctFileName;

                // Inserting data into Database
                bool success = pathImageWelcomeTabulatorServer.Insert(pathImgBLL);

                // If the data is successfully inserted then the value of success will be true else it will be false
                if (success == true)
                {
                    // Data successfully inserted
                    MessageBox.Show("Image successfully saved.", "Image Information Save Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Failed to insert data
                    MessageBox.Show("Failed to add new image.", "Image Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        UpdateCandidateImageBLL updateImgBLL = new UpdateCandidateImageBLL();
        WelcomeTabulatorServerImageDAL updateImgWelcomeTabulatorServer = new WelcomeTabulatorServerImageDAL();
        private void btnUpdateImageWelcomeForm_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to update the image?", "Update Welcome Form Image!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                // Getting data from UI
                updateImgBLL.PathImage = lblImageFilenameWelcomeForm.Text;

                // Updating data into Database
                bool success = updateImgWelcomeTabulatorServer.UpdateImage(updateImgBLL);

                // If the data is successfully updated then the value of success will be true else it will be false
                if (success == true)
                {
                    // Data successfully updated
                    MessageBox.Show("Image successfully updated.", "Updated Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Failed to updated data
                    MessageBox.Show("Failed to update new image information.", "Update Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private bool DeleteAllImagesWelcomeForm()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_PicturesWelcomeTabulatorServer";
                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
                    MessageBox.Show("All images successfully deleted.", "Deleted successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Query failed
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                // Throw message if any error occurs
                MessageBox.Show(ex.Message, "Delete data from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        private void btnDeleteAllImagesWelcomeForm_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete all images?", "Delete All Images!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                DeleteAllImagesWelcomeForm();
            }
        }

        private void btnStopwatch_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"C:\Program Files (x86)\Cool Timer\cooltimer.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Stopwatch Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTemplateForTime_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"C:\Documents\Template-for-Time.xlsx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed Template for Time!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                frmJudge1 frmJ1 = new frmJudge1();
                this.Hide();
                frmJ1.Show();

                SqlDependency.Stop(UserDAL.myconnstrng);

                // Show controls or enable controls from Form Judge 1
                frmJ1.lblJudge1.Invoke((Action)delegate { frmJ1.lblJudge1.Text = "Admin :"; });
                frmJ1.lblJudgeName.Visible = false;
                frmJ1.lblAdmin.Visible = true;

                frmJ1.btnBackToServer.Visible = true;

                frmJ1.btnBrowseImageWelcomeForm.Visible = true;
                frmJ1.btnSaveImageWelcomeForm.Visible = true;
                frmJ1.btnUpdateImageWelcomeForm.Visible = true;
                frmJ1.btnDeleteAllImagesWelcomeForm.Visible = true;
                frmJ1.btnSendToBackBestInTalentPanel.Visible = true;

                frmJ1.btnJudge2.Visible = true;
                frmJ1.btnJudge3.Visible = true;
                frmJ1.btnJudge4.Visible = true;
                frmJ1.btnJudge5.Visible = true;

                frmJ1.txtCandNo1Top5.ReadOnly = false;
                frmJ1.txtCandNo2Top5.ReadOnly = false;
                frmJ1.txtCandNo3Top5.ReadOnly = false;
                frmJ1.txtCandNo4Top5.ReadOnly = false;
                frmJ1.txtCandNo5Top5.ReadOnly = false;
                frmJ1.txtCandidateName1Top5.ReadOnly = false;
                frmJ1.txtCandidateName2Top5.ReadOnly = false;
                frmJ1.txtCandidateName3Top5.ReadOnly = false;
                frmJ1.txtCandidateName4Top5.ReadOnly = false;
                frmJ1.txtCandidateName5Top5.ReadOnly = false;
                frmJ1.btnUpdateCandNoAndCandNamesTop5.Visible = true;

                frmJ1.btnBrowseImageTop5.Visible = true;
                frmJ1.btnDeleteAllImagesTop5.Visible = true;
                frmJ1.btnUpdateImageTop5.Visible = true;
                frmJ1.txtImageIDTop5.Visible = true;
                frmJ1.btnSaveImageTop5.Visible = true;

                frmJ1.txtCandidate1.ReadOnly = false;
                frmJ1.txtCandidate2.ReadOnly = false;
                frmJ1.txtCandidate3.ReadOnly = false;
                frmJ1.txtCandidate4.ReadOnly = false;
                frmJ1.txtCandidate5.ReadOnly = false;
                frmJ1.txtCandidate6.ReadOnly = false;
                frmJ1.txtCandidate7.ReadOnly = false;
                frmJ1.txtCandidate8.ReadOnly = false;
                frmJ1.txtCandidate9.ReadOnly = false;
                frmJ1.txtCandidate10.ReadOnly = false;
                frmJ1.txtCandidate11.ReadOnly = false;
                frmJ1.txtCandidate12.ReadOnly = false;
                frmJ1.txtCandidate13.ReadOnly = false;
                frmJ1.txtCandidate14.ReadOnly = false;
                frmJ1.txtCandidate15.ReadOnly = false;
                frmJ1.btnUpdateCandidateName.Visible = true;

                frmJ1.btnBrowseImage.Visible = true;
                frmJ1.btnDeleteAllImages.Visible = true;
                frmJ1.btnUpdateImage.Visible = true;
                frmJ1.txtImageID.Visible = true;
                frmJ1.btnSave.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to exit?", "Exit!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                timerShutdown.Start();

                SqlDependency.Stop(UserDAL.myconnstrng);
            }
        }

        #region TextBoxes Judges & Admin - MouseClick - Highligted All Text
        private void txtJudge1_MouseClick(object sender, MouseEventArgs e)
        {
            txtJudge1.SelectAll();
        }

        private void txtJudge2_MouseClick(object sender, MouseEventArgs e)
        {
            txtJudge2.SelectAll();
        }

        private void txtJudge3_MouseClick(object sender, MouseEventArgs e)
        {
            txtJudge3.SelectAll();
        }

        private void txtJudge4_MouseClick(object sender, MouseEventArgs e)
        {
            txtJudge4.SelectAll();
        }

        private void txtJudge5_MouseClick(object sender, MouseEventArgs e)
        {
            txtJudge5.SelectAll();
        }

        private void txtAdmin_MouseClick(object sender, MouseEventArgs e)
        {
            txtAdmin.SelectAll();
        }
        #endregion

        #region TextBoxes Category & Result - Enter - Switch Buttons
        private void txtCategoryNo_Enter(object sender, EventArgs e)
        {
            btnSaveCategory.BringToFront();
            btnUpdateCategory.BringToFront();
            btnDeleteCategory.BringToFront();
        }

        private void txtCategoryName_Enter(object sender, EventArgs e)
        {
            btnSaveCategory.BringToFront();
            btnUpdateCategory.BringToFront();
            btnDeleteCategory.BringToFront();
        }

        private void txtResultNo_Enter(object sender, EventArgs e)
        {
            btnSaveResult.BringToFront();
            btnUpdateResult.BringToFront();
            btnDeleteResult.BringToFront();
        }

        private void txtResultName_Enter(object sender, EventArgs e)
        {
            btnSaveResult.BringToFront();
            btnUpdateResult.BringToFront();
            btnDeleteResult.BringToFront();
        }      
        #endregion

        #region TextBoxes Category No. & Result No. KeyPress - Digits only or Number only
        private void txtCategoryNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }        

        private void txtResultNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        
        #endregion

        #region Datagridview Talent - Cellformatting - Judges 1-5
        private void bunifuDataGridTalentJudge1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridTalentJudge2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridTalentJudge3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridTalentJudge4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }        

        private void bunifuDataGridTalentJudge5_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridProductionNoJudge1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridProductionNoJudge2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridProductionNoJudge3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridProductionNoJudge4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }        

        private void bunifuDataGridProductionNoJudge5_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridEveningGownJudge1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridEveningGownJudge2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridEveningGownJudge3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridEveningGownJudge4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }        

        private void bunifuDataGridEveningGownJudge5_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridResortWearJudge1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridResortWearJudge2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridResortWearJudge3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridResortWearJudge4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }        

        private void bunifuDataGridResortWearJudge5_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridOnStageQuestionsJudge1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridOnStageQuestionsJudge2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridOnStageQuestionsJudge3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridOnStageQuestionsJudge4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }        

        private void bunifuDataGridOnStageQuestionsJudge5_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridTop5Judge1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridTop5Judge2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridTop5Judge3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void bunifuDataGridTop5Judge4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }        

        private void bunifuDataGridTop5Judge5_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Format = "N2";
            }
        }
        #endregion
    }
}
