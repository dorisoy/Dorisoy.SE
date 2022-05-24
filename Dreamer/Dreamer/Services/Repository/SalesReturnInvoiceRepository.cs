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
    public class SalesReturnInvoiceRepository : ISalesReturnInvoice
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public SalesReturnInvoiceRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }

        public bool AccountSalesReturnInvoiceNoCheckExistence(int CompanyId, int FinancialYearId, string VoucherNo)
        {
            var checkResult = (from progm in _context.SalesReturnMaster
                               where progm.CompanyId == CompanyId && progm.FinancialYearId == FinancialYearId && progm.VoucherNo == VoucherNo
                               select progm.SalesReturnMasterId).Count();
            if (checkResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int AccountSalesReturnInvoiceNoCheckExistenceid(string name)
        {
            var checkResult = (from progm in _context.SalesReturnMaster
                               where progm.VoucherNo == name
                               select progm.SalesReturnMasterId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.SalesReturnMaster
                                    where progm.VoucherNo == name
                                    select progm.SalesReturnMasterId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int SalesReturnMasterId, string VoucherNo, int CompanyId, int FinancialYearId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("SalesReturnInvoiceDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@SalesReturnMasterId", SqlDbType.Int);
                para.Value = SalesReturnMasterId;
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

        public SalesReturnMaster Edit(int SalesReturnMasterId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@SalesReturnMasterId", SalesReturnMasterId);
                var result = sqlcon.Query<SalesReturnMaster>("SELECT *FROM SalesReturnMaster where SalesReturnMasterId=@SalesReturnMasterId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
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
                return val = sqlcon.Query<string>("SELECT ISNULL( MAX(SerialNo+1),1) FROM SalesReturnMaster where CompanyId=@CompanyId AND FinancialYearId=@FinancialYearId AND VoucherTypeId=@VoucherTypeId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
            }
        }

        public SalesReturnMasterView Print(int id)
        {
            var varlist = (from a in _context.SalesReturnMaster
                           join b in _context.AccountLedger on a.LedgerId equals b.LedgerId
                           join c in _context.Warehouse on a.WarehouseId equals c.WarehouseId
                           where a.SalesReturnMasterId == id
                           select new SalesReturnMasterView
                           {
                               SalesReturnMasterId = a.SalesReturnMasterId,
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

        public List<ProductView> SalesReturnInvoiceDetails(int SalesReturnMasterId)
        {
            var varlist = (from a in _context.SalesReturnDetails
                           join b in _context.Product on a.ProductId equals b.ProductId
                           join c in _context.Unit on a.UnitId equals c.UnitId
                           where a.SalesReturnMasterId == SalesReturnMasterId
                           select new ProductView
                           {
                               SalesReturnDetailsId = a.SalesReturnDetailsId,
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

        public List<SalesReturnMasterView> SalesReturnInvoiceView(int id)
        {
            var varlist = (from a in _context.SalesReturnMaster
                           join b in _context.AccountLedger on a.LedgerId equals b.LedgerId
                           join c in _context.VoucherType on a.VoucherTypeId equals c.VoucherTypeId
                           where a.CompanyId == id
                           select new SalesReturnMasterView
                           {
                               SalesReturnMasterId = a.SalesReturnMasterId,
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
        public List<SalesReturnMasterView> SalesReturnInvoiceViewwarehouse(int id)
        {
            var varlist = (from a in _context.SalesReturnMaster
                           join b in _context.AccountLedger on a.LedgerId equals b.LedgerId
                           join c in _context.VoucherType on a.VoucherTypeId equals c.VoucherTypeId
                           where a.WarehouseId == id
                           select new SalesReturnMasterView
                           {
                               SalesReturnMasterId = a.SalesReturnMasterId,
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

        public int Save(SalesReturnMaster model)
        {
            IDbContextTransaction dbTran = _context.Database.BeginTransaction();
            try
            {
                _context.SalesReturnMaster.Add(model);
                _context.SaveChanges();
                int id = model.SalesReturnMasterId;

                //OrderItems table
                foreach (var item in model.listOrder)
                {
                    //AddSalesReturnDetails
                    SalesReturnDetails details = new SalesReturnDetails();
                    details.SalesReturnMasterId = id;
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
                    _context.SalesReturnDetails.Add(details);
                    _context.SaveChanges();
                    int intPurchaseDId = details.SalesReturnDetailsId;
                    //AddStockPosting

                    StockPosting stockposting = new StockPosting();
                    stockposting.Date = model.Date;
                    stockposting.ProductId = item.ProductId;
                    stockposting.InwardQty = item.Qty;
                    stockposting.OutwardQty = 0;
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
                    stockposting.StockCalculate = "SalesReturn";
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
                ledger.Debit = 0;
                ledger.Credit = model.GrandTotal;
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
                purchaseledger.Debit = decSupplierCustomerAmount;
                purchaseledger.Credit = 0;
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
                    vatledger.Debit = model.TotalTax;
                    vatledger.Credit = 0;
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

        public bool Update(SalesReturnMaster model)
        {
            IDbContextTransaction dbTran = _context.Database.BeginTransaction();
            try
            {
                _context.SalesReturnMaster.Update(model);
                _context.SaveChanges();

                //OrderItems table
                foreach (var item in model.listOrder)
                {
                    if (item.SalesReturnDetailsId == 0)
                    {
                        //AddSalesDetails
                        SalesReturnDetails details = new SalesReturnDetails();
                        details.SalesReturnMasterId = model.SalesReturnMasterId;
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
                        _context.SalesReturnDetails.Add(details);
                        _context.SaveChanges();
                        int intPurchaseDId = details.SalesReturnDetailsId;

                        //AddStockPosting

                        StockPosting stockposting = new StockPosting();
                        stockposting.Date = model.Date;
                        stockposting.ProductId = item.ProductId;
                        stockposting.InwardQty = item.Qty;
                        stockposting.OutwardQty = 0;
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
                        stockposting.StockCalculate = "SalesReturn";
                        stockposting.CompanyId = model.CompanyId;
                        stockposting.FinancialYearId = model.FinancialYearId;
                        stockposting.AddedDate = DateTime.UtcNow;
                        _context.StockPosting.Add(stockposting);
                        _context.SaveChanges();
                    }
                    else
                    {
                        //UpdatePurchaseDetails
                        SalesReturnDetails details = new SalesReturnDetails();
                        details.SalesReturnDetailsId = item.SalesReturnDetailsId;
                        details.SalesReturnMasterId = model.SalesReturnMasterId;
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
                        details.SalesDetailsId = item.SalesDetailsId;
                        _context.SalesReturnDetails.Update(details);
                        _context.SaveChanges();

                        //UpdateStockPosting
                        //GetStockkPostingId
                        var returnstockpostingG = (from progm in _context.StockPosting
                                                   where progm.VoucherTypeId == model.VoucherTypeId && progm.DetailsId == item.SalesReturnDetailsId
                                                   select progm.StockPostingId).FirstOrDefault();




                        StockPosting stockposting = new StockPosting();
                        stockposting.StockPostingId = returnstockpostingG;
                        stockposting.Date = model.Date;
                        stockposting.ProductId = item.ProductId;
                        stockposting.InwardQty = 0;
                        stockposting.OutwardQty = item.Qty;
                        stockposting.UnitId = item.UnitId;
                        stockposting.BatchId = item.BatchId;
                        stockposting.Rate = item.Rate;
                        stockposting.DetailsId = item.SalesReturnDetailsId;
                        stockposting.InvoiceNo = model.VoucherNo;
                        stockposting.VoucherNo = model.VoucherNo;
                        stockposting.VoucherTypeId = model.VoucherTypeId;
                        stockposting.AgainstInvoiceNo = String.Empty;
                        stockposting.AgainstVoucherNo = String.Empty;
                        stockposting.AgainstVoucherTypeId = 0;
                        stockposting.WarehouseId = model.WarehouseId;
                        stockposting.StockCalculate = "SalesReturn";
                        stockposting.CompanyId = model.CompanyId;
                        stockposting.FinancialYearId = model.FinancialYearId;
                        stockposting.AddedDate = DateTime.UtcNow;
                        _context.StockPosting.Update(stockposting);
                        _context.SaveChanges();
                    }
                    
                }

                //DeleteLedgerPosting
                using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
                {
                    var paraScDelete = new DynamicParameters();
                    paraScDelete.Add("@DetailsId", model.SalesReturnMasterId);
                    paraScDelete.Add("@VoucherTypeId", model.VoucherTypeId);
                    var valueScDelete = sqlcon.Query<int>("DELETE FROM LedgerPosting where DetailsId=@DetailsId AND VoucherTypeId=@VoucherTypeId", paraScDelete, null, true, 0, commandType: CommandType.Text);
                }



                //LedgerPosting
                //Customer
                LedgerPosting ledger = new LedgerPosting();
                ledger.Date = model.Date;
                ledger.NepaliDate = String.Empty;
                ledger.LedgerId = model.LedgerId;
                ledger.Debit = 0;
                ledger.Credit = model.GrandTotal;
                ledger.VoucherNo = model.VoucherNo;
                ledger.DetailsId = model.SalesReturnMasterId;
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
                purchaseledger.Debit = decSupplierCustomerAmount;
                purchaseledger.Credit = 0;
                purchaseledger.VoucherNo = model.VoucherNo;
                purchaseledger.DetailsId = model.SalesReturnMasterId;
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
                    vatledger.Debit = model.TotalTax;
                    vatledger.Credit = 0;
                    vatledger.VoucherNo = model.VoucherNo;
                    vatledger.DetailsId = model.SalesReturnMasterId;
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
                    SalesReturnDetails x = _context.SalesReturnDetails.Find(deleteitem.DeleteSalesReturnId);
                    _context.SalesReturnDetails.Remove(x);
                    _context.SaveChanges();
                }
                foreach (var deleteitem in model.listDelete)
                {
                    var returnStockposting = (from ledgerpostingss in _context.StockPosting
                                              where ledgerpostingss.DetailsId == deleteitem.DeleteSalesReturnId && ledgerpostingss.VoucherNo == model.VoucherNo && ledgerpostingss.VoucherTypeId == model.VoucherTypeId
                                              select ledgerpostingss.StockPostingId).FirstOrDefault();
                    StockPosting returnView = _context.StockPosting.Find(returnStockposting);
                    _context.StockPosting.Remove(returnView);
                    _context.SaveChanges();
                }
                dbTran.Commit();
                return true;
            }
            catch (Exception)
            {
                dbTran.Rollback();
                return false;
            }
        }
    }
}
