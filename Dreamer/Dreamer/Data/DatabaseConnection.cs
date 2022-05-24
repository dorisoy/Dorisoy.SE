using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamer.Data
{
    public class DatabaseConnection
    {
        private readonly IConfiguration _configuration;

        public string DbConn;
        public DatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            DbConn = _configuration.GetConnectionString("DefaultConnection");
        }
        public DataTable ExecuteDataTable(string sql, List<SqlParameter> param)
        {
            SqlConnection conn = new SqlConnection(DbConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter p in param)
            {
                cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
            }
            DataTable dtResult = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dtResult);
            conn.Close();
            return dtResult;
        }
    }
}
