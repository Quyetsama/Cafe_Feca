using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cafe_cafe.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();
                return instance;
            }
            private set 
            { 
                instance = value; 
            }
        }

        private DataProvider(){}

        private string connectionString = @"Data Source=DESKTOP-EBVUMV9\MSSQLSERVER01;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand cmd = new SqlCommand(query, connection);

                if(parameter != null)
                {
                    string[] listparam = query.Split(' ');
                    int i = 0;
                    foreach(string item in listparam)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);

                connection.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return data;


        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand cmd = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listparam = query.Split(' ');
                    int i = 0;
                    foreach (string item in listparam)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = cmd.ExecuteNonQuery();

                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return data;


        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand cmd = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listparam = query.Split(' ');
                    int i = 0;
                    foreach (string item in listparam)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = cmd.ExecuteScalar();

                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return data;


        }
    }
}
