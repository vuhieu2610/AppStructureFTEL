using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EntityData.Common
{
    public class Db: IDisposable
    {
        private string _connectionString = ConfigUtil.GetConfigValue("ConnectionString");
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private SqlTransaction _transaction;

        public Db()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public void UseTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Build(string cmdText, CommandType cmdType)
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }

            if (_command == null)
            {
                _command = new SqlCommand(cmdText, _connection);
            }
            _command.CommandText = cmdText;
            _command.CommandType = cmdType;

            _reader = null;
        }

        public SqlConnection Connection {
            get
            {
                return _connection;
            }
        }

        public SqlCommand Command
        {
            get
            {
                return _command;
            }
        }

        public SqlDataReader Reader
        {
            get
            {
                if (_reader == null)
                {
                    _reader = _command.ExecuteReader();
                }
                return _reader;
            }
        }

        public SqlTransaction Transaction
        {
            get
            {
                return _transaction;
            }
        }

        public bool IsConnectionOk()
        {
            if (_connection == null || _connection.State == ConnectionState.Closed)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsCommandOk()
        {
            if (!IsConnectionOk())
            {
                return false;
            }
            else
            {
                if (_command == null || _command.Connection != Connection)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool IsReaderOk()
        {
            if (!IsCommandOk())
            {
                return false;
            }
            else
            {
                if (_reader == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public void Done()
        {
            _reader.NextResult();
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
            if (_command != null)
            {
                _command.Dispose();
                _command = null;
            }
            if (_reader != null)
            {
                _reader.Close();
                _reader = null;
            }
        }
    }
    public class DbProvider: IDisposable
    {
        Db _db;

        public DbProvider()
        {
            _db = new Db();
        }

        public DbProvider SetQuery(string cmdText, CommandType cmdType)
        {
            _db.Build(cmdText, cmdType);
            return this;
        }

        public DbProvider UseTransaction()
        {
            _db.UseTransaction();
            return this;
        }

        public DbProvider SetParameter(string name, SqlDbType type, object value, int size = 50, ParameterDirection direction = ParameterDirection.Input)
        {
            var parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
            if(size > 0) parameter.Size = size;
            if(value != DBNull.Value) parameter.Value = value;
            parameter.Direction = direction;
            _db.Command.Parameters.Add(parameter);
            return this;
        }

        public DbProvider GetResult(out int result)
        {
            result = _db.Command.ExecuteNonQuery();
            if (result == 0 && _db.Transaction != null)
            {
                _db.Transaction.Rollback();
            }
            return this;
        }

        public void ExecuteNoneQuery(out int result)
        {
            result = _db.Command.ExecuteNonQuery();
            if (result == 0 && _db.Transaction != null)
            {
                _db.Transaction.Rollback();
            }
            if (_db.Transaction != null)
            {
                _db.Transaction.Commit();
            }
            if (_db.Command != null)
            {
                _db.Command.Dispose();
            }
            if (_db.Connection != null)
            {
                _db.Connection.Close();
            }
        }

        public DbProvider GetOutValue<T>(string paramName, out T value)
        {
            value = (T)_db.Command.Parameters[paramName].Value;
            return this;
        }

        public T GetOutValue<T>(string paramName)
        {
            var value = (T)_db.Command.Parameters[paramName].Value;
            return value;
        }

        public DbProvider GetList<T>(out List<T> list) where T : new()
        {
            list = new List<T>();

            if (_db.Reader.HasRows)
            {
                while (_db.Reader.Read())
                {
                    var obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        prop.SetValue(obj, _db.Reader[prop.Name]);
                    }
                    list.Add(obj);
                }
            }

            return this;
        }

        public DbProvider GetSingle<T>(out T obj) where T : new()
        {
            obj = new T();

            if (_db.Reader.HasRows)
            {
                while (_db.Reader.Read())
                {
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        prop.SetValue(obj, _db.Reader[prop.Name]);
                    }
                    break;
                }
            }

            return this;
        }

        public DbProvider Get<T>(out T value)
        {
            value = default(T);

            if (_db.Reader.HasRows)
            {
                while (_db.Reader.Read())
                {
                    value = (T)_db.Reader[0];
                    break;
                }
            }

            return this;
        }

        public void Complete()
        {
            if (_db.Transaction != null)
            {
                _db.Transaction.Commit();
            }
            if (_db.Reader != null)
            {
                _db.Reader.Close();
            }
            if (_db.Command != null)
            {
                _db.Command.Dispose();
            }
            if (_db.Connection != null)
            {
                _db.Connection.Close();
            }
        }


        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }
    }
}
