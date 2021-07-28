using System;
using System.Collections.Generic;
using System.Data;

namespace BlTools.MySqlFluentSqlWrapper
{
    public interface IFluentCommand
    {
        FluentSqlCommand AddParam(string name, bool? value);
        FluentSqlCommand AddParam(string name, DateTime? value);
        FluentSqlCommand AddParam(string name, float? value);
        FluentSqlCommand AddParam(string name, int? value);
        FluentSqlCommand AddParam(string name, long? value);
        FluentSqlCommand AddParam(string name, object value);
        FluentSqlCommand AddParam(string name, string value);
        void ExecNonQuery();
        T ExecRead<T>(Func<IDataReader, T> itemBuilder);
        List<T> ExecReadList<T>(Func<IDataReader, T> itemBuilder);
        T ExecScalar<T>();
        FluentSqlCommand Procedure(string procedureName);
        FluentSqlCommand Query(string queryText);
        FluentSqlCommand WithoutPreparation();
        FluentSqlCommand WithTimeout(int timeoutInSeconds);
    }
}