using Dapper;
using Dreamer.Data;
using Dreamer.Data.Connection;
using Dreamer.Data.ViewModel;
using Dreamer.Services.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dreamer.Services.Repository
{
    public class InventoryReportRepository : IInventoryReport
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        DataAccess _da;
        public InventoryReportRepository(ApplicationDbContext context, DatabaseConnection conn, DataAccess da)
        {
            _context = context;
            _conn = conn;
            _da = da;
        }
        public List<PurchaseSales> CustomerCountSales(DateTime fromDate, DateTime toDate, int LedgerId, int VoucherTypeId)
        {
            throw new NotImplementedException();
        }

        public DataSet CustomerLedger(DateTime fromDate, DateTime toDate, int LedgerId, int CompanyId)
        {
            DataSet dtbl = new DataSet();
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sqldadapter = new SqlDataAdapter("CustomerLedgeer", sqlcon);
                sqldadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparam = new SqlParameter();
                sqlparam = sqldadapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime);
                sqlparam.Value = fromDate;
                sqlparam = sqldadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sqlparam.Value = toDate;
                sqlparam = sqldadapter.SelectCommand.Parameters.Add("@LedgerId", SqlDbType.Int);
                sqlparam.Value = LedgerId;
                sqlparam = sqldadapter.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int);
                sqlparam.Value = CompanyId;
                sqldadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }

        public DataSet CustomerLedgerDue(DateTime fromDate, DateTime toDate, int LedgerId)
        {
            throw new NotImplementedException();
        }

        public DataSet CustomerLedgerDueSingle(DateTime fromDate, DateTime toDate, int LedgerId)
        {
            throw new NotImplementedException();
        }

        public DataSet CustomerLedgerOpening(DateTime fromDate, int LedgerId, int CompanyId)
        {
            DataSet dtbl = new DataSet();
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sqldadapter = new SqlDataAdapter("CustomerLedgeerOpening", sqlcon);
                sqldadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparam = new SqlParameter();
                sqlparam = sqldadapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime);
                sqlparam.Value = fromDate;
                sqlparam = sqldadapter.SelectCommand.Parameters.Add("@LedgerId", SqlDbType.Int);
                sqlparam.Value = LedgerId;
                sqlparam = sqldadapter.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int);
                sqlparam.Value = CompanyId;
                sqldadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }

        public DataSet DayBook(DateTime fromDate, DateTime toDate, int VoucherTypeId, int LedgerId)
        {
            throw new NotImplementedException();
        }

        public List<FinancialReport> GettopProduct(int CompanyId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CompanyId", CompanyId);
                var ListofPlan = sqlcon.Query<FinancialReport>("GettopProduct", paramater, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }

        public DataSet LedgercountReport(DateTime fromDate, DateTime toDate, int LedgerId, string LedgerName, int CompanyId)
        {
            throw new NotImplementedException();
        }

        public List<PurchaseSales> PurchaseRepports(DateTime fromDate, DateTime toDate, int LedgerId, int VoucherTypeId)
        {
            throw new NotImplementedException();
        }
        public List<PurchaseSales> SaleReports(DateTime fromDate, DateTime toDate, int LedgerId, int VoucherTypeId)
        {
            throw new NotImplementedException();
        }

        public List<SalesMasterView> SalesInvoiceViewGraph(int CompanyId, int FinancialYearId , DateTime FromDate, DateTime ToDate)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@CompanyId", CompanyId);
                para.Add("@FinancialYearId", FinancialYearId);
                para.Add("@FromDate", FromDate);
                para.Add("@ToDate", ToDate);
                var ListofPlan = sqlcon.Query<SalesMasterView>("SalesInvoiceViewGraph", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }
        public List<PaymentReceiveView> PaymentSent(int CompanyId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@CompanyId", CompanyId);
                var ListofPlan = sqlcon.Query<PaymentReceiveView>("SELECT SUM(Amount) Amount,Date FROM PaymentMaster where CompanyId=@CompanyId Group by Date", para, null, true, 0, commandType: CommandType.Text).ToList();
                return ListofPlan;
            }
        }
        public List<PaymentReceiveView> PaymentReceive(int CompanyId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@CompanyId", CompanyId);
                var ListofPlan = sqlcon.Query<PaymentReceiveView>("SELECT SUM(Amount) Amount,Date FROM ReceiptMaster where CompanyId=@CompanyId Group by Date", para, null, true, 0, commandType: CommandType.Text).ToList();
                return ListofPlan;
            }
        }

        public DashboardView SalesTotal(int CompanyId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@CompanyId", CompanyId);
                var returnView = sqlcon.Query<DashboardView>("SELECT SUM(GrandTotal) as GrandTotal FROM SalesMaster where CompanyId=@CompanyId", para, null, true, 0, CommandType.Text).SingleOrDefault();
                return returnView;
            }
        }
        public DashboardView SalesTotalwarehouse(int WarehouseId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@WarehouseId", WarehouseId);
                var returnView = sqlcon.Query<DashboardView>("SELECT SUM(GrandTotal) as GrandTotal FROM SalesMaster where WarehouseId=@WarehouseId", para, null, true, 0, CommandType.Text).SingleOrDefault();
                return returnView;
            }
        }
        public DashboardView PurchaseTotal(int CompanyId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@CompanyId", CompanyId);
                var returnView = sqlcon.Query<DashboardView>("SELECT SUM(GrandTotal) as GrandTotal FROM PurchaseMaster where CompanyId=@CompanyId", para, null, true, 0, CommandType.Text).SingleOrDefault();
                return returnView;
            }
        }
        public DashboardView PurchaseTotalwarehouse(int WarehouseId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@WarehouseId", WarehouseId);
                var returnView = sqlcon.Query<DashboardView>("SELECT SUM(GrandTotal) as GrandTotal FROM PurchaseMaster where WarehouseId=@WarehouseId", para, null, true, 0, CommandType.Text).SingleOrDefault();
                return returnView;
            }
        }
        public DashboardView PurchaseReturnTotal(int CompanyId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@CompanyId", CompanyId);
                var returnView = sqlcon.Query<DashboardView>("SELECT SUM(GrandTotal) as GrandTotal FROM PurchaseReturnMaster where CompanyId=@CompanyId", para, null, true, 0, CommandType.Text).SingleOrDefault();
                return returnView;
            }
        }
        public DashboardView PurchaseReturnTotalwarehouse(int WarehouseId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@WarehouseId", WarehouseId);
                var returnView = sqlcon.Query<DashboardView>("SELECT SUM(GrandTotal) as GrandTotal FROM PurchaseReturnMaster where WarehouseId=@WarehouseId", para, null, true, 0, CommandType.Text).SingleOrDefault();
                return returnView;
            }
        }
        public DashboardView SalesReturnTotal(int CompanyId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@CompanyId", CompanyId);
                var returnView = sqlcon.Query<DashboardView>("SELECT SUM(GrandTotal) as GrandTotal FROM SalesReturnMaster where CompanyId=@CompanyId", para, null, true, 0, CommandType.Text).SingleOrDefault();
                return returnView;
            }
        }
        public DashboardView SalesReturnTotalwarehouse(int WarehouseId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@WarehouseId", WarehouseId);
                var returnView = sqlcon.Query<DashboardView>("SELECT SUM(GrandTotal) as GrandTotal FROM SalesReturnMaster where WarehouseId=@WarehouseId", para, null, true, 0, CommandType.Text).SingleOrDefault();
                return returnView;
            }
        }

        public List<InventoryViewFinal> StockReport(int GroupId, int ProductId, int CompanyId)
        {
            List<InventoryViewFinal> _UsersModel = new List<InventoryViewFinal>();
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand("StockReport", sqlcon);//call Stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupId", GroupId);
            cmd.Parameters.AddWithValue("@ProductId", ProductId);
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                InventoryViewFinal _UserModel = new InventoryViewFinal();
                _UserModel.ProductCode = reader["ProductCode"].ToString();
                _UserModel.ProductName = reader["ProductName"].ToString();
                _UserModel.UnitName = reader["UnitName"].ToString();
                _UserModel.PurQty = Convert.ToDecimal(reader["PurQty"].ToString());
                _UserModel.SalesQty = Convert.ToDecimal(reader["SalesQty"].ToString());
                _UserModel.Rate = Convert.ToDecimal(reader["Rate"].ToString());
                _UserModel.PurchaseStockBal = Convert.ToDecimal(reader["PurchaseStockBal"].ToString());
                _UserModel.SalesStockBalance = Convert.ToDecimal(reader["SalesStockBalance"].ToString());
                _UserModel.Stock = Convert.ToDecimal(reader["Stock"].ToString());
                _UserModel.Stockvalue = Convert.ToDecimal(reader["Stockvalue"].ToString());
                _UserModel.SalesRate = Convert.ToDecimal(reader["SalesRate"].ToString());
                _UsersModel.Add(_UserModel);
            }
            reader.Close();
            sqlcon.Close();
            return _UsersModel;
        }

        public List<PurchaseSales> SupplierCountPurchase(DateTime fromDate, DateTime toDate, int LedgerId, int VoucherTypeId)
        {
            throw new NotImplementedException();
        }

        public DataSet SupplierLedgerDueSingle(DateTime fromDate, DateTime toDate, int ledgerId)
        {
            throw new NotImplementedException();
        }

        public DataSet SuppllierLedgerDue(DateTime fromDate, DateTime toDate, int ledgerId)
        {
            throw new NotImplementedException();
        }

        public List<FinancialReport> TopReceive(int CompanyId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CompanyId", CompanyId);
                var ListofPlan = sqlcon.Query<FinancialReport>("TopReceive", paramater, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }

        public List<PurchaseMasterView> ViewAllPurchseInvoiceGraph(int CompanyId, int FinancialYearId , DateTime FromDate , DateTime ToDate)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@CompanyId", CompanyId);
                para.Add("@FinancialYearId", FinancialYearId);
                para.Add("@FromDate", FromDate);
                para.Add("@ToDate", ToDate);
                var ListofPlan = sqlcon.Query<PurchaseMasterView>("ViewAllPurchseInvoiceGraph", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }
        public DataTable StockSearch(int groupId, int productId, string criteria, int companyId)
        {
            DataTable dtblReg = new DataTable();
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sqlda = new SqlDataAdapter("StockSearch", sqlcon);
                sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param = sqlda.SelectCommand.Parameters.Add("@groupId", SqlDbType.Int);
                param.Value = groupId;
                param = sqlda.SelectCommand.Parameters.Add("@productId", SqlDbType.Int);
                param.Value = productId;
                param = sqlda.SelectCommand.Parameters.Add("@criteria", SqlDbType.NVarChar);
                param.Value = criteria;
                param = sqlda.SelectCommand.Parameters.Add("@companyId", SqlDbType.Int);
                param.Value = companyId;
                sqlda.Fill(dtblReg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
            return dtblReg;
        }
    }
}
