using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;

namespace BlTools.MySqlFluentSqlWrapper
{
    public sealed class FluentSqlCommand : IFluentCommand
    {
        private readonly MySqlCommand _command;
        private readonly MySqlParameterCollection _parameters;
        private Action<MySqlDataReader> _dataReaderAction;
        private bool _needPreparation;

        public FluentSqlCommand(string connectionString)
        {
            _command = new MySqlCommand();
            _parameters = _command.Parameters;
            _command.Connection = new MySqlConnection(connectionString);
            _needPreparation = true;
        }

        public FluentSqlCommand Procedure(string procedureName)
        {
            _command.CommandText = procedureName;
            _command.CommandType = CommandType.StoredProcedure;
            return this;
        }

        public FluentSqlCommand Query(string queryText)
        {
            _command.CommandText = queryText;
            _command.CommandType = CommandType.Text;
            return this;
        }

        public FluentSqlCommand WithTimeout(int timeoutInSeconds)
        {
            _command.CommandTimeout = timeoutInSeconds;
            return this;
        }

        public FluentSqlCommand WithoutPreparation()
        {
            _needPreparation = false;
            return this;
        }

        #region add param

        public FluentSqlCommand AddParam(string name, object value)
        {
            _parameters.AddWithValue(name, value);
            return this;
        }

        public FluentSqlCommand AddParam(string name, string value)
        {
            _parameters.AddWithValue(name, value);
            return this;
        }

        public FluentSqlCommand AddParam(string name, int? value)
        {
            _parameters.AddWithValue(name, value);
            return this;
        }

        public FluentSqlCommand AddParam(string name, float? value)
        {
            _parameters.AddWithValue(name, value);
            return this;
        }

        public FluentSqlCommand AddParam(string name, long? value)
        {
            _parameters.AddWithValue(name, value);
            return this;
        }

        public FluentSqlCommand AddParam(string name, DateTime? value)
        {
            _parameters.AddWithValue(name, value);
            return this;
        }

        public FluentSqlCommand AddParam(string name, bool? value)
        {
            _parameters.AddWithValue(name, value);
            return this;
        }

        #endregion

        public void ExecNonQuery()
        {
            Execute(ExecType.NonQuery);
        }

        public List<T> ExecReadList<T>(Func<IDataReader, T> itemBuilder)
        {
            var result = new List<T>();
            _dataReaderAction = reader => result.Add(itemBuilder(reader));
            Execute(ExecType.Reader);
            return result;
        }

        public T ExecRead<T>(Func<IDataReader, T> itemBuilder)
        {
            var result = default(T);
            _dataReaderAction = reader => result = itemBuilder(reader);
            Execute(ExecType.Reader);
            return result;
        }

        public T ExecScalar<T>()
        {
            var rawResult = Execute(ExecType.Scalar);
            var result = DBNull.Value.Equals(rawResult) ? default : (T)rawResult;
            return result;
        }

        private object Execute(ExecType execType)
        {
            object result = null;
            using (_command.Connection)
            {
                OpenConnection();
                using (_command)
                {
                    if (_needPreparation)
                    {
                        _command.Prepare();
                    }
                    switch (execType)
                    {
                        case ExecType.Reader:
                            {
                                var reader = _command.ExecuteReader();
                                using (reader)
                                {
                                    while (reader.Read())
                                    {
                                        _dataReaderAction?.Invoke(reader);
                                    }
                                }

                                break;
                            }
                        case ExecType.NonQuery:
                            {
                                _command.ExecuteNonQuery();
                                break;
                            }
                        case ExecType.Scalar:
                            {
                                result = _command.ExecuteScalar();
                                break;
                            }
                    }
                }
            }

            return result;
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

        private enum ExecType
        {
            NonQuery = 0,
            Reader = 1,
            Scalar = 2
        }
    }
}