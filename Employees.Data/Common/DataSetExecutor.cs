using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using Employees.Data.DTOs;
using System.Reflection;

namespace Employees.Data.Common
{
    public class DataSetExecutor<T> where T : class
    {
        private String connectionString;
        private Type entityType;

        protected PropertyInfo[] PropertyInfo
        {
            get
            {
                return entityType.GetProperties();
            }
        }
        public DataSetExecutor(String connectionString) {
            this.connectionString = connectionString;
            entityType = typeof(T);
        }

        public IEnumerable<T> Get(String storedProcedureName, SqlParameter[] param = null) {
            var table = new DataTable();
            using (var connection = new SqlConnection(this.connectionString)) {
                using (var adapter = new SqlDataAdapter()) {
                    var command = new SqlCommand(storedProcedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                    {
                        command.Parameters.AddRange(param);
                    }
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                }
            }
            return FromTableToDTO(table);
        }

        public void InsertDelete(String storedProcedureName, SqlParameter[] param) {
            DataTable table = new DataTable();
            table.Rows.Add();
            using (var connection = new SqlConnection(this.connectionString))
            {
                var command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(param);
                using (var adapter = new SqlDataAdapter())
                {
                    adapter.InsertCommand = command;
                    adapter.Update(table);
                }
                command.Dispose();
            }

        }


        private IEnumerable<T> FromTableToDTO(DataTable table)
        {
            List<T> result = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                int numberOfProperty = 0;
                T temp = (T)Activator.CreateInstance(this.entityType);
                foreach (var prop in this.PropertyInfo)
                {
                    prop.SetValue(temp, row[numberOfProperty]);
                    ++numberOfProperty;
                }
                result.Add(temp);
            }
            return result;
        }

  }
}
