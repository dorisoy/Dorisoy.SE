
using Dapper;
using DCMS.SE.Data;
using DCMS.SE.Data.Connection;
using DCMS.SE.Data.Setting;
using DCMS.SE.Data.ViewModel;
using DCMS.SE.Services.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DCMS.SE.Services.Repository
{
    public class CatagoryRepository : ICatagory
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public CatagoryRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public bool CheckName(string name)
        {
            var checkResult = (from progm in _context.Catagory
                                     where progm.CatagoryName == name
                               select progm.CatagoryId).Count();
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
            var checkResult = (from progm in _context.Catagory
                               where progm.CatagoryName == name
                               select progm.CatagoryId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.Catagory
                                    where progm.CatagoryName == name
                                    select progm.CatagoryId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int catagoryId)
        {
            SqlConnection sqlcon = new (_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new ("IF NOT EXISTS (SELECT CatagoryId from Product where CatagoryId=@CatagoryId) DELETE FROM Catagory where CatagoryId=@CatagoryId", sqlcon);
                cmd.CommandType = CommandType.Text;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@CatagoryId", SqlDbType.Int);
                para.Value = catagoryId;
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
        public List<CatagoryView> ViewAllCatagory()
        {
            using (SqlConnection sqlcon = new (_conn.DbConn))
            {
                var ListofPlan = sqlcon.Query<CatagoryView>("CatagoryViewForGridFill", null, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }
        public Catagory Edit(int id)
        {
            Catagory returnView = _context.Catagory.Find(id);
            return returnView;
        }

        public List<Catagory> GetAll()
        {
            using (SqlConnection sqlcon = new (_conn.DbConn))
            {
                var param = new DynamicParameters();
                var ListofPlan = sqlcon.Query<Catagory>("SELECT *FROM Catagory", null, null, true, 0, commandType: CommandType.Text).ToList();
                return ListofPlan;
            }
        }

        public int Save(Catagory model)
        {
            _context.Catagory.Add(model);
            _context.SaveChanges();
            int id = model.CatagoryId;
            return id;
        }

        public void Update(Catagory model)
        {
            _context.Catagory.Update(model);
            _context.SaveChanges();
        }
    }
}
