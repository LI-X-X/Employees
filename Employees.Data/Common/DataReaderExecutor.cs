using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data.SqlClient;

namespace Employees.Data.Common
{
    public class DataReaderExecutor<T> where T : class
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
        public DataReaderExecutor(String connString)
        {
            this.connectionString = connString;
            entityType = typeof(T);
        }

        public IEnumerable<T> Get(string storedProcedureName, SqlParameter[] param=null)
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                using (var command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (param != null)
                    {
                        command.Parameters.AddRange(param);
                    }
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            T temp = (T)Activator.CreateInstance(this.entityType);
                            foreach (var prop in this.PropertyInfo)
                            {
                                prop.SetValue(temp, reader[prop.Name]);
                            }
                            result.Add(temp);
                        }
                        reader.Close();

                    }
                    else
                        reader.Close();
                }
            }
            return result;
        }

        public void ExecuteWithoutResult(SqlParameter[] param, string storedProcedureName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddRange(param);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
