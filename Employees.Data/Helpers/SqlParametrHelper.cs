using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Employees.Data.DataManagers
{
    public static class SqlParametrHelper
    {
        public static SqlParameter[] FillSqlParametrs<TValue>(Dictionary<String, TValue> dict) {
            Int32 count = dict.Count;
            var param = new SqlParameter[count];
            Int32 i = 0;
            foreach (var item in dict)
            {
                param[i] = new SqlParameter();
                param[i].ParameterName = item.Key;
                param[i].Value = item.Value;
                ++i;
            }
            return param;
        }
    }
}
