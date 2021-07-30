using System;
using System.Collections.Generic;
using System.Data;

namespace BlTools.MySqlFluentSqlWrapper
{
    public interface IFluentSqlCommand
    {
        IFluentSqlCommand AddParam(string name, bool? value);
        IFluentSqlCommand AddParam(string name, DateTime? value);
        IFluentSqlCommand AddParam(string name, float? value);
        IFluentSqlCommand AddParam(string name, int? value);
        IFluentSqlCommand AddParam(string name, long? value);
        IFluentSqlCommand AddParam(string name, object value);
        IFluentSqlCommand AddParam(string name, string value);
        void ExecNonQuery();
        T ExecRead<T>(Func<IDataReader, T> itemBuilder);
        List<T> ExecReadList<T>(Func<IDataReader, T> itemBuilder);
        T ExecScalar<T>();
        IFluentSqlCommand Procedure(string procedureName);
        IFluentSqlCommand Query(string queryText);
        IFluentSqlCommand WithoutPreparation();
        IFluentSqlCommand WithTimeout(int timeoutInSeconds);
    }
}