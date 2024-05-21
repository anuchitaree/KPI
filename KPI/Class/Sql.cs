using KPI.KeepClass;
using KPI.Models;
using KPI.Parameter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI.Class
{
    public static class Sql
    {
        private static readonly string connetionString = Connection.SQL_CONN_STRING;
        private static SqlConnection cnn;

        public static Boolean SqlOpenConnection()
        {
            Boolean _status = false;
            try
            {
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                if (cnn.State == ConnectionState.Open)
                {
                    _status = true;
                }
                return _status;

            }
            catch
            {
                _status = false;
                return _status;
            }
        }
    }
}
