using Dapper;
using DCMS.SE.Data;
using DCMS.SE.Data.Connection;
using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.Setting;
using DCMS.SE.Data.ViewModel;
using DCMS.SE.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DCMS.SE.Services.Repository
{
    public class PurchaseReturnInvoiceRepository : IPurchaseReturnInvoice
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public PurchaseReturnInvoiceRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }

        public bool AccountPurchseReturnInvoiceNoCheckExistence(int StoreId, int FinancialYearId, string VoucherNo)
        {
            var checkResult = (from progm in _context.PurchaseReturnMaster
                               where progm.StoreId == StoreId && progm.FinancialYearId == FinancialYearId && progm.VoucherNo == VoucherNo
                               select progm.PurchaseReturnMasterId).Count();
            if (checkResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int AccountPurchseReturnInvoiceNoCheckExistenceid(string name)
        {
            var checkResult = (from progm in _context.PurchaseReturnMaster
                               where progm.VoucherNo == name
                               select progm.PurchaseReturnMasterId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.PurchaseReturnMaster
                                    where progm.VoucherNo == name
                                    select progm.PurchaseReturnMasterId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int PurchaseReturnMasterId, string VoucherNo, int StoreId, int FinancialYearId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("PurchaseReturnInvoiceDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@PurchaseReturnMasterId", SqlDbType.Int);
                para.Value = PurchaseReturnMasterId;
                para = cmd.Parameters.Add("@VoucherNo", SqlDbType.NVarChar);
                para.Value = VoucherNo;
                para = cmd.Parameters.Add("@StoreId", SqlDbType.BigInt);
                para.Value = StoreId;
                para = cmd.Parameters.Add("@FinancialYearId", SqlDbType.BigInt);
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

        public PurchaseReturnMaster Edit(int PurchaseReturnMasterId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@PurchaseReturnMasterId", PurchaseReturnMasterId);
                var result = sqlcon.Query<PurchaseReturnMaster>("SELECT *FROM PurchaseReturnMaster where PurchaseReturnMasterId=@PurchaseReturnMasterId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
                return result;
            }
        }

        public string GetVoucherNo(int StoreId, int FinancialYearId, int VoucherTypeId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                string val = string.Empty;
                var para = new DynamicParameters();
                para.Add("@StoreId", StoreId);
                para.Add("@FinancialYearId", FinancialYearId);
                para.Add("@VoucherTypeId", VoucherTypeId);
                return val = sqlcon.Query<string>("SELECT ISNULL( MAX(SerialNo+1),1) FROM PurchaseReturnMaster where StoreId=@StoreId AND FinancialYearId=@FinancialYearId AND VoucherTypeId=@VoucherTypeId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
            }
        }

        public PurchaseReturnMasterView Print(int id)
        {
            var varlist = (from a in _context.PurchaseReturnMaster
                           join b in _context.Terminal on a.TerminalId equals b.TerminalId
                           join c in _context.Warehouse on a.WarehouseId equals c.WarehouseId
                           where a.PurchaseReturnMasterId == id
                           select new PurchaseReturnMasterView
                           {
                               PurchaseReturnMasterId = a.PurchaseReturnMasterId,
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
                               TerminalCode = b.TerminalCode,
                               TerminalName = b.TerminalName,
                               Email = b.Email,
                               Phone = b.Phone,
                               Address = b.Address,
                               WarehouseName = c.Name
                           }).FirstOrDefault();

            return varlist;
        }

        public List<ProductView> PurchaseReturnInvoiceDetails(int PurchaseReturnMasterId)
        {
            var varlist = (from a in _context.PurchaseReturnDetails
                           join b in _context.Product on a.ProductId equals b.ProductId
                           join c in _context.Unit on a.UnitId equals c.UnitId
                           where a.PurchaseReturnMasterId == PurchaseReturnMasterId
                           select new ProductView
                           {
                               PurchaseReturnDetailsId = a.PurchaseReturnDetailsId,
                               ProductId = a.ProductId,
                               Qty = a.Qty,
                               UnitId = a.UnitId,
                               TaxId = a.TaxId,
                               TaxAmount = a.TaxAmount,
                               PurchaseRate = a.Rate,
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

        public List<PurchaseReturnMasterView> PurchaseReturnInvoiceView(int id)
        {
            var varlist = (from a in _context.PurchaseReturnMaster
                           join b in _context.Terminal on a.TerminalId equals b.TerminalId
                           join c in _context.VoucherType on a.VoucherTypeId equals c.VoucherTypeId
                           where a.StoreId == id
                           select new PurchaseReturnMasterView
                           {
                               PurchaseReturnMasterId = a.PurchaseReturnMasterId,
                               TerminalId = a.TerminalId,
                               Date = a.Date,
                               VoucherNo = a.VoucherNo,
                               GrandTotal = a.GrandTotal,
                               PaymentAmount = a.PayAmount,
                               Status = a.Status,
                               UserId = a.UserId,
                               BalanceDue = a.BalanceDue,
                               TerminalCode = b.TerminalCode,
                               TerminalName = b.TerminalName,
                               VoucherTypeName = c.VoucherTypeName
                           }).ToList();

            return varlist;
        }
        public List<PurchaseReturnMasterView> PurchaseReturnInvoiceViewwarehouse(int id)
        {
            var varlist = (from a in _context.PurchaseReturnMaster
                           join b in _context.Terminal on a.TerminalId equals b.TerminalId
                           join c in _context.VoucherType on a.VoucherTypeId equals c.VoucherTypeId
                           where a.WarehouseId == id
                           select new PurchaseReturnMasterView
                           {
                               PurchaseReturnMasterId = a.PurchaseReturnMasterId,
                               TerminalId = a.TerminalId,
                               Date = a.Date,
                               VoucherNo = a.VoucherNo,
                               GrandTotal = a.GrandTotal,
                               PaymentAmount = a.PayAmount,
                               Status = a.Status,
                               UserId = a.UserId,
                               BalanceDue = a.BalanceDue,
                               TerminalCode = b.TerminalCode,
                               TerminalName = b.TerminalName,
                               VoucherTypeName = c.VoucherTypeName
                           }).ToList();

            return varlist;
        }

        public int Save(PurchaseReturnMaster model)
        {
            IDbContextTransaction dbTran = _context.Database.BeginTransaction();
            try
            {
                _context.PurchaseReturnMaster.Add(model);
                _context.SaveChanges();
                int id = model.PurchaseReturnMasterId;

                //OrderItems table
                foreach (var item in model.listOrder)
                {
                    //AddPurchaseDetails
                    PurchaseReturnDetails details = new PurchaseReturnDetails();
                    details.PurchaseReturnMasterId = id;
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
                    _context.PurchaseReturnDetails.Add(details);
                    _context.SaveChanges();
                    int intPurchaseDId = details.PurchaseReturnDetailsId;
                    //AddStockPosting

                    StockPosting stockposting = new StockPosting();
                    stockposting.Date = model.Date;
                    stockposting.ProductId = item.ProductId;
                    stockposting.InwardQty = 0;
                    stockposting.OutwardQty = item.Qty;
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
                    stockposting.StockCalculate = "PurchaseReturn";
                    stockposting.StoreId = model.StoreId;
                    stockposting.FinancialYearId = model.FinancialYearId;
                    stockposting.AddedDate = DateTime.UtcNow;
                    _context.StockPosting.Add(stockposting);
                    _context.SaveChanges();
                }



                //TerminalPosting
                //Supplier
                TerminalPosting ledger = new TerminalPosting();
                ledger.Date = model.Date;
                ledger.NepaliDate = String.Empty;
                ledger.TerminalId = model.TerminalId;
                ledger.Debit = model.GrandTotal;
                ledger.Credit = 0;
                ledger.VoucherNo = model.VoucherNo;
                ledger.DetailsId = id;
                ledger.YearId = model.FinancialYearId;
                ledger.InvoiceNo = model.VoucherNo;
                ledger.VoucherTypeId = model.VoucherTypeId;
                ledger.StoreId = model.StoreId;
                ledger.LongReference = model.Narration;
                ledger.ReferenceN = model.Narration;
                ledger.ChequeNo = String.Empty;
                ledger.ChequeDate = String.Empty;
                ledger.AddedDate = DateTime.UtcNow;
                _context.TerminalPosting.Add(ledger);
                _context.SaveChanges();

                //PurchaseAccount Terminal send with out vat
                decimal decSupplierCustomerAmount = Math.Round(model.NetAmounts - model.BillDiscount, 2);
                TerminalPosting purchaseledger = new TerminalPosting();
                purchaseledger.Date = model.Date;
                purchaseledger.NepaliDate = String.Empty;
                purchaseledger.TerminalId = 11;
                purchaseledger.Debit = 0;
                purchaseledger.Credit = decSupplierCustomerAmount;
                purchaseledger.VoucherNo = model.VoucherNo;
                purchaseledger.DetailsId = id;
                purchaseledger.YearId = model.FinancialYearId;
                purchaseledger.InvoiceNo = model.VoucherNo;
                purchaseledger.VoucherTypeId = model.VoucherTypeId;
                purchaseledger.StoreId = model.StoreId;
                purchaseledger.LongReference = model.Narration;
                purchaseledger.ReferenceN = model.Narration;
                purchaseledger.ChequeNo = String.Empty;
                purchaseledger.ChequeDate = String.Empty;
                purchaseledger.AddedDate = DateTime.UtcNow;
                _context.TerminalPosting.Add(purchaseledger);
                _context.SaveChanges();


                //Tax
                if (model.TotalTax > 0)
                {
                    TerminalPosting vatledger = new TerminalPosting();
                    vatledger.Date = model.Date;
                    vatledger.NepaliDate = String.Empty;
                    vatledger.TerminalId = 14;
                    vatledger.Debit = 0;
                    vatledger.Credit = model.TotalTax;
                    vatledger.VoucherNo = model.VoucherNo;
                    vatledger.DetailsId = id;
                    vatledger.YearId = model.FinancialYearId;
                    vatledger.InvoiceNo = model.VoucherNo;
                    vatledger.VoucherTypeId = model.VoucherTypeId;
                    vatledger.StoreId = model.StoreId;
                    vatledger.LongReference = model.Narration;
                    vatledger.ReferenceN = model.Narration;
                    vatledger.ChequeNo = String.Empty;
                    vatledger.ChequeDate = String.Empty;
                    vatledger.AddedDate = DateTime.UtcNow;
                    _context.TerminalPosting.Add(vatledger);
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

        public bool Update(PurchaseReturnMaster model)
        {
            try
            {
                _context.PurchaseReturnMaster.Update(model);
                _context.SaveChanges();

                //OrderItems table
                foreach (var item in model.listOrder)
                {
                    if (item.PurchaseReturnDetailsId == 0)
                    {
                        //AddPurchaseReturnDetails
                        PurchaseReturnDetails details = new PurchaseReturnDetails();
                        details.PurchaseReturnMasterId = model.PurchaseReturnMasterId;
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
                        _context.PurchaseReturnDetails.Add(details);
                        _context.SaveChanges();
                        int intPurchaseDId = details.PurchaseReturnDetailsId;

                        //AddStockPosting

                        StockPosting stockposting = new StockPosting();
                        stockposting.Date = model.Date;
                        stockposting.ProductId = item.ProductId;
                        stockposting.InwardQty = 0;
                        stockposting.OutwardQty = item.Qty;
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
                        stockposting.StockCalculate = "PurchaseReturn";
                        stockposting.StoreId = model.StoreId;
                        stockposting.FinancialYearId = model.FinancialYearId;
                        stockposting.AddedDate = DateTime.UtcNow;
                        _context.StockPosting.Add(stockposting);
                        _context.SaveChanges();
                    }
                    else
                    {
                        //UpdatePurchaseReturnDetails
                        PurchaseReturnDetails details = new PurchaseReturnDetails();
                        details.PurchaseReturnDetailsId = item.PurchaseReturnDetailsId;
                        details.PurchaseReturnMasterId = model.PurchaseReturnMasterId;
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
                        _context.PurchaseReturnDetails.Update(details);
                        _context.SaveChanges();

                        //UpdateStockPosting
                        //GetStockkPostingId
                        var returnstockpostingG = (from progm in _context.StockPosting
                                                   where progm.VoucherTypeId == model.VoucherTypeId && progm.DetailsId == item.PurchaseReturnDetailsId
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
                        stockposting.DetailsId = item.PurchaseDetailsId;
                        stockposting.InvoiceNo = model.VoucherNo;
                        stockposting.VoucherNo = model.VoucherNo;
                        stockposting.VoucherTypeId = model.VoucherTypeId;
                        stockposting.AgainstInvoiceNo = String.Empty;
                        stockposting.AgainstVoucherNo = String.Empty;
                        stockposting.AgainstVoucherTypeId = 0;
                        stockposting.WarehouseId = model.WarehouseId;
                        stockposting.StockCalculate = "PurchaseReturn";
                        stockposting.StoreId = model.StoreId;
                        stockposting.FinancialYearId = model.FinancialYearId;
                        stockposting.AddedDate = DateTime.UtcNow;
                        _context.StockPosting.Update(stockposting);
                        _context.SaveChanges();
                    }
                    
                }

                //DeleteTerminalPosting
                using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
                {
                    var paraScDelete = new DynamicParameters();
                    paraScDelete.Add("@DetailsId", model.PurchaseReturnMasterId);
                    paraScDelete.Add("@VoucherTypeId", model.VoucherTypeId);
                    var valueScDelete = sqlcon.Query<int>("DELETE FROM TerminalPosting where DetailsId=@DetailsId AND VoucherTypeId=@VoucherTypeId", paraScDelete, null, true, 0, commandType: CommandType.Text);
                }



                //TerminalPosting
                //Supplier
                TerminalPosting ledger = new TerminalPosting();
                ledger.Date = model.Date;
                ledger.NepaliDate = String.Empty;
                ledger.TerminalId = model.TerminalId;
                ledger.Debit = model.GrandTotal;
                ledger.Credit = 0;
                ledger.VoucherNo = model.VoucherNo;
                ledger.DetailsId = model.PurchaseMasterId;
                ledger.YearId = model.FinancialYearId;
                ledger.InvoiceNo = model.VoucherNo;
                ledger.VoucherTypeId = model.VoucherTypeId;
                ledger.StoreId = model.StoreId;
                ledger.LongReference = model.Narration;
                ledger.ReferenceN = model.Narration;
                ledger.ChequeNo = String.Empty;
                ledger.ChequeDate = String.Empty;
                ledger.AddedDate = DateTime.UtcNow;
                _context.TerminalPosting.Add(ledger);
                _context.SaveChanges();

                //PurchaseAccount Terminal send with out vat
                decimal decSupplierCustomerAmount = Math.Round(model.NetAmounts - model.BillDiscount, 2);
                TerminalPosting purchaseledger = new TerminalPosting();
                purchaseledger.Date = model.Date;
                purchaseledger.NepaliDate = String.Empty;
                purchaseledger.TerminalId = 11;
                purchaseledger.Debit = 0;
                purchaseledger.Credit = decSupplierCustomerAmount;
                purchaseledger.VoucherNo = model.VoucherNo;
                purchaseledger.DetailsId = model.PurchaseMasterId;
                purchaseledger.YearId = model.FinancialYearId;
                purchaseledger.InvoiceNo = model.VoucherNo;
                purchaseledger.VoucherTypeId = model.VoucherTypeId;
                purchaseledger.StoreId = model.StoreId;
                purchaseledger.LongReference = model.Narration;
                purchaseledger.ReferenceN = model.Narration;
                purchaseledger.ChequeNo = String.Empty;
                purchaseledger.ChequeDate = String.Empty;
                purchaseledger.AddedDate = DateTime.UtcNow;
                _context.TerminalPosting.Add(purchaseledger);
                _context.SaveChanges();


                //Tax
                if (model.TotalTax > 0)
                {
                    TerminalPosting vatledger = new TerminalPosting();
                    vatledger.Date = model.Date;
                    vatledger.NepaliDate = String.Empty;
                    vatledger.TerminalId = 14;
                    vatledger.Debit = 0;
                    vatledger.Credit = model.TotalTax;
                    vatledger.VoucherNo = model.VoucherNo;
                    vatledger.DetailsId = model.PurchaseMasterId;
                    vatledger.YearId = model.FinancialYearId;
                    vatledger.InvoiceNo = model.VoucherNo;
                    vatledger.VoucherTypeId = model.VoucherTypeId;
                    vatledger.StoreId = model.StoreId;
                    vatledger.LongReference = model.Narration;
                    vatledger.ReferenceN = model.Narration;
                    vatledger.ChequeNo = String.Empty;
                    vatledger.ChequeDate = String.Empty;
                    vatledger.AddedDate = DateTime.UtcNow;
                    _context.TerminalPosting.Add(vatledger);
                    _context.SaveChanges();
                }

                foreach (var deleteitem in model.listDelete)
                {
                    PurchaseReturnDetails x = _context.PurchaseReturnDetails.Find(deleteitem.DeletePurchaseReturnId);
                    _context.PurchaseReturnDetails.Remove(x);
                    _context.SaveChanges();
                }
                foreach (var deleteitem in model.listDelete)
                {
                    var returnStockposting = (from ledgerpostingss in _context.StockPosting
                                              where ledgerpostingss.DetailsId == deleteitem.DeletePurchaseReturnId && ledgerpostingss.VoucherNo == model.VoucherNo && ledgerpostingss.VoucherTypeId == model.VoucherTypeId
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
