using DCMS.SE.Data;
using DCMS.SE.Data.Connection;
using DCMS.SE.Services.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace DCMS.SE.Services.Repository
{
    public class StockPostingRepository : IStockPosting
    {
        private readonly ApplicationDbContext _context;
        DatabaseConnection _conn;
        public StockPostingRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public decimal StockCheckForProductSale(int decProductId, int decBatchId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            decimal decStock = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("StocKCheckForProductSale", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productId", SqlDbType.Decimal);
                sprmparam.Value = decProductId;
                sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.Decimal);
                sprmparam.Value = decBatchId;
                decStock = decimal.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
            return decStock;
        }
    }
}
