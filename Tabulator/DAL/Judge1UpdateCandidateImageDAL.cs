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
    public class Judge1UpdateCandidateImageDAL
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

        #region UpdateImage data in Database
        public bool UpdateImage(UpdateCandidateImageBLL updateImage)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Pictures SET PathImage=@PathImage WHERE ImageNo=@ImageNo";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("ImageNo", updateImage.ImageNo);
                cmd.Parameters.AddWithValue("@PathImage", @"\Images\Candidates\" + updateImage.PathImage);

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
