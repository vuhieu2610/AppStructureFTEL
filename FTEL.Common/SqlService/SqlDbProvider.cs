using System.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FTEL.Common.Utilities;

namespace FTEL.Common.SqlService
{
    /// <summary>
    /// Summary description for OracleDatabaseProvider 
    /// </summary>
    public class SqlDbProvider : DbProvider
    {
        private static SqlDbProvider _SqlDbProvider;

        public static SqlDbProvider GetInstance()
        {
            if (_SqlDbProvider == null)
                _SqlDbProvider = new SqlDbProvider();
            return _SqlDbProvider;
        }

        #region CREATE NEW OBJECT
        public SqlDbProvider()
        {
            connectStr = ConfigUtil.ConnectionString;
            SqlConnection conn = new SqlConnection(connectStr);
            this.dbCommand = conn.CreateCommand();
            this.dataAdapter = new SqlDataAdapter();
            this.InTransaction = false;
        }
        public SqlDbProvider(String connectionString)
        {
            connectStr = connectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            this.dbCommand = conn.CreateCommand();
            this.dataAdapter = new SqlDataAdapter();
            this.InTransaction = false;
        }
        ////public SqlDbProvider(  String iUName, String iLogCode):this()
        ////{
        ////    vsUName = iUName;
        ////    vLogcode = iLogCode;
        ////}
        ////public SqlDbProvider(String connectionString, String iUName, String iLogCode)  
        ////{
        ////    connectStr = connectionString;
        ////    vsUName = iUName;
        ////    vLogcode = iLogCode;
        ////    SqlConnection conn = new SqlConnection(connectionString);
        ////    this.dbCommand = conn.CreateCommand();
        ////    this.dataAdapter = new SqlDataAdapter();
        ////    this.InTransaction = false;
        ////}
        public SqlDbProvider(SqlConnection connection)
        {
            connectStr = connection.ConnectionString;
            this.dbCommand = connection.CreateCommand();
            this.dataAdapter = new SqlDataAdapter();
            this.InTransaction = false;
        }
        public SqlDbProvider(String connectionString, bool handleErrors)
        {
            connectStr = connectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            this.dbCommand = conn.CreateCommand();
            this.dbCommand.Connection = conn;
            this.dataAdapter = new SqlDataAdapter();
            this.InTransaction = false;
        }
        #endregion

        #region OverRide Properties
        private string connectStr;
        public SqlCommand Command
        {
            get { return dbCommand; }
        }
        private SqlCommand dbCommand;
        protected override IDbCommand DbCommand
        {
            get { return dbCommand; }
        }
        private SqlDataAdapter dataAdapter;
        protected override IDbDataAdapter DataAdapter
        {
            get { return dataAdapter; }
        }

        #endregion

        #region PARAMETER
        /// <summary>
        /// Add Parameter into command with name, value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns>SqlParameter</returns>
        public new SqlParameter AddParameter(string name, object value)
        {
            return base.AddParameter(name, value) as SqlParameter;
        }
        /// <summary>
        /// Add Parameter into command with name, value, type and direction
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        /// <returns>SqlParameter</returns>
        public SqlParameter AddParameter(string name, object value, SqlDbType SqlDbType, ParameterDirection parameterDirection)
        {
            SqlParameter para = new SqlParameter(name, value)
            {
                SqlDbType = SqlDbType,
                Direction = parameterDirection
            };
            dbCommand.Parameters.Add(para);
            return para;
        }
        /// <summary>
        /// Add Parameter into command with name, value, type and direction
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="Size"></param>
        /// <param name="direction"></param>
        /// <returns>SqlParameter</returns>
        public SqlParameter AddParameter(string name, object value, SqlDbType SqlDbType, int parameterSize, ParameterDirection parameterDirection)
        {
            SqlParameter para = new SqlParameter(name, value)
            {
                SqlDbType = SqlDbType,
                Direction = parameterDirection,
                Size = parameterSize
            };
            dbCommand.Parameters.Add(para);
            return para;
        }
        /// <summary>
        /// Add Parameter into command with name, value, type and direction
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        /// <returns>SqlParameter</returns>
        public SqlParameter AddParameter(string name, object value, SqlDbType SqlDbType)
        {
            SqlParameter para = new SqlParameter(name, value)
            {
                SqlDbType = SqlDbType
            };
            dbCommand.Parameters.Add(para);
            return para;
        }
        /// <summary>
        /// Add Parameter into command
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Parameter</returns>
        public SqlParameter AddParameter(SqlParameter parameter)
        {
            return base.AddParameter(parameter) as SqlParameter;
        }
        /// <summary>
        /// Add Parameter into command
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Parameter</returns>
        private new IDbDataParameter AddParameter(IDbDataParameter parameter)
        {
            return base.AddParameter(parameter);
        }

        /////// <summary> Add Parameter into command with name, value. Using only for Imange </summary>
        ////public SqlParameter AddParameter_Image(string name, Byte[] iValue)
        ////{
        ////    SqlParameter imageParameter = new SqlParameter(name, SqlDbType.Image);
        ////    if (iValue == null)
        ////        imageParameter.Value = DBNull.Value;
        ////    else
        ////        imageParameter.Value = iValue;
        ////    Command.Parameters.Add(imageParameter);
        ////    return imageParameter;
        ////}
        #endregion

        #region ExecuteDataTable OverLoad
        /// <summary>
        /// Executes the query, and returns DataTable
        /// </summary>
        /// <returns></returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public DataTable ExecuteDataTable()
        {
            DataTable dta = new DataTable();
            try
            {
                dataAdapter.SelectCommand = dbCommand;
                dataAdapter.Fill(dta);

            }
            catch (Exception)
            {
                if (this.InTransaction)
                {
                    this.RollBack();
                }
                throw;
            }
            return dta;
        }

        /// <summary>
        /// Executes the query, and returns DataTable
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <param name="parameters">specified ParameterCollection </param>
        /// <returns></returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public DataTable ExecuteDataTable(string commandText, CommandType type, SqlParameterCollection parameters)
        {
            DataTable dta = new DataTable();
            try
            {
                this.SetCommandText(commandText, type);
                foreach (SqlParameter para in parameters)
                {
                    dbCommand.Parameters.Add(para);
                }
                dataAdapter.SelectCommand = dbCommand;
                dataAdapter.Fill(dta);

            }
            catch (Exception)
            {
                if (this.InTransaction)
                {
                    this.RollBack();
                }
                throw;
            }
            return dta;
        }

        /// <summary>
        /// Executes the query, and returns DataTable
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string commandText, CommandType type)
        {
            DataTable dta = new DataTable();
            try
            {
                dbCommand.CommandText = commandText;
                dbCommand.CommandType = type;
                dataAdapter.SelectCommand = dbCommand;
                dataAdapter.Fill(dta);

            }
            catch (Exception)
            {
                if (this.InTransaction)
                {
                    this.RollBack();
                }
                throw;
            }
            return dta;
        }

        /// <summary>
        /// Executes the query, and returns DataTable
        /// </summary>
        public long ExecuteDataTable(DataTable idt_table)
        {
            try
            {
                dataAdapter.SelectCommand = dbCommand;
                dataAdapter.Fill(idt_table);
                return idt_table.Rows.Count;
            }
            catch (Exception ex)
            {
                if (this.InTransaction)
                {
                    this.RollBack();
                }
                throw ex;
            }
            //return dta;
        }

        #endregion

        #region Fill OverLoad
        /// <summary>
        /// Adds or refreshes rows in the DataTable 
        /// </summary>
        /// <param name="table">The DataTable to use for table mapping.</param>
        /// <returns>The number of rows successfully added to or refreshed in the DataTable</returns>
        public long Fill(DataTable table)
        {
            int kq = 0;
            try
            {
                dataAdapter.SelectCommand = dbCommand;
                kq = dataAdapter.Fill(table);
            }
            catch (Exception)
            {
                if (this.InTransaction)
                {
                    this.RollBack();
                }
                throw;
            }
            return kq;
        }

        /// <summary>
        /// Adds or refreshes rows in DataSet to match those in the data source using the DataSet and DataTable names
        /// </summary>
        /// <param name="dataSet">A DataSet to fill with records and, if necessary, schema</param>
        /// <param name="srcTable">The name of the source table to use for table mapping</param>
        /// <returns>The number of rows successfully added to or refreshed in the DataSet</returns>
        public int Fill(DataSet dataSet, string srcTable)
        {
            int kq = 0;
            try
            {
                dataAdapter.SelectCommand = dbCommand;
                kq = dataAdapter.Fill(dataSet, srcTable);
            }
            catch (Exception)
            {
                if (this.InTransaction)
                {
                    this.RollBack();
                }
                throw;
            }
            return kq;
        }

        /// <summary>
        /// Adds or refreshes rows in a specified range in the DataSet to match those in the data source using the DataSet and DataTable names
        /// </summary>
        /// <param name="dataSet">A DataSet to fill with records and, if necessary, schema</param>
        /// <param name="startRecord">The zero-based record number to start with. </param>
        /// <param name="maxRecord">The maximum number of records to retrieve</param>
        /// <param name="srcTable">The name of the source table to use for table mapping</param>
        /// <returns>The number of rows successfully added to or refreshed in the DataSet</returns>
        public int Fill(DataSet dataSet, int startRecord, int maxRecord, string srcTable)
        {
            int kq = 0;
            try
            {
                dataAdapter.SelectCommand = dbCommand;
                kq = dataAdapter.Fill(dataSet, startRecord, maxRecord, srcTable);
            }
            catch (Exception)
            {
                if (this.InTransaction)
                {
                    this.RollBack();
                }
                throw;
            }
            return kq;
        }
        #endregion

        #region ExecuteReader OverLoad

        /// <summary>
        /// Execute Command 
        /// </summary>
        /// <returns>Return one Reader. Should close this reader when not use</returns>
        public new SqlDataReader ExecuteReader()
        {
            return base.ExecuteReader() as SqlDataReader;
        }

        /// <summary>
        /// Execute Command with specified CommandText
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <returns>Return one Reader. Should close this reader when not use</returns>
        public new SqlDataReader ExecuteReader(string commandText, CommandType type)
        {
            return base.ExecuteReader(commandText, type) as SqlDataReader;
        }

        /// <summary>
        /// Execute Command with specified CommandText and Parameters
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <param name="parameters">specified ParameterCollection </param>
        /// <returns>Return one Reader. Should close this reader when not use</returns>
        public SqlDataReader ExecuteReader(string commandText, CommandType type, SqlParameterCollection parameters)
        {
            return base.ExecuteReader(commandText, type, parameters) as SqlDataReader;
        }

        #endregion

        public static void FillReaderToObject(SqlDataReader reader, object obj)
        {
            int fieldCount = reader.FieldCount;
            Type type = obj.GetType();
            System.Reflection.PropertyInfo[] lstpro = type.GetProperties();
            Dictionary<string, System.Reflection.PropertyInfo> listProperties = new Dictionary<string, System.Reflection.PropertyInfo>();
            for (int i = 0; i < lstpro.Length; i++)
            {
                string name = "";
                object[] lstAtt = lstpro[i].GetCustomAttributes(typeof(DataFieldAttribute), true);
                if (lstAtt.Length > 0)
                {
                    name = ((DataFieldAttribute)lstAtt[0]).DataFieldName.ToLower();///[vantienpros]:Cho phép set giá trị vào property không phân biệt chữ hoa, chữ thường
				}
                else
                {
                    name = lstpro[i].Name.ToLower();
                }
                listProperties[name] = lstpro[i];
            }
            for (int i = 0; i < fieldCount; i++)
            {
                try
                {
                    string nameColumn = reader.GetName(i).ToLower();///[vantienpros]:Cho phép set giá trị vào property không phân biệt chữ hoa, chữ thường
				    if (listProperties.ContainsKey(nameColumn))
                    {
                        System.Reflection.PropertyInfo property = listProperties[nameColumn];
                        if (property.CanWrite)
                        {
                            property.SetValue(obj, reader[i], null);
                        }
                    }
                }
                catch { }
            }
        }

        public static object GetObjectFormReader(SqlDataReader reader, Type typeObject)
        {
            object obj = typeObject.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
            FillReaderToObject(reader, obj);
            return obj;
        }
        public TTarget GetObjectFromMyReader<TTarget>() where TTarget : new()
        {
            //IDataReader reader = null;
            //reader = this.ExecuteReader();
            TTarget itemReturn = new TTarget();
            if (vMyReader == null) return default(TTarget);
            while (vMyReader.Read())
            {
                TTarget obj = new TTarget();
                for (int i = 0; i < vMyReader.FieldCount; i++)
                {
                    if (vMyReader.GetValue(i) != DBNull.Value)
                    {
                        DataMapper.SetPropertyValue(obj, vMyReader.GetName(i), vMyReader.GetValue(i));
                    }
                }
                return obj;
            }
            return default(TTarget);

        }
        public object ExecuteScalar_FromMyReader()
        {
            if (vMyReader == null) return null;
            while (vMyReader.Read())
            {
                if (vMyReader.FieldCount == 0) return null;
                return vMyReader.GetValue(0);
            }
            return null;
        }

        public void Fill<T>(ICollection<T> listKq)
        {
            SqlDataReader reader = null;
            try
            {
                this.Open();
                reader = this.Command.ExecuteReader();
                while (reader.Read())
                {

                    listKq.Add((T)GetObjectFormReader(reader, typeof(T)));
                }
            }
            catch (Exception)
            {
                if (this.InTransaction)
                {
                    this.RollBack();
                }
                throw;
            }
            finally
            {
                if (reader != null) reader.Close();
                if (!this.InTransaction) this.Close();
            }
        }



        public void WriteAction_ByListActions(string iUsername = "", string iTokenCode = "", PermissionType? iAction = null, Int32? iIN_FUNCT = null, Int64? iOBID = null)
        {
            try
            {
                if (ListActioned == null || ListActioned.Count == 0) return;
                SqlCommand cmd = new SqlCommand
                {
                    Connection = new SqlConnection(connectStr)
                };
                cmd.Connection.Open();
                cmd.CommandText = "sp_V2_LOG_ACTIONS_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (WriteActionInfo ptu in ListActioned)
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        IDbDataParameter paraIN_FUNCT = cmd.CreateParameter();
                        paraIN_FUNCT.ParameterName = "IN_FUNCT";
                        if (iIN_FUNCT == null)
                            paraIN_FUNCT.Value = ptu.FUNCT;
                        else
                            paraIN_FUNCT.Value = iIN_FUNCT;
                        cmd.Parameters.Add(paraIN_FUNCT);

                        IDbDataParameter paraIN_ACTIONTYPE = cmd.CreateParameter();
                        paraIN_ACTIONTYPE.ParameterName = "IN_ACTIONTYPE";
                        if (iAction == null)
                            paraIN_ACTIONTYPE.Value = ptu.ACTIONTYPE;
                        else
                            paraIN_ACTIONTYPE.Value = (Int32)iAction;
                        cmd.Parameters.Add(paraIN_ACTIONTYPE);

                        IDbDataParameter paraIN_OBCODE = cmd.CreateParameter();
                        paraIN_OBCODE.ParameterName = "IN_OBCODE";
                        paraIN_OBCODE.Value = ptu.OBCODE;
                        cmd.Parameters.Add(paraIN_OBCODE);

                        IDbDataParameter paraIN_OBID = cmd.CreateParameter();
                        paraIN_OBID.ParameterName = "IN_OBID";
                        if (iOBID != null)
                            paraIN_OBID.Value = iOBID;
                        else
                            paraIN_OBID.Value = ptu.OBID;
                        cmd.Parameters.Add(paraIN_OBID);

                        IDbDataParameter paraIN_CREATE_BY = cmd.CreateParameter();
                        paraIN_CREATE_BY.ParameterName = "IN_CREATE_BY";
                        paraIN_CREATE_BY.Value = iUsername;
                        cmd.Parameters.Add(paraIN_CREATE_BY);

                        IDbDataParameter paraIN_QUERYSTR = cmd.CreateParameter();
                        paraIN_QUERYSTR.ParameterName = "IN_QUERYSTR";
                        paraIN_QUERYSTR.Value = ptu.QRY.ToString().TrimEnd(',');
                        cmd.Parameters.Add(paraIN_QUERYSTR);

                        IDbDataParameter paraIN_TOKENID = cmd.CreateParameter();
                        paraIN_TOKENID.ParameterName = "IN_TOKENID";
                        paraIN_TOKENID.Value = iTokenCode;
                        cmd.Parameters.Add(paraIN_TOKENID);

                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                    }
                }
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
            }
            catch (Exception ex) {
                //Console.WriteLine(ex.ToString());
            }
            finally
            {
                ListActioned = new List<WriteActionInfo>();
            }
        }

        public object Where(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ghi log hành động của người quản trị hệ thống
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="menu">Menu thực hiện</param>
        /// <param name="operation">Hành động</param>
        /// <param name="logDetails">Chi tiết log</param>
        /// <param name="logIP">Ip log của người dùng</param>
        /// <param name="result">Kết quả của hành động</param>
        public void WriteLogAdminAction(
            string userName, 
            string menu, 
            string operation, 
            string logDetails, 
            string logIP,
            bool result,
            int type)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = new SqlConnection(connectStr);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "sp_V2_LogAdminActions_AddNew";
                cmd.CommandType = CommandType.StoredProcedure;

                // Tạo params
                IDbDataParameter paramUserName = cmd.CreateParameter();
                paramUserName.ParameterName = "UserName";
                paramUserName.Value = userName;
                cmd.Parameters.Add(paramUserName);

                IDbDataParameter paramOperation = cmd.CreateParameter();
                paramOperation.ParameterName = "Operation";
                paramOperation.Value = operation;
                cmd.Parameters.Add(paramOperation);

                IDbDataParameter paramLogDetails = cmd.CreateParameter();
                paramLogDetails.ParameterName = "LogDetails";
                paramLogDetails.Value = logDetails;
                cmd.Parameters.Add(paramLogDetails);

                IDbDataParameter paramLogIP = cmd.CreateParameter();
                paramLogIP.ParameterName = "LogIP";
                paramLogIP.Value = logIP;
                cmd.Parameters.Add(paramLogIP);

                IDbDataParameter paramMenu = cmd.CreateParameter();
                paramMenu.ParameterName = "Menu";
                paramMenu.Value = menu;
                cmd.Parameters.Add(paramMenu);

                IDbDataParameter paramResult = cmd.CreateParameter();
                paramResult.ParameterName = "Result";
                paramResult.Value = result;
                cmd.Parameters.Add(paramResult);

                IDbDataParameter paramType = cmd.CreateParameter();
                paramType.ParameterName = "Type";
                paramType.Value = type;
                cmd.Parameters.Add(paramType);

                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.Message, ex);
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
        }
    }


}