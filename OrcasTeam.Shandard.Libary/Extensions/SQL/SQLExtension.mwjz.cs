using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OrcasTeam.Shandard.Libary.Extensions.SQL
{
   public static  partial  class SQLExtension
    {
        public static IList<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            IList<T> result = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                T val = new T();
                PropertyInfo[] properties = val.GetType().GetProperties();
                PropertyInfo[] array = properties;
                foreach (PropertyInfo propertyInfo in array)
                {
                    if (dataTable.Columns.Contains(propertyInfo.Name) && propertyInfo.CanWrite && row[propertyInfo.Name] != DBNull.Value && !propertyInfo.GetMethod.IsVirtual)
                    {
                        propertyInfo.SetValue(val, row[propertyInfo.Name], null);
                    }
                }
            }
            return result;
        }

        private static bool readerExists(SqlDataReader dr, string columnName)
        {
            ((DbDataReader)dr).GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
            return ((DbDataReader)dr).GetSchemaTable().DefaultView.Count > 0;
        }

        public static IList<T> ToList<T>(this SqlDataReader dataReader) where T : new()
        {
            IList<T> list = new List<T>();
            string empty = string.Empty;
            while (((DbDataReader)dataReader).Read())
            {
                T val = new T();
                PropertyInfo[] properties = val.GetType().GetProperties();
                PropertyInfo[] array = properties;
                foreach (PropertyInfo propertyInfo in array)
                {
                    empty = propertyInfo.Name;
                    if (readerExists(dataReader, empty) && !propertyInfo.GetMethod.IsVirtual && propertyInfo.CanWrite)
                    {
                        object obj = ((DbDataReader)dataReader)[empty];
                        if (obj != DBNull.Value)
                        {
                            propertyInfo.SetValue(val, obj, null);
                        }
                    }
                }
                list.Add(val);
            }
            ((DbDataReader)dataReader).Close();
            return list;
        }

        public static T Field<T>(this DataRow dataRow, string columnName)
        {
            DataColumn[] source = dataRow.Table.Columns.Cast<DataColumn>().ToArray();
            DataColumn dataColumn = source.FirstOrDefault((DataColumn r) => r.ColumnName == columnName);
            if (dataColumn == null)
            {
                throw new Exception("ColumnName " + columnName + " not exist");
            }
            if (dataColumn.AllowDBNull && dataRow[columnName] == DBNull.Value)
            {
                return default(T);
            }
            return (T)dataRow[columnName];
        }
    }
}
