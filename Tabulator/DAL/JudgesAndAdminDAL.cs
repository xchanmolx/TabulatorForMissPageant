using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabulator.BLL;

namespace Tabulator.DAL
{
    public class JudgesAndAdminDAL
    {
        #region Update data in Database
        public bool Update(JudgesAndAdminBLL judgeAdmin)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_JudgesAndAdmin SET Judge1Name=@Judge1Name, Judge2Name=@Judge2Name, Judge3Name=@Judge3Name, Judge4Name=@Judge4Name, Judge5Name=@Judge5Name, AdminName=@AdminName WHERE JudgeAdminID=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", judgeAdmin.Id);
                cmd.Parameters.AddWithValue("@Judge1Name", judgeAdmin.Judge1Name);
                cmd.Parameters.AddWithValue("@Judge2Name", judgeAdmin.Judge2Name);
                cmd.Parameters.AddWithValue("@Judge3Name", judgeAdmin.Judge3Name);
                cmd.Parameters.AddWithValue("@Judge4Name", judgeAdmin.Judge4Name);
                cmd.Parameters.AddWithValue("@Judge5Name", judgeAdmin.Judge5Name);
                cmd.Parameters.AddWithValue("@AdminName", judgeAdmin.AdminName);

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
    }
}
