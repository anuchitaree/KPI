using KPI.KeepClass;
using KPI.Models;
using KPI.Parameter;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static KPI.Class.OEE;

namespace KPI.Class
{
    public class SqlClass
    {
        public string connetionString = Connection.SQL_CONN_STRING;
        private SqlConnection cnn;

        public DataSet Dataset = new DataSet();
        public DataTable Datatable { get; set; }
        //public bool status { get; set; }

        public SqlClass()
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
                        //throw new SqlInsertException();
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

        #region  STORE PROCEDURE

        public bool SSQL_(string procedureName)
        {
            SqlCommand objCmd = new SqlCommand();
            //SqlParameter paramStoreProc;
            SqlDataAdapter dtAdapter = new SqlDataAdapter();

            bool sqlstatus = false;
            if (SqlOpenConnection())
            {
                try
                {
                    objCmd.Connection = cnn;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = procedureName;

                    //objCmd.ExecuteNonQuery();
                    dtAdapter.SelectCommand = objCmd;
                    dtAdapter.Fill(Dataset);
                    sqlstatus = true;

                }
                catch (Exception ex)
                {
                    ExcelClass.AppendErrorLogXlsx("0x0010", procedureName, ex.ToString());
                    sqlstatus = false;
                }
                finally
                {
                    SqlCloseConnection();
                }
            }
            else
            {
                ExcelClass.AppendErrorLogXlsx("0x0011", procedureName, "SQL connection failured");
                sqlstatus = false;
            }

            return sqlstatus;
        }

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
                    ExcelClass.AppendErrorLogXlsx("0x0012", procedureName, ex.ToString());
                    sqlstatus = false;
                }
                finally
                {
                    SqlCloseConnection();
                }
            }
            else
            {
                ExcelClass.AppendErrorLogXlsx("0x0013", procedureName, "SQL connection failured");
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
                    ExcelClass.AppendErrorLogXlsx("0x0014", procedureName, ex.ToString());
                    sqlstatus = false;
                }
                finally
                {
                    SqlCloseConnection();
                }
            }
            else
            {
                ExcelClass.AppendErrorLogXlsx("0x0015", procedureName, "SQL connection failured");
                sqlstatus = false;
            }

            return sqlstatus;
        }

        public bool SSQL_II(string procedureName, string paraname1, int paradata1, string paraname2, int paradata2)
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
                        DbType = DbType.Int16
                    };
                    objCmd.Parameters.Add(paramStoreProc);

                    //PARAMETER 2 FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(paraname2, paradata2)
                    {
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Int16
                    };
                    objCmd.Parameters.Add(paramStoreProc);

                    //objCmd.ExecuteNonQuery();
                    dtAdapter.SelectCommand = objCmd;
                    dtAdapter.Fill(Dataset);
                    sqlstatus = true;

                }
                catch (Exception ex)
                {
                    ExcelClass.AppendErrorLogXlsx("0x0016", procedureName, ex.ToString());
                    sqlstatus = false;
                }
                finally
                {
                    SqlCloseConnection();
                }
            }
            else
            {
                ExcelClass.AppendErrorLogXlsx("0x0017", procedureName, "SQL connection failured");
                sqlstatus = false;
            }

            return sqlstatus;
        }

        public bool SSQL_SIS(string procedureName, string paraname1, string paradata1, string paraname2, int paradata2,
            string paraname3, string paradata3)
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
                        DbType = DbType.Int32
                    };
                    objCmd.Parameters.Add(paramStoreProc);

                    //PARAMETER 3 FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(paraname3, paradata3)
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
                    ExcelClass.AppendErrorLogXlsx("0x0000", procedureName, ex.ToString());
                    sqlstatus = false;
                }
                finally
                {
                    SqlCloseConnection();
                }
            }
            else
            {
                ExcelClass.AppendErrorLogXlsx("0x0000", procedureName, "SQL connection failured");
                sqlstatus = false;
            }

            return sqlstatus;
        }

        public bool SSQL_SSS(string procedureName, string paraname1, string paradata1, string paraname2, string paradata2,
            string paraname3, string paradata3)
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
                    //PARAMETER 3 FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(paraname3, paradata3)
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
                    ExcelClass.AppendErrorLogXlsx("0x0000", procedureName, ex.ToString());
                    sqlstatus = false;
                }
                finally
                {
                    SqlCloseConnection();

                }
            }
            else
            {
                ExcelClass.AppendErrorLogXlsx("0x0000", procedureName, "SQL connection failured");
                sqlstatus = false;
            }

            return sqlstatus;
        }

        public bool SSQL_SSSS(string procedureName, string paraname1, string paradata1, string paraname2, string paradata2,
            string paraname3, string paradata3, string paraname4, string paradata4)
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
                    //PARAMETER 3 FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(paraname3, paradata3)
                    {
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String
                    };
                    objCmd.Parameters.Add(paramStoreProc);
                    //PARAMETER 4 FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(paraname4, paradata4)
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
                    ExcelClass.AppendErrorLogXlsx("0x0001", procedureName, ex.ToString());
                    sqlstatus = false;
                }
                finally
                {
                    SqlCloseConnection();

                }
            }
            else
            {
                ExcelClass.AppendErrorLogXlsx("0x0001", procedureName, "SQL connection failured");
                //        MessageBox.Show(new Form() { TopMost = true }, "0x0002", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlstatus = false;
            }

            return sqlstatus;
        }

        public bool SSQL_SISS(string procedureName, string paraname1, string paradata1, string paraname2, int paradata2,
            string paraname3, string paradata3, string paraname4, string paradata4)
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
                        DbType = DbType.Int32
                    };
                    objCmd.Parameters.Add(paramStoreProc);
                    //PARAMETER 3 FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(paraname3, paradata3)
                    {
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String
                    };
                    objCmd.Parameters.Add(paramStoreProc);
                    //PARAMETER 4 FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(paraname4, paradata4)
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
                    ExcelClass.AppendErrorLogXlsx("0x0001", procedureName, ex.ToString());
                    sqlstatus = false;
                }
                finally
                {
                    SqlCloseConnection();

                }
            }
            else
            {
                ExcelClass.AppendErrorLogXlsx("0x0001", procedureName, "SQL connection failured");
                sqlstatus = false;
            }

            return sqlstatus;
        }


        #endregion


        /////////////////////////////////   SQL LOGIN FORM   0010  ////////////////////////////////////////////////////////////
        #region  SQL LOGIN FORM   0x0000

        public string UserLoginSSQL(string procedureName, string para10, int userid, string para20, string password)
        {
            SqlCommand objCmd = new SqlCommand();
            //SqlDataAdapter dtAdapter = new SqlDataAdapter();
            SqlParameter paramStoreProc;
            //DataSet ds = new DataSet();
            string respMsg = string.Empty;

            if (SqlOpenConnection())
            {
                try
                {
                    objCmd.Connection = cnn;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = procedureName;

                    //USERNAME PARAMETER FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(para10, userid)
                    {
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String
                    };
                    objCmd.Parameters.Add(paramStoreProc);
                    //PASSWORD PARAMETER FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(para20, password)
                    {
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String
                    };
                    objCmd.Parameters.Add(paramStoreProc);

                    //OUTPUT MESSAGE FROM STORE PROCEDURE
                    objCmd.Parameters.Add("@responMessage", SqlDbType.NVarChar, 250);
                    objCmd.Parameters["@responMessage"].Direction = ParameterDirection.Output;

                    int returnCode = objCmd.ExecuteNonQuery();

                    respMsg = Convert.ToString(objCmd.Parameters["@responMessage"].Value);
                    return respMsg;
                }
                catch (Exception ex)
                {
                    ExcelClass.AppendErrorLogXlsx("0x0026", "UserLoginSSQL", ex.ToString());
                    Console.WriteLine("Error code = 0x0026 , Message : " + ex.ToString() + " (LOOP SQLUserLoginSSQL)");
                    MessageBox.Show(new Form() { TopMost = true },
                        "Error code = 0x0026, Message: " + ex.ToString() + "(LOOP SQL UserLoginSSQL)", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    SqlCloseConnection();
                }
            }
            else
            {
                respMsg = string.Empty;
                ExcelClass.AppendErrorLogXlsx("0x0027", "UserLoginSSQL", "SQL connection failed");
                Console.WriteLine("Error code = 0x0027, Message: SQL connection failed. (LOOP SQL UserLoginSSQL)");
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 0x0027, Message: SQL connection failed. (LOOP SQL UserLoginSSQL)", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return respMsg;
        }


        public string UserRegistrationSSQL(string procedureName, string para10, int userid, string para20, string password)
        {
            SqlCommand objCmd = new SqlCommand();
            //SqlDataAdapter dtAdapter = new SqlDataAdapter();
            SqlParameter paramStoreProc;
            //DataSet ds = new DataSet();
            string respMsg = string.Empty;

            if (SqlOpenConnection())
            {
                try
                {
                    objCmd.Connection = cnn;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = procedureName;

                    //USERNAME PARAMETER FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(para10, userid)
                    {
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String
                    };
                    objCmd.Parameters.Add(paramStoreProc);
                    //PASSWORD PARAMETER FOR STORE PROCEDURE
                    paramStoreProc = new SqlParameter(para20, password)
                    {
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String
                    };
                    objCmd.Parameters.Add(paramStoreProc);

                    //OUTPUT MESSAGE FROM STORE PROCEDURE
                    objCmd.Parameters.Add("@responMessage", SqlDbType.NVarChar, 250);
                    objCmd.Parameters["@responMessage"].Direction = ParameterDirection.Output;

                    int returnCode = objCmd.ExecuteNonQuery();

                    respMsg = Convert.ToString(objCmd.Parameters["@responMessage"].Value);
                    return respMsg;
                }
                catch (Exception ex)
                {
                    ExcelClass.AppendErrorLogXlsx("0x0028", "UserLoginSSQL", ex.ToString());
                    Console.WriteLine("Error code = 0x0028 , Message : " + ex.ToString() + " (LOOP SQLUserLoginSSQL)");
                    MessageBox.Show(new Form() { TopMost = true },
                        "Error code = 0x0028, Message: " + ex.ToString() + "(LOOP SQL UserLoginSSQL)", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    SqlCloseConnection();
                }
            }
            else
            {
                respMsg = string.Empty;
                ExcelClass.AppendErrorLogXlsx("0x0029", "UserLoginSSQL", "SQL connection failed");
                Console.WriteLine("Error code = 0x0029, Message: SQL connection failed. (LOOP SQL UserLoginSSQL)");
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 0x0029, Message: SQL connection failed. (LOOP SQL UserLoginSSQL)", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return respMsg;
        }



        public bool SectioncodeAuthorizationSQL(string ID)
        {
            bool sqlstatus;// = false;
            var query = new StringBuilder();
            query.Append("SELECT a.[SectionCode],s.[SectionName]FROM [Production].[dbo].[Emp_AuthorizationTable] a\n");
            query.Append(" LEFT JOIN[Production].[dbo].[Emp_SectionTable] s ON a.SectionCode = s.SectionCode\n");
            query.AppendFormat(" WHERE [userID] = '{0}'", ID);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];

                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("0x0030", "SectioncodeAuthorizationSQL", ex.ToString());
                Console.WriteLine("Error code = 0x0030 , Message : " + ex.ToString() + " (LOOP SQL SectioncodeAuthorizationSQL)");
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 0x0030 , Message : " + ex.ToString() + " (LOOP SQL SectioncodeAuthorizationSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return sqlstatus;
        }
        #endregion





        #region  1)  SQL mainform FORM  1x0000
        public bool ReadRevisionSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("Select[RevisionNo]FROM[Production].[dbo].[Software_RevisionTable] ORDER BY[RegistDate]DESC");
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];

                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x0000", "ReadRevisionSQL", ex.ToString());
                Console.WriteLine("Error code = 1x0000, Message : " + ex.ToString() + " (LOOP SQL ReadRevisionSQL(1))");
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x0000 , Message : " + ex.ToString() + " (LOOP SQL ReadRevisionSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return sqlstatus;
        }


        public bool LoadEmployeeAllSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT[userId],[FullName]FROM [Production].[dbo].[Emp_ManPowersTable] ");
            query.AppendFormat(" where Fullname is not null and [DivisionId]='{0}' and [PlantId]='{1}'", User.Division, User.Plant);
            try
            {
                DataTable dt1 = SqlRead(query.ToString()).Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string id = Convert.ToString(dt1.Rows[i].ItemArray[0]);
                        string fullname = Convert.ToString(dt1.Rows[i].ItemArray[1]);
                        Dict.EmpIDName.Add(id, fullname);
                    }
                }
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x0001", "LoadEmployeeAllSQL", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x0001 , Message : " + ex.ToString() + " (LOOP SQL LoadEmployeeAllSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return sqlstatus;
        }

        public bool OAPopUpSQL(string registdate, string hourNo)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT [Hour],[Monitor],[PercentOA],[Redalarm],[Workstatus] ");
            query.Append(" FROM [Production].[dbo].[Prod_PPASTable] ");
            query.AppendFormat(" where SectionCode='{0}' and RegistDate='{1}' and [Hour] = {2} order by [Hour]",
                User.SectionCode, registdate, hourNo);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch 
            {
                sqlstatus = false;
                //ExcelClass.AppendErrorLogXlsx("1x0002", "OAPopUpSQL", ex.ToString());
                //Console.WriteLine("Error code = 1x0002, Message : " + ex.ToString() + " (LOOP SQL OAPopUpSQL)");
                //MessageBox.Show(new Form() { TopMost = true },
                //    "Error code = 1x0002 , Message : " + ex.ToString() + " (LOOP SQL OAPopUpSQL)", "Error",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return sqlstatus;
        }



        public bool SaveErrorLogFileXlsxSQL()
        {
            bool sqlstatus = false;
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO [Production].[dbo].[Software_ErrorCodeTable] ");
            query.Append(" ([occuredDateTime], [userId],[errorCode],[functionName],[decription]) VALUES \n");
            FileInfo excelFile = new FileInfo(Paths.Errorlogxlsx);
            if (File.Exists(Paths.Errorlogxlsx) == true)
            {
                using (ExcelPackage excel = new ExcelPackage(excelFile))
                {
                    var workSheet = excel.Workbook.Worksheets["sheet1"];
                    int row = Convert.ToInt32(workSheet.Cells[1, 6].Value);
                    if (row > 2)
                    {
                        try
                        {
                            for (int i = 2; i < row; i++)
                            {
                                DateTime dt = Convert.ToDateTime(workSheet.Cells[i, 1].Value);
                                string dtstr = dt.ToString("yyyy-MM-dd HH:mm:ss");
                                string userId = Convert.ToString(workSheet.Cells[i, 2].Value);
                                userId = userId == "" ? "0" : userId;
                                string errorCode = Convert.ToString(workSheet.Cells[i, 3].Value);
                                string functionName = Convert.ToString(workSheet.Cells[i, 4].Value);
                                string decription = Convert.ToString(workSheet.Cells[i, 5].Value);
                                query.AppendFormat("('{0}',{1},'{2}','{3}','{4}') \n",
                                    dtstr, userId, errorCode, functionName, decription);
                                if (i < row - 1) query.Append(",");
                                workSheet.Cells[i, 1].Value = "";
                                workSheet.Cells[i, 2].Value = "";
                                workSheet.Cells[i, 3].Value = "";
                                workSheet.Cells[i, 4].Value = "";
                                workSheet.Cells[i, 5].Value = "";
                            }
                            workSheet.Cells[1, 6].Value = 2;
                            excel.SaveAs(excelFile);

                            var a = query.ToString();

                            SqlWrite(query.ToString());
                            sqlstatus = true;
                        }
                        catch (Exception ex)
                        {
                            sqlstatus = false;
                            ExcelClass.AppendErrorLogXlsx("1x0003", "SaveErrorLogFileXlsxSQL", ex.ToString());
                        }


                    }

                }

            }

            return sqlstatus;

        }


        public bool SaveTrackLogInSQL()
        {
            bool sqlstatus;
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO [dbo].[Emp_UserLoginTrackingTable] ");
            query.Append(" ([userID],[userSection],[LogInTime],[LogOutTime]) VALUES \n");
            query.AppendFormat("({0},'{1}','{2}','{3}') \n", User.ID, User.SectionCode,
                User.LogInTime.ToString("yyyy-MM-dd HH:mm:ss"), User.LogOutTime.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                var a = query.ToString();
                SqlWrite(a);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x0004", "SaveTrackLogInSQL", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x0004 , Message : " + ex.ToString() + " (LOOP SQL SaveTrackLogInSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return sqlstatus;

        }



        #endregion


        ///////////////////////////////// 1.1)  SQL MASTER SECTION REGISTRATION FORM    1x1100  /////////////////////
        #region 1.1)  SQL MASTER SECTION REGISTRATION FORM   1x1100
        public bool LoadSectionMemberSQL(string Sectionkey, string Shiftkey)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT m.[userId] ,p.[FullName] ,m.[functionId],m.[Rate] ,m.[functionOTId] ,m.[RateOT] \n");
            query.Append("from[Emp_SectionMemberTable] m left join Emp_ManPowersTable p  ON m.[userId] = p.[userId]");
            query.AppendFormat("Where SectionCode = '{0}'and shiftAB = '{1}' and m.[divisionID] ='{2}' and m.[plantID]='{3}' \n ",
                Sectionkey, Shiftkey, User.Division, User.Plant);
            query.AppendFormat(" ORDER BY [functionId]");
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1100", "LoadSectionMemberSQL", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1100 , Message : " + ex.ToString() + " (LOOP SQL LoadSectionMemberSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool SectionMenberRegistrationSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1101", "SectionMenberRegistrationSQL", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1101 , Message : " + ex.ToString() + " (LOOP SQL SectionMenberRegistrationSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        #endregion

        ///////////////////////////////// 1.2)  SQL MAN POWER REGISTRATION FORM   0x1200  ////////////////////////////////////////////////////////////
        #region 1.2)  SQL MAN POWER REGISTRATION FORM   1x1200 

        public bool MPregist_LoadSectionMemberIDSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT [userID] FROM [Production].[dbo].[Emp_SectionMemberTable] ");
            query.AppendFormat(" WHERE [SectionCode]='{0}' and [DivisionId]='{1}' and [PlantId]='{2}'",
                User.SectionCode, User.Division, User.Plant);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1201", "MPregist_LoadSectionMemberIDSQL", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1201, Message : " + ex.ToString() + " (LOOP SQL MPregist_LoadSectionMemberIDSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        public bool MPregist_SavePlanProductionSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                Console.WriteLine("Error code = 1x1202, Message : " + ex.ToString() + " (LOOP SQL MPregist_SavePlanProductionSQL)");
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1202 , Message : " + ex.ToString() + " (LOOP SQL MPregist_SavePlanProductionSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool MPregist_DeleteMenberRegistrationSQL(DataGridView dataGridView1a, DataGridView dataGridView1b,
            string dateToday, string SectionCode, string daynightA, string daynightB)
        {
            bool sqlstatus;

            var query = new StringBuilder();
            if (dataGridView1a.Rows.Count == 0)
            {
                //string DN = comboBox1.select
                query.Append("delete from Emp_ManPowerRegistedTable ");
                query.AppendFormat($" where[registDate] = '{dateToday}' and shiftAB = 'A' and SectionCodeFrom = '{SectionCode}' \n");

                query.Append("delete from [dbo].[Exclusion_RecordTable] ");
                query.AppendFormat(" where [SectionCode] = '{0}' and [RegistDate] ='{1}'  and ShiftAB ='A' and", SectionCode, dateToday);
                query.Append("([exclusionId] = 'F1' or [exclusionId] = 'F2' or [exclusionId] = 'F3' or [exclusionId] = 'M1') \n");

                query.Append("delete from [Production].[dbo].[Prod_TodayWorkTable] ");
                query.AppendFormat(" where [SectionCode]=  '{0}' and [RegistDate] ='{1}' and [DayNight]='{2}' \n",
                    SectionCode, dateToday, daynightA);
            }
            if (dataGridView1b.Rows.Count == 0)
            {
                query.Append("delete from Emp_ManPowerRegistedTable ");
                query.AppendFormat($" where [registDate] = '{dateToday}' and shiftAB = 'B' and SectionCodeFrom = '{SectionCode}' \n");
                query.Append("delete from [dbo].[Exclusion_RecordTable] ");
                query.AppendFormat(" where [SectionCode] = '{0}' and [RegistDate] ='{1}'  and ShiftAB ='B' and", SectionCode, dateToday);
                query.Append("([exclusionId] = 'F1' or [exclusionId] = 'F2' or [exclusionId] = 'F3' or [exclusionId] = 'M1') \n");
                query.Append("delete from [Production].[dbo].[Prod_TodayWorkTable] ");
                query.AppendFormat(" where [SectionCode]=  '{0}' and [RegistDate] ='{1}' and [DayNight]='{2}' \n",
                    SectionCode, dateToday, daynightB);
            }
            try
            {
                //var a = query.ToString();
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1203", "MPregist_DeleteMenberRegistrationSQL", ex.ToString());
            }
            return sqlstatus;
        }

        public bool MPregist_SaveMPregistrationSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1204", "MPregist_SaveMPregistrationSQL", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1204 , Message : " + ex.ToString() + " (LOOP SQL MPregist_SaveMPregistrationSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        #endregion

        ///////////////////////////////// 1.3) SQL INPUT EXCLUSION TIME FORM  1x1300  ///////
        #region EXCLUSION TIME
        public bool Exclusion_LoadExclusionNameSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT [ExclusionID],[ExclusionName] FROM [dbo].[Exclusion_ItemsTable] ");
            query.AppendFormat(" WHERE[divisionId]='{0}'and[plantId]='{1}' ORDER BY [sort] ", User.Division, User.Plant);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x0011", "Exclusion_LoadExclusionNameSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x0011 , Message : " + ex.ToString() + " (LOOP SQL Exclusion_LoadExclusionNameSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool Exclusion_SaveExclusionTimeSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1300", "Exclusion_SaveExclusionTimeSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1300 , Message : " + ex.ToString() + " (LOOP SQL Exclusion_SaveExclusionTimeSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }



        public bool Exclusion_DeleteExclusionTimeRecordSQL(string id)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("delete [dbo].[Exclusion_RecordTable] where [run] = {0}", id);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1301", "Exclusion_DeleteExclusionTimeRecordSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1301 , Message : " + ex.ToString() + " (LOOP SQL Exclusion_DeleteExclusionTimeRecordSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        #endregion

        #region 1.5)  SQL INPUT LOSS FORM   1x1400
        public bool Loss_DeleteLossDetailNameSQL(string lossId, string lossDetailId)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("delete FROM [Production].[dbo].[Loss_DetailTable]");
            query.AppendFormat(" WHERE [LossID]='{0}' and [LossDetailID]='{1}' and[sectionCode]='{2}' and [divisionId]='{3}' and [plantId]='{4}' ",
                            lossId, lossDetailId, User.SectionCode, User.Division, User.Plant);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1401", "Loss_DeleteLossDetailNameSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1401 , Message : " + ex.ToString() + " (LOOP SQL Loss_DeleteLossDetailNameSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool Loss_InsertLossDetailNameSQL(string lossId, string lossDetailName, string mcId)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("Insert into [Production].[dbo].[Loss_DetailTable] ");
            query.Append(" ([LossID],[LossDetailName],[sectionCode],[divisionId],[plantId],[recodeId],[machineId])");
            query.AppendFormat(" values('{0}','{1}','{2}','{3}','{4}',{5},'{6}')",
                lossId, lossDetailName, User.SectionCode, User.Division, User.Plant, User.ID, mcId);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1402", "Loss_InsertLossDetailNameSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1402 , Message : " + ex.ToString() + " (LOOP SQL Loss_InsertLossDetailNameSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool Loss_SaveLossTimeSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1403", "Loss_SaveLossTimeSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1403 , Message : " + ex.ToString() + " (LOOP SQL Loss_SaveLossTimeSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool Loss_LoadItemSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT [LossID],[LossName] FROM [Production].[dbo].[Loss_ItemsTable] ");
            query.AppendFormat(" WHERE [divisionId]='{0}' and [plantId]='{1}' ", User.Division, User.Plant);
            query.Append("SELECT [machineId],[machineName] FROM[Production].[dbo].[Prod_MachineNameTable] ");
            query.AppendFormat(" WHERE sectionCode = '{0}' order by sort asc", User.SectionCode);
            try
            {
                Dataset = SqlRead(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1404", "Loss_LoadItemSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1404 , Message : " + ex.ToString() + " (LOOP SQL Loss_LoadItemSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool Loss_UpdateDetailViewSQL(string lossId)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT[LossDetailID],[LossDetailName]FROM [Production].[dbo].[Loss_DetailTable] ");
            query.AppendFormat(" Where LossID = '{0}' and[sectionCode]='{1}'", lossId, User.SectionCode);
            query.AppendFormat(" and [divisionId]='{0}' and [plantId]='{1}' Order by[LossDetailName],[LossDetailID]",
                User.Division, User.Plant);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1405", "Loss_UpdateDetailViewSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1405 , Message : " + ex.ToString() + " (LOOP SQL Loss_UpdateDetailViewSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool Loss_DeleteRecordSQL(string id)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("delete FROM [Production].[dbo].[Loss_RecordTable] where [run] = {0}", id);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1406", "Loss_DeleteRecordSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1406 , Message : " + ex.ToString() + " (LOOP SQL Loss_DeleteRecordSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        public bool Loss_MachineLossDetailSQL(string mcId, string lossId)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("Select lossDetailID,lossDetailName FROM [dbo].[Loss_DetailTable] ");
            query.AppendFormat(" Where sectionCode='{0}' and [lossID]='{1}' ", User.SectionCode, lossId);
            query.AppendFormat(" and machineId ='{0}' ", mcId);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1407", "Loss_LoadRecordTableSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1407 , Message : " + ex.ToString() + " (LOOP SQL Loss_LoadRecordTableSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool Loss_UpdateMachineLossSQL(string run, string lossDetailId)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat(" UPDATE [dbo].[Loss_RecordTable]  SET lossDetailID = {0} WHERE run = {1} ", lossDetailId, run);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1408", "Loss_UpdateMachineLossSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1408 , Message : " + ex.ToString() + " (LOOP SQL Loss_UpdateMachineLossSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool Loss_EditDetailLossSQL(string run)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" SELECT [lossDetailName],[machineId] FROM [Production].[dbo].[Loss_DetailTable] ");
            query.AppendFormat("  WHERE[sectionCode] = '{0}' and lossDetailID = {1} ", User.SectionCode, run);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1409", "Loss_EditDetailLossSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1409 , Message : " + ex.ToString() + " (LOOP SQL Loss_EditDetailLossSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }




        public bool Loss_UpdateDetailLossSQL(string lossdetailname, string mcId, string run)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" update [Production].[dbo].[Loss_DetailTable] ");
            query.AppendFormat(" SET [lossDetailName]='{0}',[machineId]='{1}'  WHERE [lossDetailID]= {2} ",
                lossdetailname, mcId, run);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x140A", "Loss_UpdateDetailLossSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x140A , Message : " + ex.ToString() + " (LOOP SQL Loss_UpdateDetailLossSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        #endregion

        #region 1.7)  SQL PPAS FORM 1x1500

        public bool PPAS_LoadFrameMonitorSQL(DateTime firstdate, string daynight)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("Select[Monitor],[Period],[StdOA],[Plan100],[AccPlan]FROM [Production].[dbo].[Prod_PPASTable] ");
            query.AppendFormat(" where [SectionCode]='{0}'", User.SectionCode);
            query.Append($"and[RegistDate]='{firstdate:yyyy-MM-dd}'and[DayNight]='{daynight}' order by[Hour] asc");
            try
            {
                var a = query.ToString();
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1500", "PPAS_LoadFrameMonitorSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1500 , Message : " + ex.ToString() + " (LOOP SQL PPAS_LoadFrameMonitorSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool PPAS_RefreshDataMonitorSQL(string rundateStr, string daynight)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append($"Select[Volume],[AccVol],[PercentOA],[RedAlarm],[VolumePerHr],[Workstatus],[PercentOAavg]");
            query.Append($" FROM[Production].[dbo].[Prod_PPASTable] where[SectionCode] = '{User.SectionCode}'");
            query.Append($" and[RegistDate] = '{rundateStr}'and[DayNight] = '{daynight}'order by[Hour] asc ");
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1501", "PPAS_RefreshDataMonitorSQL)", ex.ToString());
            }
            return sqlstatus;
        }

        public bool PPAS_UpdateOABoradSQL(string plan, string actual, string diff, string or, string pn)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat($"update [Production].[dbo].[Prod_OABoardTable] set [PlanVolume] ={plan}, ");
            query.AppendFormat($" [ActualVolume]={actual},[diff]={diff},[OA]={or},[partNumber]={pn} Where[SectionCode] = '{User.SectionCode}'");
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1502", "PPAS_UpdateOABoradSQL)", ex.ToString());
            }
            return sqlstatus;
        }

        public bool PPAS_LoadRedFrontSQL(string startdate, string stopdate)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("select registDate, count(*) FROM[Production].[dbo].[Prod_PPASTable] ");
            query.AppendFormat(" where registDate between '{0}' and '{1}' ", startdate, stopdate);
            query.AppendFormat("and sectionCode = '{0}'  and alarm = 'A'  Group by registDate order by registDate asc",
                User.SectionCode);
            try
            {

                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1503", "PPAS_LoadRedFrontSQL)", ex.ToString());
            }
            return sqlstatus;
        }



        public bool OABoradSQL(string section, string plan, string diff, string or, string status)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat($"update [Production].[dbo].[Prod_OABoardTable] set [PlanVolume] ={plan}, ");
            query.AppendFormat($" [diff]={diff},[OA]={or},[status]='{status}' Where[SectionCode] = '{section}'");
            //     var a = query.ToString();
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1504", "PPAS_UpdateOABoradSQL)", ex.ToString());
            }
            return sqlstatus;
        }

        public bool OABoradTableSQL(string section)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" SELECT[planVolume],[actualVolume],[diff],[oa],[ctavg],[partNumber]   ,[status],ISNULL([oaTarget],100),[active] ");
            query.AppendFormat(" FROM[Production].[dbo].[Prod_OABoardTable] where sectionCode = '{0}' ", section);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1505", "OABoradTableSQL)", ex.ToString());
            }
            return sqlstatus;
        }


        #endregion

        ///////////////////////////////// 1.8) SQL HISTORY PPAS FORM   1x1600  ////////////////////////////
        #region 1.8) SQL HISTORY PPAS FORM 1x1600
        public bool HisPPAS_DataMonitorSQL(string rundateStr)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append($"Select[Volume],[AccVol],[PercentOA],[RedAlarm],[VolumePerHr],[WorkStatus],[PercentOAavg] ");
            query.Append($" FROM[Production].[dbo].[Prod_PPASTable] where[SectionCode] = '{User.SectionCode}'");
            query.Append($"and[RegistDate] = '{rundateStr}'order by[Hour] asc");
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1600", "HisPPAS_DataMonitorSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1600 , Message : " + ex.ToString() + " (LOOP SQL HisPPAS_DataMonitorSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        public bool HisPPAS_LoadFrameMonitorSQL(DateTime firstdate)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append($"Select[Monitor],[Period],[StdOA],[Plan100],[AccPlan]FROM [Production].[dbo].[Prod_PPASTable] ");
            query.Append($" where [SectionCode]='{User.SectionCode}'");
            query.Append($" and[RegistDate]='{firstdate:yyyy-MM-dd}' order by[Hour] asc");
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1601", "HisPPAS_LoadFrameMonitorSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1601 , Message : " + ex.ToString() + " (LOOP SQL HisPPAS_LoadFrameMonitorSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        public bool HisPPAS_HeaderEXcelSQL(string YY, string MM)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT TOP(1) [productvolumn],[numberWorkDay],[numberShift],[CycleTimeAverage],[manPower] ");
            query.AppendFormat(" FROM [Production].[dbo].[Prod_StdMonthlyTable] where sectionCode ='{0}' \n", User.SectionCode);
            query.AppendFormat("  and registYear ='{0}' and registMonth ='{1}' order by [registYear],[registMonth]", YY, MM);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1602", "HisPPAS_HeaderEXcelSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1602 , Message : " + ex.ToString() + " (LOOP SQL HisPPAS_HeaderEXcelSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }
        #endregion

        ///////////////////////////////// 1.9)  SQL  PRODUCTION VOLUME FORM   1x1700 /////////////////
        #region 1.9)  SQL  PRODUCTION VOLUME FORM 1x1700
        public bool Prod_VolumLoadSQL(string query)
        {
            bool sqlstatus;
            try
            {
                Dataset = SqlRead(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1700", "Prod_VolumLoadSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1700 , Message : " + ex.ToString() + " (LOOP SQL Prod_VolumLoadSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }




        #endregion

        ///////////////////////////////// 1.10)  SQL PRODUCTION VOLUME BY HOUR FORM   1x1800  ////////////////////////////////////////////////////////////
        #region 1.10)  SQL PRODUCTION VOLUME BY HOUR FORM 1x1800


        public bool Prod_VolumReadByPartNumberSQL(string query)
        {
            bool sqlstatus;
            try
            {
                Dataset = SqlRead(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1800", "Prod_VolumReadTimeBreakQueueMonitorSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1800 , Message : " + ex.ToString() + " (LOOP SQL Prod_VolumReadTimeBreakQueueMonitorSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }





        #endregion

        ///////////////////////////////// 1.11)  SQL REWORK FORM   1x1900  ////////////////////////////////////////////////////////////
        #region 1.11)  SQL REWORK FORM 1x1900


        public bool ReworkDefctSaveSQL(string mode, string description)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("INSERT INTO [Production].[dbo].[Rework_ModeTable] ([SectionCode],[reworkModeName],[decription])VALUES");
            query.AppendFormat("('{0}','{1}','{2}')", User.SectionCode, mode, description);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1900", "ReworkDefctSaveSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1900 , Message : " + ex.ToString() + " (LOOP SQL ReworkDefctSaveSQL)", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ReworkDefectLoadSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT [reworkModeId] ,[reworkModeName],[decription]FROM[Production].[dbo].[Rework_ModeTable]");
            query.AppendFormat(" WHERE [sectionCode] ='{0}' ", User.SectionCode);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1901", "ReworkDefctLoadSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1901 , Message : " + ex.ToString() + " (LOOP SQL ReworkDefctLoadSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ReworkDefectDeleteSQL(string key)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("DELETE  FROM [Production].[dbo].[Rework_ModeTable] ");
            query.AppendFormat(" WHERE [sectionCode] ='{0}' and [reworkModeId]={1} ", User.SectionCode, key);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1902", "ReworkDefectDeleteSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1902 , Message : " + ex.ToString() + " (LOOP SQL ReworkDefectDeleteSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        //

        public bool ReworkRecoverMethodSaveSQL(string mode, string description)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("INSERT INTO [Production].[dbo].[Rework_RepairMethodTable] ");
            query.Append(" ([sectionCode],[repairMethodName],[description]) VALUES");
            query.AppendFormat("('{0}','{1}','{2}')", User.SectionCode, mode, description);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1903", "ReworkRecoverMethodSaveSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1903 , Message : " + ex.ToString() + " (LOOP SQL ReworkRecoverMethodSaveSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ReworkRecoverMethodLoadSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT [repairMethodId],[repairMethodName],[description] FROM[Production].[dbo].[Rework_RepairMethodTable]");
            query.AppendFormat(" WHERE [sectionCode] ='{0}' ", User.SectionCode);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1904", "ReworkRecoverMethodLoadSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1904 , Message : " + ex.ToString() + " (LOOP SQL ReworkRecoverMethodLoadSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool DeleteRe_WorkMethodSQL(string key)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("DELETE  FROM [Production].[dbo].[Rework_RepairMethodTable] ");
            query.AppendFormat(" WHERE [sectionCode] ='{0}' and [repairMethodId]={1} ", User.SectionCode, key);
            //var a = query.ToString();
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1905", "DeleteRe_WorkMethodSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1905 , Message : " + ex.ToString() + " (LOOP SQL ReworkRecoverMethodDeleteSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        public bool ReworkRecoverPersonLoadSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT n.repairWhoId,e.fullName, n.run FROM (  \n ");
            query.Append("SELECT  [repairWhoId],[markColor],[remark] ,run FROM [Production].[dbo].[Rework_RecoveryPersonTable] \n ");
            query.AppendFormat(" WHERE[sectionCode] = '{0}') n  \n ", User.SectionCode);
            query.Append(" LEFT JOIN[Production].[dbo].[Emp_ManPowersTable] e ON  n.repairWhoId = e.userID \n ");
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1906", "ReworkRecoverPersonLoadSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1906 , Message : " + ex.ToString() + " (LOOP SQL ReworkRecoverPersonLoadSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ReworkRecoverPersonSaveSQL(string id)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("INSERT INTO [Production].[dbo].[Rework_RecoveryPersonTable] ([sectionCode],[repairWhoId]) VALUES \n");
            query.AppendFormat(" ('{0}',{1})", User.SectionCode, id);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1907", "ReworkRecoverPersonSaveSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1907 , Message : " + ex.ToString() + " (LOOP SQL ReworkRecoverPersonSaveSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }



        public bool ReworkSAVELOGSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1908", "ReworkSAVELOGSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1908 , Message : " + ex.ToString() + " (LOOP SQL ReworkSAVELOGSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ReworkDeleteLOGSQL(string run)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("DELETE FROM [Production].[dbo].[Rework_RecordTable] WHERE[run]={0}", run);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1909", "ReworkDeleteLOGSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1909 , Message : " + ex.ToString() + " (LOOP SQL ReworkDeleteLOGSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ReworkRecoverPersonDeleteSQL(string run)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("Delete   FROM [Production].[dbo].[Rework_RecoveryPersonTable] where run={0} ", run);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x190A", "ReworkRecoverPersonDeleteSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x190A , Message : " + ex.ToString() + " (LOOP SQL ReworkRecoverPersonDeleteSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        #endregion

        ///////////////////////////////// 1.12)  SQL OEE FORM   1x1A00  ///////////////////////////
        #region 1.12)  SQL OEE  FORM  1x1A00




        #endregion


        ///////////////////////////////// 1.13)  SQL MACHINE DOWN TIME FORM   1x1B00  ////////////////////////////////////////////////////////////
        #region 1.10)  SQL PRODUCTION VOLUME BY HOUR FORM 1x1B00


        public bool LossOEE(string section, string registdate1, string registdate2)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" SELECT[run],[MCNumber],[errorCode],[Second]FROM[Production].[dbo].[Loss_RecordTable] ");
            query.AppendFormat(" where[sectionCode] = '{0}' and[registDate]between'{1}' and '{2}' and[mcStop] = 'S' ",
                section,registdate1, registdate2);
            query.Append(" SELECT[machineId],[machineName] FROM[Production].[dbo].[Prod_MachineNameTable] ");
            query.AppendFormat(" where[sectionCode] = '{0}' ",section);
            string a = query.ToString();
            try
            {
                Dataset = SqlRead(a);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1B00 , Message : " + ex.ToString() + " (LOOP SQL LossOEE))",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }



       

        #endregion



        ///////////////////////////////// 1.14)  SQL OEE Loss more detail FORM   1x1C00  ////////////////////////////////////////////////////////////
        #region OEE Loss more detail  1x1C00

        //public bool OEE_DashBoard(string section, string registdate1, string registdate2)
        //{
        //    bool sqlstatus;
        //    var query = new StringBuilder();
        //    query.Append(" SELECT[registDate],[machineID],[LoadingTime],[A1],[A2],[A3],[P1],[P2],[P3],[P4] \n");
        //    query.Append(",[OK],[NG],[RE],[A],[P],[Q],[OEE] FROM[Production].[dbo].[OEE_MC] \n");
        //    query.AppendFormat("WHERE sectionCode='{0}' and registDate Between '{1}' and '{2}' \n", section, registdate1, registdate2);

        //    query.Append("SELECT [registDate],[A],[P],[Q],[OEE] FROM [Production].[dbo].[OEE_Line] \n");
        //    query.AppendFormat("WHERE sectionCode='{0}' and registDate Between '{1}' and '{2}' \n", section, registdate1, registdate2);

        //    query.Append(" Select[machineId],[machineName] FROM[Production].[dbo].[Prod_MachineNameTable] \n");
        //    query.AppendFormat("  Where sectionCode = '{0}' ", section);
        //    string a = query.ToString();
        //    try
        //    {
        //        Dataset = SqlRead(a);
        //        sqlstatus = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        sqlstatus = false;
        //        MessageBox.Show(new Form() { TopMost = true },
        //            "Error code = 1x1C00 , Message : " + ex.ToString() + " (LOOP SQL OEE_DashBoard))",
        //            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return sqlstatus;
        //}








        public bool OEEMCandLine(string section, string registdate, List<OeeItems> oeeMc, LineOEE oeeline)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            int row = oeeMc.Count;
            if (row == 0)
                return false;

            query.AppendFormat("Delete FROM [Production].[dbo].[OEE_MC]  Where sectionCode = '{0}' and  registDate = '{1}' \n", section, registdate);
            query.Append(" Insert into [Production].[dbo].[OEE_MC] ([sectionCode],[registDate],[machineID],");
            query.Append(" [LoadingTime],[A1],[A2],[A3],");
            query.Append(" [P1],[P2],[P3],[P4],[OK],[NG],[RE],[A],[P],[Q],[OEE]  ) Values \n");
            int i = 0;
            foreach (var item in oeeMc)
            {
                query.AppendFormat("('{0}','{1}','{2}',{3},{4},{5},{6},",
                    section, registdate, item.MachineID, item.LoadingTime, item.A1, item.A2, item.A3);
                query.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}) \n", item.P1, item.P2, item.P3, item.P4, item.OK, item.NG, item.RE,
                    item.AvailabilityPercent, item.PerformancePercent, item.QaulityPercent, item.OEEPercent);
                if (i < row - 1)
                    query.Append(",");
                i++;
            }
            query.AppendFormat("Delete From [Production].[dbo].[OEE_Line] Where sectionCode='{0}' and  registDate = '{1}' \n", section, registdate);
            query.Append("Insert into [Production].[dbo].[OEE_Line] ([sectionCode],[registDate],[A],[P],[Q],[OEE]) Values \n");
            query.AppendFormat("('{0}','{1}',{2},{3},{4},{5})", section, registdate, oeeline.A, oeeline.P, oeeline.Q, oeeline.OEE);
            string a = query.ToString();
            try
            {
                SqlWrite(a);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                MessageBox.Show(new Form() { TopMost = true },
                  "Error code = 1x1C01 , Message : " + ex.ToString() + " (LOOP SQL OEEMCandLine))",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }



        public bool OEEMCandLineDel(string section, string registdate)
        {
            bool sqlstatus;
            var query = new StringBuilder();

            query.AppendFormat("Delete FROM [Production].[dbo].[OEE_MC]  Where sectionCode = '{0}' and  registDate = '{1}' \n", section, registdate);

            query.AppendFormat("Delete From [Production].[dbo].[OEE_Line] Where sectionCode='{0}' and  registDate = '{1}' \n", section, registdate);
            string a = query.ToString();
            try
            {
                SqlWrite(a);
                sqlstatus = true;
            }
            catch // (Exception ex)
            {
                sqlstatus = false;
                //MessageBox.Show(new Form() { TopMost = true },
                //  "Error code = 1x1C02 , Message : " + ex.ToString() + " (LOOP SQL OEEMCandLineDel))",
                //  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        public bool UpdateLossRecordDB (string registDate, string section,List<UpdateLossDb> loss)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            foreach (UpdateLossDb l in loss)
            {
                query.AppendFormat("Update[Production].[dbo].[Loss_RecordTable] Set[Second] = {0}, [Pureloss]={1} where run = {2} \n", l.MixLossTime,l.PureLossTime,l.Run);
            }
            string a = query.ToString();
            try
            {
                SqlWrite(a);
                sqlstatus = true;
            }
            catch // (Exception ex)
            {
                sqlstatus = false;
            }
            return sqlstatus;
        }




        public bool GHOeeRawInsert(DateTime dt,string section,List<GHOeeRaw> raw)
        {
          //  bool sqlstatus;
            string rgd = dt.ToString("yyyy-MM-dd");
            int rowcout = raw.Count;

            string str = $" delete From[Production].[dbo].[GHOee_Raw] where registDate = '{rgd}' and sectionCode = '{section}' \n";
            SqlWrite(str);

            for (int j = 0; j < rowcout; j += 1000)
            {
                int max = 1000;
                int diff = (rowcout - j) / 1000;
                if (diff < 1)
                    max = rowcout - j;

                var query = new StringBuilder();
                query.Append("  Insert into[Production].[dbo].[GHOee_Raw] ([sectionCode],[registDate],[machineID],[Type] ,[startMinuteTime],[occurePeriodMinute])Values \n");
                for (int k = 0; k < max; k++)
                {
                    GHOeeRaw l = raw[k+j];
                    query.AppendFormat("('{0}','{1}','{2}',{3},{4},{5}) \n", section, rgd, l.MachineID, l.Type, l.StartMinuteTime, l.OccurePeriodMinute);
                    if (k < max - 1)
                        query.Append(",");
                }
                string queryStr = query.ToString();
                try
                {
                    SqlWrite(queryStr);
                }
                catch
                {
                    Console.WriteLine($"loop at J= {j}");
                }

            }

            //var query = new StringBuilder();
            //query.AppendFormat("  delete From[Production].[dbo].[GHOee_Raw] where registDate = '{0}' and sectionCode = '{1}' \n", rgd, section);
            //query.Append("  Insert into[Production].[dbo].[GHOee_Raw] ([sectionCode],[registDate],[machineID],[Type] ,[startMinuteTime],[occurePeriodMinute])Values \n");
            //int row = raw.Count;
            //int i = 0;
            //foreach (GHOeeRaw l in raw)
            //{
            //    query.AppendFormat("('{0}','{1}','{2}',{3},{4},{5}) \n",section,rgd,l.MachineID,l.Type,l.StartMinuteTime,l.OccurePeriodMinute);
            //    if (i < row - 1)
            //        query.Append(",");
            //        i++;
            //}
            //string a = query.ToString();
            //try
            //{
            //    SqlWrite(a);
            //    sqlstatus = true;
            //}
            //catch // (Exception ex)
            //{
            //    sqlstatus = false;
            //}






            return true;
        }


        #endregion

        ///////////////////////////////// 1.15)  SQL PPASEventPopupForm FORM   1x1D00  ////////////////////////////////////////////////////////////

        #region PPASEventPopupForm  1x1D00


        #endregion


        ///////////////////////////////// 1.16)  SQL  PRODUCTION VOLUME BY MACHINE FORM   1x1E00  ////////////////////////////////////////////////////////////
        #region   PRODUCTION VOLUME BY MACHINE   1x1E00


        public bool ProductVolume_MachineNameLoadSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("  SELECT [machineId] ,[machineName] FROM [Production].[dbo].[Prod_MachineNameTable] ");
            query.AppendFormat(" WHERE [sectionCode]='{0}'  ORDER BY [sort] \n", User.SectionCode);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1E00", "LoadEventLossNowSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1E00 , Message : " + ex.ToString() + " (LOOP SQL LoadEventLossNowSQL))",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ProductVolume_BYMachineLoadSQL(DateTime dtstart, DateTime dtstop)
        {
            string dt1 = dtstart.ToString("yyyy-MM-dd");
            string dt2 = dtstop.ToString("yyyy-MM-dd");
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" SELECT c.mcNumber,m.machineName,c.qty FROM ( select mcNumber,count(run) as qty FROM [dbo].[ML_RecordTable] ");
            query.AppendFormat("WHERE registDate between '{0}' and '{1}' and sectionCode = '{2}' group by mcNumber ) c ",
                dt1, dt2, User.SectionCode);
            query.Append(" INNER JOIN (Select machineId,machineName ,sort From [dbo].[Prod_MachineNameTable] ");
            query.AppendFormat(" where sectionCode= '{0}') m  ON c.mcNumber = m.machineId ", User.SectionCode);
            query.Append(" order by m.sort");
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1E01", "ProductVolume_BYMachineLoadSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1E01 , Message : " + ex.ToString() + " (LOOP SQL ProductVolume_BYMachineLoadSQL))",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ProductVolume_ProductNameLoadSQL(DateTime dtstart, DateTime dtstop)
        {

            string dt1 = dtstart.ToString("yyyy-MM-dd");
            string dt2 = dtstop.ToString("yyyy-MM-dd");
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("  select partNumber FROM [dbo].[ML_RecordTable] ");
            query.AppendFormat(" Where registDate between '{0}' and '{1}' and partNumber <> 'invalid input' Group by partNumber ", dt1, dt2);

            try
            {
                var a = query.ToString();
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x1E02", "ProductVolume_ProductNameLoadSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x1E02 , Message : " + ex.ToString() + " (LOOP SQL ProductVolume_ProductNameLoadSQL))",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        #endregion

        ///////////////////////////////// 1.17)  SQL Tracibility + EDER FORM   1x1F00  ////////////////////////////////////////////////////////////
        #region   SQL Tracibility + EDER 1x1F00



        #endregion
        ///////////////////////////////// 1.18)  SQL  VISUALIZE CYCLE TIME FORM   1x2000  ////////////////////////////////////////////////////////////
        #region   VISUALIZE CYCLE TIME 1x2000




        #endregion
        ///////////////////////////////// 1.19)  SQL  SIX LOSS GROUP FORM   1x2100  ////////////////////////////////////////////////////////////
        #region   SIX LOSS GROUP FORM    1x2100




        #endregion

        ///////////////////////////////// 1.20)  SQL  FORM   1x2200  ////////////////////////////////////////////////////////////
        #region    1x2200


        #endregion
        ///////////////////////////////// 1.21)  SQL  FORM   1x2300  ////////////////////////////////////////////////////////////
        #region    1x2300


        #endregion


        //////////////////////////////// 2.0)  SQL COMMON  1x2400  ////////////////////////////////////////////////////////////

        ///////////////////////////////// 2.1) SQL MONTHLY FORM   1x2500  ////////////////////////////////////////////////////////////
        #region 2.1) SQL MONTHLY FORM 1x2500
        public bool Monthly_SaveMasterDensoSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2500", "Monthly_SaveMasterDensoSQL)", ex.ToString());
                Console.WriteLine("Error code = 1x2500, Message : " + ex.ToString() + " (LOOP SQL Monthly_SaveMasterDensoSQL)");
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2500 , Message : " + ex.ToString() + " (LOOP SQL Monthly_SaveMasterDensoSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool Monthly_SaveMasterCustomerSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2501", "Monthly_SaveMasterCustomerSQL)", ex.ToString());
                Console.WriteLine("Error code = 1x2501, Message : " + ex.ToString() + " (LOOP SQL Monthly_SaveMasterCustomerSQL)");
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2501 , Message : " + ex.ToString() + " (LOOP SQL Monthly_SaveMasterCustomerSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        #endregion

        ///////////////////////////////// 2.2)  SQL  YEARLY FORM   1x2600  ////////////////////////////////////////////////////////////
        #region 2.2)  SQL  YEARLY FORM   1x2600
        public bool Yearly_LoadStandardRecordSQL(string section, string registyear)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT [registYear],[registMonth],[CycleTimeAverage],[StandardRatio],[oa] \n");
            query.Append($"FROM [Production].[dbo].[Prod_StdYearlyTable] WHERE [SectionCode]='{section}' and registYear ='{registyear}' \n");
            query.Append("order by registMonth asc");
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2600", "Yearly_LoadStandardRecordSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2600 , Message : " + ex.ToString() + " (LOOP SQL Yearly_LoadStandardRecordSQL))",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool Yearly_LoadNetTimeStandardSQL(string section, string registyear)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append($"SELECT [run],[PartNumber],[NetTime],[CT] FROM [Production].[dbo].[Prod_NetTimeTable] ");
            query.AppendFormat(" where SectionCode = '{0}' and [registYear] ='{1}'", section, registyear);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2601", "Yearly_LoadNetTimeStandardSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2601 , Message : " + ex.ToString() + " (LOOP SQL Yearly_LoadNetTimeStandardSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool Yearly_SaveStandardRecordSQL(string section, string year, string CT, string stdratio, string oa, string ID)
        {
            bool sqlstatus;
            string registdate = DateTime.Now.ToString("yyyy-MM-dd");
            var query = new StringBuilder();
            query.Append($"delete [Production].[dbo].[Prod_StdYearlyTable] where SectionCode ='{section}' and registYear = '{year}' \n\r");
            query.Append(" Insert into [Production].[dbo].[Prod_StdYearlyTable] ");
            query.Append(" ([sectionCode],[registYear],[registMonth],[CycleTimeAverage],[StandardRatio],[oa],[registDate],[who]) VALUES");
            query.Append($"('{section}','{year}','01',{CT},{stdratio},{oa},'{registdate}',{ID}),\n");
            query.Append($"('{section}','{year}','02',{CT},{stdratio},{oa},'{registdate}',{ID}),\n");
            query.Append($"('{section}','{year}','03',{CT},{stdratio},{oa},'{registdate}',{ID}),\n");
            query.Append($"('{section}','{year}','04',{CT},{stdratio},{oa},'{registdate}',{ID}),\n");
            query.Append($"('{section}','{year}','05',{CT},{stdratio},{oa},'{registdate}',{ID}),\n");
            query.Append($"('{section}','{year}','06',{CT},{stdratio},{oa},'{registdate}',{ID}),\n");
            query.Append($"('{section}','{year}','07',{CT},{stdratio},{oa},'{registdate}',{ID}),\n");
            query.Append($"('{section}','{year}','08',{CT},{stdratio},{oa},'{registdate}',{ID}),\n");
            query.Append($"('{section}','{year}','09',{CT},{stdratio},{oa},'{registdate}',{ID}),\n");
            query.Append($"('{section}','{year}','10',{CT},{stdratio},{oa},'{registdate}',{ID}),\n");
            query.Append($"('{section}','{year}','11',{CT},{stdratio},{oa},'{registdate}',{ID}),\n");
            query.Append($"('{section}','{year}','12',{CT},{stdratio},{oa},'{registdate}',{ID})\n");

            try
            {
                var a = query.ToString();
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2602", "Yearly_SaveStandardRecordSQ)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2602 , Message : " + ex.ToString() + " (LOOP SQL Yearly_SaveStandardRecordSQ)", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        public bool Yearly_SaveAllNetTimeSQL(string section, string year, DataGridView dgv)
        {
            var query = new StringBuilder();
            query.AppendFormat("Delete [dbo].[Prod_NetTimeTable] WHERE registYear ='{0}' and sectionCode='{1}' \n", year, section);
            query.Append("INSERT INTO [dbo].[Prod_NetTimeTable] ([registYear],[sectionCode],[partNumber],[netTime],[CT]) VALUES \n ");
            int rows = dgv.Rows.Count - 1;
            for (int i = 0; i < rows; i++)
            {
                bool status1 = double.TryParse(dgv.Rows[i].Cells[2].Value.ToString(), out double net);
                bool status2 = double.TryParse(dgv.Rows[i].Cells[3].Value.ToString(), out double ctt);

                if (dgv.Rows[i].Cells[1].Value != null && status1 == true && status2 == true)
                {
                    string pn = dgv.Rows[i].Cells[1].Value.ToString().Trim();
                    string nt = net.ToString();
                    string ct = ctt.ToString();
                    query.AppendFormat("('{0}','{1}','{2}',{3},{4}) \n", year, section, pn, nt, ct);
                    if (i < rows - 1)
                        query.AppendFormat(",");
                }
                else
                {
                    MessageBox.Show("Input wrong data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            var a = query.ToString();
            bool sqlstatus;

            try
            {
                SqlWrite(a);
                sqlstatus = true;
            }

            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2603", "Yearly_SaveAllNetTimeSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2603 , Message : " + ex.ToString() + " (LOOP SQL Yearly_UpdateStandardTimeSQL))",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;

        }

        public bool Yearly_SaveSTDRatioSQL(string section, string year, DataGridView dgv)
        {
            var query = new StringBuilder();
            query.AppendFormat("DELETE [dbo].[Prod_StdYearlyTable] WHERE sectionCode = '{0}' and registYear='{1}' \n", section, year);
            query.Append("INSERT INTO [dbo].[Prod_StdYearlyTable] \n");
            query.Append("( [sectionCode],[registYear] ,[registMonth] ,[cycleTimeAverage] ,[standardRatio],[oa],");
            query.Append("[registDate],[who] ) Values \n");
            string reg = DateTime.Now.ToString("yyyy-MM-dd");
            string who = User.ID;
            for (int i = 0; i < 12; i++)
            {
                string month = (i + 1).ToString("00");
                double.TryParse(dgv.Rows[i].Cells[2].Value.ToString(), out double ct);
                double.TryParse(dgv.Rows[i].Cells[3].Value.ToString(), out double std);
                double.TryParse(dgv.Rows[i].Cells[4].Value.ToString(), out double oa);
                string ctt = ct.ToString();
                string stdd = std.ToString();
                string oaa = oa.ToString();
                query.AppendFormat("('{0}','{1}','{2}',{3},{4},{5},'{6}',{7}) \n ", section, year, month, ctt, stdd, oaa, reg, who);
                if (i < 11)
                    query.Append(",");
            }
            var a = query.ToString();
            bool sqlstatus;
            try
            {
                SqlWrite(a);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2604", "Yearly_SaveSTDRatioSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2604 , Message : " + ex.ToString() + " (LOOP SQL Yearly_UpdateSTDRatioSQL))",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }
        #endregion

        ///////////////////////////////// 2.3)  SQL LUNCH BREAK FORM   1x2700  ///////////////////////////
        #region 2.3)  SQL LUNCH BREAK FORM  1x2700

        public bool BreakTimeLoadQuqueSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("SELECT [breakQueue]FROM [Production].[dbo].[Prod_TimeBreakTable] ");
            query.AppendFormat(" WHERE divisionID='{0}' and plantID='{1}' GROUP by [breakQueue] ", User.Division, User.Plant);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2700", "BreakTimeLoadQuqueSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2700 , Message : " + ex.ToString() + " (LOOP SQL BreakTimeLoadQuqueSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        public bool BreakTimeSaveYearQuqueSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2701", "BreakTimeSaveYearQuqueSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2701 , Message : " + ex.ToString() + " (LOOP SQL BreakTimeSaveYearQuqueSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }
        #endregion

        ///////////////////////////////// 2.4)  SQL  MACHINE NAME AND ERROR CODE  1x2800  ////////////////////////////////////////////////////////////
        #region 2.4)  SQL  MACHINE NAME AND ERROR CODE FORM   1x2800

        public bool MC_MahineNameReadSQL(string section)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT[run],[machineId],[machineName],[sort],[stationNo] FROM[dbo].[Prod_MachineNameTable] \n");
            query.AppendFormat(" where SectionCode = '{0}'", section);
            var a = query.ToString();
            try
            {
                Datatable = SqlRead(a).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2800", "MC_MahineNameReadSQL)", ex.ToString());
            }
            return sqlstatus;
        }

        public bool MC_MahineNameSaveSQL(string section, DataGridView dgv)
        {
            var query = new StringBuilder();
            var query2 = new StringBuilder();
            query.AppendFormat("Delete [dbo].[Prod_MachineNameTable]  WHERE sectionCode='{0}' \n", section);
            query.Append("INSERT INTO [dbo].[Prod_MachineNameTable] ([sectionCode],[machineId] ,[machineName] ,[sort],[stationNo]) VALUES \n ");

            query2.AppendFormat("DELETE FROM [Production].[dbo].[Oee_MachineTimeTable] WHERE sectionCode='{0}' \n", section);
            query2.Append("INSERT INTO [dbo].[Oee_MachineTimeTable] ([sectionCode],[machineId],[MTaverage]) VALUES \n");
            int rows = dgv.Rows.Count - 1;
            for (int i = 0; i < rows; i++)
            {
                bool status1 = double.TryParse(dgv.Rows[i].Cells[3].Value.ToString(), out double sort);
                bool status2 = double.TryParse(dgv.Rows[i].Cells[4].Value.ToString(), out double station);
                double.TryParse(dgv.Rows[i].Cells[5].Value.ToString(), out double mt);

                if (dgv.Rows[i].Cells[1].Value != null && status1 == true && status2 == true)
                {
                    string mcId = dgv.Rows[i].Cells[1].Value.ToString();
                    string mcName = dgv.Rows[i].Cells[2].Value.ToString();
                    string sortt = sort.ToString();
                    string stationn = station.ToString();
                    string mtt = mt.ToString();
                    query.AppendFormat("('{0}','{1}','{2}',{3},{4} ) \n", section, mcId, mcName, sortt, stationn);
                    query2.AppendFormat(" ( '{0}','{1}',{2} ) \n", section, mcId, mtt);
                    if (i < rows - 1)
                    {
                        query.AppendFormat(",");
                        query2.AppendFormat(",");
                    }
                }
                else
                {
                    MessageBox.Show("Input wrong data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            var a = query.ToString();
            var b = query2.ToString();
            bool sqlstatus;
            try
            {
                SqlWrite(a);
                SqlWrite(b);
                sqlstatus = true;
            }

            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2801", "MC_MahineNameSaveSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2801 , Message : " + ex.ToString() + " (LOOP SQL MC_MahineNameSaveSQL))",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;

        }

        public bool ErrorCodeSaveSQL(string section, string mcId, DataGridView dgv)
        {
            var query = new StringBuilder();
            query.AppendFormat("Delete [dbo].[Prod_MachineFaultCodeTable]  WHERE sectionCode='{0}' and [machineId] = '{1}' \n",
                section, mcId);
            query.Append("INSERT INTO [dbo].[Prod_MachineFaultCodeTable] ");
            query.Append(" ([sectionCode],[machineId],[faultCode],[messageAlarm],[decription]) VALUES \n ");

            int rows = dgv.Rows.Count - 1;
            for (int i = 0; i < rows; i++)
            {

                if (dgv.Rows[i].Cells[1].Value != null)
                {
                    string faultCode = dgv.Rows[i].Cells[1].Value.ToString();
                    string message = dgv.Rows[i].Cells[2].Value.ToString();
                    string decrip = dgv.Rows[i].Cells[3].Value.ToString();
                    query.AppendFormat("('{0}','{1}','{2}','{3}','{4}' ) \n", section, mcId, faultCode, message, decrip);
                    if (i < rows - 1)
                    {
                        query.AppendFormat(",");
                    }
                }
                else
                {
                    MessageBox.Show("Input wrong data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            var a = query.ToString();
            bool sqlstatus;
            try
            {
                SqlWrite(a);
                sqlstatus = true;
            }

            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2802", "ErrorCodeSaveSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2802 , Message : " + ex.ToString() + " (LOOP SQL ErrorCodeSaveSQL))",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;

        }

        public bool StationGroupReadSQL(string section)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat(" select [StationNo] FROM [dbo].[ML_NagaraStartTimeTable] Where SectionCode = '{0}' Group by StationNo", section);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("0x2403", "StationGroupReadSQL)", ex.ToString());
            }
            return sqlstatus;


        }

        public bool MachineErrorCodeReadSQL(string machineID)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" SELECT [faultCode],[messageAlarm] ,[decription]  FROM [dbo].[Prod_MachineFaultCodeTable]");
            query.AppendFormat(" Where [sectionCode] ='{0}' and [machineId]='{1}' order by faultCode asc ", User.SectionCode, machineID);
            try
            {
                var a = query.ToString();
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2803", "MachineErrorCodeReadSQL)", ex.ToString());
            }
            return sqlstatus;


        }

        #endregion

        ///////////////////////////////// 2.5)  SQL  Production Monthly Plan FORM   1x2900   ////////////////////////////
        #region 2.5)  SQL  Production Monthly Plan FORM   1x2900

        public bool ProductionPlan_PartNumberReadSQL(string year, string section)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT[partNumber]  FROM[Production].[dbo].[Prod_NetTimeTable] ");
            query.AppendFormat(" WHERE[registYear] = '{0}' and[sectionCode] = '{1}' ", year, section);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2900", "ProductionPlan_PartNumberReadSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2900 , Message : " + ex.ToString() + " (LOOP SQL ProductionPlan_PartNumberReadSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ProductionPlan_PartNumberPlanReadSQL(string year, string month, string section)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT [run],[partNumber] ,[planQty] FROM[Production].[dbo].[Prod_ProdPlanTable] ");
            query.AppendFormat(" WHERE[sectionCode] = '{0}' AND[registYear] = '{1}' AND[registMonth] = '{2}' order by [planQty] DESC \n",
                section, year, month);
            query.Append(" SELECT SUM([planQty]) FROM[Production].[dbo].[Prod_ProdPlanTable] ");
            query.AppendFormat(" WHERE[sectionCode] = '{0}' AND[registYear] = '{1}' AND[registMonth] = '{2}'", section, year, month);
            try
            {
                Dataset = SqlRead(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2901", "ProductionPlan_PartNumberPlanReadSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2901 , Message : " + ex.ToString() + " (LOOP SQL ProductionPlan_PartNumberPlanReadSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ProductionPlan_PartNumberPlanSaveSQL(string section, string year, string month, DataGridView dgv)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("DELETE FROM [dbo].[Prod_ProdPlanTable] WHERE [sectionCode] = '{0}'", section);
            query.AppendFormat(" and [registYear]='{0}' and [registMonth]='{1}'", year, month);
            if (dgv.Rows.Count > 0)
            {
                query.Append(" INSERT INTO [dbo].[Prod_ProdPlanTable] ([sectionCode],[registYear],[registMonth],[partNumber],[planQty]) VALUES \n");
                int row = dgv.Rows.Count;
                for (int i = 0; i < row; i++)
                {
                    string pn = dgv.Rows[i].Cells[2].Value.ToString();
                    string qty = dgv.Rows[i].Cells[3].Value.ToString();
                    query.AppendFormat("('{0}','{1}','{2}','{3}',{4} ) \n", section, year, month, pn, qty);
                    if (i < row - 1)
                        query.Append(",");
                }
            }
            var a = query.ToString();
            try
            {
                SqlWrite(a);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2902", "ProductionPlan_PartNumberPlanSaveSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2902 , Message : " + ex.ToString() + " (LOOP SQL ProductionPlan_PartNumberPlanSaveSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }



        #endregion

        ///////////////////////////////// 2.6)  SQL Machine Time   FORM   1x2A00   ////////////////////////////////////////////////////////////
        #region 

        public bool LoadPartNumberSQL(string section, string registyear)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append($"SELECT [PartNumber] FROM [Production].[dbo].[Prod_NetTimeTable] ");
            query.AppendFormat(" where SectionCode = '{0}' and [registYear] ='{1}'", section, registyear);
            var a = query.ToString();
            try
            {
                Datatable = SqlRead(a).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2A00 , Message : " + ex.ToString() + " (LOOP SQL Yearly_LoadPartNumberSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool MachineTimeSaveSQL(string section, string machineID, DataGridView dgv)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" Delete FROM[Production].[dbo].[OeeMachineTimes] ");
            query.AppendFormat("  Where[section_id] = '{0}' and [machine_id] = '{1}' \n", section, machineID);
            query.Append("  Insert into [Production].[dbo].[OeeMachineTimes] ([section_id],[machine_id],[part_number],[mt_min_sec],[ht_min_sec]) VALUES \n");
            int row = dgv.Rows.Count - 1;
            for (int i = 0; i < row; i++)
            {
                string partnumber = dgv.Rows[i].Cells[1].Value.ToString();
                string mTmin = dgv.Rows[i].Cells[2].Value.ToString();
                string hTmin = dgv.Rows[i].Cells[3].Value.ToString();
                query.AppendFormat("('{0}','{1}','{2}',{3},{4}) \n", section, machineID, partnumber, mTmin, hTmin);
                if (i < row - 1)
                    query.Append(",");
            }
            var a = query.ToString();
            try
            {
                SqlWrite(a);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2A01 , Message : " + ex.ToString() + " (LOOP SQL MachineTimeSaveSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }




        public bool MC_MahineTimeNameReadSQL(string section)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT[run],[machineId],[machineName] FROM[dbo].[Prod_MachineNameTable] \n");
            query.AppendFormat(" where SectionCode = '{0}'", section);
            query.Append(" SELECT[machineID],[partNumber],[MTminSec],[HTminSec]FROM[Production].[dbo].[Oee_MachineTime] ");
            query.AppendFormat("  Where[sectionCode] = '{0}' ", section);

            var a = query.ToString();
            try
            {
                Dataset = SqlRead(a);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2800", "MC_MahineTimeReadSQL)", ex.ToString());
            }
            return sqlstatus;
        }

        public bool MC_MahineTimeReadSQL(string section)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" SELECT[machine_id],[part_number],[mt_min_sec],[ht_min_sec] FROM[Production].[dbo].[oeeMachineTimes] ");
            query.AppendFormat("  Where[section_id] = '{0}' ", section);

            var a = query.ToString();
            try
            {
                Datatable = SqlRead(a).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2800", "MC_MahineTimeReadSQL)", ex.ToString());
            }
            return sqlstatus;
        }


        #endregion

        ///////////////////////////////// 2.7)  SQL  BRAKE TIME TABLE SETUP FORM   1x2B00   ////////////////////////////////////////////////////////////
        #region 2.7)  SQL  FORM   1x2B00
        public bool BrakeTable_LoadTableSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("Select[breakQueue],[hourNo],[startTime],[stopTime],[monitor],[period],[dayNight]FROM [dbo].[Prod_TimeBreakTable] \n");
            query.AppendFormat(" WHERE divisionID = '{0}' AND plantID = '{1}' ORDER BY breakQueue,[hourNo] \n",
                User.Division, User.Plant);
            query.AppendFormat("  select[breakQueue]FROM[dbo].[Prod_TimeBreakTable] WHERE divisionID='{0}' AND plantID='{1}' ",
                User.Division, User.Plant);
            query.AppendFormat("  GROUP BY[breakQueue] ORDER  BY breakQueue ");
            try
            {
                Dataset = SqlRead(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2B00", "BrakeTable_LoadTableSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2B00 , Message : " + ex.ToString() + " (LOOP SQL BrakeTable_LoadTableSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool BrakeTable_LoadTableSpecifySQL(string table)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("Select[breakQueue],[hourNo],[startTime],[stopTime],[monitor],[period],[dayNight], ");
            query.Append(" startTimeMonitor,stopTimeMonitor FROM [dbo].[Prod_TimeBreakTable] \n");
            query.AppendFormat(" WHERE divisionID = '{0}' AND plantID = '{1}' AND [breakQueue] = {2} ORDER BY breakQueue,[hourNo] \n",
                User.Division, User.Plant, table);
            try
            {
                var a = query.ToString();
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2B01", "BrakeTable_LoadTableSpecifySQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2B01 , Message : " + ex.ToString() + " (LOOP SQL BrakeTable_LoadTableSpecifySQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool BrakeTable_SaveSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2B02", "BrakeTable_SaveSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2B02 , Message : " + ex.ToString() + " (LOOP SQL BrakeTable_SaveSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool BrakeTable_SaveReplaceSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x2B03", "BrakeTable_SaveReplaceSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x2B03 , Message : " + ex.ToString() + " (LOOP SQL BrakeTable_SaveReplaceSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        #endregion




        ///////////////////////////////// 2.8)  SQL  FORM   1x2C00   ////////////////////////////////////////////////////////////


        ///////////////////////////////// 2.9)  SQL  FORM   1x2D00   ////////////////////////////////////////////////////////////

        ///////////////////////////////// 2.10)  SQL  FORM   1x2E00   ////////////////////////////////////////////////////////////





        //////////////////////////////// 3.0)  SQL SETUP  1x2F00  ////////////////////////////////////////////////////////////

        ///////////////////////////////// 3.1) SQL EXCLUSION / LOSS EDITOR ORM   1x3000  ////////////////////////////////////////////////////////////
        #region 3.1) SQL EXCLUSION / LOSS EDITOR FORM  1x3000

        public bool ExlcusionTimeReadSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" SELECT[Run],[ExclusionID],[ExclusionName],[Sort] FROM [dbo].[Exclusion_ItemsTable] ");
            query.AppendFormat(" WHERE [DivisionId]='{0}' and [PlantId]='{1}' order by [Sort] ", User.Division, User.Plant);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3000", "ExlcusionTimeReadSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3000 , Message : " + ex.ToString() + " (LOOP SQL ExlcusionTimeReadSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ExclusionTimeAddSQL(string id, string name, string sort)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("insert into [Production].[dbo].[Exclusion_ItemsTable] ( [ExclusionID] ,[ExclusionName],[Sort],[DivisionId],[PlantId])");
            query.AppendFormat("Values('{0}','{1}',{2},'{3}','{4}')", id, name, sort, User.Division, User.Plant);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3001", "ExcLoss_AddItemSQL)", ex.ToString());
                Console.WriteLine("Error code = 1x3001, Message : " + ex.ToString() + " (LOOP SQL ExcLoss_AddItemSQL)");
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3001 , Message : " + ex.ToString() + " (LOOP SQL ExcLoss_AddItemSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ExclusionTimeUpdateSQL(string code, string name, string sort, string run)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("Update [Production].[dbo].[Exclusion_ItemsTable] ");
            query.AppendFormat(" SET [ExclusionID]='{0}' ,[ExclusionName]='{1}',[Sort]={2} Where [run]={3}", code, name, sort, run);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3002", "ExcLoss_UpdateItemSQL)", ex.ToString());
                Console.WriteLine("Error code = 1x3002, Message : " + ex.ToString() + " (LOOP SQL ExcLoss_UpdateItemSQL)");
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3002 , Message : " + ex.ToString() + " (LOOP SQL ExcLoss_UpdateItemSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ExclusionTimeDeleteSQL(string run)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("delete FROM [Production].[dbo].[Exclusion_ItemsTable] where [Run] = '{0}' ", run);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3003", "ExcLoss_DeleteItemSQL)", ex.ToString());
                Console.WriteLine("Error code = 1x3003, Message : " + ex.ToString() + " (LOOP SQL ExcLoss_DeleteItemSQL)");
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3003 , Message : " + ex.ToString() + " (LOOP SQL ExcLoss_DeleteItemSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }



        public bool SixLossNameSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("SELECT [sixGroupId],[sixGroupName] FROM[Production].[dbo].[Loss_SixGroupTable] ORDER BY SIXGroupiD");
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3004", "ExcLoss_LoadSixLossNameSQ)", ex.ToString());
                Console.WriteLine("Error code = 1x3004, Message : " + ex.ToString() + " (LOOP SQL ExcLoss_LoadSixLossNameSQ)");
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3004 , Message : " + ex.ToString() + " (LOOP SQL ExcLoss_LoadSixLossNameSQ)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool LossTimeReadSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" SELECT i.[run],s.[sixGroupId],i.[lossId],i.[lossName],i.[sort],s.[run] ");
            query.Append(" FROM [Production].[dbo].[Loss_ItemsTable] i ");
            query.Append("LEFT JOIN[Production].[dbo].[Loss_SixRelatedTable] s  ");
            query.Append(" ON i.lossId = s.lossId and i.divisionID = s.divisionId and i.plantID = s.plantId ");
            query.AppendFormat(" WHERE i.[divisionId] = '{0}' and i.[plantId] = '{1}' ORDER BY i.[Sort] \n", User.Division, User.Plant);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3005", "ExcLoss_LoadTableSQL)", ex.ToString());
                Console.WriteLine("Error code = 1x3005, Message : " + ex.ToString() + " (LOOP SQL ExcLoss_LoadTableSQL)");
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3005 , Message : " + ex.ToString() + " (LOOP SQL ExcLoss_LoadTableSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }



        public bool LossTimeAddSQL(string id, string name, string sort, string six)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("insert into [Production].[dbo].[Loss_ItemsTable] ( [LossID] ,[LossName],[Sort],[DivisionId],[PlantId],[recordPersonID])");
            query.AppendFormat("Values('{0}','{1}',{2},'{3}','{4}',{5}) \n", id, name, sort, User.Division, User.Plant, User.ID);
            query.Append("insert into [Production].[dbo].[Loss_SixRelatedTable] ([sixGroupId],[lossId],[DivisionId],[PlantId])");
            query.AppendFormat(" Values({0},'{1}','{2}','{3}')", six, id, User.Division, User.Plant);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3006", "ExcLoss_AddItemSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true }, "Error code = 1x3006 ," +
                    " Message : " + ex.ToString() + " (LOOP SQL ExcLoss_AddItemSQL)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool LossTimeUpdateSQL(string code, string name, string sort, string six, string run, string srun)
        {
            bool sqlstatus;
            var query = new StringBuilder();

            query.AppendFormat("Delete FROM [dbo].[Loss_SixRelatedTable] WHERE [run]={0} ", srun);
            query.AppendFormat("Update [dbo].[Loss_ItemsTable]  SET [LossID]='{0}',[LossName]='{1}',[Sort]={2} Where [run]='{3}' \n",
                code, name, sort, run);
            query.Append("INSERT INTO [dbo].[Loss_SixRelatedTable]([sixGroupID],[lossID],[divisionId],[plantId]) VALUES ");
            query.AppendFormat("({0},'{1}','{2}','{3}')", six, code, User.Division, User.Plant);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3007", "ExcLoss_UpdateItemSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true }, "Error code = 1x3007 , " +
                    "Message : " + ex.ToString() + " (LOOP SQL ExcLoss_UpdateItemSQL)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool LossTimeDeleteSQL(string lrun, string srun)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("delete FROM [Production].[dbo].[Loss_SixRelatedTable] where [run]={0} \n", srun);
            query.AppendFormat("delete FROM [Production].[dbo].[Loss_ItemsTable] where [run] = {0} \n", lrun);
            //var a = query.ToString();
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3008", "LossTimeDeleteSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3008 , Message : " + ex.ToString() + " (LOOP SQL LossTimeDeleteSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        #endregion
        ///////////////////////////////// 3.2)  SQL EMPLYEE EDIT    1x3100  ////////////////////////////////////////////////////////////
        #region 3.2)  SQL ASSIGN SECTION   1x3100

        public bool EMP_LoadAllSQL(string where)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" SELECT [userID],[FullName],[Email],[Phone],[Grade],[Roles],[DivisionID],[PlantID],[run] ");
            query.AppendFormat(" FROM[Production].[dbo].[Emp_ManPowersTable] {0} ", where);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3100", "EMP_LoadAllSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3100 , Message : " + ex.ToString() + " (LOOP SQL EMP_LoadAllSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool EMP_AddEmployeeSQL(string eid, string name, string email, string phone, string g, string role, string div, string pla)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("insert into [Production].[dbo].[Emp_ManPowersTable] ");
            query.Append(" ([userID],[FullName],[Email],[Phone],[Grade],[Roles],[DivisionId],[PlantId]) Values ");
            query.AppendFormat($"({eid},'{name}','{email}','{phone}','{g}',{role},'{div}','{pla}')");
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3101", "EMP_AddEmployeeSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3101 , Message : " + ex.ToString() + " (LOOP SQL EMP_AddEmployeeSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool EMP_UpdateEmployeeSQL(string eid, string name, string email, string phone, string g, string role, string div, string pla)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("update [Production].[dbo].[Emp_ManPowersTable]");
            query.AppendFormat("SET fullName ='{0}',[email]='{1}',[phone]='{2}'", name, email, phone);
            query.AppendFormat(",[Grade]='{0}',[Roles]={1},[divisionId]='{2}',[PlantId]='{3}'", g, role, div, pla);
            query.AppendFormat("where userID={0}", eid);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3102", "EMP_UpdateEmployeeSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3102 , Message : " + ex.ToString() + " (LOOP SQL EMP_UpdateEmployeeSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool EMP_DeleteEmployeeSQL(string id)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("delete FROM [dbo].[Emp_AuthorizationTable] where [userID] = {0} \n", id);
            query.AppendFormat("delete FROM [dbo].[Software_ErrorCodeTable] where [userID]  = {0} \n", id);
            query.AppendFormat("delete FROM [dbo].[Emp_UserLoginTable] where [userID] = {0} \n", id);
            query.AppendFormat("delete FROM [dbo].[Emp_ManPowersTable] where [userID] = {0} \n", id);
            var a = query.ToString();
            try
            {
                SqlWrite(a);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3103", "EMP_DeleteEmployeeSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3103 , Message : " + ex.ToString() + " (LOOP SQL EMP_DeleteEmployeeSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        #endregion

        /////////////////////////////////3.3)  SQL ADD SECTION CODE   1x3200 ////////////////////////////////////////////////////////////
        #region 3.3)  ADD SECTION CODE    1x3200

        public bool AddSection_LoadDivisionPlantSQL()
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT [divisionID] FROM[Production].[dbo].[Emp_DivisionTable] SELECT[plantID] ");
            query.Append(" FROM[Production].[dbo].[Emp_PlantTable]");
            try
            {
                Dataset = SqlRead(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3200", "AddSection_LoadDivisionPlantSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3200 , Message : " + ex.ToString() + " (LOOP SQL AddSection_LoadDivisionPlantSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool AddSection_SaveNewSectionCodeSQL(string section, string sectionName, string div, string plant)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.AppendFormat("insert into [Production].[dbo].[Emp_SectionTable] values \n");
            query.AppendFormat("('{0}','{1}','{2}','{3}') \n", section, sectionName, div, plant);
            query.AppendFormat("insert into [Production].[dbo].[Prod_OABoardTable] values \n ");
            query.AppendFormat("('{0}',{1},{2},{3},{4}) \n", section, 0, 0, 0, 0);
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3201", "AddSection_SaveNewSectionCodeSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3201 , Message : " + ex.ToString() + " (LOOP SQL AddSection_SaveNewSectionCodeSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        public bool SectionListSQL(string where)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append(" SELECT[sectionCode] ,[sectionName] ,[divisionID], [plantID] FROM[Production].[dbo].[Emp_SectionTable] ");
            query.AppendFormat("{0}", where);
            query.Append(" order by divisionID,sectionCode");
            var a = query.ToString();
            try
            {
                Datatable = SqlRead(a).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3202", "SectionList)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3202 , Message : " + ex.ToString() + " (LOOP SQL SectionList)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        #endregion

        ///////////////////////////////// 3.4)  SQL ASSIGN SECTION   1x3300  ////////////////////////////////////////////////////////////
        #region 3.4)  SQL ASSIGN SECTION   1x3300

        public bool AssignSection_LoadSQL(string div, string plant)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT[userID],[FullName],[Grade]FROM [Production].[dbo].[Emp_ManPowersTable] ");
            query.AppendFormat(" Where[DivisionId] = '{0}' and [PlantId]='{1}' \n", div, plant);
            query.Append("SELECT [SectionCode],[SectionName] FROM [Production].[dbo].[Emp_SectionTable] ");
            query.AppendFormat(" Where[DivisionId] = '{0}'and [PlantId]='{1}' \n", div, plant);
            try
            {
                Dataset = SqlRead(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3300", "AssignSection_LoadSQ)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3300 , Message : " + ex.ToString() + " (LOOP SQL AssignSection_LoadSQ)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool AssignSection_SaveSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3301", "AssignSection_SaveSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3301 , Message : " + ex.ToString() + " (LOOP SQL AssignSection_SaveSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool AssignSection_LoadAuthorizationSQL(string userId)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT a.[SectionCode],s.[SectionName]FROM [Production].[dbo].[Emp_AuthorizationTable] a \n");
            query.AppendFormat("LEFT JOIN [Production].[dbo].[Emp_SectionTable] s ON a.SectionCode = s.SectionCode WHERE[userID] ={0} ", userId);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3302", "AssignSection_LoadAuthorizationSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3302 , Message : " + ex.ToString() + " (LOOP SQL AssignSection_LoadAuthorizationSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        #endregion

        ///////////////////////////////// 3.5)  SQL REUPDATE 1x3400  ////////////////////////////////////////////////////////////
        #region 3.5)  SQL RE-UPDATE   1x3400

        public bool ReUpdate_TimeBreakQueueTableSQL(string query)
        {
            bool sqlstatus;
            try
            {
                SqlWrite(query);
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3400", "ReUpdate_TimeBreakQueueTableSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3400 , Message : " + ex.ToString() + " (LOOP SQL ReUpdate_TimeBreakQueueTableSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ReUpdate_ListProductionSQL(string sectioncode)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("SELECT TOP (100) [run],[sectionCode],[registDateTime],[partNumber] FROM[Production].[dbo].[Prod_RecordTable] ");
            query.AppendFormat(" where sectionCode = '{0}' order by registDateTime desc", sectioncode);
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3401", "ReUpdate_ListProductionSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3401 , Message : " + ex.ToString() + " (LOOP SQL ReUpdate_ListProductionSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ReUpdate_PPASListSQL(string sectioncode, DateTime dt)
        {
            bool sqlstatus;
            var query = new StringBuilder();
            query.Append("Select [SectionCode],[RegistDate] ,[DayNight] ,[workHour] FROM[Production].[dbo].[Prod_TodayWorkTable]");
            query.AppendFormat(" Where SectionCode ='{0}' and RegistDate='{1}'\n", sectioncode, dt.ToString("yyyy-MM-dd"));
            query.Append("select [DayNight],[Hour],[Monitor],[Period],[Volume],[AccVol],[PercentOA],[StdOA] ,[PercentOAavg],[VolumePerHr] ");
            query.AppendFormat(",[RedAlarm] ,[workStatus] FROM [Production].[dbo].[Prod_PPASTable] where SectionCode='{0}' and RegistDate='{1}'",
                sectioncode, dt.ToString("yyyy-MM-dd"));
            try
            {
                Dataset = SqlRead(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3402", "ReUpdate_PPASListSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3402 , Message : " + ex.ToString() + " (LOOP SQL ReUpdate_PPASListSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }
        #endregion



        ///////////////////////////////// 4.1) SQL VOLUME CT BY MACHINE   1x3500 ////////////////////////////////////////////////////////////

        #region 


        #endregion
        ///////////////////////////////// 3.2)  SQL MACHINE STATUS  1x3600 ////////////////////////////////////////////////////////////
        ///
        #region SQL MACHINE STATUS  1x3600

        public bool ServiceMachineStatusInitalSQL(int category, int option)
        {
            bool sqlstatus;
            var query = new StringBuilder();

            if (category == 0)
            {
                if (option == -1)
                {
                    query.Append(" SELECT TOP(30) [run],[sectionCode],[registDate],[registDateTime] ,[partNumber] ,[mcTimeSec], ");
                    query.Append(" [mcNumber],[registYear] FROM [Production].[dbo].[Prod_RecordTable] \n");
                    query.AppendFormat("  Where  sectionCode='{0}' Order by run desc", User.SectionCode);
                }
            }
            else if (category == 1)
            {
                if (option == -1)
                {
                    query.Append(" SELECT TOP(30)  [run],[sectionCode],[registDate] ,[shiftAB],[lossID],[lossDetailID] , ");
                    query.Append(" [dateTimeStart] ,[dateTimeEnd],[Second],[MP] ,[totalMS] \n");
                    query.Append(" ,[partNumberStart],[partNumberEnd],[MCNumber] ,[errorCode] ,[divisionId] ,[plantId] ");
                    query.Append(" ,[mcStop] FROM [Production].[dbo].[Loss_RecordTable] \n");
                    query.AppendFormat("  Where  sectionCode='{0}' Order by run desc", User.SectionCode);
                }
            }
            else if (category == 2)
            {
                if (option == -1)
                {
                    query.Append(" SELECT TOP(30)  [run],[sectionCode],[registDate],[registDateTime],[partNumber],[mcTimeSec] ");
                    query.Append(" ,[mcNumber] FROM [Production].[dbo].[ML_RecordTable] \n");
                    query.AppendFormat("  Where  sectionCode='{0}' Order by run desc", User.SectionCode);
                }
            }
            else if (category == 3)
            {
                if (option == -1)
                {
                    query.Append(" SELECT TOP(30)  [run],[SectionCode],[RegistDate],[DayNight] ,[StationNo],[RegistDateTime], ");
                    query.Append(" [PartNumber],[NagaraSWTime]  FROM [Production].[dbo].[ML_NagaraStartTimeTable] \n");
                    query.AppendFormat("  Where  sectionCode='{0}' Order by run desc", User.SectionCode);
                }
            }

            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3600", "ServiceMachineStatusInitalSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3600 , Message : " + ex.ToString() + " (LOOP SQL ServiceMachineStatusInitalSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        public bool ServiceUnitAmountInitalSQL(int category)
        {
            bool sqlstatus;
            var query = new StringBuilder();

            if (category == 0)
            {

            }
            else if (category == 1)
            {
                query.Append(" select[mcNumber] FROM[Production].[dbo].[ML_RecordTable] ");
                query.AppendFormat(" where SectionCode = '{0}' group by[mcNumber]", User.SectionCode);
            }
            else if (category == 2)
            {
                query.Append(" select StationNo FROM[Production].[dbo].[ML_NagaraStartTimeTable] ");
                query.AppendFormat(" where SectionCode = '{0}' group by StationNo", User.SectionCode);
            }
            //var a = query.ToString();
            try
            {
                Datatable = SqlRead(query.ToString()).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3601", "ServiceUnitAmountInitalSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3601 , Message : " + ex.ToString() + " (LOOP SQL ServiceUnitAmountInitalSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        public bool ServiceUnitDeleteSQL(int category, string name)
        {
            bool sqlstatus;
            var query = new StringBuilder();

            if (category == 0)
            {

            }
            else if (category == 1)
            {
                query.Append("Delete FROM[Production].[dbo].[ML_RecordTable] ");
                query.AppendFormat(" where SectionCode = '{0}' and mcNumber = '{1}' ", User.SectionCode, name);
            }
            else if (category == 2)
            {
                query.Append(" Delete  FROM [Production].[dbo].[ML_NagaraStartTimeTable]");
                query.AppendFormat(" where SectionCode = '{0}' and StationNo = '{1}' ", User.SectionCode, name);
            }
            //var a = query.ToString();
            try
            {
                SqlWrite(query.ToString());
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3602", "ServiceUnitAmountInitalSQL)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3602 , Message : " + ex.ToString() + " (LOOP SQL ServiceUnitAmountInitalSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }





        #endregion

        ///////////////////////////////// 3.3)  SQL MACHINE LEANING   1x3700 ////////////////////////////////////////////////////////////

        #region SQL MACHINE LEANING  1x3700



        #endregion


        ///////////////////////////////// 3.4)  SQL Reupdate    1x3800 ////////////////////////////////////////////////////////////
        #region Reupdate    1x3800
        public bool ServiceSectionCode()
        {
            bool sqlstatus;
            string query = "select sectionCode  FROM[Production].[dbo].[Prod_OABoardTable]  where active = 'A' order by sectionCode desc";
            try
            {
                Datatable = SqlRead(query).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                ExcelClass.AppendErrorLogXlsx("1x3800", "ServiceSectionCode)", ex.ToString());
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3800 , Message : " + ex.ToString() + " (LOOP SQL ServiceSectionCode)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }

        public bool ReadSectionCodeNameSQL(string section)
        {
            bool sqlstatus;
            string query = $"SELECT[sectionName] FROM[Production].[dbo].[Emp_SectionTable]  where[sectionCode] = '{section}'";
            try
            {
                Datatable = SqlRead(query).Tables[0];
                sqlstatus = true;
            }
            catch (Exception ex)
            {
                sqlstatus = false;
                MessageBox.Show(new Form() { TopMost = true },
                    "Error code = 1x3801 , Message : " + ex.ToString() + " (LOOP SQL ReadSectionCodeNameSQL)",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlstatus;
        }


        #endregion


    }






    public class MemberSqlRead
    {
        public DataSet dataset = new DataSet();
        public bool status;

        public MemberSqlRead()
        {
        }
    }


    //Exception
    public class SqlConnectionException : Exception
    {
        //string name;

        public SqlConnectionException()
        {
        }

        //public SqlConnectionException(string message)
        //    : base(message)
        //{
        //    this.name = message;
        //}

        //public SqlConnectionException(string message, Exception inner) : base(message, inner)
        //{
        //}

        public override string ToString()
        {
            return "SQL open connection Fail.";
        }
    }

    public class SqlCmdInvalidException : Exception
    {
        //string name;

        public SqlCmdInvalidException()
        {
        }

        //public SqlCmdInvalidException(string message)
        //    : base(message)
        //{
        //    this.name = message;
        //}

        //public SqlCmdInvalidException(string message, Exception inner) : base(message, inner)
        //{
        //}

        public override string ToString()
        {
            return "SQL command Invalid.";
        }
    }

    public class SqlInsertException : Exception
    {
        //string name;

        public SqlInsertException()
        {
        }

        //public SqlInsertException(string message)
        //    : base(message)
        //{
        //    this.name = message;
        //}

        //public SqlInsertException(string message, Exception inner) : base(message, inner)
        //{
        //}

        public override string ToString()
        {
            return "SQL Insert fail.";
        }
    }

    public class SqlreadFailException : Exception
    {
        //string name;

        public SqlreadFailException()
        {
        }

        //public SqlreadFailException(string message)
        //    : base(message)
        //{
        //    this.name = message;
        //}

        //public SqlreadFailException(string message, Exception inner) : base(message, inner)
        //{
        //}

        public override string ToString()
        {
            return "SQL read fail.";
        }
    }
}

