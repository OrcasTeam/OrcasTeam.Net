using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace OrcasTeam.Shandard.Libary.Extensions
{
    public static class SqlExtension
    {

        /// <summary>
        ///         将Dt转换为List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            IList<T> result = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                var val = new T();
                var properties = val.GetType().GetProperties();
                var array = properties;
                foreach (var propertyInfo in array)
                    if (dataTable.Columns.Contains(propertyInfo.Name) && propertyInfo.CanWrite &&
                        row[propertyInfo.Name] != DBNull.Value && !propertyInfo.GetMethod.IsVirtual)
                        propertyInfo.SetValue(val, row[propertyInfo.Name], null);
            }

            return result;
        }

        private static bool ReaderExists(SqlDataReader dr, string columnName)
        {
            dr.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
            return dr.GetSchemaTable().DefaultView.Count > 0;
        }

        /// <summary>
        ///     取出DataReader中的数据存储到List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this SqlDataReader dataReader) where T : new()
        {
            IList<T> list = new List<T>();
            string empty;
            while (dataReader.Read())
            {
                var val = new T();
                var properties = val.GetType().GetProperties();
                var array = properties;
                foreach (var propertyInfo in array)
                {
                    empty = propertyInfo.Name;
                    if (ReaderExists(dataReader, empty) && !propertyInfo.GetMethod.IsVirtual && propertyInfo.CanWrite)
                    {
                        var obj = dataReader[empty];
                        if (obj != DBNull.Value) propertyInfo.SetValue(val, obj, null);
                    }
                }

                list.Add(val);
            }

            dataReader.Close();
            return list;
        }

        /// <summary>
        ///     在DataRow中查询指定列名数据
        /// </summary>
        /// <typeparam name="T">当前列的数据类型</typeparam>
        /// <param name="dataRow"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static T Field<T>(this DataRow dataRow, string columnName)
        {
            var source = dataRow.Table.Columns.Cast<DataColumn>().ToArray();
            var dataColumn = source.FirstOrDefault(r => r.ColumnName == columnName);
            if (dataColumn == null) throw new Exception("ColumnName " + columnName + " not exist");
            if (dataColumn.AllowDBNull && dataRow[columnName] == DBNull.Value) return default;
            return (T) dataRow[columnName];
        }
    }
}