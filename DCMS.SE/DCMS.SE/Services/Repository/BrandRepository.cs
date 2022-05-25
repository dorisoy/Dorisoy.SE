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
    public class BrandRepository : IBrand
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public BrandRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public bool CheckName(string name)
        {
            var checkResult = (from progm in _context.Brand
                                     where progm.Name == name
                               select progm.BrandId).Count();
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
            var checkResult = (from progm in _context.Brand
                               where progm.Name == name
                               select progm.BrandId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.Brand
                                    where progm.Name == name
                                    select progm.BrandId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int BrandId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT BrandId from Product where BrandId=@BrandId) DELETE FROM Brand where BrandId=@BrandId", sqlcon);
                cmd.CommandType = CommandType.Text;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@BrandId", SqlDbType.Int);
                para.Value = BrandId;
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

        public Brand Edit(int id)
        {
            Brand returnView = _context.Brand.Find(id);
            return returnView;
        }

        public List<Brand> GetAll()
        {
            var view = _context.Brand.ToList();
            return view;
        }

        public int Save(Brand model)
        {
            _context.Brand.Add(model);
            _context.SaveChanges();
            int id = model.BrandId;
            return id;
        }

        public void Update(Brand model)
        {
            _context.Brand.Update(model);
            _context.SaveChanges();
        }
    }
}
