using System;
using System.Collections.Generic;
using System.Data;

namespace BlTools.MySqlFluentSqlWrapper
{
    public interface IFluentCommand
    {
        IFluentCommand AddParam(string name, bool? value);
        IFluentCommand AddParam(string name, DateTime? value);
        IFluentCommand AddParam(string name, float? value);
        IFluentCommand AddParam(string name, int? value);
        IFluentCommand AddParam(string name, long? value);
        IFluentCommand AddParam(string name, object value);
        IFluentCommand AddParam(string name, string value);
        void ExecNonQuery();
        T ExecRead<T>(Func<IDataReader, T> itemBuilder);
        List<T> ExecReadList<T>(Func<IDataReader, T> itemBuilder);
        T ExecScalar<T>();
        IFluentCommand Procedure(string procedureName);
        IFluentCommand Query(string queryText);
        IFluentCommand WithoutPreparation();
        IFluentCommand WithTimeout(int timeoutInSeconds);
    }
}