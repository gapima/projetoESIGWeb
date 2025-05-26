using ESIGWeb.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace ESIGWeb.Data
{
    public static class DatabaseHelper
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
        public static DataTable GetViewData()
        {
            using (var conn = new OracleConnection(ConnectionString))
            {
                string query = "SELECT * FROM VW_RELATORIO_SALARIOS";
                using (var cmd = new OracleCommand(query, conn))
                {
                    conn.Open();
                    DataTable dt = new DataTable();
                    using (var da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    return dt;
                }
            }
        }
    }
}
