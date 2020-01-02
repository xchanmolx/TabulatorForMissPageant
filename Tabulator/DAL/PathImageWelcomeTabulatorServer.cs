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
    public class PathImageWelcomeTabulatorServer
    {
        #region Insert data in Database
        public bool Insert(PathImageBLL pathImg)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "INSERT INTO tbl_PicturesWelcomeTabulatorServer (PathImage) VALUES (@PathImage)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@PathImage", @"\Images\WelcomeTabulatorServer\" + pathImg.PathImage);

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
    }
}
