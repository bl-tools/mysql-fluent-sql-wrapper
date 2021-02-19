using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;

namespace BlTools.MySqlFluentSqlWrapper
{
    public sealed class FluentSqlCommand
    {
        private readonly MySqlCommand _command;
        private readonly MySqlParameterCollection _parameters;
        private Action<MySqlDataReader> _dataReaderAction;

        public FluentSqlCommand(string connectionString)
        {
            _command = new MySqlCommand();
            _parameters = _command.Parameters;
            _command.Connection = new MySqlConnection(connectionString);
        }

        public FluentSqlCommand Procedure(string procedureName)
        {
            _command.CommandText = procedureName;
            _command.CommandType = CommandType.StoredProcedure;
            return this;
        }

        public FluentSqlCommand WithTimeout(int timeoutInSeconds)
        {
            _command.CommandTimeout = timeoutInSeconds;
            return this;
        }

        public FluentSqlCommand AddParameter(string parameterName, object value)
        {
            _parameters.AddWithValue(parameterName, value);
            return this;
        }

        public void ExecuteNonQuery()
        {
            Execute(true);
        }

        public List<T> ExecuteReadRecordsList<T>(Func<IDataReader, T> recordBuilder)
        {
            var result = new List<T>();
            _dataReaderAction = reader => result.Add(recordBuilder(reader));
            Execute(false);
            return result;
        }

        public T ExecuteReadRecord<T>(Func<IDataReader, T> recordBuilder)
        {
            var result = default(T);
            _dataReaderAction = reader => result = recordBuilder(reader);
            Execute(false);
            return result;
        }

        private void Execute(bool isNonQuery)
        {
            using (_command.Connection)
            {
                OpenConnection();
                using (_command)
                {
                    if (isNonQuery)
                    {
                        _command.ExecuteNonQuery();
                    }
                    else
                    {
                        var reader = _command.ExecuteReader();
                        using (reader)
                        {
                            while (reader.Read())
                            {
                                _dataReaderAction?.Invoke(reader);
                            }
                        }
                    }
                }
            }
        }

        private void OpenConnection()
        {
            if (_command.Connection == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(_command.Connection)} is null, {nameof(OpenConnection)} is not possible");
            }

            if (_command.Connection.State != ConnectionState.Open)
            {
                _command.Connection.Open();
            }
        }
    }
}