using Dapper;
using DCMS.SE.Data;
using DCMS.SE.Data.Connection;
using DCMS.SE.Data.Setting;
using DCMS.SE.Services.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DCMS.SE.Services.Repository
{
    public class CompanyRepository : ICompany
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public CompanyRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public Company Edit(int CompanyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CompanyId", CompanyId, DbType.Int64);

            Company company = new Company();

            using (var conn = new SqlConnection(_conn.DbConn))
            {

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    company = conn.QueryFirstOrDefault<Company>("SELECT *FROM Company where CompanyId=@CompanyId", parameters, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return company;
        }

        public List<Company> GetAll()
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var param = new DynamicParameters();
                var ListofPlan = sqlcon.Query<Company>("SELECT *FROM Company", null, null, true, 0, commandType: CommandType.Text).ToList();
                return ListofPlan;
            }
        }

        public void Update(Company model)
        {
            _context.Company.Update(model);
            _context.SaveChanges();
        }
    }
}
