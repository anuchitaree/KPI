using KPI.KeepClass;
using KPI.Parameter;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace KPI.Class
{
    public class SqlClassWGR
    {
        public string connetionString = Connection.SQL_CONN_STRING_WGR; //string.Empty;
        private SqlConnection cnn;

        public DataSet Dataset = new DataSet();
        public DataTable Datatable { get; set; }
        //public bool status { get; set; }

        public SqlClassWGR()
        {
        }

        #region MAIN CONNECTION

        public Boolean SqlOpenConnection()
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

        public void SqlCloseConnection()
        {
            cnn.Close();
            cnn.Dispose();
        }

        public DataSet SqlRead(string cmd)
        {
            SqlCommand objCmd = new SqlCommand();
            SqlDataAdapter dtAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();

            if (SqlOpenConnection())
            {
                try
                {
                    objCmd.Connection = cnn;
                    objCmd.CommandText = cmd;
                    objCmd.CommandType = CommandType.Text;

                    dtAdapter.SelectCommand = objCmd;
                    dtAdapter.Fill(ds);
                }
                catch
                {
                    throw new SqlCmdInvalidException();
                }
                finally
                {
                    SqlCloseConnection();
                }
            }
            else
            {
                throw new SqlConnectionException();
            }

            return ds;
        }

        public bool SqlWrite(string cmd)
        {
            bool _status;// = false;
            int returnCode;// = 0;

            if (SqlOpenConnection())
            {
                SqlCommand objCmd = new SqlCommand();

                try
                {
                    objCmd.Connection = cnn;
                    objCmd.CommandText = cmd;
                    objCmd.CommandType = CommandType.Text;

                    try
                    {
                        returnCode = objCmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw new SqlCmdInvalidException();
                    }

                    if (returnCode == 0)
                    {
                        _status = false;
                        throw new SqlInsertException();
                    }
                    else
                    {
                        _status = true;
                    }
                }




                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    SqlCloseConnection();
                    objCmd.Dispose();
                }
            }
            else
            {
                //_status = false;
                throw new SqlConnectionException();
            }

            return _status;
        }
        #endregion
        ////////////////////////////// STORE PROCEDURE ////////////////////////////////////////

        #region  for reference only  STORE PROCEDURE  4x1000

        public bool SSQL_S(string procedureName, string paraname1, string paradata1)
        {
            SqlCommand objCmd = new SqlCommand();
            SqlParameter paramStoreProc;
            SqlDataAdapter dtAdapter = new SqlDataAdapter();

            bool sqlstatus = false;
            if (SqlOpenConnection())
            {
                try
                {
                    objCmd.Connection = cnn;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = procedureName;

                    //PARAMETER 1 FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(paraname1, paradata1)
                    {
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String
                    };
                    objCmd.Parameters.Add(paramStoreProc);

                    //objCmd.ExecuteNonQuery();
                    dtAdapter.SelectCommand = objCmd;
                    dtAdapter.Fill(Dataset);
                    sqlstatus = true;

                }
                catch (Exception ex)
                {
                    ExcelClass.AppendErrorLogXlsx("4x1000", procedureName, ex.ToString());
                    sqlstatus = false;
                }
                finally
                {
                    SqlCloseConnection();
                }
            }
            else
            {
                ExcelClass.AppendErrorLogXlsx("4x1001", procedureName, "SQL connection failured");
                sqlstatus = false;
            }

            return sqlstatus;
        }



        public bool SSQL_SS(string procedureName, string paraname1, string paradata1, string paraname2, string paradata2)
        {
            SqlCommand objCmd = new SqlCommand();
            SqlParameter paramStoreProc;
            SqlDataAdapter dtAdapter = new SqlDataAdapter();

            bool sqlstatus = false;
            if (SqlOpenConnection())
            {
                try
                {
                    objCmd.Connection = cnn;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = procedureName;

                    //PARAMETER 1 FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(paraname1, paradata1)
                    {
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String
                    };
                    objCmd.Parameters.Add(paramStoreProc);

                    //PARAMETER 2 FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(paraname2, paradata2)
                    {
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String
                    };
                    objCmd.Parameters.Add(paramStoreProc);

                    //objCmd.ExecuteNonQuery();
                    dtAdapter.SelectCommand = objCmd;
                    dtAdapter.Fill(Dataset);
                    sqlstatus = true;

                }
                catch (Exception ex)
                {
                    ExcelClass.AppendErrorLogXlsx("4x1002", procedureName, ex.ToString());
                    sqlstatus = false;
                }
                finally
                {
                    SqlCloseConnection();
                }
            }
            else
            {
                ExcelClass.AppendErrorLogXlsx("4x1003", procedureName, "SQL connection failured");
                sqlstatus = false;
            }

            return sqlstatus;
        }








        #endregion

        #region  4x1100

        public bool DeleteRecord_RD_HL_CalibrationTableSQL(string run)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("DELETE FROM [WGR].[dbo].[RD_HL_CalibrationTable] WHERE run={0} \n", run);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("4x1100", "DeleteRecord_RD_HL_CalibrationTableSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true }, "Error code = 4x1100 , Message : " + ex.ToString() + " (LOOP SQL OeeSaveLDeleteRecord_RD_HL_CalibrationTableSQLosstimeSQL))", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool WarningRecord_RD_HL_NormalTableSQL(string section)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("SELECT[run],[currentDateTime],[dayNight],[machineId],[serialPartNumber],[chamber],[leakRate],");
            query.AppendFormat("[NGmode],[OKNGPiece],[warning],[ack] FROM [WGR].[dbo].[RD_HL_NormalTable]");
            query.AppendFormat(" where (warning='W' or OKNGPiece='NG') and ack is null and [sectionCode]='{0}'", section);
            var a = query.ToString();
            try
            {
                Datatable = SqlRead(a).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("4x1101", "WarningRecord_RD_HL_NormalTableSQL)", ex.ToString());
                  MessageBox.Show(new Form() { TopMost = true }, "Error code = 4x1101 , Message : " 
                      + ex.ToString() + " (LOOP SQL WarningRecord_RD_HL_NormalTableSQL))", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool StockMonitor(string section)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("select m.PartNumber,m.QTY,l.LowerLimit,l.UpperLimit from ");
            query.AppendFormat(" (select * from[WGR].[dbo].[StockMonitor] where SectionCode = '{0}') m ", section);
            query.Append("  left join[WGR].[dbo].[StockLimit] l  on m.PartNumber = l.PartNumber and m.SectionCode = l.SectionCode ");

            var a = query.ToString();
            try
            {
                Datatable = SqlRead(a).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("4x1102", "StockMonitor)", ex.ToString());
            }
            return sqlstatus;
        }

        public bool StockDataInputInsert(string section, string partnumber, string piece, string mode)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("insert into [WGR].[dbo].[StockDataInput] ([SectionCode],[PartNumber],[PiecePerKanban],[UserId],[Status]) ");
            query.AppendFormat("  Values('{0}', '{1}', {2}, {3}, 0 )", section, partnumber, piece, mode);
            var a = query.ToString();
            try
            {
                SqlWrite(a);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("4x1103", "StockDataInputInsert)", ex.ToString());
            }
            return sqlstatus;
        }

        public bool StockPiecePerKanban(string section)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("select PartNumber, Isnull(PiecePerKanban,0) FROM [WGR].[dbo].[StockLimit] where ");
            query.AppendFormat(" SectionCode = '{0}' ", section);
            var a = query.ToString();
            try
            {
                Datatable = SqlRead(a).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("4x1104", "StockPiecePerKanban)", ex.ToString());
            }
            return sqlstatus;
        }

        public bool ReadStockDataInput(string section)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT [run],[RegistDate],[PartNumber],[PiecePerKanban] FROM [WGR].[dbo].[StockDataInput] ");
            query.AppendFormat(" where Status = 0 and sectionCode ='{0}' order by RegistDate desc ", section);
            var a = query.ToString();
            try
            {
                Datatable = SqlRead(a).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("4x1105", "ReadStockDataInput)", ex.ToString());
            }
            return sqlstatus;
        }

        public bool ReadStockMonitor(string section)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("Select l.PartNumber,isnull(m.QTY,0),l.LowerLimit,l.UpperLimit FROM ");
            query.AppendFormat(" ( SELECT SectionCode,[PartNumber],[QTY] FROM[WGR].[dbo].[StockMonitor] Where[SectionCode] = '{0}') m ", section);
            query.AppendFormat(" RIGHT JOIN(SELECT * FROM[WGR].[dbo].[StockLimit] WHERE SectionCode = '{0}') l ", section);
            query.Append(" ON m.PartNumber = l.PartNumber and m.SectionCode = l.SectionCode ");
            var a = query.ToString();
            try
            {
                Datatable = SqlRead(a).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("4x1106", "ReadStockDataInput)", ex.ToString());
            }
            return sqlstatus;
        }

        public bool ReadHeliumSectionCode()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" Select w.sectionCode,o.sectionName From(Select sectionCode \n");
            query.Append(" FROM[WGR].[dbo].[RD_HL_CalibrationTable] Group by sectionCode ) w \n");
            query.Append(" Left join[Production].[dbo].[Emp_SectionTable] o ON w.sectionCode = o.sectionCode \n");
            var a = query.ToString();
            try
            {
                Datatable = SqlRead(a).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("4x1107", "ReadHeliumSectionCode)", ex.ToString());
            }
            return sqlstatus;
        }


        public bool ReadHeliumCalbrationData(string section, string date, string chamber)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" SELECT TOP(1)[firstBG],[secondBG],[firstSG],[secondSG],[judgeSN],[NGSetPoint] \n");
            query.AppendFormat(" FROM [WGR].[dbo].[RD_HL_Calibration] where sectionCode = '{0}' \n", section);
            query.AppendFormat(" and registDate = '{0}' and chamber = {1}  and dayNight = 'D'   \n",
                                date, chamber);
            query.Append(" order by currentDateTime desc");

            query.Append(" SELECT TOP(1)[firstBG],[secondBG],[firstSG],[secondSG],[judgeSN],[NGSetPoint] \n");
            query.AppendFormat(" FROM [WGR].[dbo].[RD_HL_Calibration] where sectionCode = '{0}' \n", section);
            query.AppendFormat(" and registDate = '{0}' and chamber = {1}  and dayNight = 'N' \n",
                date, chamber);
            query.Append(" order by currentDateTime desc");

            var a = query.ToString();
            try
            {
                Dataset = SqlRead(a);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("4x1107", "ReadHeliumCalbrationData)", ex.ToString());
            }
            return sqlstatus;
        }


        public bool TransferTableHeliumCalbrationData(string section, DateTime date, string daynignt, int chamber)
        {
            bool sqlstatus;
            int yy = date.Year;
            int mm = date.Month;
            int dd = date.Day;
            DateTime t1 = DateTime.Now;
            DateTime t2 = DateTime.Now;
            if (daynignt == "D")
            {
                t1 = new DateTime(yy, mm, dd, 23, 00, 00);
                t1 = t1.AddDays(-1);
                t2 = new DateTime(yy, mm, dd, 08, 00, 00);
            }
            else if (daynignt == "N")
            {
                t1 = new DateTime(yy, mm, dd, 11, 00, 00);
                t2 = new DateTime(yy, mm, dd, 20, 00, 00);
            }
            var query = new StringBuilder();

            query.Append(" Insert into[WGR].[dbo].[RD_HL_Calibration] \n");
            query.AppendFormat(" SELECT TOP(1)[sectionCode],'{0}',[currentDateTime],'{1}', \n", date.ToString("yyyy-MM-dd"), daynignt);
            query.Append(" [machineId],[chamber],[firstBG],[secondBG],[firstSG],[secondSG],[judgeSN], \n");
            query.Append(" [safetyFactor]  ,[NGSetPoint],[AckPerson] \n");
            query.Append(" ,[AckDateTime] FROM[WGR].[dbo].[RD_HL_CalibrationTable] \n");
            query.AppendFormat(" Where sectionCode = '{0}' and calibrationCompleted = 'OK' and chamber = {1} \n", section, chamber);
            query.AppendFormat(" and currentDateTime between '{0}' and '{1}' ", t1, t2);
            query.Append(" Order by currentDateTime desc ");

            var a = query.ToString();
            try
            {
                 SqlWrite(a);
                sqlstatus = true;
            }
            catch // (Exception ex)
            {
                sqlstatus = false;
                //  ExcelClass.AppendErrorLogXlsx("4x1108", "TransferTableHeliumCalbrationData)", ex.ToString());
            }
            return sqlstatus;
        }

        #endregion
    }
}
