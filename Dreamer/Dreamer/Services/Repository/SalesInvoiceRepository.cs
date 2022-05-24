using Dapper;
using Dreamer.Data;
using Dreamer.Data.Connection;
using Dreamer.Data.Inventory;
using Dreamer.Data.Setting;
using Dreamer.Data.ViewModel;
using Dreamer.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dreamer.Services.Repository
{
    public class SalesInvoiceRepository : ISalesInvoice
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public SalesInvoiceRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }

        public bool AccountSalesInvoiceNoCheckExistence(int CompanyId, int FinancialYearId, string VoucherNo)
        {
            var checkResult = (from progm in _context.SalesMaster
                               where progm.CompanyId == CompanyId && progm.FinancialYearId == FinancialYearId && progm.VoucherNo == VoucherNo
                               select progm.SalesMasterId).Count();
            if (checkResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int AccountSalesInvoiceNoCheckExistenceid(string name)
        {
            var checkResult = (from progm in _context.SalesMaster
                               where progm.VoucherNo == name
                               select progm.SalesMasterId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.SalesMaster
                                    where progm.VoucherNo == name
                                    select progm.SalesMasterId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public bool DeleteSalesInvoice(int SalesMasterId, string VoucherNo, int CompanyId, int FinancialYearId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("SalesInvoiceDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@SalesMasterId", SqlDbType.Int);
                para.Value = SalesMasterId;
                para = cmd.Parameters.Add("@VoucherNo", SqlDbType.NVarChar);
                para.Value = VoucherNo;
                para = cmd.Parameters.Add("@CompanyId", SqlDbType.Int);
                para.Value = CompanyId;
                para = cmd.Parameters.Add("@FinancialYearId", SqlDbType.Int);
                para.Value = FinancialYearId;
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

        public SalesMaster EditSalesMaster(int SalesMasterId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@SalesMasterId", SalesMasterId);
                var result = sqlcon.Query<SalesMaster>("SELECT *FROM SalesMaster where SalesMasterId=@SalesMasterId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
                return result;
            }
        }

        public string GetVoucherNo(int CompanyId, int FinancialYearId, int VoucherTypeId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                string val = string.Empty;
                var para = new DynamicParameters();
                para.Add("@CompanyId", CompanyId);
                para.Add("@FinancialYearId", FinancialYearId);
                para.Add("@VoucherTypeId", VoucherTypeId);
                return val = sqlcon.Query<string>("SELECT ISNULL( MAX(SerialNo+1),1) FROM SalesMaster where CompanyId=@CompanyId AND FinancialYearId=@FinancialYearId AND VoucherTypeId=@VoucherTypeId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
            }
        }

        public SalesMasterView PrintSalesMasterView(int id)
        {
            var varlist = (from a in _context.SalesMaster
                           join b in _context.AccountLedger on a.LedgerId equals b.LedgerId
                           join c in _context.Warehouse on a.WarehouseId equals c.WarehouseId
                           where a.SalesMasterId == id
                           select new SalesMasterView
                           {
                               SalesMasterId = a.SalesMasterId,
                               Date = a.Date,
                               VoucherNo = a.VoucherNo,
                               BillDiscount = a.BillDiscount,
                               TaxRate = a.TaxRate,
                               TotalTax = a.TotalTax,
                               TotalAmount = a.TotalAmount,
                               GrandTotal = a.GrandTotal,
                               UserId = a.UserId,
                               Status = a.Status,
                               ShippingAmount = a.ShippingAmount,
                               LedgerCode = b.LedgerCode,
                               LedgerName = b.LedgerName,
                               Email = b.Email,
                               Phone = b.Phone,
                               Address = b.Address,
                               WarehouseName = c.Name
                           }).FirstOrDefault();

            return varlist;
        }

        public List<SalesMasterView> SaleReportsdetails(DateTime FromDate, DateTime ToDate, int LedgerId, int WarehouseId, string Status)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@FromDate", FromDate);
                para.Add("@ToDate", ToDate);
                para.Add("@LedgerId", LedgerId);
                para.Add("@WarehouseId", WarehouseId);
                para.Add("@Status", Status);
                var ListofPlan = sqlcon.Query<SalesMasterView>("SaleReports", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }

        public List<ProductView> SalesInvoiceDetails(int SalesMasterId)
        {
            var varlist = (from a in _context.SalesDetails
                           join b in _context.Product on a.ProductId equals b.ProductId
                           join c in _context.Unit on a.UnitId equals c.UnitId
                           where a.SalesMasterId == SalesMasterId
                           select new ProductView
                           {
                               SalesDetailsId = a.SalesDetailsId,
                               ProductId = a.ProductId,
                               Qty = a.Qty,
                               UnitId = a.UnitId,
                               TaxId = a.TaxId,
                               TaxAmount = a.TaxAmount,
                               SalesRate = a.Rate,
                               Amount = a.Amount,
                               Discount = a.Discount,
                               DiscountAmount = a.DiscountAmount,
                               NetAmount = a.NetAmount,
                               ProductName = b.ProductName,
                               ProductCode = b.ProductCode,
                               UnitName = c.UnitName
                           }).ToList();

            return varlist;
        }

        public List<SalesMasterView> SalesInvoiceView(int id)
        {
            var varlist = (from a in _context.SalesMaster
                           join b in _context.AccountLedger on a.LedgerId equals b.LedgerId
                           join c in _context.VoucherType on a.VoucherTypeId equals c.VoucherTypeId
                           where a.CompanyId == id
                           select new SalesMasterView
                           {
                               SalesMasterId = a.SalesMasterId,
                               LedgerId = a.LedgerId,
                               Date = a.Date,
                               VoucherNo = a.VoucherNo,
                               GrandTotal = a.GrandTotal,
                               PaymentAmount = a.PayAmount,
                               Status = a.Status,
                               UserId = a.UserId,
                               BalanceDue = a.GrandTotal - a.PayAmount,
                               LedgerCode = b.LedgerCode,
                               LedgerName = b.LedgerName,
                               VoucherTypeName = c.VoucherTypeName
                           }).ToList();

            return varlist;
        }
        public List<SalesMasterView> SalesInvoiceViewWarehouse(int id)
        {
            var varlist = (from a in _context.SalesMaster
                           join b in _context.AccountLedger on a.LedgerId equals b.LedgerId
                           join c in _context.VoucherType on a.VoucherTypeId equals c.VoucherTypeId
                           where a.WarehouseId == id
                           select new SalesMasterView
                           {
                               SalesMasterId = a.SalesMasterId,
                               LedgerId = a.LedgerId,
                               Date = a.Date,
                               VoucherNo = a.VoucherNo,
                               GrandTotal = a.GrandTotal,
                               PaymentAmount = a.PayAmount,
                               Status = a.Status,
                               UserId = a.UserId,
                               BalanceDue = a.GrandTotal - a.PayAmount,
                               LedgerCode = b.LedgerCode,
                               LedgerName = b.LedgerName,
                               VoucherTypeName = c.VoucherTypeName
                           }).ToList();

            return varlist;
        }

        public int Save(SalesMaster model)
        {
            IDbContextTransaction dbTran = _context.Database.BeginTransaction();
            try
            {
                _context.SalesMaster.Add(model);
                _context.SaveChanges();
                int id = model.SalesMasterId;

                //OrderItems table
                foreach (var item in model.listOrder)
                {
                    //AddSalesDetails
                    SalesDetails details = new SalesDetails();
                    details.SalesMasterId = id;
                    details.ProductId = item.ProductId;
                    details.Qty = item.Qty;
                    details.UnitId = item.UnitId;
                    details.BatchId = item.BatchId;
                    details.Rate = item.Rate;
                    details.Amount = item.Amount;
                    details.NetAmount = item.NetAmount;
                    details.GrossAmount = item.GrossAmount;
                    details.Discount = item.Discount;
                    details.DiscountAmount = item.DiscountAmount;
                    details.TaxAmount = item.TaxAmount;
                    details.TaxId = item.TaxId;
                    _context.SalesDetails.Add(details);
                    _context.SaveChanges();
                    int intPurchaseDId = details.SalesDetailsId;
                    //AddStockPosting

                    StockPosting stockposting = new StockPosting();
                    stockposting.Date = model.Date;
                    stockposting.ProductId = item.ProductId;
                    stockposting.InwardQty = 0;
                    stockposting.OutwardQty =item.Qty;
                    stockposting.UnitId = item.UnitId;
                    stockposting.BatchId = item.BatchId;
                    stockposting.Rate = item.Rate;
                    stockposting.DetailsId = intPurchaseDId;
                    stockposting.InvoiceNo = model.VoucherNo;
                    stockposting.VoucherNo = model.VoucherNo;
                    stockposting.VoucherTypeId = model.VoucherTypeId;
                    stockposting.AgainstInvoiceNo = String.Empty;
                    stockposting.AgainstVoucherNo = String.Empty;
                    stockposting.AgainstVoucherTypeId = 0;
                    stockposting.WarehouseId = model.WarehouseId;
                    stockposting.StockCalculate = "Sales";
                    stockposting.CompanyId = model.CompanyId;
                    stockposting.FinancialYearId = model.FinancialYearId;
                    stockposting.AddedDate = DateTime.UtcNow;
                    _context.StockPosting.Add(stockposting);
                    _context.SaveChanges();
                }



                //LedgerPosting
                //Customer
                LedgerPosting ledger = new LedgerPosting();
                ledger.Date = model.Date;
                ledger.NepaliDate = String.Empty;
                ledger.LedgerId = model.LedgerId;
                ledger.Debit = model.GrandTotal;
                ledger.Credit = 0;
                ledger.VoucherNo = model.VoucherNo;
                ledger.DetailsId = id;
                ledger.YearId = model.FinancialYearId;
                ledger.InvoiceNo = model.VoucherNo;
                ledger.VoucherTypeId = model.VoucherTypeId;
                ledger.CompanyId = model.CompanyId;
                ledger.LongReference = model.Narration;
                ledger.ReferenceN = model.Narration;
                ledger.ChequeNo = String.Empty;
                ledger.ChequeDate = String.Empty;
                ledger.AddedDate = DateTime.UtcNow;
                _context.LedgerPosting.Add(ledger);
                _context.SaveChanges();

                //SalesAccount Ledger send with out vat
                decimal decSupplierCustomerAmount = Math.Round(model.NetAmounts - model.BillDiscount, 2);
                LedgerPosting purchaseledger = new LedgerPosting();
                purchaseledger.Date = model.Date;
                purchaseledger.NepaliDate = String.Empty;
                purchaseledger.LedgerId = 10;
                purchaseledger.Debit = 0;
                purchaseledger.Credit = decSupplierCustomerAmount;
                purchaseledger.VoucherNo = model.VoucherNo;
                purchaseledger.DetailsId = id;
                purchaseledger.YearId = model.FinancialYearId;
                purchaseledger.InvoiceNo = model.VoucherNo;
                purchaseledger.VoucherTypeId = model.VoucherTypeId;
                purchaseledger.CompanyId = model.CompanyId;
                purchaseledger.LongReference = model.Narration;
                purchaseledger.ReferenceN = model.Narration;
                purchaseledger.ChequeNo = String.Empty;
                purchaseledger.ChequeDate = String.Empty;
                purchaseledger.AddedDate = DateTime.UtcNow;
                _context.LedgerPosting.Add(purchaseledger);
                _context.SaveChanges();


                //Tax
                if (model.TotalTax > 0)
                {
                    LedgerPosting vatledger = new LedgerPosting();
                    vatledger.Date = model.Date;
                    vatledger.NepaliDate = String.Empty;
                    vatledger.LedgerId = 14;
                    vatledger.Debit = 0;
                    vatledger.Credit = model.TotalTax;
                    vatledger.VoucherNo = model.VoucherNo;
                    vatledger.DetailsId = id;
                    vatledger.YearId = model.FinancialYearId;
                    vatledger.InvoiceNo = model.VoucherNo;
                    vatledger.VoucherTypeId = model.VoucherTypeId;
                    vatledger.CompanyId = model.CompanyId;
                    vatledger.LongReference = model.Narration;
                    vatledger.ReferenceN = model.Narration;
                    vatledger.ChequeNo = String.Empty;
                    vatledger.ChequeDate = String.Empty;
                    vatledger.AddedDate = DateTime.UtcNow;
                    _context.LedgerPosting.Add(vatledger);
                    _context.SaveChanges();
                }
                dbTran.Commit();
                return id;
            }
            catch (Exception)
            {
                dbTran.Rollback();
                return 0;
            }
}

        public bool Update(SalesMaster model)
        {
            try
            {

                SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
                sqlcon.Open();
                var para = new DynamicParameters();
                para.Add("@SalesMasterId", model.SalesMasterId);
                para.Add("@SerialNo", model.SerialNo);
                para.Add("@VoucherNo", model.VoucherNo);
                para.Add("@WarehouseId", model.WarehouseId);
                para.Add("@VoucherTypeId", model.VoucherTypeId);
                para.Add("@Date", model.Date);
                para.Add("@LedgerId", model.LedgerId);
                para.Add("@Narration", model.Narration);
                para.Add("@OrderMasterId", model.OrderMasterId);
                para.Add("@TotalTax", model.TotalTax);
                para.Add("@TaxRate", model.TaxRate);
                para.Add("@DisPer", model.DisPer);
                para.Add("@BillDiscount", model.BillDiscount);
                para.Add("@ShippingAmount", model.ShippingAmount);
                para.Add("@GrandTotal", model.GrandTotal);
                para.Add("@TotalAmount", model.TotalAmount);
                para.Add("@NetAmounts", model.NetAmounts);
                para.Add("@PayAmount", model.PayAmount);
                para.Add("@BalanceDue", model.BalanceDue);
                para.Add("@Status", model.Status);
                para.Add("@PreviousDue", model.PreviousDue);
                para.Add("@PaymentId", model.PaymentId);
                para.Add("@POS", model.POS);
                para.Add("@UserId", model.UserId);
                para.Add("@FinancialYearId", model.FinancialYearId);
                para.Add("@CompanyId", model.CompanyId);
                para.Add("@AddedDate", model.AddedDate);
                para.Add("@ModifyDate", DateTime.Now);
                sqlcon.Execute("SalesInvoiceUpdate", para, null, 0, CommandType.StoredProcedure);

                //OrderItems table
                foreach (var item in model.listOrder)
                {
                    if (item.SalesDetailsId == 0)
                    {
                        //AddSalesDetails
                        SalesDetails details = new SalesDetails();

                        var paraOpening = new DynamicParameters();
                        paraOpening.Add("@SalesMasterId", model.SalesMasterId);
                        paraOpening.Add("@OrderDetailsId", item.OrderDetailsId);
                        paraOpening.Add("@ProductId", item.ProductId);
                        paraOpening.Add("@Qty", item.Qty);
                        paraOpening.Add("@UnitId", item.UnitId);
                        paraOpening.Add("@BatchId", item.BatchId);
                        paraOpening.Add("@Rate", item.Rate);
                        paraOpening.Add("@Amount", item.Amount);
                        paraOpening.Add("@NetAmount", item.NetAmount);
                        paraOpening.Add("@GrossAmount", item.GrossAmount);
                        paraOpening.Add("@Discount", item.Discount);
                        paraOpening.Add("@DiscountAmount", item.DiscountAmount);
                        paraOpening.Add("@TaxAmount", item.TaxAmount);
                        paraOpening.Add("@TaxId", item.TaxId);
                        paraOpening.Add("@JourDId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        sqlcon.Execute("SalesDetailsInsert", paraOpening, null, 0, CommandType.StoredProcedure);
                        int longdetailsId = paraOpening.Get<int>("JourDId");

                        //AddStockPosting

                        StockPosting stockposting = new StockPosting();
                        var parastock = new DynamicParameters();
                        parastock.Add("@Date", model.Date);
                        parastock.Add("@NepaliDate", string.Empty);
                        parastock.Add("@ProductId", item.ProductId);
                        parastock.Add("@InwardQty", 0);
                        parastock.Add("@OutwardQty", item.Qty);
                        parastock.Add("@UnitId", item.UnitId);
                        parastock.Add("@BatchId", item.BatchId);
                        parastock.Add("@Rate", item.Rate);
                        parastock.Add("@DetailsId", longdetailsId);
                        parastock.Add("@InvoiceNo", model.VoucherNo);
                        parastock.Add("@VoucherNo", model.VoucherNo);
                        parastock.Add("@VoucherTypeId", model.VoucherTypeId);
                        parastock.Add("@AgainstInvoiceNo", string.Empty);
                        parastock.Add("@AgainstVoucherNo", string.Empty);
                        parastock.Add("@AgainstVoucherTypeId", 0);
                        parastock.Add("@WarehouseId", model.WarehouseId);
                        parastock.Add("@StockCalculate", "Sales");
                        parastock.Add("@CompanyId", model.CompanyId);
                        parastock.Add("@FinancialYearId", model.FinancialYearId);
                        parastock.Add("@AddedDate", DateTime.Now);
                        var valuesStock = sqlcon.Query<long>("StockPostingInsert", parastock, null, true, 0, commandType: CommandType.StoredProcedure);

                    }
                    else
                    {
                        //UpdatePurchaseDetails
                        SalesDetails details = new SalesDetails();
                        var paraOpening = new DynamicParameters();
                        paraOpening.Add("@SalesDetailsId", item.SalesDetailsId);
                        paraOpening.Add("@SalesMasterId", model.SalesMasterId);
                        paraOpening.Add("@OrderDetailsId", item.OrderDetailsId);
                        paraOpening.Add("@ProductId", item.ProductId);
                        paraOpening.Add("@Qty", item.Qty);
                        paraOpening.Add("@UnitId", item.UnitId);
                        paraOpening.Add("@BatchId", item.BatchId);
                        paraOpening.Add("@Rate", item.Rate);
                        paraOpening.Add("@Amount", item.Amount);
                        paraOpening.Add("@NetAmount", item.NetAmount);
                        paraOpening.Add("@GrossAmount", item.GrossAmount);
                        paraOpening.Add("@Discount", item.Discount);
                        paraOpening.Add("@DiscountAmount", item.DiscountAmount);
                        paraOpening.Add("@TaxAmount", item.TaxAmount);
                        paraOpening.Add("@TaxId", item.TaxId);
                        sqlcon.Execute("SalesDetailsUpdate", paraOpening, null, 0, CommandType.StoredProcedure);

                        //UpdateStockPosting
                        //GetStockkPostingId
                        var returnstockpostingG = (from progm in _context.StockPosting
                                                   where progm.VoucherTypeId == model.VoucherTypeId && progm.DetailsId == item.SalesDetailsId
                                                   select progm.StockPostingId).FirstOrDefault();




                        StockPosting stockposting = new StockPosting();
                        stockposting.StockPostingId = returnstockpostingG;
                        var parastock = new DynamicParameters();
                        parastock.Add("@StockPostingId", returnstockpostingG);
                        parastock.Add("@Date", model.Date);
                        parastock.Add("@NepaliDate", string.Empty);
                        parastock.Add("@ProductId", item.ProductId);
                        parastock.Add("@InwardQty", 0);
                        parastock.Add("@OutwardQty", item.Qty);
                        parastock.Add("@UnitId", item.UnitId);
                        parastock.Add("@BatchId", item.BatchId);
                        parastock.Add("@Rate", item.Rate);
                        parastock.Add("@DetailsId", item.SalesDetailsId);
                        parastock.Add("@InvoiceNo", model.VoucherNo);
                        parastock.Add("@VoucherNo", model.VoucherNo);
                        parastock.Add("@VoucherTypeId", model.VoucherTypeId);
                        parastock.Add("@AgainstInvoiceNo", string.Empty);
                        parastock.Add("@AgainstVoucherNo", string.Empty);
                        parastock.Add("@AgainstVoucherTypeId", 0);
                        parastock.Add("@WarehouseId", model.WarehouseId);
                        parastock.Add("@StockCalculate", "Sales");
                        parastock.Add("@CompanyId", model.CompanyId);
                        parastock.Add("@FinancialYearId", model.FinancialYearId);
                        parastock.Add("@ModifyDate", DateTime.Now);
                        var valuesStock = sqlcon.Query<long>("StockPostingUpdate", parastock, null, true, 0, commandType: CommandType.StoredProcedure);

                    }

                }

                //DeleteLedgerPosting
                //using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
                //{
                    var paraScDelete = new DynamicParameters();
                    paraScDelete.Add("@DetailsId", model.SalesMasterId);
                    paraScDelete.Add("@VoucherTypeId", model.VoucherTypeId);
                    var valueScDelete = sqlcon.Query<int>("DELETE FROM LedgerPosting where DetailsId=@DetailsId AND VoucherTypeId=@VoucherTypeId", paraScDelete, null, true, 0, commandType: CommandType.Text);
                //}



                //LedgerPosting
                //Customer
                LedgerPosting ledger = new LedgerPosting();
                ledger.Date = model.Date;
                ledger.NepaliDate = String.Empty;
                ledger.LedgerId = model.LedgerId;
                ledger.Debit = model.GrandTotal;
                ledger.Credit = 0;
                ledger.VoucherNo = model.VoucherNo;
                ledger.DetailsId = model.SalesMasterId;
                ledger.YearId = model.FinancialYearId;
                ledger.InvoiceNo = model.VoucherNo;
                ledger.VoucherTypeId = model.VoucherTypeId;
                ledger.CompanyId = model.CompanyId;
                ledger.LongReference = model.Narration;
                ledger.ReferenceN = model.Narration;
                ledger.ChequeNo = String.Empty;
                ledger.ChequeDate = String.Empty;
                ledger.AddedDate = DateTime.UtcNow;
                _context.LedgerPosting.Add(ledger);
                _context.SaveChanges();

                //SalesAccount Ledger send with out vat
                decimal decSupplierCustomerAmount = Math.Round(model.NetAmounts - model.BillDiscount, 2);
                LedgerPosting purchaseledger = new LedgerPosting();
                purchaseledger.Date = model.Date;
                purchaseledger.NepaliDate = String.Empty;
                purchaseledger.LedgerId = 10;
                purchaseledger.Debit = 0;
                purchaseledger.Credit = decSupplierCustomerAmount;
                purchaseledger.VoucherNo = model.VoucherNo;
                purchaseledger.DetailsId = model.SalesMasterId;
                purchaseledger.YearId = model.FinancialYearId;
                purchaseledger.InvoiceNo = model.VoucherNo;
                purchaseledger.VoucherTypeId = model.VoucherTypeId;
                purchaseledger.CompanyId = model.CompanyId;
                purchaseledger.LongReference = model.Narration;
                purchaseledger.ReferenceN = model.Narration;
                purchaseledger.ChequeNo = String.Empty;
                purchaseledger.ChequeDate = String.Empty;
                purchaseledger.AddedDate = DateTime.UtcNow;
                _context.LedgerPosting.Add(purchaseledger);
                _context.SaveChanges();


                //Tax
                if (model.TotalTax > 0)
                {
                    LedgerPosting vatledger = new LedgerPosting();
                    vatledger.Date = model.Date;
                    vatledger.NepaliDate = String.Empty;
                    vatledger.LedgerId = 14;
                    vatledger.Debit = 0;
                    vatledger.Credit = model.TotalTax;
                    vatledger.VoucherNo = model.VoucherNo;
                    vatledger.DetailsId = model.SalesMasterId;
                    vatledger.YearId = model.FinancialYearId;
                    vatledger.InvoiceNo = model.VoucherNo;
                    vatledger.VoucherTypeId = model.VoucherTypeId;
                    vatledger.CompanyId = model.CompanyId;
                    vatledger.LongReference = model.Narration;
                    vatledger.ReferenceN = model.Narration;
                    vatledger.ChequeNo = String.Empty;
                    vatledger.ChequeDate = String.Empty;
                    vatledger.AddedDate = DateTime.UtcNow;
                    _context.LedgerPosting.Add(vatledger);
                    _context.SaveChanges();
                }

                foreach (var deleteitem in model.listDelete)
                {
                    SalesDetails x = _context.SalesDetails.Find(deleteitem.SalesDetailsId);
                    _context.SalesDetails.Remove(x);
                    _context.SaveChanges();
                }
                foreach (var deleteitem in model.listDelete)
                {
                    var returnStockposting = (from ledgerpostingss in _context.StockPosting
                                              where ledgerpostingss.DetailsId == deleteitem.SalesDetailsId && ledgerpostingss.VoucherNo == model.VoucherNo && ledgerpostingss.VoucherTypeId == model.VoucherTypeId
                                              select ledgerpostingss.StockPostingId).FirstOrDefault();
                    StockPosting returnView = _context.StockPosting.Find(returnStockposting);
                    _context.StockPosting.Remove(returnView);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
