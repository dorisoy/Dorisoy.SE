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
    public class WarehouseRepository : IWarehouse
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public WarehouseRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public bool CheckName(string name)
        {
            var checkResult = (from progm in _context.Warehouse
                               where progm.Name == name
                               select progm.WarehouseId).Count();
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
            var checkResult = (from progm in _context.Warehouse
                               where progm.Name == name
                               select progm.WarehouseId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.Warehouse
                                    where progm.Name == name
                                    select progm.WarehouseId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int WarehouseId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("WarehouseDelete", sqlcon);
                cmd.CommandType = CommandType.Text;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@WarehouseId", SqlDbType.Int);
                para.Value = WarehouseId;
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

        public Warehouse Edit(int id)
        {
            Warehouse returnView = _context.Warehouse.Find(id);
            return returnView;
        }

        public List<Warehouse> GetAll()
        {
            var view = _context.Warehouse.ToList();
            return view;
        }

        public int Save(Warehouse model)
        {
            _context.Warehouse.Add(model);
            _context.SaveChanges();
            int id = model.WarehouseId;
            return id;
        }

        public void Update(Warehouse model)
        {
            _context.Warehouse.Update(model);
            _context.SaveChanges();
        }
    }
}
