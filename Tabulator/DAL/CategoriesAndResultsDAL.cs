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
    public class CategoriesAndResultsDAL
    {      
        #region InsertCategory data in Database
        public bool InsertCategory(CategoriesAndResultsBLL cat)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "INSERT INTO tbl_Categories (CategoryNo, CategoryName) VALUES (@CategoryNo, @CategoryName)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CategoryNo", cat.CategoryNo);
                cmd.Parameters.AddWithValue("@CategoryName", cat.CategoryName);

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

        #region UpdateCategory data in Database
        public bool UpdateCategory(CategoriesAndResultsBLL cat)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Categories SET CategoryNo=@CategoryNo, CategoryName=@CategoryName WHERE CategoryNo=@CategoryNo";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CategoryNo", cat.CategoryNo);
                cmd.Parameters.AddWithValue("@CategoryName", cat.CategoryName);

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

        #region DeleteCategory data from Database
        public bool DeleteCategory(CategoriesAndResultsBLL cat)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_Categories WHERE CategoryNo=@CategoryNo";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CategoryNo", cat.CategoryNo);

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

        #region InsertResult data in Database
        public bool InsertResult(CategoriesAndResultsBLL res)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "INSERT INTO tbl_Results (ResultNo, ResultName) VALUES (@ResultNo, @ResultName)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ResultNo", res.ResultNo);
                cmd.Parameters.AddWithValue("@ResultName", res.ResultName);

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

        #region UpdateResult data in Database
        public bool UpdateResult(CategoriesAndResultsBLL res)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "UPDATE tbl_Results SET ResultNo=@ResultNo, ResultName=@ResultName WHERE ResultNo=@ResultNo";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ResultNo", res.ResultNo);
                cmd.Parameters.AddWithValue("@ResultName", res.ResultName);

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

        #region DeleteResult data from Database
        public bool DeleteResult(CategoriesAndResultsBLL res)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(UserDAL.myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_Results WHERE ResultNo=@ResultNo";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ResultNo", res.ResultNo);

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
