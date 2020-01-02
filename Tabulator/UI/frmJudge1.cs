using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabulator.BLL;
using Tabulator.DAL;

namespace Tabulator.UI
{
    public partial class frmJudge1 : Form
    {
        public frmJudge1()
        {
            InitializeComponent();
        }

        private async void frmJudge1_Load(object sender, EventArgs e)
        {
            await DoVariousThingsFromTheUIThreadParallelAsync();

            cmbCategories.Focus();
        }

        private void MoreSynchronousProcessing()
        {
            // Execute it directly (synchronously), since we are also a synchronous method.            
            FillPictureBoxWelcomeForm();
            FillTxtboxJudge1Name();
            FillcmbCategories();
            FillcmbResults();
            FillTxtboxCandidateName();
            FillTextBoxCandidateNoTop5();
            FillTxtboxCandidateNameTop5();
        }

        private async Task DoVariousThingsFromTheUIThreadParallelAsync()
        {
            // I have a bunch of async work to do, and I am executed on the UI thread.
            try
            {
                List<Task> tasks = new List<Task>();

                tasks.Add(Task.Run(() => MoreSynchronousProcessing()));

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parallel Async Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //private async Task DoVariousThingsFromTheUIThreadAsync()
        //{
        //    // I have a bunch of async work to do, and I am executed on the UI thread.
        //    await Task.Run(() => MoreSynchronousProcessing());
        //}
        
        DataSet ds1, ds2;

        private void FillPictureBoxWelcomeForm()
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_PicturesWelcomeForm";
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
                            pctrBoxWelcomeMissAloguinsan.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
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
                    //cmbCategories.ValueMember = "CategoryID";
                    //cmbCategories.DisplayMember = "CategoryName";
                    //cmbCategories.DataSource = ds1.Tables["tbl_Categories"];
                    cmbCategories.Invoke((Action)delegate 
                    {
                        cmbCategories.ValueMember = "CategoryID";
                        cmbCategories.DisplayMember = "CategoryName";
                        cmbCategories.DataSource = ds1.Tables["tbl_Categories"];
                    });
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
                    //cmbResults.ValueMember = "ResultID";
                    //cmbResults.DisplayMember = "ResultName";
                    //cmbResults.DataSource = ds2.Tables["tbl_Results"];
                    cmbResults.Invoke((Action)delegate
                    {
                        cmbResults.ValueMember = "ResultID";
                        cmbResults.DisplayMember = "ResultName";
                        cmbResults.DataSource = ds2.Tables["tbl_Results"];
                    });
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

        delegate void SetTextCallback(string text);

        private void SetTextlblJudge1Name(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lblJudgeName.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextlblJudge1Name);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblJudgeName.Text = text;
            }
        }

        public void FillTxtboxJudge1Name()
        {
            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT Judge1Name FROM tbl_JudgesAndAdmin";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();

                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        // Judge1 Name
                        read.Read();
                        //lblJudgeName.Text = (read.GetValue(0).ToString());
                        SetTextlblJudge1Name(read.GetValue(0).ToString());
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

        private void SetTexttxtCandNo1Top5(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandNo1Top5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandNo1Top5);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandNo1Top5.Text = text;
            }
        }

        private void SetTexttxtCandNo2Top5(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandNo2Top5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandNo2Top5);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandNo2Top5.Text = text;
            }
        }

        private void SetTexttxtCandNo3Top5(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandNo3Top5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandNo3Top5);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandNo3Top5.Text = text;
            }
        }

        private void SetTexttxtCandNo4Top5(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandNo4Top5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandNo4Top5);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandNo4Top5.Text = text;
            }
        }

        private void SetTexttxtCandNo5Top5(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandNo5Top5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandNo5Top5);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandNo5Top5.Text = text;
            }
        }

        private void FillTextBoxCandidateNoTop5()
        {
            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT (CandidateNo) FROM tbl_Judge1Top5";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();

                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        // Candidates No.
                        read.Read();
                        txtCandNo1Top5.Text = (read.GetValue(0).ToString());
                        read.Read();
                        txtCandNo2Top5.Text = (read.GetValue(0).ToString());
                        read.Read();
                        txtCandNo3Top5.Text = (read.GetValue(0).ToString());
                        read.Read();
                        txtCandNo4Top5.Text = (read.GetValue(0).ToString());
                        read.Read();
                        txtCandNo5Top5.Text = (read.GetValue(0).ToString());
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

        private void SetTexttxtCandidateName1Top5(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidateName1Top5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidateName1Top5);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidateName1Top5.Text = text;
            }
        }

        private void SetTexttxtCandidateName2Top5(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidateName2Top5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidateName2Top5);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidateName2Top5.Text = text;
            }
        }

        private void SetTexttxtCandidateName3Top5(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidateName3Top5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidateName3Top5);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidateName3Top5.Text = text;
            }
        }

        private void SetTexttxtCandidateName4Top5(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidateName4Top5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidateName4Top5);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidateName4Top5.Text = text;
            }
        }

        private void SetTexttxtCandidateName5Top5(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidateName5Top5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidateName5Top5);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidateName5Top5.Text = text;
            }
        }

        private void FillTxtboxCandidateNameTop5()
        {
            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT (CandidateName) FROM tbl_Judge1Top5";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();

                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        // Candidates Name
                        read.Read();
                        //txtCandidateName1Top5.Text = (read.GetValue(0).ToString());
                        SetTexttxtCandidateName1Top5(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidateName2Top5(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidateName3Top5(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidateName4Top5(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidateName5Top5(read.GetValue(0).ToString());
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

        private void SetTexttxtCandidate1(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate1);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate1.Text = text;
            }
        }

        private void SetTexttxtCandidate2(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate2.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate2);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate2.Text = text;
            }
        }

        private void SetTexttxtCandidate3(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate3.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate3);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate3.Text = text;
            }
        }

        private void SetTexttxtCandidate4(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate4.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate4);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate4.Text = text;
            }
        }

        private void SetTexttxtCandidate5(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate5);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate5.Text = text;
            }
        }

        private void SetTexttxtCandidate6(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate6.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate6);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate6.Text = text;
            }
        }

        private void SetTexttxtCandidate7(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate7.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate7);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate7.Text = text;
            }
        }

        private void SetTexttxtCandidate8(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate8.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate8);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate8.Text = text;
            }
        }

        private void SetTexttxtCandidate9(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate9.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate9);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate9.Text = text;
            }
        }

        private void SetTexttxtCandidate10(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate10.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate10);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate10.Text = text;
            }
        }

        private void SetTexttxtCandidate11(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate11.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate11);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate11.Text = text;
            }
        }

        private void SetTexttxtCandidate12(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate12.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate12);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate12.Text = text;
            }
        }

        private void SetTexttxtCandidate13(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate13.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate13);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate13.Text = text;
            }
        }

        private void SetTexttxtCandidate14(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate14.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate14);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate14.Text = text;
            }
        }

        private void SetTexttxtCandidate15(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCandidate15.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTexttxtCandidate15);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCandidate15.Text = text;
            }
        }

        private void FillTxtboxCandidateName()
        {
            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT (CandidateName) FROM tbl_Judge1Talent";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();

                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        // Candidates Name
                        read.Read();
                        //txtCandidate1.Text = (read.GetValue(0).ToString());
                        SetTexttxtCandidate1(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate2(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate3(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate4(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate5(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate6(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate7(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate8(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate9(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate10(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate11(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate12(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate13(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate14(read.GetValue(0).ToString());
                        read.Read();
                        SetTexttxtCandidate15(read.GetValue(0).ToString());

                        txtCandidate1Talent.Text = txtCandidate1.Text;

                        txtNo2Talent.Text = txtNo2.Text;
                        txtCandidate2Talent.Text = txtCandidate2.Text;

                        txtNo3Talent.Text = txtNo3.Text;
                        txtCandidate3Talent.Text = txtCandidate3.Text;

                        txtNo4Talent.Text = txtNo4.Text;
                        txtCandidate4Talent.Text = txtCandidate4.Text;

                        txtNo5Talent.Text = txtNo5.Text;
                        txtCandidate5Talent.Text = txtCandidate5.Text;

                        txtNo6Talent.Text = txtNo6.Text;
                        txtCandidate6Talent.Text = txtCandidate6.Text;

                        txtNo7Talent.Text = txtNo7.Text;
                        txtCandidate7Talent.Text = txtCandidate7.Text;

                        txtNo8Talent.Text = txtNo8.Text;
                        txtCandidate8Talent.Text = txtCandidate8.Text;

                        txtNo9Talent.Text = txtNo9.Text;
                        txtCandidate9Talent.Text = txtCandidate9.Text;

                        txtNo10Talent.Text = txtNo10.Text;
                        txtCandidate10Talent.Text = txtCandidate10.Text;

                        txtNo11Talent.Text = txtNo11.Text;
                        txtCandidate11Talent.Text = txtCandidate11.Text;

                        txtNo12Talent.Text = txtNo12.Text;
                        txtCandidate12Talent.Text = txtCandidate12.Text;

                        txtNo13Talent.Text = txtNo13.Text;
                        txtCandidate13Talent.Text = txtCandidate13.Text;

                        txtNo14Talent.Text = txtNo14.Text;
                        txtCandidate14Talent.Text = txtCandidate14.Text;

                        txtNo15Talent.Text = txtNo15.Text;
                        txtCandidate15Talent.Text = txtCandidate15.Text;
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

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategories.SelectedIndex == 0)
            {
                panelWelcomeJudge1.BringToFront();
                lblJudgeName.Focus();
            }
            else if (cmbCategories.SelectedIndex == 1)
            {
                btnJudge1TalentSend.Visible = true;

                btnJudge1ProductionNumberSend.Visible = false;
                btnJudge1EveningGownSend.Visible = false;
                btnJudge1ResortWearSend.Visible = false;
                btnJudge1OnStageQuestionsSend.Visible = false;

                lblCategoryTitle.Text = cmbCategories.Text;
                lblCategoryTitle.ForeColor = Color.OrangeRed;
                lblCandidateNo.ForeColor = Color.OrangeRed;
                lblCandidateName.ForeColor = Color.OrangeRed;
                lblScore.ForeColor = Color.OrangeRed;
                btnJudge1TalentSend.SendToBack();
                panelBody.BringToFront();
                Clear();

                FillTxtboxCandidateName();                                

                lblScore.Visible = false;
                lblScores.Visible = false;
                lblScores100.Visible = false;

                ShowPanel1BestInTalentAndHideRestPanel();
                TabStopAllTxtScore1_15();
            }
            else if (cmbCategories.SelectedIndex == 2)
            {
                btnJudge1ProductionNumberSend.Visible = true;

                btnJudge1TalentSend.Visible = false;
                btnJudge1EveningGownSend.Visible = false;
                btnJudge1ResortWearSend.Visible = false;
                btnJudge1OnStageQuestionsSend.Visible = false;

                txtScore1.Focus();
                lblCategoryTitle.Text = cmbCategories.Text;
                lblCategoryTitle.ForeColor = Color.DarkCyan;
                lblCandidateNo.ForeColor = Color.DarkCyan;
                lblCandidateName.ForeColor = Color.DarkCyan;
                lblScore.ForeColor = Color.DarkCyan;
                btnJudge1ProductionNumberSend.BringToFront();
                panelBody.BringToFront();
                Clear();

                FillTxtboxCandidateName();

                lblScore.Visible = true;
                lblScores.Visible = true;
                lblScores100.Visible = false;

                HideAllBestInTalentPanel();
                TabStopAllToTrueTxtScore1_15();
            }
            else if (cmbCategories.SelectedIndex == 3)
            {
                btnJudge1EveningGownSend.Visible = true;

                btnJudge1TalentSend.Visible = false;
                btnJudge1ProductionNumberSend.Visible = false;
                btnJudge1ResortWearSend.Visible = false;
                btnJudge1OnStageQuestionsSend.Visible = false;

                txtScore1.Focus();
                lblCategoryTitle.Text = cmbCategories.Text;
                lblCategoryTitle.ForeColor = Color.SeaGreen;
                lblCandidateNo.ForeColor = Color.SeaGreen;
                lblCandidateName.ForeColor = Color.SeaGreen;
                lblScore.ForeColor = Color.SeaGreen;
                btnJudge1EveningGownSend.BringToFront();
                panelBody.BringToFront();
                Clear();

                FillTxtboxCandidateName();                

                lblScore.Visible = true;
                lblScores.Visible = true;
                lblScores100.Visible = false;

                HideAllBestInTalentPanel();
                TabStopAllToTrueTxtScore1_15();
            }
            else if (cmbCategories.SelectedIndex == 4)
            {
                btnJudge1ResortWearSend.Visible = true;

                btnJudge1TalentSend.Visible = false;
                btnJudge1EveningGownSend.Visible = false;
                btnJudge1ProductionNumberSend.Visible = false;
                btnJudge1OnStageQuestionsSend.Visible = false;

                txtScore1.Focus();
                lblCategoryTitle.Text = cmbCategories.Text;
                lblCategoryTitle.ForeColor = Color.Purple;
                lblCandidateNo.ForeColor = Color.Purple;
                lblCandidateName.ForeColor = Color.Purple;
                lblScore.ForeColor = Color.Purple;
                btnJudge1ResortWearSend.BringToFront();
                panelBody.BringToFront();
                Clear();

                FillTxtboxCandidateName();                

                lblScore.Visible = true;
                lblScores.Visible = true;
                lblScores100.Visible = false;

                HideAllBestInTalentPanel();
                TabStopAllToTrueTxtScore1_15();
            }
            else if (cmbCategories.SelectedIndex == 5)
            {
                btnJudge1OnStageQuestionsSend.Visible = true;

                btnJudge1TalentSend.Visible = false;
                btnJudge1EveningGownSend.Visible = false;
                btnJudge1ResortWearSend.Visible = false;
                btnJudge1ProductionNumberSend.Visible = false;

                txtScore1.Focus();
                lblCategoryTitle.Text = cmbCategories.Text;
                lblCategoryTitle.ForeColor = Color.DodgerBlue;
                lblCandidateNo.ForeColor = Color.DodgerBlue;
                lblCandidateName.ForeColor = Color.DodgerBlue;
                lblScore.ForeColor = Color.DodgerBlue;
                btnJudge1OnStageQuestionsSend.BringToFront();
                panelBody.BringToFront();
                Clear();

                FillTxtboxCandidateName();                

                lblScore.Visible = true;
                lblScores.Visible = true;
                lblScores100.Visible = false;

                HideAllBestInTalentPanel();
                TabStopAllToTrueTxtScore1_15();
            }
            else if (cmbCategories.SelectedIndex == 6)
            {
                txtScore1Top5.Focus();
                lblTop5.Text = cmbCategories.Text;
                panelTop5.BringToFront();
                Clear();

                FillTextBoxCandidateNoTop5();
                FillTxtboxCandidateNameTop5();
            }
        }

        private void cmbResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbResults.SelectedIndex == 0)
            {
                lblJudgeName.Focus();
            }
            else if (cmbResults.SelectedIndex == 1) // Best In Talent
            {
                frmAverageTalent averageTalent = new frmAverageTalent();
                averageTalent.Show();
                averageTalent.lblHead.Text = cmbResults.Text;

                averageTalent.bunifuDataGridAverageTalent.Columns[4].Visible = false;

                averageTalent.FormClosed += FormClosed;
            }
            else if (cmbResults.SelectedIndex == 2) // Best In Production Number
            {
                frmAverageProductionNo averageProductionNo = new frmAverageProductionNo();
                averageProductionNo.Show();
                averageProductionNo.lblHead.Text = cmbResults.Text;

                averageProductionNo.bunifuDataGridAverageProductionNo.Columns[4].Visible = false;

                averageProductionNo.FormClosed += FormClosed;
            }
            else if (cmbResults.SelectedIndex == 3) // Best In Evening Gown
            {
                frmAverageEveningGown averageEveningGown = new frmAverageEveningGown();
                averageEveningGown.Show();
                averageEveningGown.lblHead.Text = cmbResults.Text;

                averageEveningGown.bunifuDataGridAverageEveningGown.Columns[4].Visible = false;

                averageEveningGown.FormClosed += FormClosed;
            }
            else if (cmbResults.SelectedIndex == 4) // Best In Resort Wear
            {
                frmAverageResortWear averageResortWear = new frmAverageResortWear();
                averageResortWear.Show();
                averageResortWear.lblHead.Text = cmbResults.Text;

                averageResortWear.bunifuDataGridAverageResortWear.Columns[4].Visible = false;

                averageResortWear.FormClosed += FormClosed;
            }
            else if (cmbResults.SelectedIndex == 5) // On Stage Questions
            {
                frmAverageOnStageQuestions averageOnStageQuestions = new frmAverageOnStageQuestions();
                averageOnStageQuestions.Show();
                averageOnStageQuestions.lblHead.Text = cmbResults.Text;

                averageOnStageQuestions.bunifuDataGridAverageOnStageQuestions.Columns[4].Visible = false;

                averageOnStageQuestions.FormClosed += FormClosed;
            }
            else if (cmbResults.SelectedIndex == 6) // Top 5
            {
                frmAverageTop5 averageTop5 = new frmAverageTop5();
                averageTop5.Show();
                averageTop5.lblHead.Text = cmbResults.Text;

                averageTop5.bunifuDataGridAverageTop5.Columns[4].Visible = false;

                averageTop5.FormClosed += FormClosed;
            }
            else if (cmbResults.SelectedIndex == 7) // Miss Pageant
            {
                frmAverageMissPageant averageMissPageant = new frmAverageMissPageant();
                averageMissPageant.Show();
                averageMissPageant.lblHead.Text = cmbResults.Text;

                averageMissPageant.bunifuDataGridAverageMissPageant.Columns[4].Visible = false;

                averageMissPageant.FormClosed += FormClosed;
            }
        }

        new void FormClosed(object sender, FormClosedEventArgs e)
        {
            lblJudgeName.Focus();
            cmbResults.SelectedIndex = 0;
        }

        JudgeBLL judge1BLL = new JudgeBLL();
        Judge1TalentDAL judge1DAL = new Judge1TalentDAL();
        private void SendScoreJudge1Talent()
        {
            float score1, score2, score3, score4, score5, score6, score7, score8, score9, score10, score11, score12, score13, score14, score15;
            float.TryParse(txtScore1.Text, out score1);
            float.TryParse(txtScore2.Text, out score2);
            float.TryParse(txtScore3.Text, out score3);
            float.TryParse(txtScore4.Text, out score4);
            float.TryParse(txtScore5.Text, out score5);
            float.TryParse(txtScore6.Text, out score6);
            float.TryParse(txtScore7.Text, out score7);
            float.TryParse(txtScore8.Text, out score8);
            float.TryParse(txtScore9.Text, out score9);
            float.TryParse(txtScore10.Text, out score10);
            float.TryParse(txtScore11.Text, out score11);
            float.TryParse(txtScore12.Text, out score12);
            float.TryParse(txtScore13.Text, out score13);
            float.TryParse(txtScore14.Text, out score14);
            float.TryParse(txtScore15.Text, out score15);

            // Getting data from UI
            judge1BLL.Score1 = score1;
            judge1BLL.Score2 = score2;
            judge1BLL.Score3 = score3;
            judge1BLL.Score4 = score4;
            judge1BLL.Score5 = score5;
            judge1BLL.Score6 = score6;
            judge1BLL.Score7 = score7;
            judge1BLL.Score8 = score8;
            judge1BLL.Score9 = score9;
            judge1BLL.Score10 = score10;
            judge1BLL.Score11 = score11;
            judge1BLL.Score12 = score12;
            judge1BLL.Score13 = score13;
            judge1BLL.Score14 = score14;
            judge1BLL.Score15 = score15;

            // Updating data into Database
            bool success =  judge1DAL.Update1(judge1BLL);
            judge1DAL.Update2(judge1BLL);
            judge1DAL.Update3(judge1BLL);
            judge1DAL.Update4(judge1BLL);
            judge1DAL.Update5(judge1BLL);
            judge1DAL.Update6(judge1BLL);
            judge1DAL.Update7(judge1BLL);
            judge1DAL.Update8(judge1BLL);
            judge1DAL.Update9(judge1BLL);
            judge1DAL.Update10(judge1BLL);
            judge1DAL.Update11(judge1BLL);
            judge1DAL.Update12(judge1BLL);
            judge1DAL.Update13(judge1BLL);
            judge1DAL.Update14(judge1BLL);
            judge1DAL.Update15(judge1BLL);

            // If the data is successfully updated then the value of success will be true else it will be false
            if (success == true)
            {
                // Data successfully updated
                MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Failed to update data
                MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        Judge1ProductionNoDAL judge1ProductionNoDAL = new Judge1ProductionNoDAL();
        private void SendScoreJudge1ProductionNo()
        {
            float score1, score2, score3, score4, score5, score6, score7, score8, score9, score10, score11, score12, score13, score14, score15;
            float.TryParse(txtScore1.Text, out score1);
            float.TryParse(txtScore2.Text, out score2);
            float.TryParse(txtScore3.Text, out score3);
            float.TryParse(txtScore4.Text, out score4);
            float.TryParse(txtScore5.Text, out score5);
            float.TryParse(txtScore6.Text, out score6);
            float.TryParse(txtScore7.Text, out score7);
            float.TryParse(txtScore8.Text, out score8);
            float.TryParse(txtScore9.Text, out score9);
            float.TryParse(txtScore10.Text, out score10);
            float.TryParse(txtScore11.Text, out score11);
            float.TryParse(txtScore12.Text, out score12);
            float.TryParse(txtScore13.Text, out score13);
            float.TryParse(txtScore14.Text, out score14);
            float.TryParse(txtScore15.Text, out score15);

            float total = (score1 + score2 + score3);

            // Getting data from UI
            judge1BLL.Score1 = score1;
            judge1BLL.Score2 = score2;
            judge1BLL.Score3 = score3;
            judge1BLL.Score4 = score4;
            judge1BLL.Score5 = score5;
            judge1BLL.Score6 = score6;
            judge1BLL.Score7 = score7;
            judge1BLL.Score8 = score8;
            judge1BLL.Score9 = score9;
            judge1BLL.Score10 = score10;
            judge1BLL.Score11 = score11;
            judge1BLL.Score12 = score12;
            judge1BLL.Score13 = score13;
            judge1BLL.Score14 = score14;
            judge1BLL.Score15 = score15;

            judge1BLL.Total = total;

            // Updating data into Database
            bool success = judge1ProductionNoDAL.Update1(judge1BLL);
            judge1ProductionNoDAL.Update2(judge1BLL);
            judge1ProductionNoDAL.Update3(judge1BLL);
            judge1ProductionNoDAL.Update4(judge1BLL);
            judge1ProductionNoDAL.Update5(judge1BLL);
            judge1ProductionNoDAL.Update6(judge1BLL);
            judge1ProductionNoDAL.Update7(judge1BLL);
            judge1ProductionNoDAL.Update8(judge1BLL);
            judge1ProductionNoDAL.Update9(judge1BLL);
            judge1ProductionNoDAL.Update10(judge1BLL);
            judge1ProductionNoDAL.Update11(judge1BLL);
            judge1ProductionNoDAL.Update12(judge1BLL);
            judge1ProductionNoDAL.Update13(judge1BLL);
            judge1ProductionNoDAL.Update14(judge1BLL);
            judge1ProductionNoDAL.Update15(judge1BLL);

            judge1ProductionNoDAL.UpdateDependency1(judge1BLL);

            // If the data is successfully updated then the value of success will be true else it will be false
            if (success == true)
            {
                // Data successfully updated
                MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Failed to update data
                MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        Judge1EveningGownDAL judge1EveningGown = new Judge1EveningGownDAL();
        private void SendScoreJudge1EveningGown()
        {
            float score1, score2, score3, score4, score5, score6, score7, score8, score9, score10, score11, score12, score13, score14, score15;
            float.TryParse(txtScore1.Text, out score1);
            float.TryParse(txtScore2.Text, out score2);
            float.TryParse(txtScore3.Text, out score3);
            float.TryParse(txtScore4.Text, out score4);
            float.TryParse(txtScore5.Text, out score5);
            float.TryParse(txtScore6.Text, out score6);
            float.TryParse(txtScore7.Text, out score7);
            float.TryParse(txtScore8.Text, out score8);
            float.TryParse(txtScore9.Text, out score9);
            float.TryParse(txtScore10.Text, out score10);
            float.TryParse(txtScore11.Text, out score11);
            float.TryParse(txtScore12.Text, out score12);
            float.TryParse(txtScore13.Text, out score13);
            float.TryParse(txtScore14.Text, out score14);
            float.TryParse(txtScore15.Text, out score15);

            float total = (score1 + score2 + score3);

            // Getting data from UI
            judge1BLL.Score1 = score1;
            judge1BLL.Score2 = score2;
            judge1BLL.Score3 = score3;
            judge1BLL.Score4 = score4;
            judge1BLL.Score5 = score5;
            judge1BLL.Score6 = score6;
            judge1BLL.Score7 = score7;
            judge1BLL.Score8 = score8;
            judge1BLL.Score9 = score9;
            judge1BLL.Score10 = score10;
            judge1BLL.Score11 = score11;
            judge1BLL.Score12 = score12;
            judge1BLL.Score13 = score13;
            judge1BLL.Score14 = score14;
            judge1BLL.Score15 = score15;

            judge1BLL.Total = total;

            // Updating data into Database
            bool success = judge1EveningGown.Update1(judge1BLL);
            judge1EveningGown.Update2(judge1BLL);
            judge1EveningGown.Update3(judge1BLL);
            judge1EveningGown.Update4(judge1BLL);
            judge1EveningGown.Update5(judge1BLL);
            judge1EveningGown.Update6(judge1BLL);
            judge1EveningGown.Update7(judge1BLL);
            judge1EveningGown.Update8(judge1BLL);
            judge1EveningGown.Update9(judge1BLL);
            judge1EveningGown.Update10(judge1BLL);
            judge1EveningGown.Update11(judge1BLL);
            judge1EveningGown.Update12(judge1BLL);
            judge1EveningGown.Update13(judge1BLL);
            judge1EveningGown.Update14(judge1BLL);
            judge1EveningGown.Update15(judge1BLL);

            judge1EveningGown.UpdateDependency1(judge1BLL);

            // If the data is successfully updated then the value of success will be true else it will be false
            if (success == true)
            {
                // Data successfully updated
                MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Failed to update data
                MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        Judge1ResortWearDAL judge1ResortWearDAL = new Judge1ResortWearDAL();
        private void SendScoreJudge1ResortWear()
        {
            float score1, score2, score3, score4, score5, score6, score7, score8, score9, score10, score11, score12, score13, score14, score15;
            float.TryParse(txtScore1.Text, out score1);
            float.TryParse(txtScore2.Text, out score2);
            float.TryParse(txtScore3.Text, out score3);
            float.TryParse(txtScore4.Text, out score4);
            float.TryParse(txtScore5.Text, out score5);
            float.TryParse(txtScore6.Text, out score6);
            float.TryParse(txtScore7.Text, out score7);
            float.TryParse(txtScore8.Text, out score8);
            float.TryParse(txtScore9.Text, out score9);
            float.TryParse(txtScore10.Text, out score10);
            float.TryParse(txtScore11.Text, out score11);
            float.TryParse(txtScore12.Text, out score12);
            float.TryParse(txtScore13.Text, out score13);
            float.TryParse(txtScore14.Text, out score14);
            float.TryParse(txtScore15.Text, out score15);

            float total = (score1 + score2 + score3);

            // Getting data from UI
            judge1BLL.Score1 = score1;
            judge1BLL.Score2 = score2;
            judge1BLL.Score3 = score3;
            judge1BLL.Score4 = score4;
            judge1BLL.Score5 = score5;
            judge1BLL.Score6 = score6;
            judge1BLL.Score7 = score7;
            judge1BLL.Score8 = score8;
            judge1BLL.Score9 = score9;
            judge1BLL.Score10 = score10;
            judge1BLL.Score11 = score11;
            judge1BLL.Score12 = score12;
            judge1BLL.Score13 = score13;
            judge1BLL.Score14 = score14;
            judge1BLL.Score15 = score15;

            judge1BLL.Total = total;

            // Updating data into Database
            bool success = judge1ResortWearDAL.Update1(judge1BLL);
            judge1ResortWearDAL.Update2(judge1BLL);
            judge1ResortWearDAL.Update3(judge1BLL);
            judge1ResortWearDAL.Update4(judge1BLL);
            judge1ResortWearDAL.Update5(judge1BLL);
            judge1ResortWearDAL.Update6(judge1BLL);
            judge1ResortWearDAL.Update7(judge1BLL);
            judge1ResortWearDAL.Update8(judge1BLL);
            judge1ResortWearDAL.Update9(judge1BLL);
            judge1ResortWearDAL.Update10(judge1BLL);
            judge1ResortWearDAL.Update11(judge1BLL);
            judge1ResortWearDAL.Update12(judge1BLL);
            judge1ResortWearDAL.Update13(judge1BLL);
            judge1ResortWearDAL.Update14(judge1BLL);
            judge1ResortWearDAL.Update15(judge1BLL);

            judge1ResortWearDAL.UpdateDependency1(judge1BLL);

            // If the data is successfully updated then the value of success will be true else it will be false
            if (success == true)
            {
                // Data successfully updated
                MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Failed to update data
                MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }   

        Judge1OnStageQuestionsDAL judge1OnStageQuestionsDAL = new Judge1OnStageQuestionsDAL();
        private void SendScoreJudge1OnStageQuestions()
        {
            float score1, score2, score3, score4, score5, score6, score7, score8, score9, score10, score11, score12, score13, score14, score15;
            float.TryParse(txtScore1.Text, out score1);
            float.TryParse(txtScore2.Text, out score2);
            float.TryParse(txtScore3.Text, out score3);
            float.TryParse(txtScore4.Text, out score4);
            float.TryParse(txtScore5.Text, out score5);
            float.TryParse(txtScore6.Text, out score6);
            float.TryParse(txtScore7.Text, out score7);
            float.TryParse(txtScore8.Text, out score8);
            float.TryParse(txtScore9.Text, out score9);
            float.TryParse(txtScore10.Text, out score10);
            float.TryParse(txtScore11.Text, out score11);
            float.TryParse(txtScore12.Text, out score12);
            float.TryParse(txtScore13.Text, out score13);
            float.TryParse(txtScore14.Text, out score14);
            float.TryParse(txtScore15.Text, out score15);

            float total = (score1 + score2 + score3);

            // Getting data from UI
            judge1BLL.Score1 = score1;
            judge1BLL.Score2 = score2;
            judge1BLL.Score3 = score3;
            judge1BLL.Score4 = score4;
            judge1BLL.Score5 = score5;
            judge1BLL.Score6 = score6;
            judge1BLL.Score7 = score7;
            judge1BLL.Score8 = score8;
            judge1BLL.Score9 = score9;
            judge1BLL.Score10 = score10;
            judge1BLL.Score11 = score11;
            judge1BLL.Score12 = score12;
            judge1BLL.Score13 = score13;
            judge1BLL.Score14 = score14;
            judge1BLL.Score15 = score15;

            judge1BLL.Total = total;

            // Updating data into Database
            bool success = judge1OnStageQuestionsDAL.Update1(judge1BLL);
            judge1OnStageQuestionsDAL.Update2(judge1BLL);
            judge1OnStageQuestionsDAL.Update3(judge1BLL);
            judge1OnStageQuestionsDAL.Update4(judge1BLL);
            judge1OnStageQuestionsDAL.Update5(judge1BLL);
            judge1OnStageQuestionsDAL.Update6(judge1BLL);
            judge1OnStageQuestionsDAL.Update7(judge1BLL);
            judge1OnStageQuestionsDAL.Update8(judge1BLL);
            judge1OnStageQuestionsDAL.Update9(judge1BLL);
            judge1OnStageQuestionsDAL.Update10(judge1BLL);
            judge1OnStageQuestionsDAL.Update11(judge1BLL);
            judge1OnStageQuestionsDAL.Update12(judge1BLL);
            judge1OnStageQuestionsDAL.Update13(judge1BLL);
            judge1OnStageQuestionsDAL.Update14(judge1BLL);
            judge1OnStageQuestionsDAL.Update15(judge1BLL);

            judge1OnStageQuestionsDAL.UpdateDependency1(judge1BLL);

            // If the data is successfully updated then the value of success will be true else it will be false
            if (success == true)
            {
                // Data successfully updated
                MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Failed to update data
                MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        Judge1Top5DAL Judge1Top5DAL = new Judge1Top5DAL();
        private void SendScoreJudge1Top5()
        {
            float score1, score2, score3, score4, score5;
            float.TryParse(txtScore1Top5.Text, out score1);
            float.TryParse(txtScore2Top5.Text, out score2);
            float.TryParse(txtScore3Top5.Text, out score3);
            float.TryParse(txtScore4Top5.Text, out score4);
            float.TryParse(txtScore5Top5.Text, out score5);

            float total = (score1 + score2 + score3);

            // Getting data from UI
            judge1BLL.Score1 = score1;
            judge1BLL.Score2 = score2;
            judge1BLL.Score3 = score3;
            judge1BLL.Score4 = score4;
            judge1BLL.Score5 = score5;

            judge1BLL.Total = total;

            // Updating data into Database
            bool success = Judge1Top5DAL.Update1(judge1BLL);
            Judge1Top5DAL.Update2(judge1BLL);
            Judge1Top5DAL.Update3(judge1BLL);
            Judge1Top5DAL.Update4(judge1BLL);
            Judge1Top5DAL.Update5(judge1BLL);

            Judge1Top5DAL.UpdateDependency1(judge1BLL);

            // If the data is successfully updated then the value of success will be true else it will be false
            if (success == true)
            {
                // Data successfully updated
                MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Failed to update data
                MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        Judge1UpdateCandNoAndCandNamesTop5DAL updatesCandNoAndCandNameDAL = new Judge1UpdateCandNoAndCandNamesTop5DAL();
        private void UpdateCandNoAndCandNamesTop5()
        {
            // Getting data from UI
            judge1BLL.CandidateNo1 = txtCandNo1Top5.Text;
            judge1BLL.CandidateNo2 = txtCandNo2Top5.Text;
            judge1BLL.CandidateNo3 = txtCandNo3Top5.Text;
            judge1BLL.CandidateNo4 = txtCandNo4Top5.Text;
            judge1BLL.CandidateNo5 = txtCandNo5Top5.Text;

            judge1BLL.CandidateName1 = txtCandidateName1Top5.Text;
            judge1BLL.CandidateName2 = txtCandidateName2Top5.Text;
            judge1BLL.CandidateName3 = txtCandidateName3Top5.Text;
            judge1BLL.CandidateName4 = txtCandidateName4Top5.Text;
            judge1BLL.CandidateName5 = txtCandidateName5Top5.Text;

            // Updating data into Database

            // Top 5 Judge 1
            bool success = updatesCandNoAndCandNameDAL.Update1Judge1Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update2Judge1Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update3Judge1Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update4Judge1Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update5Judge1Top5(judge1BLL);

            // Top 5 Judge 2
            updatesCandNoAndCandNameDAL.Update1Judge2Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update2Judge2Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update3Judge2Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update4Judge2Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update5Judge2Top5(judge1BLL);

            // Top 5 Judge 3
            updatesCandNoAndCandNameDAL.Update1Judge3Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update2Judge3Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update3Judge3Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update4Judge3Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update5Judge3Top5(judge1BLL);

            // Top 5 Judge 4
            updatesCandNoAndCandNameDAL.Update1Judge4Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update2Judge4Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update3Judge4Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update4Judge4Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update5Judge4Top5(judge1BLL);

            // Top 5 Judge 5
            updatesCandNoAndCandNameDAL.Update1Judge5Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update2Judge5Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update3Judge5Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update4Judge5Top5(judge1BLL);
            updatesCandNoAndCandNameDAL.Update5Judge5Top5(judge1BLL);

            // If the data is successfully updated then the value of success will be true else it will be false
            if (success == true)
            {
                // Data successfully updated
                MessageBox.Show("Candidate # and Name successfully updated.", "Updated Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Failed to update data
                MessageBox.Show("Failed to update new candidate # and name information.", "Update Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        UpdateCandidateNamesBLL updateName = new UpdateCandidateNamesBLL();
        Judge1UpdateCandidateNamesDAL updateNameDAL = new Judge1UpdateCandidateNamesDAL();
        private void UpdateCandidateNames()
        {
            // Getting data from UI
            updateName.Name1 = txtCandidate1.Text;
            updateName.Name2 = txtCandidate2.Text;
            updateName.Name3 = txtCandidate3.Text;
            updateName.Name4 = txtCandidate4.Text;
            updateName.Name5 = txtCandidate5.Text;
            updateName.Name6 = txtCandidate6.Text;
            updateName.Name7 = txtCandidate7.Text;
            updateName.Name8 = txtCandidate8.Text;
            updateName.Name9 = txtCandidate9.Text;
            updateName.Name10 = txtCandidate10.Text;
            updateName.Name11 = txtCandidate11.Text;
            updateName.Name12 = txtCandidate12.Text;
            updateName.Name13 = txtCandidate13.Text;
            updateName.Name14 = txtCandidate14.Text;
            updateName.Name15 = txtCandidate15.Text;

            // Updating data into Database

            // Judge 1 Talent
            bool success = updateNameDAL.Update1(updateName);
            updateNameDAL.Update2(updateName);
            updateNameDAL.Update3(updateName);
            updateNameDAL.Update4(updateName);
            updateNameDAL.Update5(updateName);
            updateNameDAL.Update6(updateName);
            updateNameDAL.Update7(updateName);
            updateNameDAL.Update8(updateName);
            updateNameDAL.Update9(updateName);
            updateNameDAL.Update10(updateName);
            updateNameDAL.Update11(updateName);
            updateNameDAL.Update12(updateName);
            updateNameDAL.Update13(updateName);
            updateNameDAL.Update14(updateName);
            updateNameDAL.Update15(updateName);

            // Judge 1 Production Number
            updateNameDAL.Update1ProductionNo(updateName);
            updateNameDAL.Update2ProductionNo(updateName);
            updateNameDAL.Update3ProductionNo(updateName);
            updateNameDAL.Update4ProductionNo(updateName);
            updateNameDAL.Update5ProductionNo(updateName);
            updateNameDAL.Update6ProductionNo(updateName);
            updateNameDAL.Update7ProductionNo(updateName);
            updateNameDAL.Update8ProductionNo(updateName);
            updateNameDAL.Update9ProductionNo(updateName);
            updateNameDAL.Update10ProductionNo(updateName);
            updateNameDAL.Update11ProductionNo(updateName);
            updateNameDAL.Update12ProductionNo(updateName);
            updateNameDAL.Update13ProductionNo(updateName);
            updateNameDAL.Update14ProductionNo(updateName);
            updateNameDAL.Update15ProductionNo(updateName);

            // Judge 1 Evening Gown
            updateNameDAL.Update1EveningGown(updateName);
            updateNameDAL.Update2EveningGown(updateName);
            updateNameDAL.Update3EveningGown(updateName);
            updateNameDAL.Update4EveningGown(updateName);
            updateNameDAL.Update5EveningGown(updateName);
            updateNameDAL.Update6EveningGown(updateName);
            updateNameDAL.Update7EveningGown(updateName);
            updateNameDAL.Update8EveningGown(updateName);
            updateNameDAL.Update9EveningGown(updateName);
            updateNameDAL.Update10EveningGown(updateName);
            updateNameDAL.Update11EveningGown(updateName);
            updateNameDAL.Update12EveningGown(updateName);
            updateNameDAL.Update13EveningGown(updateName);
            updateNameDAL.Update14EveningGown(updateName);
            updateNameDAL.Update15EveningGown(updateName);

            // Judge 1 Resort Wear
            updateNameDAL.Update1ResortWear(updateName);
            updateNameDAL.Update2ResortWear(updateName);
            updateNameDAL.Update3ResortWear(updateName);
            updateNameDAL.Update4ResortWear(updateName);
            updateNameDAL.Update5ResortWear(updateName);
            updateNameDAL.Update6ResortWear(updateName);
            updateNameDAL.Update7ResortWear(updateName);
            updateNameDAL.Update8ResortWear(updateName);
            updateNameDAL.Update9ResortWear(updateName);
            updateNameDAL.Update10ResortWear(updateName);
            updateNameDAL.Update11ResortWear(updateName);
            updateNameDAL.Update12ResortWear(updateName);
            updateNameDAL.Update13ResortWear(updateName);
            updateNameDAL.Update14ResortWear(updateName);
            updateNameDAL.Update15ResortWear(updateName);

            // Judge 1 On Stage Questions
            updateNameDAL.Update1OnStageQuestions(updateName);
            updateNameDAL.Update2OnStageQuestions(updateName);
            updateNameDAL.Update3OnStageQuestions(updateName);
            updateNameDAL.Update4OnStageQuestions(updateName);
            updateNameDAL.Update5OnStageQuestions(updateName);
            updateNameDAL.Update6OnStageQuestions(updateName);
            updateNameDAL.Update7OnStageQuestions(updateName);
            updateNameDAL.Update8OnStageQuestions(updateName);
            updateNameDAL.Update9OnStageQuestions(updateName);
            updateNameDAL.Update10OnStageQuestions(updateName);
            updateNameDAL.Update11OnStageQuestions(updateName);
            updateNameDAL.Update12OnStageQuestions(updateName);
            updateNameDAL.Update13OnStageQuestions(updateName);
            updateNameDAL.Update14OnStageQuestions(updateName);
            updateNameDAL.Update15OnStageQuestions(updateName);

            // Judge 2 Talent
            updateNameDAL.Update12Talent(updateName);
            updateNameDAL.Update22Talent(updateName);
            updateNameDAL.Update32Talent(updateName);
            updateNameDAL.Update42Talent(updateName);
            updateNameDAL.Update52Talent(updateName);
            updateNameDAL.Update62Talent(updateName);
            updateNameDAL.Update72Talent(updateName);
            updateNameDAL.Update82Talent(updateName);
            updateNameDAL.Update92Talent(updateName);
            updateNameDAL.Update102Talent(updateName);
            updateNameDAL.Update112Talent(updateName);
            updateNameDAL.Update122Talent(updateName);
            updateNameDAL.Update132Talent(updateName);
            updateNameDAL.Update142Talent(updateName);
            updateNameDAL.Update152Talent(updateName);

            // Judge 2 Production Number
            updateNameDAL.Update12ProductionNo2(updateName);
            updateNameDAL.Update22ProductionNo2(updateName);
            updateNameDAL.Update32ProductionNo2(updateName);
            updateNameDAL.Update42ProductionNo2(updateName);
            updateNameDAL.Update52ProductionNo2(updateName);
            updateNameDAL.Update62ProductionNo2(updateName);
            updateNameDAL.Update72ProductionNo2(updateName);
            updateNameDAL.Update82ProductionNo2(updateName);
            updateNameDAL.Update92ProductionNo2(updateName);
            updateNameDAL.Update102ProductionNo2(updateName);
            updateNameDAL.Update112ProductionNo2(updateName);
            updateNameDAL.Update122ProductionNo2(updateName);
            updateNameDAL.Update132ProductionNo2(updateName);
            updateNameDAL.Update142ProductionNo2(updateName);
            updateNameDAL.Update152ProductionNo2(updateName);

            // Judge 2 Evening Gown
            updateNameDAL.Update12EveningGown2(updateName);
            updateNameDAL.Update22EveningGown2(updateName);
            updateNameDAL.Update32EveningGown2(updateName);
            updateNameDAL.Update42EveningGown2(updateName);
            updateNameDAL.Update52EveningGown2(updateName);
            updateNameDAL.Update62EveningGown2(updateName);
            updateNameDAL.Update72EveningGown2(updateName);
            updateNameDAL.Update82EveningGown2(updateName);
            updateNameDAL.Update92EveningGown2(updateName);
            updateNameDAL.Update102EveningGown2(updateName);
            updateNameDAL.Update112EveningGown2(updateName);
            updateNameDAL.Update122EveningGown2(updateName);
            updateNameDAL.Update132EveningGown2(updateName);
            updateNameDAL.Update142EveningGown2(updateName);
            updateNameDAL.Update152EveningGown2(updateName);

            // Judge 2 Resort Wear
            updateNameDAL.Update12ResortWear2(updateName);
            updateNameDAL.Update22ResortWear2(updateName);
            updateNameDAL.Update32ResortWear2(updateName);
            updateNameDAL.Update42ResortWear2(updateName);
            updateNameDAL.Update52ResortWear2(updateName);
            updateNameDAL.Update62ResortWear2(updateName);
            updateNameDAL.Update72ResortWear2(updateName);
            updateNameDAL.Update82ResortWear2(updateName);
            updateNameDAL.Update92ResortWear2(updateName);
            updateNameDAL.Update102ResortWear2(updateName);
            updateNameDAL.Update112ResortWear2(updateName);
            updateNameDAL.Update122ResortWear2(updateName);
            updateNameDAL.Update132ResortWear2(updateName);
            updateNameDAL.Update142ResortWear2(updateName);
            updateNameDAL.Update152ResortWear2(updateName);

            // Judge 2 On Stage Questions
            updateNameDAL.Update12OnStageQuestions2(updateName);
            updateNameDAL.Update22OnStageQuestions2(updateName);
            updateNameDAL.Update32OnStageQuestions2(updateName);
            updateNameDAL.Update42OnStageQuestions2(updateName);
            updateNameDAL.Update52OnStageQuestions2(updateName);
            updateNameDAL.Update62OnStageQuestions2(updateName);
            updateNameDAL.Update72OnStageQuestions2(updateName);
            updateNameDAL.Update82OnStageQuestions2(updateName);
            updateNameDAL.Update92OnStageQuestions2(updateName);
            updateNameDAL.Update102OnStageQuestions2(updateName);
            updateNameDAL.Update112OnStageQuestions2(updateName);
            updateNameDAL.Update122OnStageQuestions2(updateName);
            updateNameDAL.Update132OnStageQuestions2(updateName);
            updateNameDAL.Update142OnStageQuestions2(updateName);
            updateNameDAL.Update152OnStageQuestions2(updateName);

            // Judge 3 Talent
            updateNameDAL.Update13Talent3(updateName);
            updateNameDAL.Update23Talent3(updateName);
            updateNameDAL.Update33Talent3(updateName);
            updateNameDAL.Update43Talent3(updateName);
            updateNameDAL.Update53Talent3(updateName);
            updateNameDAL.Update63Talent3(updateName);
            updateNameDAL.Update73Talent3(updateName);
            updateNameDAL.Update83Talent3(updateName);
            updateNameDAL.Update93Talent3(updateName);
            updateNameDAL.Update103Talent3(updateName);
            updateNameDAL.Update113Talent3(updateName);
            updateNameDAL.Update123Talent3(updateName);
            updateNameDAL.Update133Talent3(updateName);
            updateNameDAL.Update143Talent3(updateName);
            updateNameDAL.Update153Talent3(updateName);

            // Judge 3 Production Number
            updateNameDAL.Update13ProductionNo3(updateName);
            updateNameDAL.Update23ProductionNo3(updateName);
            updateNameDAL.Update33ProductionNo3(updateName);
            updateNameDAL.Update43ProductionNo3(updateName);
            updateNameDAL.Update53ProductionNo3(updateName);
            updateNameDAL.Update63ProductionNo3(updateName);
            updateNameDAL.Update73ProductionNo3(updateName);
            updateNameDAL.Update83ProductionNo3(updateName);
            updateNameDAL.Update93ProductionNo3(updateName);
            updateNameDAL.Update103ProductionNo3(updateName);
            updateNameDAL.Update113ProductionNo3(updateName);
            updateNameDAL.Update123ProductionNo3(updateName);
            updateNameDAL.Update133ProductionNo3(updateName);
            updateNameDAL.Update143ProductionNo3(updateName);
            updateNameDAL.Update153ProductionNo3(updateName);

            // Judge 3 Evening Gown
            updateNameDAL.Update13EveningGown3(updateName);
            updateNameDAL.Update23EveningGown3(updateName);
            updateNameDAL.Update33EveningGown3(updateName);
            updateNameDAL.Update43EveningGown3(updateName);
            updateNameDAL.Update53EveningGown3(updateName);
            updateNameDAL.Update63EveningGown3(updateName);
            updateNameDAL.Update73EveningGown3(updateName);
            updateNameDAL.Update83EveningGown3(updateName);
            updateNameDAL.Update93EveningGown3(updateName);
            updateNameDAL.Update103EveningGown3(updateName);
            updateNameDAL.Update113EveningGown3(updateName);
            updateNameDAL.Update123EveningGown3(updateName);
            updateNameDAL.Update133EveningGown3(updateName);
            updateNameDAL.Update143EveningGown3(updateName);
            updateNameDAL.Update153EveningGown3(updateName);

            // Judge 3 Resort Wear
            updateNameDAL.Update13ResortWear3(updateName);
            updateNameDAL.Update23ResortWear3(updateName);
            updateNameDAL.Update33ResortWear3(updateName);
            updateNameDAL.Update43ResortWear3(updateName);
            updateNameDAL.Update53ResortWear3(updateName);
            updateNameDAL.Update63ResortWear3(updateName);
            updateNameDAL.Update73ResortWear3(updateName);
            updateNameDAL.Update83ResortWear3(updateName);
            updateNameDAL.Update93ResortWear3(updateName);
            updateNameDAL.Update103ResortWear3(updateName);
            updateNameDAL.Update113ResortWear3(updateName);
            updateNameDAL.Update123ResortWear3(updateName);
            updateNameDAL.Update133ResortWear3(updateName);
            updateNameDAL.Update143ResortWear3(updateName);
            updateNameDAL.Update153ResortWear3(updateName);

            // Judge 3 On Stage Questions
            updateNameDAL.Update13OnStageQuestions3(updateName);
            updateNameDAL.Update23OnStageQuestions3(updateName);
            updateNameDAL.Update33OnStageQuestions3(updateName);
            updateNameDAL.Update43OnStageQuestions3(updateName);
            updateNameDAL.Update53OnStageQuestions3(updateName);
            updateNameDAL.Update63OnStageQuestions3(updateName);
            updateNameDAL.Update73OnStageQuestions3(updateName);
            updateNameDAL.Update83OnStageQuestions3(updateName);
            updateNameDAL.Update93OnStageQuestions3(updateName);
            updateNameDAL.Update103OnStageQuestions3(updateName);
            updateNameDAL.Update113OnStageQuestions3(updateName);
            updateNameDAL.Update123OnStageQuestions3(updateName);
            updateNameDAL.Update133OnStageQuestions3(updateName);
            updateNameDAL.Update143OnStageQuestions3(updateName);
            updateNameDAL.Update153OnStageQuestions3(updateName);

            // Judge 4 Talent
            updateNameDAL.Update14Talent4(updateName);
            updateNameDAL.Update24Talent4(updateName);
            updateNameDAL.Update34Talent4(updateName);
            updateNameDAL.Update44Talent4(updateName);
            updateNameDAL.Update54Talent4(updateName);
            updateNameDAL.Update64Talent4(updateName);
            updateNameDAL.Update74Talent4(updateName);
            updateNameDAL.Update84Talent4(updateName);
            updateNameDAL.Update94Talent4(updateName);
            updateNameDAL.Update104Talent4(updateName);
            updateNameDAL.Update114Talent4(updateName);
            updateNameDAL.Update124Talent4(updateName);
            updateNameDAL.Update134Talent4(updateName);
            updateNameDAL.Update144Talent4(updateName);
            updateNameDAL.Update154Talent4(updateName);

            // Judge 4 Production Number
            updateNameDAL.Update14ProductionNo4(updateName);
            updateNameDAL.Update24ProductionNo4(updateName);
            updateNameDAL.Update34ProductionNo4(updateName);
            updateNameDAL.Update44ProductionNo4(updateName);
            updateNameDAL.Update54ProductionNo4(updateName);
            updateNameDAL.Update64ProductionNo4(updateName);
            updateNameDAL.Update74ProductionNo4(updateName);
            updateNameDAL.Update84ProductionNo4(updateName);
            updateNameDAL.Update94ProductionNo4(updateName);
            updateNameDAL.Update104ProductionNo4(updateName);
            updateNameDAL.Update114ProductionNo4(updateName);
            updateNameDAL.Update124ProductionNo4(updateName);
            updateNameDAL.Update134ProductionNo4(updateName);
            updateNameDAL.Update144ProductionNo4(updateName);
            updateNameDAL.Update154ProductionNo4(updateName);

            // Judge 4 Evening Gown
            updateNameDAL.Update14EveningGown4(updateName);
            updateNameDAL.Update24EveningGown4(updateName);
            updateNameDAL.Update34EveningGown4(updateName);
            updateNameDAL.Update44EveningGown4(updateName);
            updateNameDAL.Update54EveningGown4(updateName);
            updateNameDAL.Update64EveningGown4(updateName);
            updateNameDAL.Update74EveningGown4(updateName);
            updateNameDAL.Update84EveningGown4(updateName);
            updateNameDAL.Update94EveningGown4(updateName);
            updateNameDAL.Update104EveningGown4(updateName);
            updateNameDAL.Update114EveningGown4(updateName);
            updateNameDAL.Update124EveningGown4(updateName);
            updateNameDAL.Update134EveningGown4(updateName);
            updateNameDAL.Update144EveningGown4(updateName);
            updateNameDAL.Update154EveningGown4(updateName);

            // Judge 4 Resort Wear
            updateNameDAL.Update14ResortWear4(updateName);
            updateNameDAL.Update24ResortWear4(updateName);
            updateNameDAL.Update34ResortWear4(updateName);
            updateNameDAL.Update44ResortWear4(updateName);
            updateNameDAL.Update54ResortWear4(updateName);
            updateNameDAL.Update64ResortWear4(updateName);
            updateNameDAL.Update74ResortWear4(updateName);
            updateNameDAL.Update84ResortWear4(updateName);
            updateNameDAL.Update94ResortWear4(updateName);
            updateNameDAL.Update104ResortWear4(updateName);
            updateNameDAL.Update114ResortWear4(updateName);
            updateNameDAL.Update124ResortWear4(updateName);
            updateNameDAL.Update134ResortWear4(updateName);
            updateNameDAL.Update144ResortWear4(updateName);
            updateNameDAL.Update154ResortWear4(updateName);

            // Judge 4 On Stage Questions
            updateNameDAL.Update14OnStageQuestions4(updateName);
            updateNameDAL.Update24OnStageQuestions4(updateName);
            updateNameDAL.Update34OnStageQuestions4(updateName);
            updateNameDAL.Update44OnStageQuestions4(updateName);
            updateNameDAL.Update54OnStageQuestions4(updateName);
            updateNameDAL.Update64OnStageQuestions4(updateName);
            updateNameDAL.Update74OnStageQuestions4(updateName);
            updateNameDAL.Update84OnStageQuestions4(updateName);
            updateNameDAL.Update94OnStageQuestions4(updateName);
            updateNameDAL.Update104OnStageQuestions4(updateName);
            updateNameDAL.Update114OnStageQuestions4(updateName);
            updateNameDAL.Update124OnStageQuestions4(updateName);
            updateNameDAL.Update134OnStageQuestions4(updateName);
            updateNameDAL.Update144OnStageQuestions4(updateName);
            updateNameDAL.Update154OnStageQuestions4(updateName);

            // Judge 5 Talent
            updateNameDAL.Update15Talent5(updateName);
            updateNameDAL.Update25Talent5(updateName);
            updateNameDAL.Update35Talent5(updateName);
            updateNameDAL.Update45Talent5(updateName);
            updateNameDAL.Update55Talent5(updateName);
            updateNameDAL.Update65Talent5(updateName);
            updateNameDAL.Update75Talent5(updateName);
            updateNameDAL.Update85Talent5(updateName);
            updateNameDAL.Update95Talent5(updateName);
            updateNameDAL.Update105Talent5(updateName);
            updateNameDAL.Update115Talent5(updateName);
            updateNameDAL.Update125Talent5(updateName);
            updateNameDAL.Update135Talent5(updateName);
            updateNameDAL.Update145Talent5(updateName);
            updateNameDAL.Update155Talent5(updateName);

            // Judge 5 Production Number
            updateNameDAL.Update15ProductionNo5(updateName);
            updateNameDAL.Update25ProductionNo5(updateName);
            updateNameDAL.Update35ProductionNo5(updateName);
            updateNameDAL.Update45ProductionNo5(updateName);
            updateNameDAL.Update55ProductionNo5(updateName);
            updateNameDAL.Update65ProductionNo5(updateName);
            updateNameDAL.Update75ProductionNo5(updateName);
            updateNameDAL.Update85ProductionNo5(updateName);
            updateNameDAL.Update95ProductionNo5(updateName);
            updateNameDAL.Update105ProductionNo5(updateName);
            updateNameDAL.Update115ProductionNo5(updateName);
            updateNameDAL.Update125ProductionNo5(updateName);
            updateNameDAL.Update135ProductionNo5(updateName);
            updateNameDAL.Update145ProductionNo5(updateName);
            updateNameDAL.Update155ProductionNo5(updateName);

            // Judge 5 Evening Gown
            updateNameDAL.Update15EveningGown5(updateName);
            updateNameDAL.Update25EveningGown5(updateName);
            updateNameDAL.Update35EveningGown5(updateName);
            updateNameDAL.Update45EveningGown5(updateName);
            updateNameDAL.Update55EveningGown5(updateName);
            updateNameDAL.Update65EveningGown5(updateName);
            updateNameDAL.Update75EveningGown5(updateName);
            updateNameDAL.Update85EveningGown5(updateName);
            updateNameDAL.Update95EveningGown5(updateName);
            updateNameDAL.Update105EveningGown5(updateName);
            updateNameDAL.Update115EveningGown5(updateName);
            updateNameDAL.Update125EveningGown5(updateName);
            updateNameDAL.Update135EveningGown5(updateName);
            updateNameDAL.Update145EveningGown5(updateName);
            updateNameDAL.Update155EveningGown5(updateName);

            // Judge 5 Resort Wear
            updateNameDAL.Update15ResortWear5(updateName);
            updateNameDAL.Update25ResortWear5(updateName);
            updateNameDAL.Update35ResortWear5(updateName);
            updateNameDAL.Update45ResortWear5(updateName);
            updateNameDAL.Update55ResortWear5(updateName);
            updateNameDAL.Update65ResortWear5(updateName);
            updateNameDAL.Update75ResortWear5(updateName);
            updateNameDAL.Update85ResortWear5(updateName);
            updateNameDAL.Update95ResortWear5(updateName);
            updateNameDAL.Update105ResortWear5(updateName);
            updateNameDAL.Update115ResortWear5(updateName);
            updateNameDAL.Update125ResortWear5(updateName);
            updateNameDAL.Update135ResortWear5(updateName);
            updateNameDAL.Update145ResortWear5(updateName);
            updateNameDAL.Update155ResortWear5(updateName);

            // Judge 5 On Stage Questions
            updateNameDAL.Update15OnStageQuestions5(updateName);
            updateNameDAL.Update25OnStageQuestions5(updateName);
            updateNameDAL.Update35OnStageQuestions5(updateName);
            updateNameDAL.Update45OnStageQuestions5(updateName);
            updateNameDAL.Update55OnStageQuestions5(updateName);
            updateNameDAL.Update65OnStageQuestions5(updateName);
            updateNameDAL.Update75OnStageQuestions5(updateName);
            updateNameDAL.Update85OnStageQuestions5(updateName);
            updateNameDAL.Update95OnStageQuestions5(updateName);
            updateNameDAL.Update105OnStageQuestions5(updateName);
            updateNameDAL.Update115OnStageQuestions5(updateName);
            updateNameDAL.Update125OnStageQuestions5(updateName);
            updateNameDAL.Update135OnStageQuestions5(updateName);
            updateNameDAL.Update145OnStageQuestions5(updateName);
            updateNameDAL.Update155OnStageQuestions5(updateName);

            // If the data is successfully updated then the value of success will be true else it will be false
            if (success == true)
            {
                // Data successfully updated 
                MessageBox.Show("Name successfully updated.", "Updated Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Failed to update data
                MessageBox.Show("Failed to update new name information.", "Update Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        UpdateCandidateImageBLL updateImgBLL = new UpdateCandidateImageBLL();
        Judge1UpdateCandidateImageDAL updateImgDAL = new Judge1UpdateCandidateImageDAL();
        private void UpdateCandidateImage()
        {
            // Getting data from UI
            updateImgBLL.ImageNo = Convert.ToInt32(txtImageID.Text);
            updateImgBLL.PathImage = lblFilenameImage.Text;

            // Updating data into Database
            bool success = updateImgDAL.UpdateImage(updateImgBLL);

            // If the data is successfully updated then the value of success will be true else it will be false
            if (success == true)
            {
                // Data successfully updated
                MessageBox.Show("Image successfully updated Judge1.", "Judge1 Updated Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Failed to updated data
                MessageBox.Show("Failed to update new image information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        Judge1UpdateCandidateImageTop5DAL updateImgTop5DAL = new Judge1UpdateCandidateImageTop5DAL();
        private void UpdateCandidateImageTop5()
        {
            // Getting data from UI
            updateImgBLL.ImageNo = Convert.ToInt32(txtImageIDTop5.Text);
            updateImgBLL.PathImage = lblImageFilenameTop5.Text;

            // Updating data into Database
            bool success = updateImgTop5DAL.UpdateImageTop5(updateImgBLL);

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

        private bool DeleteAllImages()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_Pictures";
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

        private bool DeleteAllImagesTop5()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_PicturesTop5";
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

        private bool DeleteAllImagesWelcomeForm()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_PicturesWelcomeForm";
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

        private void btnDeleteAllImages_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete all images?", "Delete All Images!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                DeleteAllImages();
            }
        }

        private void btnUpdateImage_Click(object sender, EventArgs e)
        {
            if (ValidateUpdateButton())
            {
                UpdateCandidateImage();
            }
        }

        private void btnUpdateCandidateName_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to update names?", "Update Names!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                UpdateCandidateNames();
            }
        }
        private void btnJudge1TalentSend_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalent())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    FillTxtboxJudge1Name();
                    SendScoreJudge1Talent();

                    btnJudge1TalentSend.Enabled = false;
                    btnJudge1TalentSend.LabelText = "Scores already sent!";
                    btnJudge1TalentSend.Font = new Font("Arial", 10, FontStyle.Regular); 
                }
            } 
        }

        private void btnJudge1ProductionNumberSend_Click(object sender, EventArgs e)
        {
            if (ValidatorForm())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    
                    FillTxtboxJudge1Name();
                    SendScoreJudge1ProductionNo();

                    btnJudge1ProductionNumberSend.Enabled = false;
                    btnJudge1ProductionNumberSend.LabelText = "Scores already sent!";
                    btnJudge1ProductionNumberSend.Font = new Font("Arial", 10, FontStyle.Regular); 
                }
            }             
        }

        private void btnJudge1EveningGownSend_Click(object sender, EventArgs e)
        {
            if (ValidatorForm())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SendScoreJudge1EveningGown();

                    btnJudge1EveningGownSend.Enabled = false;
                    btnJudge1EveningGownSend.LabelText = "Scores already sent!";
                    btnJudge1EveningGownSend.Font = new Font("Arial", 10, FontStyle.Regular); 
                }
            }             
        }

        private void btnJudge1ResortWearSend_Click(object sender, EventArgs e)
        {            
            if (ValidatorForm())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SendScoreJudge1ResortWear();

                    btnJudge1ResortWearSend.Enabled = false;
                    btnJudge1ResortWearSend.LabelText = "Scores already sent!";
                    btnJudge1ResortWearSend.Font = new Font("Arial", 10, FontStyle.Regular); 
                }
            }             
        }

        private void btnJudge1OnStageQuestionsSend_Click(object sender, EventArgs e)
        {            
            if (ValidatorForm())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SendScoreJudge1OnStageQuestions();

                    btnJudge1OnStageQuestionsSend.Enabled = false;
                    btnJudge1OnStageQuestionsSend.LabelText = "Scores already sent!";
                    btnJudge1OnStageQuestionsSend.Font = new Font("Arial", 10, FontStyle.Regular); 
                }
            }             
        }

        private void btnJudge1Top5Send_Click(object sender, EventArgs e)
        {            
            if (ValidatorForm())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SendScoreJudge1Top5();

                    btnJudge1Top5Send.Enabled = false;
                    btnJudge1Top5Send.LabelText = "Scores already sent!";
                    btnJudge1Top5Send.Font = new Font("Arial", 10, FontStyle.Regular); 
                }
            }            
        }

        private void btnBrowseImageTop5_Click(object sender, EventArgs e)
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
                        File.Copy(ofd.FileName, paths + @"\Images\Top5\" + correctFileName, true);
                        lblImageFilenameTop5.Text = correctFileName;
                        pctrBoxCandidatesTop5.Image = Image.FromFile(ofd.FileName);

                        txtImageIDTop5.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Browse Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnDeleteAllImagesTop5_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete all images?", "Delete All Images!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                DeleteAllImagesTop5();
            }
        }

        private void btnUpdateImageTop5_Click(object sender, EventArgs e)
        {
            if (ValidateUpdateButtonTop5())
            {
                UpdateCandidateImageTop5();
            }
        }

        PathImageTop5DAL pathImageTop5DAL = new PathImageTop5DAL();
        private void btnSaveImageTop5_Click(object sender, EventArgs e)
        {
           if (ValidateSaveButtonImageTop5())
            {
                string correctFileName = Path.GetFileName(ofd.FileName);

                // Getting data from UI
                pathImgBLL.ImageNo = Convert.ToInt32(txtImageIDTop5.Text);
                pathImgBLL.PathImage = correctFileName;

                // Inserting data into Database
                bool success = pathImageTop5DAL.Insert(pathImgBLL);

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

        private void btnUpdateCandNoAndCandNamesTop5_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to update candidate # and names?", "Update No. & Names!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                UpdateCandNoAndCandNamesTop5();
            }
        }
        
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
                        File.Copy(ofd.FileName, paths + @"\Images\WelcomeForm\" + correctFileName, true);
                        lblImageFilenameWelcomeForm.Text = correctFileName;
                        pctrBoxWelcomeMissAloguinsan.Image = Image.FromFile(ofd.FileName);                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Browse Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        PathImageWelcomeFormDAL pathImageWelcomeFormDAL = new PathImageWelcomeFormDAL();
        private void btnSaveImageWelcomeForm_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to save the image?", "Save Welcome Form Images!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                string correctFileName = Path.GetFileName(ofd.FileName);

                // Getting data from UI
                pathImgBLL.PathImage = correctFileName;

                // Inserting data into Database
                bool success = pathImageWelcomeFormDAL.Insert(pathImgBLL);

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

        Judge1UpdateWelcomeFormImageDAL updateImgWelcomeFormDAL = new Judge1UpdateWelcomeFormImageDAL();
        private void btnUpdateImageWelcomeForm_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to update the image?", "Update Welcome Form Image!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                // Getting data from UI
                updateImgBLL.PathImage = lblImageFilenameWelcomeForm.Text;

                // Updating data into Database
                bool success = updateImgWelcomeFormDAL.UpdateImage(updateImgBLL);

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

        private void btnDeleteAllImagesWelcomeForm_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete all images?", "Delete All Images!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                DeleteAllImagesWelcomeForm();
            }
        }

        OpenFileDialog ofd = new OpenFileDialog();
        private void btnBrowsePicture_Click(object sender, EventArgs e)
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
                        File.Copy(ofd.FileName, paths + @"\Images\Candidates\" + correctFileName, true);
                        lblFilenameImage.Text = correctFileName;
                        pctrBoxCandidates.Image = Image.FromFile(ofd.FileName);

                        txtImageID.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Browse Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        PathImageBLL pathImgBLL = new PathImageBLL();
        PathImageDAL pathImgDAL = new PathImageDAL();
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateSaveButton())
            {
                string correctFileName = Path.GetFileName(ofd.FileName);

                // Getting data from UI
                pathImgBLL.ImageNo = Convert.ToInt32(txtImageID.Text);
                pathImgBLL.PathImage = correctFileName;

                // Inserting data into Database
                bool success = pathImgDAL.Insert(pathImgBLL);

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

                // Refreshing Data Grid View
                //DataTable dt = dal.Select();
                //dgvResponders.DataSource = dt;

                //DataView dv = dt.DefaultView;
                //dv.Sort = "IDNumber";
                //DataTable sortedDT = dv.ToTable(); 
            }
        }

        private Timer timerJ1Name;
        public void InitTimerJ1Name()
        {
            timerJ1Name = new Timer();
            timerJ1Name.Tick += new EventHandler(timerJudge1Name_Tick);
            timerJ1Name.Interval = 3000; // in milliseconds
            timerJ1Name.Start();
        }

        private void timerJudge1Name_Tick(object sender, EventArgs e)
        {            
            timerJ1Name.Start();
            FillTxtboxJudge1Name();             
        }

        private bool ValidateUpdateButton()
        {
            bool bStatus = true;

            int imgNo;
            int.TryParse(txtImageID.Text, out imgNo);

            if (imgNo == 0 || imgNo > 15)
            {
                MessageBox.Show("Please input the Image ID & not greater than 15 candidates", "Update Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidateUpdateButtonTop5()
        {
            bool bStatus = true;

            int imgNo;
            int.TryParse(txtImageIDTop5.Text, out imgNo);

            if (imgNo == 0 || imgNo > 5)
            {
                MessageBox.Show("Please input the Image ID & not greater than 5 candidates", "Update Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidateSaveButton()
        {
            bool bStatus = true;

            int imgNo;
            int.TryParse(txtImageID.Text, out imgNo);

            if (imgNo == 0 || imgNo > 15)
            {
                MessageBox.Show("Please input the Image ID & not greater than 15 candidates", "Save Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidateSaveButtonImageTop5()
        {
            bool bStatus = true;

            int imgNo;
            int.TryParse(txtImageIDTop5.Text, out imgNo);

            if (imgNo == 0 || imgNo > 5)
            {
                MessageBox.Show("Please input the Image ID & not greater than 5 candidates", "Save Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorForm()
        {
            bool bStatus = true;

            float score1, score2, score3, score4, score5, score6, score7, score8, score9, score10, score11, score12, score13, score14, score15, score1Top5, score2Top5, score3Top5, score4Top5, score5Top5;
            float.TryParse(txtScore1.Text, out score1);
            float.TryParse(txtScore2.Text, out score2);
            float.TryParse(txtScore3.Text, out score3);
            float.TryParse(txtScore4.Text, out score4);
            float.TryParse(txtScore5.Text, out score5);
            float.TryParse(txtScore6.Text, out score6);
            float.TryParse(txtScore7.Text, out score7);
            float.TryParse(txtScore8.Text, out score8);
            float.TryParse(txtScore9.Text, out score9);
            float.TryParse(txtScore10.Text, out score10);
            float.TryParse(txtScore11.Text, out score11);
            float.TryParse(txtScore12.Text, out score12);
            float.TryParse(txtScore13.Text, out score13);
            float.TryParse(txtScore14.Text, out score14);
            float.TryParse(txtScore15.Text, out score15);
            float.TryParse(txtScore1Top5.Text, out score1Top5);
            float.TryParse(txtScore2Top5.Text, out score2Top5);
            float.TryParse(txtScore3Top5.Text, out score3Top5);
            float.TryParse(txtScore4Top5.Text, out score4Top5);
            float.TryParse(txtScore5Top5.Text, out score5Top5);

            if (score1 > 10)
            {
                errorProviderJudge1.SetError(txtScore1, "10 is the highest score");
                bStatus = false;
            }
            else if (score2 > 10)
            {
                errorProviderJudge1.SetError(txtScore2, "10 is the highest score");
                bStatus = false;
            }
            else if (score3 > 10)
            {
                errorProviderJudge1.SetError(txtScore3, "10 is the highest score");
                bStatus = false;
            }
            else if (score4 > 10)
            {
                errorProviderJudge1.SetError(txtScore4, "10 is the highest score");
                bStatus = false;
            }
            else if (score5 > 10)
            {
                errorProviderJudge1.SetError(txtScore5, "10 is the highest score");
                bStatus = false;
            }
            else if (score6 > 10)
            {
                errorProviderJudge1.SetError(txtScore6, "10 is the highest score");
                bStatus = false;
            }
            else if (score7 > 10)
            {
                errorProviderJudge1.SetError(txtScore7, "10 is the highest score");
                bStatus = false;
            }
            else if (score8 > 10)
            {
                errorProviderJudge1.SetError(txtScore8, "10 is the highest score");
                bStatus = false;
            }
            else if (score9 > 10)
            {
                errorProviderJudge1.SetError(txtScore9, "10 is the highest score");
                bStatus = false;
            }
            else if (score10 > 10)
            {
                errorProviderJudge1.SetError(txtScore10, "10 is the highest score");
                bStatus = false;
            }
            else if (score11 > 10)
            {
                errorProviderJudge1.SetError(txtScore11, "10 is the highest score");
                bStatus = false;
            }
            else if (score12 > 10)
            {
                errorProviderJudge1.SetError(txtScore12, "10 is the highest score");
                bStatus = false;
            }
            else if (score13 > 10)
            {
                errorProviderJudge1.SetError(txtScore13, "10 is the highest score");
                bStatus = false;
            }
            else if (score14 > 10)
            {
                errorProviderJudge1.SetError(txtScore14, "10 is the highest score");
                bStatus = false;
            }
            else if (score15 > 10)
            {
                errorProviderJudge1.SetError(txtScore15, "10 is the highest score");
                bStatus = false;
            }
            else if (score1Top5 > 10)
            {
                errorProviderJudge1.SetError(txtScore1Top5, "10 is the highest score");
                bStatus = false;
            }
            else if (score2Top5 > 10)
            {
                errorProviderJudge1.SetError(txtScore2Top5, "10 is the highest score");
                bStatus = false;
            }
            else if (score3Top5 > 10)
            {
                errorProviderJudge1.SetError(txtScore3Top5, "10 is the highest score");
                bStatus = false;
            }
            else if (score4Top5 > 10)
            {
                errorProviderJudge1.SetError(txtScore4Top5, "10 is the highest score");
                bStatus = false;
            }
            else if (score5Top5 > 10)
            {
                errorProviderJudge1.SetError(txtScore5Top5, "10 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalent()
        {
            bool bStatus = true;

            float score1, score2, score3, score4, score5, score6, score7, score8, score9, score10, score11, score12, score13, score14, score15;
            float.TryParse(txtScore1.Text, out score1);
            float.TryParse(txtScore2.Text, out score2);
            float.TryParse(txtScore3.Text, out score3);
            float.TryParse(txtScore4.Text, out score4);
            float.TryParse(txtScore5.Text, out score5);
            float.TryParse(txtScore6.Text, out score6);
            float.TryParse(txtScore7.Text, out score7);
            float.TryParse(txtScore8.Text, out score8);
            float.TryParse(txtScore9.Text, out score9);
            float.TryParse(txtScore10.Text, out score10);
            float.TryParse(txtScore11.Text, out score11);
            float.TryParse(txtScore12.Text, out score12);
            float.TryParse(txtScore13.Text, out score13);
            float.TryParse(txtScore14.Text, out score14);
            float.TryParse(txtScore15.Text, out score15);            

            if (score1 > 100)
            {
                errorProviderJudge1.SetError(txtScore1, "100 is the highest score");
                bStatus = false;
            }
            else if (score2 > 100)
            {
                errorProviderJudge1.SetError(txtScore2, "100 is the highest score");
                bStatus = false;
            }
            else if (score3 > 100)
            {
                errorProviderJudge1.SetError(txtScore3, "100 is the highest score");
                bStatus = false;
            }
            else if (score4 > 100)
            {
                errorProviderJudge1.SetError(txtScore4, "100 is the highest score");
                bStatus = false;
            }
            else if (score5 > 100)
            {
                errorProviderJudge1.SetError(txtScore5, "100 is the highest score");
                bStatus = false;
            }
            else if (score6 > 100)
            {
                errorProviderJudge1.SetError(txtScore6, "100 is the highest score");
                bStatus = false;
            }
            else if (score7 > 100)
            {
                errorProviderJudge1.SetError(txtScore7, "100 is the highest score");
                bStatus = false;
            }
            else if (score8 > 100)
            {
                errorProviderJudge1.SetError(txtScore8, "100 is the highest score");
                bStatus = false;
            }
            else if (score9 > 100)
            {
                errorProviderJudge1.SetError(txtScore9, "100 is the highest score");
                bStatus = false;
            }
            else if (score10 > 100)
            {
                errorProviderJudge1.SetError(txtScore10, "100 is the highest score");
                bStatus = false;
            }
            else if (score11 > 100)
            {
                errorProviderJudge1.SetError(txtScore11, "100 is the highest score");
                bStatus = false;
            }
            else if (score12 > 100)
            {
                errorProviderJudge1.SetError(txtScore12, "100 is the highest score");
                bStatus = false;
            }
            else if (score13 > 100)
            {
                errorProviderJudge1.SetError(txtScore13, "100 is the highest score");
                bStatus = false;
            }
            else if (score14 > 100)
            {
                errorProviderJudge1.SetError(txtScore14, "100 is the highest score");
                bStatus = false;
            }
            else if (score15 > 100)
            {
                errorProviderJudge1.SetError(txtScore15, "100 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private void Clear()
        {            
            txtScore1.Text = null;
            txtScore2.Text = "0.0";
            txtScore3.Text = "0.0";
            txtScore4.Text = "0.0";
            txtScore5.Text = "0.0";
            txtScore6.Text = "0.0";
            txtScore7.Text = "0.0";
            txtScore8.Text = "0.0";
            txtScore9.Text = "0.0";
            txtScore10.Text = "0.0";
            txtScore11.Text = "0.0";
            txtScore12.Text = "0.0";
            txtScore13.Text = "0.0";
            txtScore14.Text = "0.0";
            txtScore15.Text = "0.0";

            // TextBoxes Scores Top 5            
            txtScore1Top5.Text = null;
            txtScore2Top5.Text = "0.0";
            txtScore3Top5.Text = "0.0";
            txtScore4Top5.Text = "0.0";
            txtScore5Top5.Text = "0.0";
        }

        private void ShowPanel1BestInTalentAndHideRestPanel()
        {
            panel1BestInTalent.Show();
            panel1BestInTalent.BringToFront();

            txtScore1Talent1.Focus();
        }

        private void ProceedToPanel2BestInTalent()
        {
            panel1BestInTalent.Hide();

            panel2BestInTalent.Show();
            panel2BestInTalent.BringToFront();

            txtScore2Talent1.Focus();
        }

        private void ProceedToPanel3BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();

            panel3BestInTalent.Show();
            panel3BestInTalent.BringToFront();

            txtScore3Talent1.Focus();
        }

        private void ProceedToPanel4BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();

            panel4BestInTalent.Show();
            panel4BestInTalent.BringToFront();

            txtScore4Talent1.Focus();
        }

        private void ProceedToPanel5BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();
            panel4BestInTalent.Hide();

            panel5BestInTalent.Show();
            panel5BestInTalent.BringToFront();

            txtScore5Talent1.Focus();
        }

        private void ProceedToPanel6BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();
            panel4BestInTalent.Hide();
            panel5BestInTalent.Hide();

            panel6BestInTalent.Show();
            panel6BestInTalent.BringToFront();

            txtScore6Talent1.Focus();
        }

        private void ProceedToPanel7BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();
            panel4BestInTalent.Hide();
            panel5BestInTalent.Hide();
            panel6BestInTalent.Hide();

            panel7BestInTalent.Show();
            panel7BestInTalent.BringToFront();

            txtScore7Talent1.Focus();
        }

        private void ProceedToPanel8BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();
            panel4BestInTalent.Hide();
            panel5BestInTalent.Hide();
            panel6BestInTalent.Hide();
            panel7BestInTalent.Hide();

            panel8BestInTalent.Show();
            panel8BestInTalent.BringToFront();

            txtScore8Talent1.Focus();
        }

        private void ProceedToPanel9BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();
            panel4BestInTalent.Hide();
            panel5BestInTalent.Hide();
            panel6BestInTalent.Hide();
            panel7BestInTalent.Hide();
            panel8BestInTalent.Hide();

            panel9BestInTalent.Show();
            panel9BestInTalent.BringToFront();

            txtScore9Talent1.Focus();
        }

        private void ProceedToPanel10BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();
            panel4BestInTalent.Hide();
            panel5BestInTalent.Hide();
            panel6BestInTalent.Hide();
            panel7BestInTalent.Hide();
            panel8BestInTalent.Hide();
            panel9BestInTalent.Hide();

            panel10BestInTalent.Show();
            panel10BestInTalent.BringToFront();

            txtScore10Talent1.Focus();
        }

        private void ProceedToPanel11BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();
            panel4BestInTalent.Hide();
            panel5BestInTalent.Hide();
            panel6BestInTalent.Hide();
            panel7BestInTalent.Hide();
            panel8BestInTalent.Hide();
            panel9BestInTalent.Hide();
            panel10BestInTalent.Hide();

            panel11BestInTalent.Show();
            panel11BestInTalent.BringToFront();

            txtScore11Talent1.Focus();
        }

        private void ProceedToPanel12BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();
            panel4BestInTalent.Hide();
            panel5BestInTalent.Hide();
            panel6BestInTalent.Hide();
            panel7BestInTalent.Hide();
            panel8BestInTalent.Hide();
            panel9BestInTalent.Hide();
            panel10BestInTalent.Hide();
            panel11BestInTalent.Hide();

            panel12BestInTalent.Show();
            panel12BestInTalent.BringToFront();

            txtScore12Talent1.Focus();
        }

        private void ProceedToPanel13BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();
            panel4BestInTalent.Hide();
            panel5BestInTalent.Hide();
            panel6BestInTalent.Hide();
            panel7BestInTalent.Hide();
            panel8BestInTalent.Hide();
            panel9BestInTalent.Hide();
            panel10BestInTalent.Hide();
            panel11BestInTalent.Hide();
            panel12BestInTalent.Hide();

            panel13BestInTalent.Show();
            panel13BestInTalent.BringToFront();

            txtScore13Talent1.Focus();
        }

        private void ProceedToPanel14BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();
            panel4BestInTalent.Hide();
            panel5BestInTalent.Hide();
            panel6BestInTalent.Hide();
            panel7BestInTalent.Hide();
            panel8BestInTalent.Hide();
            panel9BestInTalent.Hide();
            panel10BestInTalent.Hide();
            panel11BestInTalent.Hide();
            panel12BestInTalent.Hide();
            panel13BestInTalent.Hide();

            panel14BestInTalent.Show();
            panel14BestInTalent.BringToFront();

            txtScore14Talent1.Focus();
        }

        private void ProceedToPanel15BestInTalent()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();
            panel4BestInTalent.Hide();
            panel5BestInTalent.Hide();
            panel6BestInTalent.Hide();
            panel7BestInTalent.Hide();
            panel8BestInTalent.Hide();
            panel9BestInTalent.Hide();
            panel10BestInTalent.Hide();
            panel11BestInTalent.Hide();
            panel12BestInTalent.Hide();
            panel13BestInTalent.Hide();
            panel14BestInTalent.Hide();

            panel15BestInTalent.Show();
            panel15BestInTalent.BringToFront();

            txtScore15Talent1.Focus();
        }

        private void HideAllBestInTalentPanel()
        {
            panel1BestInTalent.Hide();
            panel2BestInTalent.Hide();
            panel3BestInTalent.Hide();
            panel4BestInTalent.Hide();
            panel5BestInTalent.Hide();
            panel6BestInTalent.Hide();
            panel7BestInTalent.Hide();
            panel8BestInTalent.Hide();
            panel9BestInTalent.Hide();
            panel10BestInTalent.Hide();
            panel11BestInTalent.Hide();
            panel12BestInTalent.Hide();
            panel13BestInTalent.Hide();
            panel14BestInTalent.Hide();
            panel15BestInTalent.Hide();
        }        

        private void TabStopAllTxtScore1_15()
        {
            txtScore1.TabStop = false;
            txtScore2.TabStop = false;
            txtScore3.TabStop = false;
            txtScore4.TabStop = false;
            txtScore5.TabStop = false;
            txtScore6.TabStop = false;
            txtScore7.TabStop = false;
            txtScore8.TabStop = false;
            txtScore9.TabStop = false;
            txtScore10.TabStop = false;
            txtScore11.TabStop = false;
            txtScore12.TabStop = false;
            txtScore13.TabStop = false;
            txtScore14.TabStop = false;
            txtScore15.TabStop = false;
        }

        private void TabStopAllToTrueTxtScore1_15()
        {
            txtScore1.TabStop = true;
            txtScore2.TabStop = true;
            txtScore3.TabStop = true;
            txtScore4.TabStop = true;
            txtScore5.TabStop = true;
            txtScore6.TabStop = true;
            txtScore7.TabStop = true;
            txtScore8.TabStop = true;
            txtScore9.TabStop = true;
            txtScore10.TabStop = true;
            txtScore11.TabStop = true;
            txtScore12.TabStop = true;
            txtScore13.TabStop = true;
            txtScore14.TabStop = true;
            txtScore15.TabStop = true;
        }

        private bool ValidatorFormBestInTalentCandidate1()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore1Talent1.Text, out score1);
            float.TryParse(txtScore1Talent2.Text, out score2);
            float.TryParse(txtScore1Talent3.Text, out score3);
            float.TryParse(txtScore1Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore1Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore1Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore1Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore1Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }        

        private bool ValidatorFormBestInTalentCandidate2()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore2Talent1.Text, out score1);
            float.TryParse(txtScore2Talent2.Text, out score2);
            float.TryParse(txtScore2Talent3.Text, out score3);
            float.TryParse(txtScore2Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore2Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore2Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore2Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore2Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate3()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore3Talent1.Text, out score1);
            float.TryParse(txtScore3Talent2.Text, out score2);
            float.TryParse(txtScore3Talent3.Text, out score3);
            float.TryParse(txtScore3Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore3Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore3Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore3Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore3Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate4()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore4Talent1.Text, out score1);
            float.TryParse(txtScore4Talent2.Text, out score2);
            float.TryParse(txtScore4Talent3.Text, out score3);
            float.TryParse(txtScore4Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore4Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore4Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore4Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore4Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate5()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore5Talent1.Text, out score1);
            float.TryParse(txtScore5Talent2.Text, out score2);
            float.TryParse(txtScore5Talent3.Text, out score3);
            float.TryParse(txtScore5Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore5Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore5Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore5Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore5Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate6()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore6Talent1.Text, out score1);
            float.TryParse(txtScore6Talent2.Text, out score2);
            float.TryParse(txtScore6Talent3.Text, out score3);
            float.TryParse(txtScore6Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore6Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore6Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore6Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore6Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate7()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore7Talent1.Text, out score1);
            float.TryParse(txtScore7Talent2.Text, out score2);
            float.TryParse(txtScore7Talent3.Text, out score3);
            float.TryParse(txtScore7Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore7Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore7Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore7Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore7Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate8()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore8Talent1.Text, out score1);
            float.TryParse(txtScore8Talent2.Text, out score2);
            float.TryParse(txtScore8Talent3.Text, out score3);
            float.TryParse(txtScore8Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore8Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore8Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore8Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore8Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate9()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore9Talent1.Text, out score1);
            float.TryParse(txtScore9Talent2.Text, out score2);
            float.TryParse(txtScore9Talent3.Text, out score3);
            float.TryParse(txtScore9Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore9Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore9Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore9Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore9Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate10()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore10Talent1.Text, out score1);
            float.TryParse(txtScore10Talent2.Text, out score2);
            float.TryParse(txtScore10Talent3.Text, out score3);
            float.TryParse(txtScore10Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore10Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore10Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore10Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore10Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate11()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore11Talent1.Text, out score1);
            float.TryParse(txtScore11Talent2.Text, out score2);
            float.TryParse(txtScore11Talent3.Text, out score3);
            float.TryParse(txtScore11Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore11Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore11Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore11Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore11Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate12()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore12Talent1.Text, out score1);
            float.TryParse(txtScore12Talent2.Text, out score2);
            float.TryParse(txtScore12Talent3.Text, out score3);
            float.TryParse(txtScore12Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore12Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore12Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore12Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore12Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate13()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore13Talent1.Text, out score1);
            float.TryParse(txtScore13Talent2.Text, out score2);
            float.TryParse(txtScore13Talent3.Text, out score3);
            float.TryParse(txtScore13Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore13Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore13Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore13Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore13Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate14()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore14Talent1.Text, out score1);
            float.TryParse(txtScore14Talent2.Text, out score2);
            float.TryParse(txtScore14Talent3.Text, out score3);
            float.TryParse(txtScore14Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore14Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore14Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore14Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore14Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private bool ValidatorFormBestInTalentCandidate15()
        {
            bool bStatus = true;

            float score1, score2, score3, score4;
            float.TryParse(txtScore15Talent1.Text, out score1);
            float.TryParse(txtScore15Talent2.Text, out score2);
            float.TryParse(txtScore15Talent3.Text, out score3);
            float.TryParse(txtScore15Talent4.Text, out score4);

            if (score1 > 20)
            {
                errorProviderJudge1.SetError(txtScore15Talent1, "20 is the highest score");
                bStatus = false;
            }
            else if (score2 > 25)
            {
                errorProviderJudge1.SetError(txtScore15Talent2, "25 is the highest score");
                bStatus = false;
            }
            else if (score3 > 25)
            {
                errorProviderJudge1.SetError(txtScore15Talent3, "25 is the highest score");
                bStatus = false;
            }
            else if (score4 > 30)
            {
                errorProviderJudge1.SetError(txtScore15Talent4, "30 is the highest score");
                bStatus = false;
            }

            return bStatus;
        }

        private void btnScoreCandidate1_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate1())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore1Talent1.Text, out score1);
                    float.TryParse(txtScore1Talent2.Text, out score2);
                    float.TryParse(txtScore1Talent3.Text, out score3);
                    float.TryParse(txtScore1Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score1 = total;
                                    

                    // Updating data into Database
                    bool success = judge1DAL.Update1(judge1BLL);
                    judge1DAL.UpdateDependency1(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        FillTxtboxJudge1Name();
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate1.Enabled = false;
                    btnScoreCandidate1.LabelText = "Scores already sent!";
                    btnScoreCandidate1.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore1Talent1.ReadOnly = true;
                    txtScore1Talent2.ReadOnly = true;
                    txtScore1Talent3.ReadOnly = true;
                    txtScore1Talent4.ReadOnly = true;

                    ProceedToPanel2BestInTalent();
                } 
            }
        }

        private void btnScoreCandidate2_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate2())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore2Talent1.Text, out score1);
                    float.TryParse(txtScore2Talent2.Text, out score2);
                    float.TryParse(txtScore2Talent3.Text, out score3);
                    float.TryParse(txtScore2Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score2 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update2(judge1BLL);
                    judge1DAL.UpdateDependency2(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate2.Enabled = false;
                    btnScoreCandidate2.LabelText = "Scores already sent!";
                    btnScoreCandidate2.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore2Talent1.ReadOnly = true;
                    txtScore2Talent2.ReadOnly = true;
                    txtScore2Talent3.ReadOnly = true;
                    txtScore2Talent4.ReadOnly = true;

                    ProceedToPanel3BestInTalent();
                } 
            }
        }

        private void btnScoreCandidate3_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate3())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore3Talent1.Text, out score1);
                    float.TryParse(txtScore3Talent2.Text, out score2);
                    float.TryParse(txtScore3Talent3.Text, out score3);
                    float.TryParse(txtScore3Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score3 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update3(judge1BLL);
                    judge1DAL.UpdateDependency3(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate3.Enabled = false;
                    btnScoreCandidate3.LabelText = "Scores already sent!";
                    btnScoreCandidate3.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore3Talent1.ReadOnly = true;
                    txtScore3Talent2.ReadOnly = true;
                    txtScore3Talent3.ReadOnly = true;
                    txtScore3Talent4.ReadOnly = true;

                    ProceedToPanel4BestInTalent();
                } 
            }
        }

        private void btnScoreCandidate4_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate4())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore4Talent1.Text, out score1);
                    float.TryParse(txtScore4Talent2.Text, out score2);
                    float.TryParse(txtScore4Talent3.Text, out score3);
                    float.TryParse(txtScore4Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score4 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update4(judge1BLL);
                    judge1DAL.UpdateDependency4(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate4.Enabled = false;
                    btnScoreCandidate4.LabelText = "Scores already sent!";
                    btnScoreCandidate4.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore4Talent1.ReadOnly = true;
                    txtScore4Talent2.ReadOnly = true;
                    txtScore4Talent3.ReadOnly = true;
                    txtScore4Talent4.ReadOnly = true;

                    ProceedToPanel5BestInTalent();
                }
            }
        }

        private void btnScoreCandidate5_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate5())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore5Talent1.Text, out score1);
                    float.TryParse(txtScore5Talent2.Text, out score2);
                    float.TryParse(txtScore5Talent3.Text, out score3);
                    float.TryParse(txtScore5Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score5 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update5(judge1BLL);
                    judge1DAL.UpdateDependency5(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate5.Enabled = false;
                    btnScoreCandidate5.LabelText = "Scores already sent!";
                    btnScoreCandidate5.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore5Talent1.ReadOnly = true;
                    txtScore5Talent2.ReadOnly = true;
                    txtScore5Talent3.ReadOnly = true;
                    txtScore5Talent4.ReadOnly = true;

                    ProceedToPanel6BestInTalent();
                }
            }
        }

        private void btnScoreCandidate6_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate6())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore6Talent1.Text, out score1);
                    float.TryParse(txtScore6Talent2.Text, out score2);
                    float.TryParse(txtScore6Talent3.Text, out score3);
                    float.TryParse(txtScore6Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score6 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update6(judge1BLL);
                    judge1DAL.UpdateDependency6(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate6.Enabled = false;
                    btnScoreCandidate6.LabelText = "Scores already sent!";
                    btnScoreCandidate6.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore6Talent1.ReadOnly = true;
                    txtScore6Talent2.ReadOnly = true;
                    txtScore6Talent3.ReadOnly = true;
                    txtScore6Talent4.ReadOnly = true;

                    ProceedToPanel7BestInTalent();
                }
            }
        }

        private void btnScoreCandidate7_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate7())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore7Talent1.Text, out score1);
                    float.TryParse(txtScore7Talent2.Text, out score2);
                    float.TryParse(txtScore7Talent3.Text, out score3);
                    float.TryParse(txtScore7Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score7 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update7(judge1BLL);
                    judge1DAL.UpdateDependency7(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate7.Enabled = false;
                    btnScoreCandidate7.LabelText = "Scores already sent!";
                    btnScoreCandidate7.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore7Talent1.ReadOnly = true;
                    txtScore7Talent2.ReadOnly = true;
                    txtScore7Talent3.ReadOnly = true;
                    txtScore7Talent4.ReadOnly = true;

                    ProceedToPanel8BestInTalent();
                }
            }
        }

        private void btnScoreCandidate8_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate8())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore8Talent1.Text, out score1);
                    float.TryParse(txtScore8Talent2.Text, out score2);
                    float.TryParse(txtScore8Talent3.Text, out score3);
                    float.TryParse(txtScore8Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score8 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update8(judge1BLL);
                    judge1DAL.UpdateDependency8(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate8.Enabled = false;
                    btnScoreCandidate8.LabelText = "Scores already sent!";
                    btnScoreCandidate8.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore8Talent1.ReadOnly = true;
                    txtScore8Talent2.ReadOnly = true;
                    txtScore8Talent3.ReadOnly = true;
                    txtScore8Talent4.ReadOnly = true;

                    ProceedToPanel9BestInTalent();
                }
            }
        }

        private void btnScoreCandidate9_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate9())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore9Talent1.Text, out score1);
                    float.TryParse(txtScore9Talent2.Text, out score2);
                    float.TryParse(txtScore9Talent3.Text, out score3);
                    float.TryParse(txtScore9Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score9 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update9(judge1BLL);
                    judge1DAL.UpdateDependency9(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate9.Enabled = false;
                    btnScoreCandidate9.LabelText = "Scores already sent!";
                    btnScoreCandidate9.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore9Talent1.ReadOnly = true;
                    txtScore9Talent2.ReadOnly = true;
                    txtScore9Talent3.ReadOnly = true;
                    txtScore9Talent4.ReadOnly = true;

                    ProceedToPanel10BestInTalent();
                }
            }
        }

        private void btnScoreCandidate10_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate10())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore10Talent1.Text, out score1);
                    float.TryParse(txtScore10Talent2.Text, out score2);
                    float.TryParse(txtScore10Talent3.Text, out score3);
                    float.TryParse(txtScore10Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score10 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update10(judge1BLL);
                    judge1DAL.UpdateDependency10(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate10.Enabled = false;
                    btnScoreCandidate10.LabelText = "Scores already sent!";
                    btnScoreCandidate10.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore10Talent1.ReadOnly = true;
                    txtScore10Talent2.ReadOnly = true;
                    txtScore10Talent3.ReadOnly = true;
                    txtScore10Talent4.ReadOnly = true;

                    ProceedToPanel11BestInTalent();
                }
            }
        }

        private void btnScoreCandidate11_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate11())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore11Talent1.Text, out score1);
                    float.TryParse(txtScore11Talent2.Text, out score2);
                    float.TryParse(txtScore11Talent3.Text, out score3);
                    float.TryParse(txtScore11Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score11 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update11(judge1BLL);
                    judge1DAL.UpdateDependency11(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate11.Enabled = false;
                    btnScoreCandidate11.LabelText = "Scores already sent!";
                    btnScoreCandidate11.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore11Talent1.ReadOnly = true;
                    txtScore11Talent2.ReadOnly = true;
                    txtScore11Talent3.ReadOnly = true;
                    txtScore11Talent4.ReadOnly = true;

                    ProceedToPanel12BestInTalent();
                }
            }
        }

        private void btnScoreCandidate12_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate12())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore12Talent1.Text, out score1);
                    float.TryParse(txtScore12Talent2.Text, out score2);
                    float.TryParse(txtScore12Talent3.Text, out score3);
                    float.TryParse(txtScore12Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score12 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update12(judge1BLL);
                    judge1DAL.UpdateDependency12(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate12.Enabled = false;
                    btnScoreCandidate12.LabelText = "Scores already sent!";
                    btnScoreCandidate12.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore12Talent1.ReadOnly = true;
                    txtScore12Talent2.ReadOnly = true;
                    txtScore12Talent3.ReadOnly = true;
                    txtScore12Talent4.ReadOnly = true;

                    ProceedToPanel13BestInTalent();
                }
            }
        }

        private void btnScoreCandidate13_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate13())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore13Talent1.Text, out score1);
                    float.TryParse(txtScore13Talent2.Text, out score2);
                    float.TryParse(txtScore13Talent3.Text, out score3);
                    float.TryParse(txtScore13Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score13 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update13(judge1BLL);
                    judge1DAL.UpdateDependency13(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate13.Enabled = false;
                    btnScoreCandidate13.LabelText = "Scores already sent!";
                    btnScoreCandidate13.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore13Talent1.ReadOnly = true;
                    txtScore13Talent2.ReadOnly = true;
                    txtScore13Talent3.ReadOnly = true;
                    txtScore13Talent4.ReadOnly = true;

                    ProceedToPanel14BestInTalent();
                }
            }
        }

        private void btnScoreCandidate14_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate14())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore14Talent1.Text, out score1);
                    float.TryParse(txtScore14Talent2.Text, out score2);
                    float.TryParse(txtScore14Talent3.Text, out score3);
                    float.TryParse(txtScore14Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score14 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update14(judge1BLL);
                    judge1DAL.UpdateDependency14(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate14.Enabled = false;
                    btnScoreCandidate14.LabelText = "Scores already sent!";
                    btnScoreCandidate14.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore14Talent1.ReadOnly = true;
                    txtScore14Talent2.ReadOnly = true;
                    txtScore14Talent3.ReadOnly = true;
                    txtScore14Talent4.ReadOnly = true;

                    ProceedToPanel15BestInTalent();
                }
            }
        }

        private void btnScoreCandidate15_Click(object sender, EventArgs e)
        {
            if (ValidatorFormBestInTalentCandidate15())
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to send the scores? You can't send again after you click yes.", "Sending Scores!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    float score1, score2, score3, score4;
                    float.TryParse(txtScore15Talent1.Text, out score1);
                    float.TryParse(txtScore15Talent2.Text, out score2);
                    float.TryParse(txtScore15Talent3.Text, out score3);
                    float.TryParse(txtScore15Talent4.Text, out score4);

                    float total = (score1 + score2 + score3 + score4);

                    // Getting data from UI
                    judge1BLL.Score15 = total;


                    // Updating data into Database
                    bool success = judge1DAL.Update15(judge1BLL);
                    judge1DAL.UpdateDependency15(judge1BLL);

                    // If the data is successfully updated then the value of success will be true else it will be false
                    if (success == true)
                    {
                        // Data successfully updated
                        MessageBox.Show("Thank you, for sending the scores '" + lblJudgeName.Text + "'.", "Judge1 Sent Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Failed to update data
                        MessageBox.Show("Failed to send new score information.", "Judge1 Information Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    btnScoreCandidate15.Enabled = false;
                    btnScoreCandidate15.LabelText = "Scores already sent!";
                    btnScoreCandidate15.Font = new Font("Arial", 10, FontStyle.Regular);

                    txtScore15Talent1.ReadOnly = true;
                    txtScore15Talent2.ReadOnly = true;
                    txtScore15Talent3.ReadOnly = true;
                    txtScore15Talent4.ReadOnly = true;
                }
            }
        }

        private void btnNext1_Click(object sender, EventArgs e)
        {
            ProceedToPanel2BestInTalent();
        }

        private void btnPrevious2_Click(object sender, EventArgs e)
        {
            ShowPanel1BestInTalentAndHideRestPanel();
        }

        private void btnNext2_Click(object sender, EventArgs e)
        {
            ProceedToPanel3BestInTalent();
        }

        private void btnPrevious3_Click(object sender, EventArgs e)
        {
            ProceedToPanel2BestInTalent();
        }

        private void btnNext3_Click(object sender, EventArgs e)
        {
            ProceedToPanel4BestInTalent();
        }

        private void btnPrevious4_Click(object sender, EventArgs e)
        {
            ProceedToPanel3BestInTalent();
        }

        private void btnNext4_Click(object sender, EventArgs e)
        {
            ProceedToPanel5BestInTalent();
        }

        private void btnPrevious5_Click(object sender, EventArgs e)
        {
            ProceedToPanel4BestInTalent();
        }

        private void btnNext5_Click(object sender, EventArgs e)
        {
            ProceedToPanel6BestInTalent();
        }

        private void btnPrevious6_Click(object sender, EventArgs e)
        {
            ProceedToPanel5BestInTalent();
        }

        private void btnNext6_Click(object sender, EventArgs e)
        {
            ProceedToPanel7BestInTalent();
        }

        private void btnPrevious7_Click(object sender, EventArgs e)
        {
            ProceedToPanel6BestInTalent();
        }

        private void btnNext7_Click(object sender, EventArgs e)
        {
            ProceedToPanel8BestInTalent();
        }

        private void btnPrevious8_Click(object sender, EventArgs e)
        {
            ProceedToPanel7BestInTalent();
        }

        private void btnNext8_Click(object sender, EventArgs e)
        {
            ProceedToPanel9BestInTalent();
        }

        private void btnPrevious9_Click(object sender, EventArgs e)
        {
            ProceedToPanel8BestInTalent();
        }

        private void btnNext9_Click(object sender, EventArgs e)
        {
            ProceedToPanel10BestInTalent();
        }

        private void btnPrevious10_Click(object sender, EventArgs e)
        {
            ProceedToPanel9BestInTalent();
        }

        private void btnNext10_Click(object sender, EventArgs e)
        {
            ProceedToPanel11BestInTalent();
        }

        private void btnPrevious11_Click(object sender, EventArgs e)
        {
            ProceedToPanel10BestInTalent();
        }

        private void btnNext11_Click(object sender, EventArgs e)
        {
            ProceedToPanel12BestInTalent();
        }

        private void btnPrevious12_Click(object sender, EventArgs e)
        {
            ProceedToPanel11BestInTalent();
        }

        private void btnNext12_Click(object sender, EventArgs e)
        {
            ProceedToPanel13BestInTalent();
        }

        private void btnPrevious13_Click(object sender, EventArgs e)
        {
            ProceedToPanel12BestInTalent();
        }

        private void btnNext13_Click(object sender, EventArgs e)
        {
            ProceedToPanel14BestInTalent();
        }

        private void btnPrevious14_Click(object sender, EventArgs e)
        {
            ProceedToPanel13BestInTalent();
        }

        private void btnNext14_Click(object sender, EventArgs e)
        {
            ProceedToPanel15BestInTalent();
        }

        private void btnPrevious15_Click(object sender, EventArgs e)
        {
            ProceedToPanel14BestInTalent();
        }

        private void btnJudge2_Click(object sender, EventArgs e)
        {
            try
            {
                frmJudge2 judge2 = new frmJudge2();
                this.Hide();
                judge2.Show();

                SqlDependency.Stop(UserDAL.myconnstrng);

                judge2.btnBackToServer.Visible = true;
                judge2.btnSendToBackBestInTalentPanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnJudge3_Click(object sender, EventArgs e)
        {
            try
            {
                frmJudge3 judge3 = new frmJudge3();
                this.Hide();
                judge3.Show();

                SqlDependency.Stop(UserDAL.myconnstrng);

                judge3.btnBackToServer.Visible = true;
                judge3.btnSendToBackBestInTalentPanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnJudge4_Click(object sender, EventArgs e)
        {
            try
            {
                frmJudge4 judge4 = new frmJudge4();
                this.Hide();
                judge4.Show();

                SqlDependency.Stop(UserDAL.myconnstrng);

                judge4.btnBackToServer.Visible = true;
                judge4.btnSendToBackBestInTalentPanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnJudge5_Click(object sender, EventArgs e)
        {
            try
            {
                frmJudge5 judge5 = new frmJudge5();
                this.Hide();
                judge5.Show();

                SqlDependency.Stop(UserDAL.myconnstrng);

                judge5.btnBackToServer.Visible = true;
                judge5.btnSendToBackBestInTalentPanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBackToServer_Click(object sender, EventArgs e)
        {
            try
            {
                frmMain frmServer = new frmMain();
                this.Hide();
                frmServer.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSendToBackBestInTalentPanel_Click(object sender, EventArgs e)
        {
            btnJudge1TalentSend.Visible = true;

            btnJudge1ProductionNumberSend.Visible = false;
            btnJudge1EveningGownSend.Visible = false;
            btnJudge1ResortWearSend.Visible = false;
            btnJudge1OnStageQuestionsSend.Visible = false;

            lblCategoryTitle.Text = cmbCategories.Text;
            lblCategoryTitle.ForeColor = Color.OrangeRed;
            lblCandidateNo.ForeColor = Color.OrangeRed;
            lblCandidateName.ForeColor = Color.OrangeRed;
            lblScore.ForeColor = Color.OrangeRed;
            btnJudge1TalentSend.SendToBack();
            panelBody.BringToFront();
            Clear();

            FillTxtboxCandidateName();

            HideAllBestInTalentPanel();

            lblScore.Visible = true;
            lblScores.Visible = false;
            lblScores100.Visible = true;

            txtScore1.Focus();

            TabStopAllToTrueTxtScore1_15();
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

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to exit?", "Exit!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                timerShutdown.Start();
            }
        }        

        #region TextBoxes Scores KeyPress - 2 decimal places & clear errorprovider
        private void txtScore1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore1, "");
            }
        }

        private void txtScore2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score2;
            float.TryParse(txtScore2.Text, out score2);

            if (score2 > 0 || score2 <= 10)
            {
                errorProviderJudge1.SetError(txtScore2, "");
            }
        }

        private void txtScore3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score3;
            float.TryParse(txtScore3.Text, out score3);

            if (score3 > 0 || score3 <= 10)
            {
                errorProviderJudge1.SetError(txtScore3, "");
            }
        }

        private void txtScore4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score4;
            float.TryParse(txtScore4.Text, out score4);

            if (score4 > 0 || score4 <= 10)
            {
                errorProviderJudge1.SetError(txtScore4, "");
            }
        }

        private void txtScore5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore5.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score5;
            float.TryParse(txtScore5.Text, out score5);

            if (score5 > 0 || score5 <= 10)
            {
                errorProviderJudge1.SetError(txtScore5, "");
            }
        }

        private void txtScore6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore6.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score6;
            float.TryParse(txtScore6.Text, out score6);

            if (score6 > 0 || score6 <= 10)
            {
                errorProviderJudge1.SetError(txtScore6, "");
            }
        }

        private void txtScore7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore7.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score7;
            float.TryParse(txtScore7.Text, out score7);

            if (score7 > 0 || score7 <= 10)
            {
                errorProviderJudge1.SetError(txtScore7, "");
            }
        }

        private void txtScore8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore8.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score8;
            float.TryParse(txtScore8.Text, out score8);

            if (score8 > 0 || score8 <= 10)
            {
                errorProviderJudge1.SetError(txtScore8, "");
            }
        }

        private void txtScore9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore9.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score9;
            float.TryParse(txtScore9.Text, out score9);

            if (score9 > 0 || score9 <= 10)
            {
                errorProviderJudge1.SetError(txtScore9, "");
            }
        }

        private void txtScore10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore10.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score10;
            float.TryParse(txtScore10.Text, out score10);

            if (score10 > 0 || score10 <= 10)
            {
                errorProviderJudge1.SetError(txtScore10, "");
            }
        }

        private void txtScore11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore11.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score11;
            float.TryParse(txtScore11.Text, out score11);

            if (score11 > 0 || score11 <= 10)
            {
                errorProviderJudge1.SetError(txtScore11, "");
            }
        }

        private void txtScore12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore12.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score12;
            float.TryParse(txtScore12.Text, out score12);

            if (score12 > 0 || score12 <= 10)
            {
                errorProviderJudge1.SetError(txtScore12, "");
            }
        }

        private void txtScore13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore13.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score13;
            float.TryParse(txtScore13.Text, out score13);

            if (score13 > 0 || score13 <= 10)
            {
                errorProviderJudge1.SetError(txtScore13, "");
            }
        }

        private void txtScore14_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore14.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score14;
            float.TryParse(txtScore14.Text, out score14);

            if (score14 > 0 || score14 <= 10)
            {
                errorProviderJudge1.SetError(txtScore14, "");
            }
        }

        private void txtScore15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore15.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score15;
            float.TryParse(txtScore15.Text, out score15);

            if (score15 > 0 || score15 <= 10)
            {
                errorProviderJudge1.SetError(txtScore15, "");
            }
        }

        private void txtScore1Top5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore1Top5.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1Top5;
            float.TryParse(txtScore1Top5.Text, out score1Top5);

            if (score1Top5 > 0 || score1Top5 <= 10)
            {
                errorProviderJudge1.SetError(txtScore1Top5, "");
            }
        }

        private void txtScore2Top5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore2Top5.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score2Top5;
            float.TryParse(txtScore2Top5.Text, out score2Top5);

            if (score2Top5 > 0 || score2Top5 <= 10)
            {
                errorProviderJudge1.SetError(txtScore2Top5, "");
            }
        }

        private void txtScore3Top5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore3Top5.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score3Top5;
            float.TryParse(txtScore3Top5.Text, out score3Top5);

            if (score3Top5 > 0 || score3Top5 <= 10)
            {
                errorProviderJudge1.SetError(txtScore3Top5, "");
            }
        }

        private void txtScore4Top5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore4Top5.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score4Top5;
            float.TryParse(txtScore4Top5.Text, out score4Top5);

            if (score4Top5 > 0 || score4Top5 <= 10)
            {
                errorProviderJudge1.SetError(txtScore4Top5, "");
            }
        }

        private void txtScore5Top5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore5Top5.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score5Top5;
            float.TryParse(txtScore5Top5.Text, out score5Top5);

            if (score5Top5 > 0 || score5Top5 <= 10)
            {
                errorProviderJudge1.SetError(txtScore5Top5, "");
            }
        }

        private void txtImageID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                if (Regex.IsMatch(
                 txtImageID.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }
            else e.Handled = e.KeyChar != (char)Keys.Back;
        }

        private void txtScore1Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore1Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore1Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore1Talent1, "");
            }
        }

        private void txtScore1Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore1Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore1Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore1Talent2, "");
            }
        }

        private void txtScore1Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore1Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore1Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore1Talent3, "");
            }
        }

        private void txtScore1Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore1Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore1Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore1Talent4, "");
            }
        }

        private void txtScore2Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore2Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore2Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore2Talent1, "");
            }
        }

        private void txtScore2Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore2Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore2Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore2Talent2, "");
            }
        }

        private void txtScore2Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore2Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore2Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore2Talent3, "");
            }
        }

        private void txtScore2Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore2Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore2Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore2Talent4, "");
            }
        }

        private void txtScore3Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore3Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore3Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore3Talent1, "");
            }
        }

        private void txtScore3Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore3Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore3Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore3Talent2, "");
            }
        }

        private void txtScore3Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore3Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore3Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore3Talent3, "");
            }
        }

        private void txtScore3Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore3Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore3Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore3Talent4, "");
            }
        }

        private void txtScore4Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore4Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore4Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore4Talent1, "");
            }
        }

        private void txtScore4Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore4Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore4Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore4Talent2, "");
            }
        }

        private void txtScore4Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore4Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore4Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore4Talent3, "");
            }
        }

        private void txtScore4Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore4Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore4Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore4Talent4, "");
            }
        }

        private void txtScore5Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore5Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore5Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore5Talent1, "");
            }
        }

        private void txtScore5Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore5Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore5Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore5Talent2, "");
            }
        }

        private void txtScore5Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore5Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore5Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore5Talent3, "");
            }
        }

        private void txtScore5Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore5Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore5Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore5Talent4, "");
            }
        }

        private void txtScore6Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore6Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore6Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore6Talent1, "");
            }
        }

        private void txtScore6Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore6Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore6Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore6Talent2, "");
            }
        }

        private void txtScore6Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore6Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore6Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore6Talent3, "");
            }
        }

        private void txtScore6Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore6Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore6Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore6Talent4, "");
            }
        }

        private void txtScore7Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore7Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore7Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore7Talent1, "");
            }
        }

        private void txtScore7Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore7Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore7Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore7Talent2, "");
            }
        }

        private void txtScore7Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore7Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore7Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore7Talent3, "");
            }
        }

        private void txtScore7Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore7Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore7Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore7Talent4, "");
            }
        }

        private void txtScore8Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore8Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore8Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore8Talent1, "");
            }
        }

        private void txtScore8Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore8Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore8Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore8Talent2, "");
            }
        }

        private void txtScore8Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore8Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore8Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore8Talent3, "");
            }
        }

        private void txtScore8Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore8Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore8Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore8Talent4, "");
            }
        }

        private void txtScore9Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore9Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore9Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore9Talent1, "");
            }
        }

        private void txtScore9Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore9Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore9Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore9Talent2, "");
            }
        }

        private void txtScore9Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore9Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore9Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore9Talent3, "");
            }
        }

        private void txtScore9Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore9Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore9Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore9Talent4, "");
            }
        }

        private void txtScore10Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore10Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore10Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore10Talent1, "");
            }
        }

        private void txtScore10Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore10Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore10Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore10Talent2, "");
            }
        }

        private void txtScore10Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore10Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore10Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore10Talent3, "");
            }
        }

        private void txtScore10Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore10Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore10Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore10Talent4, "");
            }
        }

        private void txtScore11Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore11Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore11Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore11Talent1, "");
            }
        }

        private void txtScore11Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore11Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore11Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore11Talent2, "");
            }
        }

        private void txtScore11Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore11Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore11Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore11Talent3, "");
            }
        }

        private void txtScore11Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore11Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore11Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore11Talent4, "");
            }
        }

        private void txtScore12Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore12Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore12Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore12Talent1, "");
            }
        }

        private void txtScore12Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore12Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore12Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore12Talent2, "");
            }
        }

        private void txtScore12Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore12Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore12Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore12Talent3, "");
            }
        }

        private void txtScore12Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore12Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore12Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore12Talent4, "");
            }
        }

        private void txtScore13Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore13Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore13Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore13Talent1, "");
            }
        }

        private void txtScore13Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore13Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore13Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore13Talent2, "");
            }
        }

        private void txtScore13Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore13Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore13Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore13Talent3, "");
            }
        }

        private void txtScore13Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore13Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore13Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore13Talent4, "");
            }
        }

        private void txtScore14Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore14Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore14Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore14Talent1, "");
            }
        }

        private void txtScore14Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore14Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore14Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore14Talent2, "");
            }
        }

        private void txtScore14Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore14Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore14Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore14Talent3, "");
            }
        }

        private void txtScore14Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore14Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore14Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore14Talent4, "");
            }
        }

        private void txtScore15Talent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore15Talent1.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore15Talent1.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore15Talent1, "");
            }
        }

        private void txtScore15Talent2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore15Talent2.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore15Talent2.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore15Talent2, "");
            }
        }

        private void txtScore15Talent3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore15Talent3.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore15Talent3.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore15Talent3, "");
            }
        }

        private void txtScore15Talent4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {
                if (Regex.IsMatch(
                 txtScore15Talent4.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }

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

            float score1;
            float.TryParse(txtScore15Talent4.Text, out score1);

            if (score1 > 0 || score1 <= 10)
            {
                errorProviderJudge1.SetError(txtScore15Talent4, "");
            }
        }
        #endregion

        #region TextBoxes Scores Mouse Click - Highlight Text
        private void txtScore1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore1.SelectAll();
        }

        private void txtScore2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore2.SelectAll();
        }

        private void txtScore3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore3.SelectAll();
        }

        private void txtScore4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore4.SelectAll();
        }

        private void txtScore5_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore5.SelectAll();
        }

        private void txtScore6_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore6.SelectAll();
        }

        private void txtScore7_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore7.SelectAll();
        }

        private void txtScore8_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore8.SelectAll();
        }

        private void txtScore9_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore9.SelectAll();
        }

        private void txtScore10_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore10.SelectAll();
        }

        private void txtScore11_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore11.SelectAll();
        }

        private void txtScore12_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore12.SelectAll();
        }

        private void txtScore13_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore13.SelectAll();
        }

        private void txtScore14_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore14.SelectAll();
        }

        private void txtScore15_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore15.SelectAll();
        }

        private void txtScore1Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore1Top5.SelectAll();
        }

        private void txtScore2Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore2Top5.SelectAll();
        }

        private void txtScore3Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore3Top5.SelectAll();
        }

        private void txtScore4Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore4Top5.SelectAll();
        }

        private void txtScore5Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore5Top5.SelectAll();
        }

        private void txtScore1Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore1Talent1.SelectAll();
        }

        private void txtScore1Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore1Talent2.SelectAll();
        }

        private void txtScore1Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore1Talent3.SelectAll();
        }

        private void txtScore1Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore1Talent4.SelectAll();
        }

        private void txtScore2Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore2Talent1.SelectAll();
        }

        private void txtScore2Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore2Talent2.SelectAll();
        }

        private void txtScore2Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore2Talent3.SelectAll();
        }

        private void txtScore2Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore2Talent4.SelectAll();
        }

        private void txtScore3Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore3Talent1.SelectAll();
        }

        private void txtScore3Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore3Talent2.SelectAll();
        }

        private void txtScore3Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore3Talent3.SelectAll();
        }

        private void txtScore3Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore3Talent4.SelectAll();
        }

        private void txtScore4Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore4Talent1.SelectAll();
        }

        private void txtScore4Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore4Talent2.SelectAll();
        }

        private void txtScore4Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore4Talent3.SelectAll();
        }

        private void txtScore4Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore4Talent4.SelectAll();
        }

        private void txtScore5Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore5Talent1.SelectAll();
        }

        private void txtScore5Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore5Talent2.SelectAll();
        }

        private void txtScore5Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore5Talent3.SelectAll();
        }

        private void txtScore5Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore5Talent4.SelectAll();
        }

        private void txtScore6Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore6Talent1.SelectAll();
        }

        private void txtScore6Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore6Talent2.SelectAll();
        }

        private void txtScore6Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore6Talent3.SelectAll();
        }

        private void txtScore6Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore6Talent4.SelectAll();
        }

        private void txtScore7Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore7Talent1.SelectAll();
        }

        private void txtScore7Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore7Talent2.SelectAll();
        }

        private void txtScore7Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore7Talent3.SelectAll();
        }

        private void txtScore7Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore7Talent4.SelectAll();
        }

        private void txtScore8Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore8Talent1.SelectAll();
        }

        private void txtScore8Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore8Talent2.SelectAll();
        }

        private void txtScore8Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore8Talent3.SelectAll();
        }

        private void txtScore8Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore8Talent4.SelectAll();
        }

        private void txtScore9Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore9Talent1.SelectAll();
        }

        private void txtScore9Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore9Talent2.SelectAll();
        }

        private void txtScore9Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore9Talent3.SelectAll();
        }

        private void txtScore9Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore9Talent4.SelectAll();
        }

        private void txtScore10Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore10Talent1.SelectAll();
        }

        private void txtScore10Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore10Talent2.SelectAll();
        }

        private void txtScore10Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore10Talent3.SelectAll();
        }

        private void txtScore10Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore10Talent4.SelectAll();
        }

        private void txtScore11Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore11Talent1.SelectAll();
        }

        private void txtScore11Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore11Talent2.SelectAll();
        }

        private void txtScore11Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore11Talent3.SelectAll();
        }

        private void txtScore11Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore11Talent4.SelectAll();
        }

        private void txtScore12Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore12Talent1.SelectAll();
        }

        private void txtScore12Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore12Talent2.SelectAll();
        }

        private void txtScore12Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore12Talent3.SelectAll();
        }

        private void txtScore12Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore12Talent4.SelectAll();
        }

        private void txtScore13Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore13Talent1.SelectAll();
        }

        private void txtScore13Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore13Talent2.SelectAll();
        }

        private void txtScore13Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore13Talent3.SelectAll();
        }

        private void txtScore13Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore13Talent4.SelectAll();
        }

        private void txtScore14Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore14Talent1.SelectAll();
        }

        private void txtScore14Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore14Talent2.SelectAll();
        }

        private void txtScore14Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore14Talent3.SelectAll();
        }

        private void txtScore14Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore14Talent4.SelectAll();
        }

        private void txtScore15Talent1_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore15Talent1.SelectAll();
        }

        private void txtScore15Talent2_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore15Talent2.SelectAll();
        }

        private void txtScore15Talent3_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore15Talent3.SelectAll();
        }

        private void txtScore15Talent4_MouseClick(object sender, MouseEventArgs e)
        {
            txtScore15Talent4.SelectAll();
        }
        #endregion

        #region TextBoxes Scores Enter - Load image database path into pictureBox
        private void txtScore1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 1";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 2";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 3";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 4";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore5_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 5";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore6_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 6";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore7_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 7";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore8_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 8";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore9_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 9";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore10_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 10";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore11_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 11";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore12_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 12";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore13_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 13";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore14_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 14";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore15_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 15";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore1Top5_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_PicturesTop5 WHERE ImageNo = 1";
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
                            pctrBoxCandidatesTop5.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblImageFilenameTop5.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore2Top5_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_PicturesTop5 WHERE ImageNo = 2";
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
                            pctrBoxCandidatesTop5.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblImageFilenameTop5.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore3Top5_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_PicturesTop5 WHERE ImageNo = 3";
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
                            pctrBoxCandidatesTop5.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblImageFilenameTop5.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore4Top5_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_PicturesTop5 WHERE ImageNo = 4";
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
                            pctrBoxCandidatesTop5.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblImageFilenameTop5.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore5Top5_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_PicturesTop5 WHERE ImageNo = 5";
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
                            pctrBoxCandidatesTop5.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblImageFilenameTop5.Text = Convert.ToString(read["PathImage"]);
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

        private void txtScore1Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 1";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores1Talent.Text = "1.0 - 20.0";
        }

        private void txtScore1Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 1";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores1Talent.Text = "1.0 - 25.0";
        }

        private void txtScore1Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 1";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores1Talent.Text = "1.0 - 25.0";
        }

        private void txtScore1Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 1";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores1Talent.Text = "1.0 - 30.0";
        }

        private void txtScore2Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 2";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores2Talent.Text = "1.0 - 20.0";
        }

        private void txtScore2Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 2";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores2Talent.Text = "1.0 - 25.0";
        }

        private void txtScore2Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 2";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores2Talent.Text = "1.0 - 25.0";
        }

        private void txtScore2Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 2";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores2Talent.Text = "1.0 - 30.0";
        }

        private void txtScore3Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 3";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores3Talent.Text = "1.0 - 20.0";
        }

        private void txtScore3Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 3";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores3Talent.Text = "1.0 - 25.0";
        }

        private void txtScore3Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 3";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores3Talent.Text = "1.0 - 25.0";
        }

        private void txtScore3Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 3";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores3Talent.Text = "1.0 - 30.0";
        }

        private void txtScore4Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 4";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores4Talent.Text = "1.0 - 20.0";
        }

        private void txtScore4Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 4";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores4Talent.Text = "1.0 - 25.0";
        }

        private void txtScore4Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 4";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores4Talent.Text = "1.0 - 25.0";
        }

        private void txtScore4Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 4";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores4Talent.Text = "1.0 - 30.0";
        }

        private void txtScore5Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 5";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores5Talent.Text = "1.0 - 20.0";
        }

        private void txtScore5Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 5";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores5Talent.Text = "1.0 - 25.0";
        }

        private void txtScore5Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 5";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores5Talent.Text = "1.0 - 25.0";
        }

        private void txtScore5Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 5";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores5Talent.Text = "1.0 - 30.0";
        }

        private void txtScore6Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 6";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores6Talent.Text = "1.0 - 20.0";
        }

        private void txtScore6Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 6";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores6Talent.Text = "1.0 - 25.0";
        }

        private void txtScore6Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 6";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores6Talent.Text = "1.0 - 25.0";
        }

        private void txtScore6Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 6";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores6Talent.Text = "1.0 - 30.0";
        }

        private void txtScore7Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 7";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores7Talent.Text = "1.0 - 20.0";
        }

        private void txtScore7Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 7";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores7Talent.Text = "1.0 - 25.0";
        }

        private void txtScore7Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 7";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores7Talent.Text = "1.0 - 25.0";
        }

        private void txtScore7Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 7";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores7Talent.Text = "1.0 - 30.0";
        }

        private void txtScore8Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 8";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores8Talent.Text = "1.0 - 20.0";
        }

        private void txtScore8Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 8";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores8Talent.Text = "1.0 - 25.0";
        }

        private void txtScore8Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 8";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores8Talent.Text = "1.0 - 25.0";
        }

        private void txtScore8Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 8";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores8Talent.Text = "1.0 - 30.0";
        }

        private void txtScore9Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 9";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores9Talent.Text = "1.0 - 20.0";
        }

        private void txtScore9Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 9";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores9Talent.Text = "1.0 - 25.0";
        }

        private void txtScore9Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 9";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores9Talent.Text = "1.0 - 25.0";
        }

        private void txtScore9Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 9";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores9Talent.Text = "1.0 - 30.0";
        }

        private void txtScore10Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 10";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores10Talent.Text = "1.0 - 20.0";
        }

        private void txtScore10Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 10";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores10Talent.Text = "1.0 - 25.0";
        }

        private void txtScore10Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 10";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores10Talent.Text = "1.0 - 25.0";
        }

        private void txtScore10Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 10";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores10Talent.Text = "1.0 - 30.0";
        }

        private void txtScore11Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 11";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores11Talent.Text = "1.0 - 20.0";
        }

        private void txtScore11Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 11";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores11Talent.Text = "1.0 - 25.0";
        }

        private void txtScore11Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 11";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores11Talent.Text = "1.0 - 25.0";
        }

        private void txtScore11Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 11";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores11Talent.Text = "1.0 - 30.0";
        }

        private void txtScore12Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 12";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores12Talent.Text = "1.0 - 20.0";
        }

        private void txtScore12Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 12";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores12Talent.Text = "1.0 - 25.0";
        }

        private void txtScore12Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 12";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores12Talent.Text = "1.0 - 25.0";
        }

        private void txtScore12Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 12";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores12Talent.Text = "1.0 - 30.0";
        }

        private void txtScore13Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 13";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores13Talent.Text = "1.0 - 20.0";
        }

        private void txtScore13Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 13";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores13Talent.Text = "1.0 - 25.0";
        }

        private void txtScore13Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 13";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores13Talent.Text = "1.0 - 25.0";
        }

        private void txtScore13Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 13";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores13Talent.Text = "1.0 - 30.0";
        }

        private void txtScore14Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 14";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores14Talent.Text = "1.0 - 20.0";
        }

        private void txtScore14Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 14";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores14Talent.Text = "1.0 - 25.0";
        }

        private void txtScore14Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 14";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores14Talent.Text = "1.0 - 25.0";
        }

        private void txtScore14Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 14";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores14Talent.Text = "1.0 - 30.0";
        }

        private void txtScore15Talent1_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 15";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores15Talent.Text = "1.0 - 20.0";
        }

        private void txtScore15Talent2_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 15";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores15Talent.Text = "1.0 - 25.0";
        }

        private void txtScore15Talent3_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 15";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores14Talent.Text = "1.0 - 25.0";
        }

        private void txtScore15Talent4_Enter(object sender, EventArgs e)
        {
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

            using (SqlConnection conn = new SqlConnection(UserDAL.myconnstrng))
            {
                string query = "SELECT PathImage FROM tbl_Pictures WHERE ImageNo = 15";
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
                            pctrBoxCandidates.Image = Image.FromFile(paths + Convert.ToString(read["PathImage"]));
                            lblFilenameImage.Text = Convert.ToString(read["PathImage"]);
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

            lblScores15Talent.Text = "1.0 - 30.0";
        }
        #endregion

        #region TextBoxes Scores KeyUp - Move to next textBox when click enter & move down arrow key & highlight text
        private void txtScore1_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore1.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore1.SelectAll();
            }    
        }

        private void txtScore2_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore2.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore2.SelectAll();
            }            
        }

        private void txtScore3_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore3.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore3.SelectAll();
            }
        }

        private void txtScore4_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore4.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore4.SelectAll();
            }
        }

        private void txtScore5_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore5.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore5.SelectAll();
            }
        }

        private void txtScore6_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore6.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore6.SelectAll();
            }
        }

        private void txtScore7_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore7.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore7.SelectAll();
            }
        }

        private void txtScore8_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore8.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore8.SelectAll();
            }
        }

        private void txtScore9_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore9.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore9.SelectAll();
            }
        }

        private void txtScore10_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore10.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore10.SelectAll();
            }
        }

        private void txtScore11_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore11.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore11.SelectAll();
            }
        }

        private void txtScore12_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore12.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore12.SelectAll();
            }
        }

        private void txtScore13_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore13.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore13.SelectAll();
            }
        }

        private void txtScore14_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore14.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore14.SelectAll();
            }
        }        

        private void txtScore15_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore15.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore15.SelectAll();
            }
        }

        private void txtScore1Top5_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore1Top5.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore1Top5.SelectAll();
            }
        }

        private void txtScore2Top5_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore2Top5.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore2Top5.SelectAll();
            }
        }

        private void txtScore3Top5_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore3Top5.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore3Top5.SelectAll();
            }
        }

        private void txtScore4Top5_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore4Top5.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore4Top5.SelectAll();
            }
        }

        private void txtScore5Top5_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore5Top5.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore5Top5.SelectAll();
            }
        }

        private void txtScore1Talent1_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore1Talent1.SelectAll();
            }
        }

        private void txtScore1Talent2_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore1Talent2.SelectAll();
            }
        }

        private void txtScore1Talent3_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore1Talent3.SelectAll();
            }
        }

        private void txtScore1Talent4_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore1Talent4.SelectAll();
            }
        }

        private void txtScore2Talent1_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore2Talent1.SelectAll();
            }
        }

        private void txtScore2Talent2_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore2Talent2.SelectAll();
            }
        }

        private void txtScore2Talent3_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore2Talent3.SelectAll();
            }
        }

        private void txtScore2Talent4_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore2Talent4.SelectAll();
            }
        }

        private void txtScore3Talent1_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore3Talent1.SelectAll();
            }
        }

        private void txtScore3Talent2_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore3Talent2.SelectAll();
            }
        }

        private void txtScore3Talent3_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore3Talent3.SelectAll();
            }
        }

        private void txtScore3Talent4_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore3Talent4.SelectAll();
            }
        }

        private void txtScore4Talent1_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore4Talent1.SelectAll();
            }
        }

        private void txtScore4Talent2_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore4Talent2.SelectAll();
            }
        }

        private void txtScore4Talent3_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore4Talent3.SelectAll();
            }
        }

        private void txtScore4Talent4_KeyUp(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore4Talent4.SelectAll();
            }
        }

        private void txtScore5Talent1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore5Talent1.SelectAll();
            }
        }

        private void txtScore5Talent2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore5Talent2.SelectAll();
            }
        }

        private void txtScore5Talent3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore5Talent3.SelectAll();
            }
        }

        private void txtScore5Talent4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore5Talent4.SelectAll();
            }
        }

        private void txtScore6Talent1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore6Talent1.SelectAll();
            }
        }

        private void txtScore6Talent2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore6Talent2.SelectAll();
            }
        }

        private void txtScore6Talent3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore6Talent3.SelectAll();
            }
        }

        private void txtScore6Talent4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore6Talent4.SelectAll();
            }
        }

        private void txtScore7Talent1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore7Talent1.SelectAll();
            }
        }

        private void txtScore7Talent2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore7Talent2.SelectAll();
            }
        }

        private void txtScore7Talent3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore7Talent3.SelectAll();
            }
        }

        private void txtScore7Talent4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore7Talent4.SelectAll();
            }
        }

        private void txtScore8Talent1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore8Talent1.SelectAll();
            }
        }

        private void txtScore8Talent2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore8Talent2.SelectAll();
            }
        }

        private void txtScore8Talent3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore8Talent3.SelectAll();
            }
        }

        private void txtScore8Talent4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore8Talent4.SelectAll();
            }
        }

        private void txtScore9Talent1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore9Talent1.SelectAll();
            }
        }

        private void txtScore9Talent2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore9Talent2.SelectAll();
            }
        }

        private void txtScore9Talent3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore9Talent3.SelectAll();
            }
        }

        private void txtScore9Talent4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore9Talent4.SelectAll();
            }
        }

        private void txtScore10Talent1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore10Talent1.SelectAll();
            }
        }

        private void txtScore10Talent2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore10Talent2.SelectAll();
            }
        }

        private void txtScore10Talent3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore10Talent3.SelectAll();
            }
        }

        private void txtScore10Talent4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore10Talent4.SelectAll();
            }
        }

        private void txtScore11Talent1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore11Talent1.SelectAll();
            }
        }

        private void txtScore11Talent2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore11Talent2.SelectAll();
            }
        }

        private void txtScore11Talent3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore11Talent3.SelectAll();
            }
        }

        private void txtScore11Talent4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore11Talent4.SelectAll();
            }
        }

        private void txtScore12Talent1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore12Talent1.SelectAll();
            }
        }

        private void txtScore12Talent2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore12Talent2.SelectAll();
            }
        }

        private void txtScore12Talent3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore12Talent3.SelectAll();
            }
        }

        private void txtScore12Talent4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore12Talent4.SelectAll();
            }
        }

        private void txtScore13Talent1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore13Talent1.SelectAll();
            }
        }

        private void txtScore13Talent2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore13Talent2.SelectAll();
            }
        }

        private void txtScore13Talent3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore13Talent3.SelectAll();
            }
        }

        private void txtScore13Talent4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore13Talent4.SelectAll();
            }
        }

        private void txtScore14Talent1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore14Talent1.SelectAll();
            }
        }

        private void txtScore14Talent2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore14Talent2.SelectAll();
            }
        }

        private void txtScore14Talent3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore14Talent3.SelectAll();
            }
        }

        private void txtScore14Talent4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore14Talent4.SelectAll();
            }
        }

        private void txtScore15Talent1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore15Talent1.SelectAll();
            }
        }

        private void txtScore15Talent2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore15Talent2.SelectAll();
            }
        }

        private void txtScore15Talent3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore15Talent3.SelectAll();
            }
        }

        private void txtScore15Talent4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                txtScore15Talent4.SelectAll();
            }
        }
        #endregion

        #region TextBoxes Candidates Name Mouse Click - Highlighted text
        private void txtCandidate1_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate1.SelectAll();
        }

        private void txtCandidate2_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate2.SelectAll();
        }

        private void txtCandidate3_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate3.SelectAll();
        }

        private void txtCandidate4_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate4.SelectAll();
        }

        private void txtCandidate5_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate5.SelectAll();
        }

        private void txtCandidate6_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate6.SelectAll();
        }

        private void txtCandidate7_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate7.SelectAll();
        }

        private void txtCandidate8_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate8.SelectAll();
        }

        private void txtCandidate9_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate9.SelectAll();
        }

        private void txtCandidate10_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate10.SelectAll();
        }

        private void txtCandidate11_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate11.SelectAll();
        }

        private void txtCandidate12_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate12.SelectAll();
        }

        private void txtCandidate13_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate13.SelectAll();
        }

        private void txtCandidate14_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate14.SelectAll();
        }        

        private void txtCandidate15_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidate15.SelectAll();
        }

        private void txtCandidateName1Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidateName1Top5.SelectAll();
        }

        private void txtCandidateName2Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidateName2Top5.SelectAll();
        }

        private void txtCandidateName3Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidateName3Top5.SelectAll();
        }

        private void txtCandidateName4Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidateName4Top5.SelectAll();
        }        

        private void txtCandidateName5Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandidateName5Top5.SelectAll();
        }
        #endregion

        #region TextBoxes Candidate No. for Top 5 - Mouse Click - Higlighted Text
        private void txtCandNo1Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandNo1Top5.SelectAll();
        }

        private void txtCandNo2Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandNo2Top5.SelectAll();
        }

        private void txtCandNo3Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandNo3Top5.SelectAll();
        }

        private void txtCandNo4Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandNo4Top5.SelectAll();
        }       

        private void txtCandNo5Top5_MouseClick(object sender, MouseEventArgs e)
        {
            txtCandNo5Top5.SelectAll();
        }
        #endregion

        #region TextBoxes Candidate No. Top 5 - KeyPress, input numbers only
        private void txtCandNo1Top5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                if (Regex.IsMatch(
                 txtCandNo1Top5.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }
            else e.Handled = e.KeyChar != (char)Keys.Back;
        }

        private void txtCandNo2Top5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                if (Regex.IsMatch(
                 txtCandNo2Top5.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }
            else e.Handled = e.KeyChar != (char)Keys.Back;
        }

        private void txtCandNo3Top5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                if (Regex.IsMatch(
                 txtCandNo3Top5.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }
            else e.Handled = e.KeyChar != (char)Keys.Back;
        }

        private void txtCandNo4Top5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                if (Regex.IsMatch(
                 txtCandNo4Top5.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }
            else e.Handled = e.KeyChar != (char)Keys.Back;
        }

        private void pctrBoxWelcomeMissAloguinsan_Click(object sender, EventArgs e)
        {

        }        

        private void txtCandNo5Top5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                if (Regex.IsMatch(
                 txtCandNo5Top5.Text,
                 "^\\d*\\.\\d{2}$")) e.Handled = true;
            }
            else e.Handled = e.KeyChar != (char)Keys.Back;
        }
        #endregion
    }
}
