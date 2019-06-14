using System.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Configuration;
using Newtonsoft.Json;
using FTEL.Common.Utilities;

namespace FTEL.Common.SqlService
{
    public class MyLogObjectDetail
    {
        /// <summary>
        /// Parameter Name
        /// </summary>
        public string prn { get; set; }
        /// <summary>
        /// Parameter Value
        /// </summary>
        public string pvl { get; set; }
    }
    public class MyLogObject
    {
        public string cmd { get; set; }
        public List<MyLogObjectDetail> lst { get; set; }
    }
    public class WriteActionInfo
    {
        public System.Text.StringBuilder QRY { get; set; }
        public Int32 FUNCT { get; set; }
        public PermissionType ACTIONTYPE { get; set; }
        public String OBCODE { get; set; }
        public long OBID { get; set; }
        public WriteActionInfo()
        {
            QRY = new System.Text.StringBuilder();
            OBCODE = string.Empty;
            ACTIONTYPE = PermissionType.NoAction;
            //--------------------------
        }
        public WriteActionInfo(string iQry) : this()
        {
            QRY.Append(iQry);
        }
        public WriteActionInfo(string iQry, Int32 iFunct, PermissionType iActionType) : this()
        {
            QRY.Append(iQry);
            FUNCT = iFunct;
            ACTIONTYPE = iActionType;
        }
        public WriteActionInfo(string iQry, Int32 iFunct, PermissionType iActionType, string iOBCODE, long iOBID) : this()
        {
            QRY.Append(iQry);
            FUNCT = iFunct;
            ACTIONTYPE = iActionType;
            OBCODE = iOBCODE;
            OBID = iOBID;
        }
    }


    public abstract class DbProvider : IDisposable
    {

        //internal string vLogcode = "";
        //internal string vsUName = "NotSetUName";

        public static string GetConfigurationValueFromKey(string keyName)
        {
            try
            {
                return ConfigurationManager.AppSettings[keyName];
            }
            catch (Exception)
            {
                // ignored
                return string.Empty;
            }
        }


        protected abstract IDbCommand DbCommand
        {
            get;
        }

        protected abstract IDbDataAdapter DataAdapter
        {
            get;
        }

        #region Properties

        /// <summary>
        /// True if this in transaction
        /// </summary>
        protected bool InTransaction { get; set; } = false;

        /// <summary>
        /// Indicates or specifies how the DbCommand.CommandText property
        /// is interpreted.
        /// </summary>
        public CommandType CommandType
        {
            set { DbCommand.CommandType = value; }
            get { return DbCommand.CommandType; }
        }

        /// <summary>
        /// CommandText của DB
        /// </summary>
        public string CommandText
        {
            set { DbCommand.CommandText = value; }
            get { return DbCommand.CommandText; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set Command Text and Command Type
        /// And Clear All Parameter
        /// </summary>
        /// <param name="commandText">The Transact-SQL statement, table name or stored procedure to execute at the data source</param>
        /// <param name="type">A value indicating how the commandText property is to be interpreted</param>
        public virtual void SetCommandText(string commandText, CommandType type)
        {
            DbCommand.CommandText = commandText;
            DbCommand.CommandType = type;
            DbCommand.Parameters.Clear();
        }
        /// <summary>
        /// Set Command Text and Command Type. Auto add Schema from WebConfig (name = cfgSCHEMA)
        /// And Clear All Parameter
        /// </summary>
        /// <param name="commandText">The Transact-SQL statement, table name or stored procedure to execute at the data source</param>
        /// <param name="type">A value indicating how the commandText property is to be interpreted</param>
        public virtual void SetCommandText2(string commandText, CommandType type)
        {
            DbCommand.CommandText = (type == CommandType.StoredProcedure ? commandText : commandText);
            DbCommand.CommandType = type;
            DbCommand.Parameters.Clear();
        }
        #endregion

        #region Parameter
        /// <summary>
        /// Add Parameter into command with name, value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns>Parameter</returns>
        public virtual IDbDataParameter AddParameter(string name, object value)
        {
            IDbDataParameter para = DbCommand.CreateParameter();
            para.ParameterName = name;
            para.Value = value;
            DbCommand.Parameters.Add(para);
            return para;
        }

        /// <summary>
        /// Add Parameter into command with name, value, type and direction
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        /// <returns>Parameter</returns>
        public virtual IDbDataParameter AddParameter(string name, object value, DbType type, ParameterDirection direction)
        {
            IDbDataParameter para = DbCommand.CreateParameter();
            para.ParameterName = name;
            para.Value = value;
            para.DbType = type;
            para.Direction = direction;
            DbCommand.Parameters.Add(para);
            return para;
        }

        /// <summary>
        /// Add Parameter into command
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Parameter</returns>
        public IDbDataParameter AddParameter(IDbDataParameter parameter)
        {
            DbCommand.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// Clear Parameter Collection
        /// </summary>
        public void ClearParameter()
        {
            DbCommand.Parameters.Clear();
        }
        #endregion

        #region Connection and Dispose Object
        /// <summary>
        /// Open Connection
        /// </summary>
        /// <exception cref="Exception If Open error"></exception>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Open()
        {
            if (this.InTransaction)
            {
                return;
            }
            if (DbCommand.Connection.State != ConnectionState.Open)
            {
                DbCommand.Connection.Open();
                //isOpen = true;
            }

        }

        /// <summary>
        /// Close Connection
        /// </summary>
        public void Close()
        {
            this.InTransaction = false;
            DbCommand.Connection.Close();
            //isOpen = false;
        }

        /// <summary>
        /// Dispose Object DatabaseProvider
        /// </summary>
        public void Dispose()
        {
            this.Close();
            DbCommand.Connection.Dispose();
            DbCommand.Dispose();
        }
        #endregion

        #region ExecuteReader Overload
        /// <summary>
        /// Execute Command 
        /// </summary>
        /// <returns>Return one Reader. Should close this reader when not use</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual IDataReader ExecuteReader()
        {
            IDataReader reader = null;
            try
            {
                this.Open();
                reader = DbCommand.ExecuteReader();
            }
            catch (Exception)
            {
                if (this.InTransaction)
                {
                    this.RollBack();
                }
                this.Close();
                throw;
            }
            return reader;
        }

        /// <summary>
        /// Execute Command with specified CommandText
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <returns>Return one Reader. Should close this reader when not use</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual IDataReader ExecuteReader(string commandText, CommandType type)
        {
            IDataReader reader = null;
            try
            {
                this.Open();
                DbCommand.CommandText = commandText;
                DbCommand.CommandType = type;
                reader = DbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                if (this.InTransaction)
                {
                    this.RollBack();
                }
                this.Close();
                throw;
            }

            return reader;
        }

        /// <summary>
        /// Execute Command with specified CommandText and Parameters
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <param name="parameters">specified ParameterCollection </param>
        /// <returns>Return one Reader. Should close this reader when not use</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual IDataReader ExecuteReader(string commandText, CommandType type, IDataParameterCollection parameters)
        {
            IDataReader reader = null;
            try
            {
                this.Open();
                DbCommand.CommandText = commandText;
                DbCommand.CommandType = type;
                DbCommand.Parameters.Clear();
                foreach (IDbDataParameter para in parameters)
                {
                    DbCommand.Parameters.Add(para);
                }
                reader = DbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                if (this.InTransaction)
                {
                    this.RollBack();
                }
                this.Close();
                throw;
            }

            return reader;
        }
        /// <summary>
        /// Execute Command with specified CommandText and Parameters
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <param name="parameters">specified ParameterCollection </param>
        /// <returns>Return one Reader. Should close this reader when not use</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual IDataReader ExecuteReader(string commandText, CommandType type, params IDbDataParameter[] parameters)
        {
            IDataReader reader = null;
            try
            {
                this.Open();
                DbCommand.CommandText = commandText;
                DbCommand.CommandType = type;
                DbCommand.Parameters.Clear();
                foreach (IDbDataParameter para in parameters)
                {
                    DbCommand.Parameters.Add(para);
                }
                reader = DbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                if (this.InTransaction)
                {
                    this.RollBack();
                }
                this.Close();
                throw;
            }

            return reader;
        }

        public List<TTarget> ExecuteReader<TTarget>(IDataReader reader) where TTarget : new()
        {
            //IDataReader reader = null;
            //reader = this.ExecuteReader();
            List<TTarget> list = new List<TTarget>();
            if (reader == null) return list;
            while (reader.Read())
            {
                TTarget obj = new TTarget();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetValue(i) != DBNull.Value)
                    {
                        DataMapper.SetPropertyValue(obj, reader.GetName(i), reader.GetValue(i));
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        public IDataReader vMyReader;
        public void ExecuteReader_ToMyReader()
        {
            vMyReader = ExecuteReader();
        }
        public void ExecuteReader_NextResult()
        {
            vMyReader.NextResult();
        }
        public void ExecuteReader_Close()
        {
            vMyReader.Close();
        }
        public List<TTarget> ExecuteReader_frmMyReader<TTarget>() where TTarget : new()
        {
            //IDataReader reader = null;
            //reader = this.ExecuteReader();
            List<TTarget> list = new List<TTarget>();
            if (vMyReader == null) return list;
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
                list.Add(obj);
            }
            return list;
        }

        #endregion

        /// <summary>
        /// Execute Command with specified CommandText and Parameters
        /// </summary>
        /// <typeparam name="TTarget">Tên object map với data</typeparam>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <param name="parameters">specified ParameterCollection </param>
        /// <returns>Return List Object.</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public IList<TTarget> ExecuteListObject<TTarget>(string commandText, CommandType type, params IDbDataParameter[] parameters) where TTarget : new()
        {
            IDataReader reader = null;
            reader = this.ExecuteReader(commandText, type, parameters);
            IList<TTarget> list = new List<TTarget>();
            if (reader == null) return list;
            try
            {
                //ValidateMappings<TTarget>(reader);
                while (reader.Read())
                {
                    TTarget obj = new TTarget();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetValue(i) != DBNull.Value)
                        {
                            DataMapper.SetPropertyValue(obj, reader.GetName(i), reader.GetValue(i));
                        }
                    }
                    list.Add(obj);
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
                if (!this.InTransaction)
                {
                    this.Close();
                }
                else
                {
                    reader.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// Execute Command with specified CommandText and Parameters
        /// </summary>
        /// <typeparam name="TTarget">Tên object map với data</typeparam>
        /// <returns>Return List Object.</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Dictionary<TKey, TTarget> ExecuteDictionaryObject<TKey, TTarget>(string keyField) where TTarget : new()
        {
            IDataReader reader = null;
            reader = this.ExecuteReader();
            Dictionary<TKey, TTarget> list = new Dictionary<TKey, TTarget>();
            if (reader == null) return list;
            try
            {
                //ValidateMappings<TTarget>(reader);
                while (reader.Read())
                {
                    TTarget obj = new TTarget();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetValue(i) != DBNull.Value)
                        {
                            DataMapper.SetPropertyValue(obj, reader.GetName(i), reader.GetValue(i));
                        }
                    }
                    TKey key = (TKey)Convert.ChangeType(reader[keyField], typeof(TKey));
                    list.Add(key, obj);
                }
                reader.Close();
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
                if (!this.InTransaction)
                {
                    this.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// Execute Command with specified CommandText and Parameters
        /// </summary>
        /// <returns>Return Dictionary Object.</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Dictionary<TKey, TTarget> ExecuteDictionaryObject<TKey, TTarget>()
            where TTarget : new()
            where TKey : new()
        {
            IDataReader reader = null;
            reader = this.ExecuteReader();
            Dictionary<TKey, TTarget> list = new Dictionary<TKey, TTarget>();
            if (reader == null)
                return list;
            try
            {
                //ValidateMappings<TTarget>(reader);
                while (reader.Read())
                {
                    TTarget obj = new TTarget();
                    TKey key = new TKey();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetValue(i) != DBNull.Value)
                        {
                            DataMapper.SetPropertyValue(obj, reader.GetName(i), reader.GetValue(i));
                            DataMapper.SetPropertyValue(key, reader.GetName(i), reader.GetValue(i));
                        }
                    }
                    list.Add(key, obj);
                }
                reader.Close();
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
                if (!this.InTransaction)
                {
                    this.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// Execute Command with specified CommandText and Parameters
        /// </summary>
        /// <returns>Return Dictionary Object.</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Dictionary<string, TTarget> ExecuteDictionaryObjectByStringKey<TTarget>(params string[] keyFields) where TTarget : new()
        {
            IDataReader reader = null;
            reader = this.ExecuteReader();
            Dictionary<string, TTarget> list = new Dictionary<string, TTarget>();
            if (reader == null)
                return list;
            try
            {
                Dictionary<string, int> listField = new Dictionary<string, int>();
                while (reader.Read())
                {
                    TTarget obj = new TTarget();
                    string key = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        listField[reader.GetName(i)] = i;
                        if (reader.GetValue(i) != DBNull.Value)
                        {
                            DataMapper.SetPropertyValue(obj, reader.GetName(i), reader.GetValue(i));
                            DataMapper.SetPropertyValue(key, reader.GetName(i), reader.GetValue(i));
                        }
                    }
                    for (var i = 0; i < keyFields.Length; i++)
                    {
                        string keyField = keyFields[i];
                        if (listField.ContainsKey(keyField))
                        {
                            if (key != "")
                            {
                                key += "-";
                            }
                            key += reader[listField[keyField]];
                        }
                        else
                        {
                            reader.Close();
                            throw new IndexOutOfRangeException(string.Format("Key Field '{0}' not found", keyField));
                        }
                    }
                    if (list.ContainsKey(key))
                    {
                        reader.Close();
                        throw new DuplicateNameException(string.Format("Duplicate value key '{0}'", key));
                    }
                    list.Add(key, obj);
                }
                reader.Close();
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
                if (!this.InTransaction)
                {
                    this.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// Execute Command Fill to IDictionary. Bỏ qua trùng
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="list"></param>
        /// <param name="keyField"></param>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void FillDictionaryObject<TKey, TTarget>(IDictionary<TKey, TTarget> list, string keyField) where TTarget : new()
        {
            IDataReader reader = null;
            reader = this.ExecuteReader();
            if (reader == null)
                return;
            try
            {
                //ValidateMappings<TTarget>(reader);
                while (reader.Read())
                {
                    TKey key = (TKey)Convert.ChangeType(reader[keyField], typeof(TKey));
                    if (list.ContainsKey(key))
                        continue;
                    TTarget obj = new TTarget();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetValue(i) != DBNull.Value)
                        {
                            DataMapper.SetPropertyValue(obj, reader.GetName(i), reader.GetValue(i));
                        }
                    }

                    list.Add(key, obj);
                }
                reader.Close();
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
                if (!this.InTransaction)
                {
                    this.Close();
                }
            }
        }


        /// <summary>
        /// Execute Command with specified CommandText and Parameters
        /// </summary>
        /// <typeparam name="TTarget">Tên object map với data</typeparam>
        /// <returns>Return List Object.</returns>
        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public List<TTarget> ExecuteListObject<TTarget>() where TTarget : new()
        {
            IDataReader reader = null;
            reader = this.ExecuteReader();
            List<TTarget> list = new List<TTarget>();
            if (reader == null) return list;
            try
            {
                TTarget tmp = new TTarget();
                List<PropertyInfo> mpropertyInfos = tmp.GetType().GetProperties().ToList();
                //ValidateMappings<TTarget>(reader);
                while (reader.Read())
                {
                    TTarget obj = new TTarget();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetValue(i) != DBNull.Value)
                        {
                            ////DataMapper.SetPropertyValue(obj, reader.GetName(i), reader.GetValue(i));
                            foreach (PropertyInfo prt in mpropertyInfos.FindAll(c => c.CanWrite && c.Name.ToLower() == reader.GetName(i).ToLower()))
                            {
                                Type pType = DataMapper.GetPropertyType(prt.PropertyType);
                                Type vType = DataMapper.GetPropertyType(reader.GetValue(i).GetType());
                                if (pType.Equals(vType))
                                {
                                    // types match, just copy value
                                    prt.SetValue(obj, reader.GetValue(i), null);
                                }
                                else
                                {
                                    // types don't match, try to coerce
                                    if (pType.Equals(typeof(Guid)))
                                        prt.SetValue(obj, new Guid(reader.GetValue(i).ToString()), null);
                                    else if (pType.IsEnum && vType.Equals(typeof(string)))
                                        prt.SetValue(obj, Enum.Parse(pType, reader.GetValue(i).ToString()), null);
                                    else if (pType.Equals(typeof(string)))
                                        prt.SetValue(obj, Convert.ToString(reader.GetValue(i)), null);
                                    else
                                        prt.SetValue(obj, Convert.ChangeType(reader.GetValue(i), pType), null);
                                }
                            }
                        }
                    }
                    list.Add(obj);
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
                reader.Close();
                if (!this.InTransaction)
                {
                    this.Close();
                }
            }
            return list;
        }
        /// <summary>
        /// Execute Command with specified CommandText and Parameters
        /// </summary>
        /// <typeparam name="TTarget">Tên class cơ bản Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, Single and string</typeparam>
        /// <returns>Return List Primitive.</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public List<TTarget> ExecuteList<TTarget>(string fieldName)
        {
            if (!(typeof(TTarget).IsPrimitive || typeof(TTarget).Equals(typeof(string)) || typeof(TTarget).Equals(typeof(Guid))))
            {
                throw new Exception("ExecuteList only use with TTarget is Primitive ( Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, Single, Guid and String)");
            }

            IDataReader reader = null;
            reader = this.ExecuteReader();
            List<TTarget> list = new List<TTarget>();
            if (reader == null) return list;
            try
            {
                int index = -1;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (fieldName.ToLower() == reader.GetName(i).ToLower())
                    {
                        index = i; break;
                    }
                }
                if (index == -1)
                {
                    throw new Exception(string.Format("FieldName '{0}' can not find", fieldName));
                }
                while (reader.Read())
                {
                    if (reader.IsDBNull(index))
                    {
                        list.Add(default(TTarget));
                    }
                    else
                    {
                        list.Add((TTarget)Convert.ChangeType(reader[index], typeof(TTarget)));
                    }
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
                reader.Close();
                if (!this.InTransaction)
                {
                    this.Close();
                }
            }
            return list;
        }
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public List<TTarget> ExecuteList_FromMyReader<TTarget>(string fieldName)
        {
            if (!(typeof(TTarget).IsPrimitive || typeof(TTarget).Equals(typeof(string)) || typeof(TTarget).Equals(typeof(Guid))))
            {
                throw new Exception("ExecuteList only use with TTarget is Primitive ( Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, Single, Guid and String)");
            }

            List<TTarget> list = new List<TTarget>();
            if (vMyReader == null) return list;
            try
            {
                int index = -1;
                for (int i = 0; i < vMyReader.FieldCount; i++)
                {
                    if (fieldName.ToLower() == vMyReader.GetName(i).ToLower())
                    {
                        index = i; break;
                    }
                }
                if (index == -1)
                {
                    throw new Exception(string.Format("FieldName '{0}' can not find", fieldName));
                }
                while (vMyReader.Read())
                {
                    if (vMyReader.IsDBNull(index))
                    {
                        list.Add(default(TTarget));
                    }
                    else
                    {
                        list.Add((TTarget)Convert.ChangeType(vMyReader[index], typeof(TTarget)));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public TTarget ExecuteObject<TTarget>(string commandText, CommandType type, params IDbDataParameter[] parameters) where TTarget : new()
        {
            IList<TTarget> kq = this.ExecuteListObject<TTarget>(commandText, type, parameters);
            if (kq.Count > 0)
            {
                return kq[0];
            }
            return default(TTarget);
        }

        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public TTarget ExecuteObject<TTarget>() where TTarget : new()
        {
            IList<TTarget> kq = this.ExecuteListObject<TTarget>();
            if (kq.Count > 0)
            {
                return kq[0];
            }
            return default(TTarget);
        }

        private static void ValidateMappings<TTarget>(IDataRecord reader)
        {
            List<PropertyInfo> props = new List<PropertyInfo>(DataMapper.GetSourceProperties(typeof(TTarget)));
            for (int i = 0; i < reader.FieldCount; i++)
            {
                PropertyInfo propinfo = props.Find(
                    delegate (PropertyInfo pi)
                    {
                        return DataMapper.MatchPropertyInfo(pi, reader.GetName(i));
                    });
                if (propinfo == null)
                {
                    string err = string.Format("Property '{0}' of type '{1}' is missing from the type '{2}'", reader.GetName(i), reader.GetFieldType(i), typeof(TTarget).FullName);
                    throw new Exception(err);
                }
            }
        }


        #region ExecuteNonQuery Overload
        /// <summary>
        /// Execute Query Not return record
        /// </summary>
        /// <returns>number of rows affected</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual int ExecuteNonQuery()
        {
            int row = 0;
            try
            {
                this.Open();
                row = DbCommand.ExecuteNonQuery();

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
                if (!this.InTransaction)
                {
                    this.Close();
                }
            }
            return row;
        }

        /// <summary>
        /// Execute Query Not return record with specified CommandText
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <returns>number of rows affected</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual int ExecuteNonQuery(string commandText, CommandType type)
        {
            int row = 0;
            try
            {
                this.Open();
                DbCommand.CommandText = commandText;
                DbCommand.CommandType = type;
                row = DbCommand.ExecuteNonQuery();
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
                if (!this.InTransaction)
                {
                    this.Close();
                }
            }

            return row;
        }

        /// <summary>
        /// Execute Query Not return record with specified CommandText and specified Parameters
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <param name="parameters">specified ParameterCollection </param>
        /// <returns>number of rows affected</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual int ExecuteNonQuery(string commandText, CommandType type, IDataParameterCollection parameters)
        {
            int row = 0;
            try
            {
                this.Open();
                DbCommand.CommandText = commandText;
                DbCommand.CommandType = type;
                DbCommand.Parameters.Clear();
                foreach (IDbDataParameter para in parameters)
                {
                    DbCommand.Parameters.Add(para);
                }
                row = DbCommand.ExecuteNonQuery();
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
                if (!this.InTransaction)
                {
                    this.Close();
                }
            }
            return row;
        }
        #endregion

        #region ExecuteScalar Overload
        /// <summary>
        /// Executes the query, and returns the first column of the first row in the resultset returned by the query. Extra columns or rows are ignored.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual object ExecuteScalar()
        {
            object row = null;
            try
            {
                this.Open();
                row = DbCommand.ExecuteScalar();
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
                if (!this.InTransaction)
                {
                    this.Close();
                }
            }
            return row;
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the resultset returned by the query. Extra columns or rows are ignored.
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual object ExecuteScalar(string commandText, CommandType type)
        {
            object row = null;
            try
            {
                this.Open();
                DbCommand.CommandText = commandText;
                DbCommand.CommandType = type;
                row = DbCommand.ExecuteScalar();
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
                if (!this.InTransaction)
                {
                    this.Close();
                }
            }
            return row;
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the resultset returned by the query. Extra columns or rows are ignored.
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <param name="parameters">specified ParameterCollection </param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual object ExecuteScalar(string commandText, CommandType type, IDataParameterCollection parameters)
        {
            object row = null;
            try
            {
                this.Open();
                DbCommand.CommandText = commandText;
                DbCommand.CommandType = type;
                DbCommand.Parameters.Clear();
                foreach (IDbDataParameter para in parameters)
                {
                    DbCommand.Parameters.Add(para);
                }
                row = DbCommand.ExecuteScalar();
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
                if (!this.InTransaction)
                {
                    this.Close();
                }
            }
            return row;
        }
        #endregion

        #region ExecuteDatSet Overload
        /// <summary>
        /// Executes the query, and returns DataSet
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual DataSet ExecuteDataSet()
        {
            DataSet dta = new DataSet();
            try
            {

                DataAdapter.SelectCommand = DbCommand;
                DataAdapter.Fill(dta);

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
        /// Executes the query, and returns DataSet
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual DataSet ExecuteDatSet(string commandText, CommandType type)
        {
            DataSet dta = new DataSet();
            try
            {
                DbCommand.CommandText = commandText;
                DbCommand.CommandType = type;
                DataAdapter.SelectCommand = DbCommand;
                DataAdapter.Fill(dta);
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
        /// Executes the query, and returns DataSet
        /// </summary>
        /// <param name="commandText">specified CommandText</param>
        /// <param name="type">CommandType of CommandText</param>
        /// <param name="parameters">specified ParameterCollection </param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual DataSet ExecuteDatSet(string commandText, CommandType type, IDataParameterCollection parameters)
        {
            DataSet dta = new DataSet();
            try
            {
                DbCommand.CommandText = commandText;
                DbCommand.CommandType = type;
                DbCommand.Parameters.Clear();
                foreach (IDbDataParameter para in parameters)
                {
                    DbCommand.Parameters.Add(para);
                }
                DataAdapter.SelectCommand = DbCommand;
                DataAdapter.Fill(dta);

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
        /// Adds or refreshes rows in the DataSet
        /// </summary>
        /// <param name="dataSet">A DataSet to fill with records and, if necessary, schema</param>
        /// <returns>
        /// The number of rows successfully added to or refreshed in the DataSet.
        /// </returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual int Fill(DataSet dataSet)
        {
            int kq = 0;
            try
            {
                this.Open();

                DataAdapter.SelectCommand = DbCommand;
                kq = DataAdapter.Fill(dataSet);

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

        #region Transaction
        /// <summary>
        /// Begin Transcation
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual void BeginTransaction()
        {
            if (this.InTransaction)
            {
                return;
            }
            if (DbCommand.Connection.State != ConnectionState.Open)
            {
                DbCommand.Connection.Open();
            }
            IDbTransaction transaction = DbCommand.Connection.BeginTransaction();
            DbCommand.Transaction = transaction;
            InTransaction = true;
        }

        /// <summary>
        /// Begin Transcation
        /// </summary>
        /// <param name="lv">IsolationLevel of Transcation</param>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual void BeginTransaction(IsolationLevel lv)
        {
            IDbTransaction transaction = DbCommand.Connection.BeginTransaction(lv);
            DbCommand.Transaction = transaction;
            InTransaction = true;
        }

        /// <summary>
        /// Comit Transcation. Only run if Begin Transcation call
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual void Comit()
        {
            if (InTransaction)
            {
                DbCommand.Transaction.Commit();
            }
            InTransaction = false;
        }

        /// <summary>
        /// RollBack Transcation.
        /// RollBAck AutoRun if Execute has error. Only run if Begin Transcation call
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual void RollBack()
        {
            if (InTransaction)
            {
                try
                {
                    DbCommand.Transaction.Rollback();
                }
                catch
                {
                    this.Close();
                }
                finally
                {
                    InTransaction = false;
                }
            }
        }
        #endregion




        #region Using for SQLProvider & OraProvider

        public List<WriteActionInfo> ListActioned { get; set; }
        internal virtual string GetActionStr()
        {
            if (DbCommand == null) return string.Empty;
            MyLogObject rt = new MyLogObject() { cmd = DbCommand.CommandText, lst = new List<MyLogObjectDetail>() };
            foreach (IDataParameter ptu in DbCommand.Parameters)
            {
                try
                {
                    if (ptu.ParameterName == "OUT_ERR_CODE" || ptu.ParameterName == "OUT_ERR_MSG" || ptu.ParameterName == "OUT_RET") continue;
                    if (ptu.Value != null)
                        rt.lst.Add(new MyLogObjectDetail() { prn = ptu.ParameterName, pvl = ptu.Value.ToString() });
                    else
                        rt.lst.Add(new MyLogObjectDetail() { prn = ptu.ParameterName });
                }
                catch { }
            }
            return JsonConvert.SerializeObject(rt);
        }
        internal void ListActions_AddItem(WriteActionInfo item, bool isStartNewAction = false)
        {
            if (ListActioned == null) ListActioned = new List<WriteActionInfo>();
            if (ListActioned.Count == 0 || isStartNewAction)
                ListActioned.Add(item);
            else
            {
                int cnt = ListActioned.Count - 1;
                if (ListActioned[cnt].FUNCT.Equals(item.FUNCT) && ListActioned[cnt].ACTIONTYPE.Equals(item.ACTIONTYPE) && ListActioned[cnt].OBCODE.Equals(item.OBCODE) && ListActioned[cnt].OBID.Equals(item.OBID))
                {
                    ListActioned[cnt].QRY.AppendLine(item.QRY.ToString());
                    ListActioned[cnt].QRY.Append(",");
                }
                else
                    ListActioned.Add(item);
            }
        }
        internal void ListActions_AddToLastItem(WriteActionInfo item)
        {
            if (ListActioned == null) ListActioned = new List<WriteActionInfo>();
            if (ListActioned.Count == 0)
                ListActioned.Add(item);
            else
            { 
                ListActioned[ListActioned.Count - 1].QRY.AppendLine(item.QRY.ToString());
                ListActioned[ListActioned.Count - 1].QRY.AppendLine(",");
            }
        }
        ////internal virtual System.Text.StringBuilder GetActionStr()
        ////{
        ////    return null;
        ////}

        ////public void WriteAction_AddToListActions()
        ////{
        ////    ////try
        ////    ////{
        ////    ////    ListActions_AddItem(new WriteActionInfo(GetActionStr().ToString()));
        ////    ////}
        ////    ////catch { }
        ////}
        public void WriteAction_AddToListActionsLastItem()
        {
            try
            {
                ListActions_AddToLastItem(new WriteActionInfo(GetActionStr()));
            }
            catch { }
        }

        ////public void WriteAction_AddToListActions(string iCommandStr)
        ////{
        ////////    try
        ////////    {
        ////////        ListActions_AddItem(new WriteActionInfo(iCommandStr));
        ////////    }
        ////////    catch { }
        ////}
        ////public void WriteAction_AddToListActions(string FUNCT, PermissionType ACTIONTYPE)
        ////{
        ////    try
        ////    {
        ////        ListActions_AddItem(new WriteActionInfo(GetActionStr().ToString(), FUNCT, ACTIONTYPE));
        ////    }
        ////    catch { }
        ////}
        public void WriteAction_AddToListActions(Int32 FUNCT, PermissionType ACTIONTYPE, string OBCODE, long OBID)
        {
            try
            {
                ListActions_AddItem(new WriteActionInfo(GetActionStr(), FUNCT, ACTIONTYPE, OBCODE, OBID));
            }
            catch { }
        }
        ////public void WriteAction_AddToListActions(string FUNCT, PermissionType ACTIONTYPE, string OBCODE, string OBID, bool IsNewAction)
        ////{
        ////    try
        ////    {
        ////        if (IsNewAction)
        ////            ListActions_AddItem(new WriteActionInfo(string.Empty, FUNCT, ACTIONTYPE, OBCODE, OBID), true);
        ////        else
        ////            ListActions_AddItem(new WriteActionInfo(GetActionStr().ToString(), FUNCT, ACTIONTYPE, OBCODE, OBID));
        ////    }
        ////    catch { }
        ////}



        #endregion


    }
}