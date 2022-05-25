using Dapper;
using DCMS.SE.Data;
using DCMS.SE.Data.Connection;
using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using DCMS.SE.Services.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DCMS.SE.Services.Repository
{
    public class AccountLedgerRepository : IAccountLedger
    {
       private readonly ApplicationDbContext _context;
       private readonly DatabaseConnection _conn;
        public AccountLedgerRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public string SerialNoCode(int CompanyId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                string val = string.Empty;
                var para = new DynamicParameters();
                para.Add("@CompanyId", CompanyId);
                return val = sqlcon.Query<string>("SELECT ISNULL( MAX(LedgerCode+1),1) FROM AccountLedger where CompanyId=@CompanyId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
            }
        }
        public bool Delete(int LedgerId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("AccountLedgerDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@LedgerId", SqlDbType.Int);
                para.Value = LedgerId;
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

        public AccountLedger Edit(int id)
        {
            AccountLedger returnView = _context.AccountLedger.Find(id);
            return returnView;
        }

        public List<AccountLedgerView> GetAll(int CompanyId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@CompanyId", CompanyId);
                var ListofPlan = sqlcon.Query<AccountLedgerView>("SELECT dbo.AccountLedger.LedgerId,dbo.AccountLedger.Email, dbo.AccountLedger.LedgerName, dbo.AccountLedger.LedgerCode, dbo.AccountGroup.AccountGroupId, dbo.AccountGroup.AccountGroupName, dbo.Area.AreaName, dbo.AccountLedger.IsDefault,  dbo.AccountLedger.Phone, dbo.AccountLedger.Mobile, dbo.Area.AreaId FROM            dbo.AccountLedger INNER JOIN dbo.AccountGroup ON dbo.AccountLedger.AccountGroupId = dbo.AccountGroup.AccountGroupId INNER JOIN  dbo.Area ON dbo.AccountLedger.AreaId = dbo.Area.AreaId where AccountLedger.CompanyId=@CompanyId", para, null, true, 0, commandType: CommandType.Text).ToList();
                return ListofPlan;
            }
        }
        public List<AccountLedgerView> ViewAllCustomer(int CompanyId)
        {
            var result = (from a in _context.AccountLedger
                          join b in _context.AccountGroup on a.AccountGroupId equals b.AccountGroupId
                          where a.CompanyId == CompanyId && a.AccountGroupId == 26
                          select new AccountLedgerView
                          {
                              LedgerId = a.LedgerId,
                              LedgerCode = a.LedgerCode,
                              LedgerName = a.LedgerName,
                              Phone = a.Phone,
                              Email = a.Email,
                              Country = a.Country,
                              City = a.City,
                              AccountGroupName = b.AccountGroupName
                          }).ToList();
            return result;
        }
        public List<AccountLedgerView> ViewAllSupplier(int CompanyId)
        {
            var result = (from a in _context.AccountLedger
                          join b in _context.AccountGroup on a.AccountGroupId equals b.AccountGroupId
                          where a.CompanyId == CompanyId && a.AccountGroupId == 22
                          select new AccountLedgerView
                          {
                              LedgerId = a.LedgerId,
                              LedgerCode = a.LedgerCode,
                              LedgerName = a.LedgerName,
                              Phone = a.Phone,
                              Email = a.Email,
                              Country = a.Country,
                              City = a.City,
                              AccountGroupName = b.AccountGroupName
                          }).ToList();
            return result;
        }
        public int Save(AccountLedger model)
        {
            _context.AccountLedger.Add(model);
            _context.SaveChanges();
            int id = model.LedgerId;
            return id;
        }

        public void Update(AccountLedger model)
        {
            _context.AccountLedger.Update(model);
            _context.SaveChanges();
        }
        public bool CheckName(string name)
        {
            var checkResult = (from progm in _context.AccountLedger
                               where progm.LedgerCode == name
                               select progm.LedgerId).Count();
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
            var checkResult = (from progm in _context.AccountLedger
                               where progm.LedgerCode == name
                               select progm.LedgerId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.AccountLedger
                                    where progm.LedgerCode == name
                                    select progm.LedgerId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }
        public List<AccountLedger> GetCashFill()
        {
            var result = (from a in _context.AccountLedger
                          select new AccountLedger
                          {
                              LedgerId = a.LedgerId,
                              LedgerCode = a.LedgerCode
                          }).ToList();
            return result;
        }
        public List<AccountLedger> GetBankFill()
        {
            var result = (from a in _context.AccountLedger
                          select new AccountLedger
                          {
                              LedgerId = a.LedgerId,
                              LedgerCode = a.LedgerCode
                          }).ToList();
            return result;
        }

        public List<AccountLedgerView> ViewAllExpensesCategory(int id)
        {
            var result = (from a in _context.AccountLedger
                          join b in _context.AccountGroup on a.AccountGroupId equals b.AccountGroupId
                          where a.CompanyId == id && a.AccountGroupId == 13 || a.AccountGroupId == 15
                          select new AccountLedgerView
                          {
                              LedgerId = a.LedgerId,
                              LedgerCode = a.LedgerCode,
                              LedgerName = a.LedgerName,
                              Phone = a.Phone,
                              Email = a.Email,
                              Country = a.Country,
                              City = a.City,
                              AccountGroupName = b.AccountGroupName
                          }).ToList();
            return result;
        }

        public List<AccountGroup> ExpensesCategory()
        {
            var result = (from a in _context.AccountGroup
                          where a.AccountGroupId == 13 || a.AccountGroupId == 15
                          select new AccountGroup
                          {
                              AccountGroupId = a.AccountGroupId,
                              AccountGroupName = a.AccountGroupName
                          }).ToList();
            return result;
        }
    }
}
