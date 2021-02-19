using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BlTools.MySqlFluentSqlWrapper
{
    public static class DataRecordExtensions
    {
        public static int GetInt32(this IDataRecord reader, string name)
        {
            return reader.GetInt32(reader.GetOrdinal(name));
        }

        public static int? GetInt32Null(this IDataRecord reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? (int?) null : reader.GetInt32(ordinal);
        }

        public static long GetInt64(this IDataRecord reader, string name)
        {
            return reader.GetInt64(reader.GetOrdinal(name));
        }

        public static long? GetInt64Null(this IDataRecord reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? (long?) null : reader.GetInt64(ordinal);
        }

        public static double GetDouble(this IDataRecord reader, string name)
        {
            return reader.GetDouble(reader.GetOrdinal(name));
        }

        public static double? GetDoubleNull(this IDataRecord reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? (double?) null : reader.GetDouble(ordinal);
        }

        public static float GetFloat(this IDataReader reader, string name)
        {
            return reader.GetFloat(reader.GetOrdinal(name));
        }

        public static float? GetFloatNull(this IDataReader reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? (float?)null : reader.GetFloat(ordinal);
        }

        public static DateTime GetDateTime(this IDataRecord reader, string name)
        {
            return reader.GetDateTime(reader.GetOrdinal(name));
        }

        public static DateTime? GetDateTimeNull(this IDataRecord reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? (DateTime?) null : reader.GetDateTime(ordinal);
        }

        public static decimal GetDecimal(this IDataRecord reader, string name)
        {
            return reader.GetDecimal(reader.GetOrdinal(name));
        }

        public static decimal? GetDecimalNull(this IDataRecord reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? (decimal?) null : reader.GetDecimal(ordinal);
        }

        public static string GetStringNull(this IDataRecord reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return GetStringNull(reader, ordinal);
        }

        public static string GetStringNull(this IDataRecord reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        public static bool GetBoolean(this IDataReader reader, string name)
        {
            return reader.GetBoolean(reader.GetOrdinal(name));
        }

        public static bool? GetBooleanNull(this IDataReader reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? (bool?)null : reader.GetBoolean(ordinal);
        }

        public static Guid GetGuid(this IDataReader reader, string name)
        {
            return reader.GetGuid(reader.GetOrdinal(name));
        }

        public static Guid? GetGuidNull(this IDataReader reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal) ? (Guid?)null : reader.GetGuid(ordinal);
        }

        public static List<string> GetListString(this IDataReader reader, string name)
        {
            var arrayValues = (string[])reader.GetValue(reader.GetOrdinal(name));
            return arrayValues.ToList();
        }

        public static byte[] GetByteArray(this IDataReader reader, string name)
        {
            var arrayValues = (byte[])reader.GetValue(reader.GetOrdinal(name));
            return arrayValues;
        }
    }
}