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
    public class FinancialYearRepository : IFinancialYear
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public FinancialYearRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public bool CheckName(string name)
        {
            var checkResult = (from progm in _context.FinancialYear
                                     where progm.FiscalYear == name
                               select progm.FinancialYearId).Count();
            if (checkResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CheckNameId(string name)
        {
            var checkResult = (from progm in _context.FinancialYear
                                     where progm.FiscalYear == name
                               select progm.FinancialYearId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.FinancialYear
                                    where progm.FiscalYear == name
                                    select progm.FinancialYearId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int FinancialYearId)
        {
            SqlConnection sqlcon = new(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new("DELETE FROM FinancialYear where FinancialYearId=@FinancialYearId", sqlcon)
                {
                    CommandType = CommandType.Text
                };
                SqlParameter para = new ();
                para = cmd.Parameters.Add("@FinancialYearId", SqlDbType.Int);
                para.Value = FinancialYearId;
                long rowAffacted = cmd.ExecuteNonQuery();
                if (rowAffacted > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public FinancialYear Edit(int id)
        {
            FinancialYear returnView = _context.FinancialYear.Find(id);
            return returnView;
        }

        public List<FinancialYear> GetAll()
        {
            using SqlConnection sqlcon = new(_conn.DbConn);
            var param = new DynamicParameters();
            var ListofPlan = sqlcon.Query<FinancialYear>("SELECT *FROM FinancialYear", null, null, true, 0, commandType: CommandType.Text).ToList();
            return ListofPlan;
        }

        public int Save(FinancialYear model)
        {
            _context.FinancialYear.Add(model);
            _context.SaveChanges();
            int id = model.FinancialYearId;
            return id;
        }

        public void Update(FinancialYear model)
        {
            _context.FinancialYear.Update(model);
            _context.SaveChanges();
        }
    }
}
