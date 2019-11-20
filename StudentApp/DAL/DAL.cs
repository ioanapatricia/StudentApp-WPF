using System;
using System.Data;
using System.Data.SqlClient;

namespace StudentApp.DAL
{
    public class DataAccessLayer
    {
        private const string ConnectionString = "Server= DESKTOP-T1KQEKO;Database= StudentDB;Integrated Security=True;";

        public DataTable GetQueryResult(string query)
        {
            var tbl = new DataTable();

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    connection.Open();

                    //create data adapter
                    var da = new SqlDataAdapter(cmd);
                    //this will query your database and return the result to your datatable
                    da.Fill(tbl);
                    //conn.Close();
                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //MessageBox.Show(ex.Message);
            }

            return tbl;
        }

        public void ExecuteNonQuery(string insertQuery)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(insertQuery,connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public object GetValueFromDb(string query)
        {
            object obj = null;  
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command =new SqlCommand(query,connection);
                command.Connection.Open();
                obj = command.ExecuteScalar();
            }   

            return obj;
        }
    }
}
