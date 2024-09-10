using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Prog_II_Act_1.Properties;
using System.Data;

namespace Prog_II_Act_1.Utils
{
    public class DataHelper
    {
        private static DataHelper? _instance;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Resources.CnnString);
        }

        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }

        public DataTable ExecuteSPQuery(string sp, List<Parameter>? parametros)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(sp,_connection);
            try
            {
                _connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                if(parametros != null)
                {
                    foreach (Parameter param in parametros)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }
                    dt.Load(cmd.ExecuteReader());
                }
            } catch (Exception ex)
            {
                dt = null;
            } finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            return dt;

        }

        public int ExecuteSPDML(string sp, List<Parameter>? parameters)
        {
            int rows = 0;
            SqlCommand cmd = new SqlCommand(sp,_connection);
            try
            {
                _connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                if(parameters != null)
                {
                    foreach (Parameter param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Name,param.Value);
                    }
                }
                rows = cmd.ExecuteNonQuery();
            } catch (Exception ex)
            {
                rows = 0;
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                    _connection.Close();
            }

            return rows;
        }
    }
}
