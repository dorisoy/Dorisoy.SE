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
    public class TerminalRepository : ITerminal
    {
       private readonly ApplicationDbContext _context;
       private readonly DatabaseConnection _conn;
        public TerminalRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public string SerialNoCode(int StoreId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                string val = string.Empty;
                var para = new DynamicParameters();
                para.Add("@StoreId", StoreId);
                return val = sqlcon.Query<string>("SELECT ISNULL( MAX(TerminalCode+1),1) FROM Terminal where StoreId=@StoreId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
            }
        }
        public bool Delete(int TerminalId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("TerminalDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@TerminalId", SqlDbType.Int);
                para.Value = TerminalId;
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

        public Terminal Edit(int id)
        {
            Terminal returnView = _context.Terminal.Find(id);
            return returnView;
        }

        public List<TerminalView> GetAll(int StoreId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@StoreId", StoreId);
                var ListofPlan = sqlcon.Query<TerminalView>("SELECT dbo.Terminal.TerminalId,dbo.Terminal.Email, dbo.Terminal.TerminalName, dbo.Terminal.TerminalCode, dbo.AccountGroup.AccountGroupId, dbo.AccountGroup.AccountGroupName, dbo.Area.AreaName, dbo.Terminal.IsDefault,  dbo.Terminal.Phone, dbo.Terminal.Mobile, dbo.Area.AreaId FROM            dbo.Terminal INNER JOIN dbo.AccountGroup ON dbo.Terminal.AccountGroupId = dbo.AccountGroup.AccountGroupId INNER JOIN  dbo.Area ON dbo.Terminal.AreaId = dbo.Area.AreaId where Terminal.StoreId=@StoreId", para, null, true, 0, commandType: CommandType.Text).ToList();
                return ListofPlan;
            }
        }
        public List<TerminalView> ViewAllCustomer(int StoreId)
        {
            var result = (from a in _context.Terminal
                          join b in _context.AccountGroup on a.AccountGroupId equals b.AccountGroupId
                          where a.StoreId == StoreId && a.AccountGroupId == 26
                          select new TerminalView
                          {
                              TerminalId = a.TerminalId,
                              TerminalCode = a.TerminalCode,
                              TerminalName = a.TerminalName,
                              Phone = a.Phone,
                              Email = a.Email,
                              Country = a.Country,
                              City = a.City,
                              AccountGroupName = b.AccountGroupName
                          }).ToList();
            return result;
        }
        public List<TerminalView> ViewAllSupplier(int StoreId)
        {
            var result = (from a in _context.Terminal
                          join b in _context.AccountGroup on a.AccountGroupId equals b.AccountGroupId
                          where a.StoreId == StoreId && a.AccountGroupId == 22
                          select new TerminalView
                          {
                              TerminalId = a.TerminalId,
                              TerminalCode = a.TerminalCode,
                              TerminalName = a.TerminalName,
                              Phone = a.Phone,
                              Email = a.Email,
                              Country = a.Country,
                              City = a.City,
                              AccountGroupName = b.AccountGroupName
                          }).ToList();
            return result;
        }
        public int Save(Terminal model)
        {
            _context.Terminal.Add(model);
            _context.SaveChanges();
            int id = model.TerminalId;
            return id;
        }

        public void Update(Terminal model)
        {
            _context.Terminal.Update(model);
            _context.SaveChanges();
        }
        public bool CheckName(string name)
        {
            var checkResult = (from progm in _context.Terminal
                               where progm.TerminalCode == name
                               select progm.TerminalId).Count();
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
            var checkResult = (from progm in _context.Terminal
                               where progm.TerminalCode == name
                               select progm.TerminalId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.Terminal
                                    where progm.TerminalCode == name
                                    select progm.TerminalId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }
        public List<Terminal> GetCashFill()
        {
            var result = (from a in _context.Terminal
                          select new Terminal
                          {
                              TerminalId = a.TerminalId,
                              TerminalCode = a.TerminalCode
                          }).ToList();
            return result;
        }
        public List<Terminal> GetBankFill()
        {
            var result = (from a in _context.Terminal
                          select new Terminal
                          {
                              TerminalId = a.TerminalId,
                              TerminalCode = a.TerminalCode
                          }).ToList();
            return result;
        }

        public List<TerminalView> ViewAllExpensesCategory(int id)
        {
            var result = (from a in _context.Terminal
                          join b in _context.AccountGroup on a.AccountGroupId equals b.AccountGroupId
                          where a.StoreId == id && a.AccountGroupId == 13 || a.AccountGroupId == 15
                          select new TerminalView
                          {
                              TerminalId = a.TerminalId,
                              TerminalCode = a.TerminalCode,
                              TerminalName = a.TerminalName,
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
