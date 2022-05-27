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
    public class StoreRepository : IStore
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public StoreRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public Store Edit(int StoreId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("StoreId", StoreId, DbType.Int64);

            Store Store = new ();

            using (SqlConnection conn = new (_conn.DbConn))
            {

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    Store = conn.QueryFirstOrDefault<Store>("SELECT *FROM Store where StoreId=@StoreId", parameters, commandType: CommandType.Text);
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
            return Store;
        }

        public List<Store> GetAll()
        {
            using SqlConnection sqlcon = new(_conn.DbConn);
            var param = new DynamicParameters();
            var ListofPlan = sqlcon.Query<Store>("SELECT *FROM Store", null, null, true, 0, commandType: CommandType.Text).ToList();
            return ListofPlan;
        }

        public void Update(Store model)
        {
            _context.Store.Update(model);
            _context.SaveChanges();
        }
    }
}
