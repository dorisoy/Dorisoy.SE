using DCMS.SE.Data;
using DCMS.SE.Data.Connection;
using DCMS.SE.Data.Inventory;
using DCMS.SE.Services.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DCMS.SE.Services.Repository
{
    public class CurrencyRepository : ICurrency
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public CurrencyRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public bool CheckName(string name)
        {
            var checkResult = (from progm in _context.Currency
                                     where progm.CurrencyName == name
                               select progm.CurrencyId).Count();
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
            var checkResult = (from progm in _context.Currency
                               where progm.CurrencyName == name
                               select progm.CurrencyId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.Currency
                                    where progm.CurrencyName == name
                                    select progm.CurrencyId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int CurrencyId)
        {
            SqlConnection sqlcon = new (_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new ("IF NOT EXISTS (SELECT CurrencyId from Store where CurrencyId=@CurrencyId) DELETE FROM Currency where CurrencyId=@CurrencyId", sqlcon);
                cmd.CommandType = CommandType.Text;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@CurrencyId", SqlDbType.Int);
                para.Value = CurrencyId;
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

        public Currency Edit(int id)
        {
            Currency returnView = _context.Currency.Find(id);
            return returnView;
        }

        public List<Currency> GetAll()
        {
            var view = _context.Currency.ToList();
            return view;
        }

        public int Save(Currency model)
        {
            _context.Currency.Add(model);
            _context.SaveChanges();
            int id = model.CurrencyId;
            return id;
        }

        public void Update(Currency model)
        {
            _context.Currency.Update(model);
            _context.SaveChanges();
        }
    }
}
