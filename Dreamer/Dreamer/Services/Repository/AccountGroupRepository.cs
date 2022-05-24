using Dapper;
using Dreamer.Data;
using Dreamer.Data.Connection;
using Dreamer.Data.Inventory;
using Dreamer.Data.ViewModel;
using Dreamer.Services.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dreamer.Services.Repository
{
    public class AccountGroupRepository : IAccountGroup
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public AccountGroupRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public bool CheckName(string name)
        {
            var checkResultCount = (from progm in _context.AccountGroup
                                    where progm.AccountGroupName == name
                                     select progm.AccountGroupId).Count();
            if (checkResultCount > 0)
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
            var checkResultCount = (from progm in _context.AccountGroup
                                     where progm.AccountGroupName == name
                                    select progm.AccountGroupId).Count();
            if (checkResultCount > 0)
            {

                var result = (from progm in _context.AccountGroup
                              where progm.AccountGroupName == name
                                    select progm.AccountGroupId).FirstOrDefault();
                return result;
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int AccountGroupId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT AccountGroupId from AccountLedger where AccountGroupId=@AccountGroupId) DELETE FROM AccountGroup where AccountGroupId=@AccountGroupId", sqlcon);
                cmd.CommandType = CommandType.Text;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@AccountGroupId", SqlDbType.Int);
                para.Value = AccountGroupId;
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

        public AccountGroup Edit(int AccountGroupId)
        {
            var viewResult = _context.AccountGroup.Find(AccountGroupId);
            return viewResult;
        }

        public List<AccountGroup> GetAll()
        {
            var result = (from a in _context.AccountGroup
                               select new AccountGroup
                               {
                                   AccountGroupId = a.AccountGroupId,
                                   AccountGroupName = a.AccountGroupName,
                                   GroupUnder = a.GroupUnder,
                                   Narration = a.Narration,
                                   Nature = a.Nature,
                                   IsDefault = a.IsDefault
                               }).ToList();
            return result;
        }

        public bool Save(AccountGroup model)
        {
            _context.AccountGroup.Add(model);
            _context.SaveChanges();
            return true;
        }

        public bool Update(AccountGroup model)
        {
            _context.AccountGroup.Update(model);
            _context.SaveChanges();
            return true;
        }

        public List<AccountGroupView> ViewAllAccountGroup()
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var ListofPlan = sqlcon.Query<AccountGroupView>("AccountGroupViewAllGridFill", null, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }
    }
}
