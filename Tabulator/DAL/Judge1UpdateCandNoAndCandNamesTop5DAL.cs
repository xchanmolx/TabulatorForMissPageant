using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabulator.BLL;

namespace Tabulator.DAL
{
    public class Judge1UpdateCandNoAndCandNamesTop5DAL
    {
        #region Select data from Database
        public DataTable Select()
        {
            // Static method to connect Database
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            // To hold the data from Database
            DataTable dt = new DataTable();
            try
            {
                // SQL Query to get data from Database
                string sql = "SELECT * FROM tbl_Judge1";

                // For executing command
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Getting data from Database
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                // Database connection open
                conn.Open();

                // Fill data in our DataTable
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                // Throw message if any error occurs
                MessageBox.Show(ex.Message, "Select data from Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                // Closing connection
                conn.Close();
            }

            // Return the value in DataTable
            return dt;
        }
        #endregion

        #region Insert data in Database
        public bool Insert(JudgeBLL judge1)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "INSERT INTO tbl_Judge1 (candidate1, candidate2, candidate3) VALUES (@candidate1, @candidate2, @candidate3)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@candidate1", judge1.Score1);
                cmd.Parameters.AddWithValue("@candidate2", judge1.Score2);
                cmd.Parameters.AddWithValue("@candidate3", judge1.Score3);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                // If the query is executed successfully then the value to rows will be greaten than 0 else it will be less than 0
                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Insert data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        // Top 5 Judge 1
        #region Update1Judge1Top5 data in Database
        public bool Update1Judge1Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge1Top5 SET CandidateNo=@CandidateNo1, CandidateName=@CandidateName1 WHERE CandidateID=1";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo1", updateCandNoAndCandName.CandidateNo1);
                cmd.Parameters.AddWithValue("@CandidateName1", updateCandNoAndCandName.CandidateName1);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update2Judge1Top5 data in Database
        public bool Update2Judge1Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge1Top5 SET CandidateNo=@CandidateNo2, CandidateName=@CandidateName2 WHERE CandidateID=2";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo2", updateCandNoAndCandName.CandidateNo2);
                cmd.Parameters.AddWithValue("@CandidateName2", updateCandNoAndCandName.CandidateName2);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update3Judge1Top5 data in Database
        public bool Update3Judge1Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge1Top5 SET CandidateNo=@CandidateNo3, CandidateName=@CandidateName3 WHERE CandidateID=3";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo3", updateCandNoAndCandName.CandidateNo3);
                cmd.Parameters.AddWithValue("@CandidateName3", updateCandNoAndCandName.CandidateName3);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update4Judge1Top5 data in Database
        public bool Update4Judge1Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge1Top5 SET CandidateNo=@CandidateNo4, CandidateName=@CandidateName4 WHERE CandidateID=4";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo4", updateCandNoAndCandName.CandidateNo4);
                cmd.Parameters.AddWithValue("@CandidateName4", updateCandNoAndCandName.CandidateName4);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update5Judge1Top5 data in Database
        public bool Update5Judge1Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge1Top5 SET CandidateNo=@CandidateNo5, CandidateName=@CandidateName5 WHERE CandidateID=5";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo5", updateCandNoAndCandName.CandidateNo5);
                cmd.Parameters.AddWithValue("@CandidateName5", updateCandNoAndCandName.CandidateName5);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion        

        // Top 5 Judge 2
        #region Update1Judge2Top5 data in Database
        public bool Update1Judge2Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge2Top5 SET CandidateNo=@CandidateNo1, CandidateName=@CandidateName1 WHERE CandidateID=1";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo1", updateCandNoAndCandName.CandidateNo1);
                cmd.Parameters.AddWithValue("@CandidateName1", updateCandNoAndCandName.CandidateName1);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update2Judge2Top5 data in Database
        public bool Update2Judge2Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge2Top5 SET CandidateNo=@CandidateNo2, CandidateName=@CandidateName2 WHERE CandidateID=2";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo2", updateCandNoAndCandName.CandidateNo2);
                cmd.Parameters.AddWithValue("@CandidateName2", updateCandNoAndCandName.CandidateName2);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update3Judge2Top5 data in Database
        public bool Update3Judge2Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge2Top5 SET CandidateNo=@CandidateNo3, CandidateName=@CandidateName3 WHERE CandidateID=3";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo3", updateCandNoAndCandName.CandidateNo3);
                cmd.Parameters.AddWithValue("@CandidateName3", updateCandNoAndCandName.CandidateName3);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update4Judge2Top5 data in Database
        public bool Update4Judge2Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge2Top5 SET CandidateNo=@CandidateNo4, CandidateName=@CandidateName4 WHERE CandidateID=4";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo4", updateCandNoAndCandName.CandidateNo4);
                cmd.Parameters.AddWithValue("@CandidateName4", updateCandNoAndCandName.CandidateName4);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update5Judge2Top5 data in Database
        public bool Update5Judge2Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge2Top5 SET CandidateNo=@CandidateNo5, CandidateName=@CandidateName5 WHERE CandidateID=5";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo5", updateCandNoAndCandName.CandidateNo5);
                cmd.Parameters.AddWithValue("@CandidateName5", updateCandNoAndCandName.CandidateName5);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion        

        // Top 5 Judge 3
        #region Update1Judge3Top5 data in Database
        public bool Update1Judge3Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge3Top5 SET CandidateNo=@CandidateNo1, CandidateName=@CandidateName1 WHERE CandidateID=1";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo1", updateCandNoAndCandName.CandidateNo1);
                cmd.Parameters.AddWithValue("@CandidateName1", updateCandNoAndCandName.CandidateName1);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update2Judge3Top5 data in Database
        public bool Update2Judge3Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge3Top5 SET CandidateNo=@CandidateNo2, CandidateName=@CandidateName2 WHERE CandidateID=2";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo2", updateCandNoAndCandName.CandidateNo2);
                cmd.Parameters.AddWithValue("@CandidateName2", updateCandNoAndCandName.CandidateName2);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update3Judge3Top5 data in Database
        public bool Update3Judge3Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge3Top5 SET CandidateNo=@CandidateNo3, CandidateName=@CandidateName3 WHERE CandidateID=3";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo3", updateCandNoAndCandName.CandidateNo3);
                cmd.Parameters.AddWithValue("@CandidateName3", updateCandNoAndCandName.CandidateName3);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update4Judge3Top5 data in Database
        public bool Update4Judge3Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge3Top5 SET CandidateNo=@CandidateNo4, CandidateName=@CandidateName4 WHERE CandidateID=4";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo4", updateCandNoAndCandName.CandidateNo4);
                cmd.Parameters.AddWithValue("@CandidateName4", updateCandNoAndCandName.CandidateName4);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update5Judge3Top5 data in Database
        public bool Update5Judge3Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge3Top5 SET CandidateNo=@CandidateNo5, CandidateName=@CandidateName5 WHERE CandidateID=5";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo5", updateCandNoAndCandName.CandidateNo5);
                cmd.Parameters.AddWithValue("@CandidateName5", updateCandNoAndCandName.CandidateName5);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion        

        // Top 5 Judge 4
        #region Update1Judge4Top5 data in Database
        public bool Update1Judge4Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge4Top5 SET CandidateNo=@CandidateNo1, CandidateName=@CandidateName1 WHERE CandidateID=1";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo1", updateCandNoAndCandName.CandidateNo1);
                cmd.Parameters.AddWithValue("@CandidateName1", updateCandNoAndCandName.CandidateName1);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update2Judge4Top5 data in Database
        public bool Update2Judge4Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge4Top5 SET CandidateNo=@CandidateNo2, CandidateName=@CandidateName2 WHERE CandidateID=2";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo2", updateCandNoAndCandName.CandidateNo2);
                cmd.Parameters.AddWithValue("@CandidateName2", updateCandNoAndCandName.CandidateName2);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update3Judge4Top5 data in Database
        public bool Update3Judge4Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge4Top5 SET CandidateNo=@CandidateNo3, CandidateName=@CandidateName3 WHERE CandidateID=3";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo3", updateCandNoAndCandName.CandidateNo3);
                cmd.Parameters.AddWithValue("@CandidateName3", updateCandNoAndCandName.CandidateName3);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update4Judge4Top5 data in Database
        public bool Update4Judge4Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge4Top5 SET CandidateNo=@CandidateNo4, CandidateName=@CandidateName4 WHERE CandidateID=4";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo4", updateCandNoAndCandName.CandidateNo4);
                cmd.Parameters.AddWithValue("@CandidateName4", updateCandNoAndCandName.CandidateName4);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update5Judge4Top5 data in Database
        public bool Update5Judge4Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge4Top5 SET CandidateNo=@CandidateNo5, CandidateName=@CandidateName5 WHERE CandidateID=5";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo5", updateCandNoAndCandName.CandidateNo5);
                cmd.Parameters.AddWithValue("@CandidateName5", updateCandNoAndCandName.CandidateName5);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion        

        // Top 5 Judge 5
        #region Update1Judge5Top5 data in Database
        public bool Update1Judge5Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge5Top5 SET CandidateNo=@CandidateNo1, CandidateName=@CandidateName1 WHERE CandidateID=1";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo1", updateCandNoAndCandName.CandidateNo1);
                cmd.Parameters.AddWithValue("@CandidateName1", updateCandNoAndCandName.CandidateName1);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update2Judge5Top5 data in Database
        public bool Update2Judge5Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge5Top5 SET CandidateNo=@CandidateNo2, CandidateName=@CandidateName2 WHERE CandidateID=2";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo2", updateCandNoAndCandName.CandidateNo2);
                cmd.Parameters.AddWithValue("@CandidateName2", updateCandNoAndCandName.CandidateName2);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update3Judge5Top5 data in Database
        public bool Update3Judge5Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge5Top5 SET CandidateNo=@CandidateNo3, CandidateName=@CandidateName3 WHERE CandidateID=3";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo3", updateCandNoAndCandName.CandidateNo3);
                cmd.Parameters.AddWithValue("@CandidateName3", updateCandNoAndCandName.CandidateName3);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update4Judge5Top5 data in Database
        public bool Update4Judge5Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge5Top5 SET CandidateNo=@CandidateNo4, CandidateName=@CandidateName4 WHERE CandidateID=4";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo4", updateCandNoAndCandName.CandidateNo4);
                cmd.Parameters.AddWithValue("@CandidateName4", updateCandNoAndCandName.CandidateName4);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Update5Judge5Top5 data in Database
        public bool Update5Judge5Top5(JudgeBLL updateCandNoAndCandName)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Judge5Top5 SET CandidateNo=@CandidateNo5, CandidateName=@CandidateName5 WHERE CandidateID=5";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CandidateNo5", updateCandNoAndCandName.CandidateNo5);
                cmd.Parameters.AddWithValue("@CandidateName5", updateCandNoAndCandName.CandidateName5);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
                MessageBox.Show(ex.Message, "Update data in Database Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion        

        #region Delete data from Database
        public bool Delete(JudgeBLL judge1)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_Judge1 WHERE JudgeID=@Judge1ID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Judge1ID", judge1.JudgeID);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Query successful
                    isSuccess = true;
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
        #endregion
    }
}
