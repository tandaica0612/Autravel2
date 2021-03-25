using Autravel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Autravel.Controllers.Huy.Models
{
    public class SqlModule
    {
         public static DataTable GetDataTable(string sql, params object[] parameters)
        {
            DataTable result = new DataTable();
            SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
            if (conn == null)
            {
                throw new InvalidCastException("SqlConnection is invalid for this database");
            }
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddRange(parameters);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    result.Load(reader);
                }
                conn.Close();

                return result;
            }
        }

        public static void ExcuteCommand(string CommandStr = "")
        {
            SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
            if (conn == null)
            {
                throw new InvalidCastException("SqlConnection is invalid for this database");
            }
            var CMD = new SqlCommand(CommandStr, conn);
            conn.Open();
            CMD.CommandTimeout = 9999;
            CMD.ExecuteNonQuery();

            conn.Close();
        }
    }
}