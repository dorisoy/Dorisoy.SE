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
    public class PurchaseInvoiceRepository : IPurchaseInvoice
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public PurchaseInvoiceRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }

        public bool AccountPurchseInvoiceNoCheckExistence(int StoreId, int FinancialYearId, string VoucherNo)
        {
            var checkResult = (from progm in _context.PurchaseMaster
                               where progm.StoreId == StoreId && progm.FinancialYearId == FinancialYearId && progm.VoucherNo == VoucherNo
                               select progm.PurchaseMasterId).Count();
            if (checkResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int AccountPurchseInvoiceNoCheckExistenceid(string name)
        {
            var checkResult = (from progm in _context.PurchaseMaster
                               where progm.VoucherNo == name
                               select progm.PurchaseMasterId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.PurchaseMaster
                                    where progm.VoucherNo == name
                                    select progm.PurchaseMasterId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public bool DeletePurchseInvoice(int PurchaseMasterId, string VoucherNo, int StoreId, int FinancialYearId)
        {
            SqlConnection sqlcon = new (_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new ("PurchseInvoiceDelete", sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter para = new ();
                para = cmd.Parameters.Add("@PurchaseMasterId", SqlDbType.Int);
                para.Value = PurchaseMasterId;
                para = cmd.Parameters.Add("@VoucherNo", SqlDbType.NVarChar);
                para.Value = VoucherNo;
                para = cmd.Parameters.Add("@StoreId", SqlDbType.Int);
                para.Value = StoreId;
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

        public PurchaseMaster EditPurchaseMaster(int PurchaseMasterId)
        {
            using SqlConnection sqlcon = new(_conn.DbConn);
            var para = new DynamicParameters();
            para.Add("@PurchaseMasterId", PurchaseMasterId);
            var result = sqlcon.Query<PurchaseMaster>("SELECT *FROM PurchaseMaster where PurchaseMasterId=@PurchaseMasterId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
            return result;
        }

        public string GetVoucherNo(int StoreId, int FinancialYearId, int VoucherTypeId)
        {
            using SqlConnection sqlcon = new(_conn.DbConn);
            string val = string.Empty;
            var para = new DynamicParameters();
            para.Add("@StoreId", StoreId);
            para.Add("@FinancialYearId", FinancialYearId);
            para.Add("@VoucherTypeId", VoucherTypeId);

            return val = sqlcon.Query<string>("SELECT ISNULL( MAX(SerialNo+1),1) FROM PurchaseMaster where StoreId=@StoreId AND FinancialYearId=@FinancialYearId AND VoucherTypeId=@VoucherTypeId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
        }

        public PurchaseMasterView PrintPurchaseMasterView(int id)
        {
            var varlist = (from a in _context.PurchaseMaster
                           join b in _context.Manufacturer on a.TerminalId equals b.ManufacturerId
                           join c in _context.Warehouse on a.WarehouseId equals c.WarehouseId
                           where a.PurchaseMasterId == id
                           select new PurchaseMasterView
                           {
                               PurchaseMasterId = a.PurchaseMasterId,
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
                               TerminalCode = b.ManufacturerCode,
                               TerminalName = b.ManufacturerName,
                               Email = b.Email,
                               Phone = b.Phone,
                               Address = b.Address,
                               WarehouseName = c.Name
                           }).FirstOrDefault();

            return varlist;
        }

        public List<PurchaseMasterView> PurchaseInvoiceDetails(int id, DateTime fromDate, DateTime toDate)
        {
            var varlist = (from a in _context.PurchaseMaster
                           join b in _context.Manufacturer on a.TerminalId equals b.ManufacturerId
                           join c in _context.VoucherType on a.VoucherTypeId equals c.VoucherTypeId
                           where a.StoreId == id && a.Date >= fromDate && a.Date <= toDate
                           select new PurchaseMasterView
                           {
                               PurchaseMasterId = a.PurchaseMasterId,
                               Date = a.Date,
                               VoucherNo = a.VoucherNo,
                               GrandTotal = a.GrandTotal,
                               Status = a.Status,
                               UserId = a.UserId,
                               BalanceDue = a.GrandTotal - a.PreviousDue,
                               TerminalCode = b.ManufacturerCode,
                               TerminalName = b.ManufacturerName,
                               VoucherTypeName = c.VoucherTypeName
                           }).ToList();

            return varlist;
        }

        public List<ProductView> PurchaseInvoiceDetails(int PurchaseMasterId)
        {
            var varlist = (from a in _context.PurchaseDetails
                           join b in _context.Product on a.ProductId equals b.ProductId
                           join c in _context.Unit on a.UnitId equals c.UnitId
                           where a.PurchaseMasterId == PurchaseMasterId
                           select new ProductView
                           {
                               PurchaseDetailsId = a.PurchaseDetailsId,
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

        public List<PurchaseMasterView> PurchaseInvoiceView(int id)
        {
            var varlist = (from a in _context.PurchaseMaster
                           join b in _context.Manufacturer on a.TerminalId equals b.ManufacturerId
                           join c in _context.VoucherType on a.VoucherTypeId equals c.VoucherTypeId
                           where a.StoreId == id
                           select new PurchaseMasterView
                           {
                               PurchaseMasterId = a.PurchaseMasterId,
                               TerminalId = a.TerminalId,
                               Date = a.Date,
                               VoucherNo = a.VoucherNo,
                               GrandTotal = a.GrandTotal,
                               PaymentAmount = a.PayAmount,
                               Status = a.Status,
                               UserId = a.UserId,
                               BalanceDue = a.BalanceDue,
                               TerminalCode = b.ManufacturerCode,
                               TerminalName = b.ManufacturerName,
                               VoucherTypeName = c.VoucherTypeName
                           }).ToList();

            return varlist;
        }
        public List<PurchaseMasterView> PurchaseInvoiceViewwarehouse(int id)
        {
            var varlist = (from a in _context.PurchaseMaster
                           join b in _context.Manufacturer on a.TerminalId equals b.ManufacturerId
                           join c in _context.VoucherType on a.VoucherTypeId equals c.VoucherTypeId
                           where a.WarehouseId == id
                           select new PurchaseMasterView
                           {
                               PurchaseMasterId = a.PurchaseMasterId,
                               TerminalId = a.TerminalId,
                               Date = a.Date,
                               VoucherNo = a.VoucherNo,
                               GrandTotal = a.GrandTotal,
                               PaymentAmount = a.PayAmount,
                               Status = a.Status,
                               UserId = a.UserId,
                               BalanceDue = a.BalanceDue,
                               TerminalCode = b.ManufacturerCode,
                               TerminalName = b.ManufacturerName,
                               VoucherTypeName = c.VoucherTypeName
                           }).ToList();

            return varlist;
        }

        public int Save(PurchaseMaster model)
        {
            IDbContextTransaction dbTran = _context.Database.BeginTransaction();
            try
            {
                _context.PurchaseMaster.Add(model);
                _context.SaveChanges();
                int id = model.PurchaseMasterId;

                //OrderItems table
                foreach (var item in model.listOrder)
                {
                    //AddPurchaseDetails
                    PurchaseDetails details = new()
                    {
                        PurchaseMasterId = id,
                        ProductId = item.ProductId,
                        Qty = item.Qty,
                        UnitId = item.UnitId,
                        BatchId = item.BatchId,
                        Rate = item.Rate,
                        Amount = item.Amount,
                        NetAmount = item.NetAmount,
                        GrossAmount = item.GrossAmount,
                        Discount = item.Discount,
                        DiscountAmount = item.DiscountAmount,
                        TaxAmount = item.TaxAmount,
                        TaxId = item.TaxId
                    };
                    _context.PurchaseDetails.Add(details);
                    _context.SaveChanges();
                    int intPurchaseDId = details.PurchaseDetailsId;
                    //AddStockPosting

                    StockPosting stockposting = new()
                    {
                        Date = model.Date,
                        ProductId = item.ProductId,
                        InwardQty = item.Qty,
                        OutwardQty = 0,
                        UnitId = item.UnitId,
                        BatchId = item.BatchId,
                        Rate = item.Rate,
                        DetailsId = intPurchaseDId,
                        InvoiceNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherTypeId = model.VoucherTypeId,
                        AgainstInvoiceNo = String.Empty,
                        AgainstVoucherNo = String.Empty,
                        AgainstVoucherTypeId = 0,
                        WarehouseId = model.WarehouseId,
                        StockCalculate = "Purchase",
                        StoreId = model.StoreId,
                        FinancialYearId = model.FinancialYearId,
                        AddedDate = DateTime.UtcNow
                    };
                    _context.StockPosting.Add(stockposting);
                    _context.SaveChanges();
                }



                //ManufacturerPosting
                //Supplier
                ManufacturerPosting Manufacturer = new()
                {
                    Date = model.Date,
                    NepaliDate = String.Empty,
                    ManufacturerId = model.TerminalId,
                    Debit = 0,
                    Credit = model.GrandTotal,
                    VoucherNo = model.VoucherNo,
                    DetailsId = id,
                    YearId = model.FinancialYearId,
                    InvoiceNo = model.VoucherNo,
                    VoucherTypeId = model.VoucherTypeId,
                    StoreId = model.StoreId,
                    LongReference = model.Narration,
                    ReferenceN = model.Narration,
                    ChequeNo = String.Empty,
                    ChequeDate = String.Empty,
                    AddedDate = DateTime.UtcNow
                };
                _context.ManufacturerPosting.Add(Manufacturer);
                _context.SaveChanges();

                //PurchaseAccount Manufacturer send with out vat
                decimal decSupplierCustomerAmount = Math.Round(model.NetAmounts - model.BillDiscount, 2);
                ManufacturerPosting purchaseManufacturer = new()
                {
                    Date = model.Date,
                    NepaliDate = String.Empty,
                    ManufacturerId = 11,
                    Debit = decSupplierCustomerAmount,
                    Credit = 0,
                    VoucherNo = model.VoucherNo,
                    DetailsId = id,
                    YearId = model.FinancialYearId,
                    InvoiceNo = model.VoucherNo,
                    VoucherTypeId = model.VoucherTypeId,
                    StoreId = model.StoreId,
                    LongReference = model.Narration,
                    ReferenceN = model.Narration,
                    ChequeNo = String.Empty,
                    ChequeDate = String.Empty,
                    AddedDate = DateTime.UtcNow
                };
                _context.ManufacturerPosting.Add(purchaseManufacturer);
                _context.SaveChanges();


                //Tax
                if (model.TotalTax > 0)
                {
                    ManufacturerPosting vatManufacturer = new()
                    {
                        Date = model.Date,
                        NepaliDate = String.Empty,
                        ManufacturerId = 14,
                        Debit = model.TotalTax,
                        Credit = 0,
                        VoucherNo = model.VoucherNo,
                        DetailsId = id,
                        YearId = model.FinancialYearId,
                        InvoiceNo = model.VoucherNo,
                        VoucherTypeId = model.VoucherTypeId,
                        StoreId = model.StoreId,
                        LongReference = model.Narration,
                        ReferenceN = model.Narration,
                        ChequeNo = String.Empty,
                        ChequeDate = String.Empty,
                        AddedDate = DateTime.UtcNow
                    };
                    _context.ManufacturerPosting.Add(vatManufacturer);
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

        public bool Update(PurchaseMaster model)
        {
            try
            {
                SqlConnection sqlcon = new (_conn.DbConn);
                sqlcon.Open();
                var para = new DynamicParameters();
                para.Add("@PurchaseMasterId", model.PurchaseMasterId);
                para.Add("@SerialNo", model.SerialNo);
                para.Add("@VoucherNo", model.VoucherNo);
                para.Add("@WarehouseId", model.WarehouseId);
                para.Add("@VoucherTypeId", model.VoucherTypeId);
                para.Add("@Date", model.Date);
                para.Add("@ManufacturerId", model.TerminalId);
                para.Add("@Narration", model.Narration);
                para.Add("@PurchaseOrderMasterId", model.PurchaseOrderMasterId);
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
                para.Add("@UserId", model.UserId);
                para.Add("@FinancialYearId", model.FinancialYearId);
                para.Add("@StoreId", model.StoreId);
                para.Add("@AddedDate", model.AddedDate);
                para.Add("@ModifyDate", DateTime.Now);
                sqlcon.Execute("PurchaseInvoiceUpdate", para, null, 0, CommandType.StoredProcedure);


                //OrderItems table
                foreach (var item in model.listOrder)
                {
                    if (item.PurchaseDetailsId == 0)
                    {
                        //AddPurchaseDetails
                        PurchaseDetails details = new ();

                        var paraOpening = new DynamicParameters();
                        paraOpening.Add("@PurchaseMasterId", model.PurchaseMasterId);
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
                        sqlcon.Execute("PurchaseDetailsInsert", paraOpening, null, 0, CommandType.StoredProcedure);
                        int longdetailsId = paraOpening.Get<int>("JourDId");

                        //AddStockPosting

                        StockPosting stockposting = new ();
                        var parastock = new DynamicParameters();
                        parastock.Add("@Date", model.Date);
                        parastock.Add("@NepaliDate", string.Empty);
                        parastock.Add("@ProductId", item.ProductId);
                        parastock.Add("@InwardQty", item.Qty);
                        parastock.Add("@OutwardQty", 0);
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
                        parastock.Add("@StockCalculate", "Purchase");
                        parastock.Add("@StoreId", model.StoreId);
                        parastock.Add("@FinancialYearId", model.FinancialYearId);
                        parastock.Add("@AddedDate", DateTime.Now);
                        var valuesStock = sqlcon.Query<long>("StockPostingInsert", parastock, null, true, 0, commandType: CommandType.StoredProcedure);

                    }
                    else
                    {
                        //UpdatePurchaseDetails

                        PurchaseDetails details = new ();

                        var paraOpening = new DynamicParameters();
                        paraOpening.Add("@PurchaseDetailsId", item.PurchaseDetailsId);
                        paraOpening.Add("@PurchaseMasterId", model.PurchaseMasterId);
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
                        sqlcon.Execute("PurchaseDetailsUpdate", paraOpening, null, 0, CommandType.StoredProcedure);

                        //UpdateStockPosting
                        //GetStockkPostingId
                        var returnstockpostingG = (from progm in _context.StockPosting
                                                   where progm.VoucherTypeId == model.VoucherTypeId && progm.DetailsId == item.PurchaseDetailsId
                                                   select progm.StockPostingId).FirstOrDefault();




                        StockPosting stockposting = new()
                        {
                            StockPostingId = returnstockpostingG
                        };
                        var parastock = new DynamicParameters();
                        parastock.Add("@StockPostingId", returnstockpostingG);
                        parastock.Add("@Date", model.Date);
                        parastock.Add("@NepaliDate", string.Empty);
                        parastock.Add("@ProductId", item.ProductId);
                        parastock.Add("@InwardQty", item.Qty);
                        parastock.Add("@OutwardQty", 0);
                        parastock.Add("@UnitId", item.UnitId);
                        parastock.Add("@BatchId", item.BatchId);
                        parastock.Add("@Rate", item.Rate);
                        parastock.Add("@DetailsId", item.PurchaseDetailsId);
                        parastock.Add("@InvoiceNo", model.VoucherNo);
                        parastock.Add("@VoucherNo", model.VoucherNo);
                        parastock.Add("@VoucherTypeId", model.VoucherTypeId);
                        parastock.Add("@AgainstInvoiceNo", string.Empty);
                        parastock.Add("@AgainstVoucherNo", string.Empty);
                        parastock.Add("@AgainstVoucherTypeId", 0);
                        parastock.Add("@WarehouseId", model.WarehouseId);
                        parastock.Add("@StockCalculate", "Purchase");
                        parastock.Add("@StoreId", model.StoreId);
                        parastock.Add("@FinancialYearId", model.FinancialYearId);
                        parastock.Add("@ModifyDate", DateTime.Now);
                        var valuesStock = sqlcon.Query<long>("StockPostingUpdate", parastock, null, true, 0, commandType: CommandType.StoredProcedure);

                    }

                }

                //DeleteManufacturerPosting
                    var paraScDelete = new DynamicParameters();
                    paraScDelete.Add("@DetailsId", model.PurchaseMasterId);
                    paraScDelete.Add("@VoucherTypeId", model.VoucherTypeId);
                    var valueScDelete = sqlcon.Query<int>("DELETE FROM ManufacturerPosting where DetailsId=@DetailsId AND VoucherTypeId=@VoucherTypeId", paraScDelete, null, true, 0, commandType: CommandType.Text);


                //ManufacturerPosting
                //Supplier
                ManufacturerPosting Manufacturer = new()
                {
                    Date = model.Date,
                    NepaliDate = String.Empty,
                    ManufacturerId = model.TerminalId,
                    Debit = 0,
                    Credit = model.GrandTotal,
                    VoucherNo = model.VoucherNo,
                    DetailsId = model.PurchaseMasterId,
                    YearId = model.FinancialYearId,
                    InvoiceNo = model.VoucherNo,
                    VoucherTypeId = model.VoucherTypeId,
                    StoreId = model.StoreId,
                    LongReference = model.Narration,
                    ReferenceN = model.Narration,
                    ChequeNo = String.Empty,
                    ChequeDate = String.Empty,
                    AddedDate = DateTime.UtcNow
                };
                _context.ManufacturerPosting.Add(Manufacturer);
                _context.SaveChanges();

                //PurchaseAccount Manufacturer send with out vat
                decimal decSupplierCustomerAmount = Math.Round(model.NetAmounts - model.BillDiscount, 2);
                ManufacturerPosting purchaseManufacturer = new()
                {
                    Date = model.Date,
                    NepaliDate = String.Empty,
                    ManufacturerId = 11,
                    Debit = decSupplierCustomerAmount,
                    Credit = 0,
                    VoucherNo = model.VoucherNo,
                    DetailsId = model.PurchaseMasterId,
                    YearId = model.FinancialYearId,
                    InvoiceNo = model.VoucherNo,
                    VoucherTypeId = model.VoucherTypeId,
                    StoreId = model.StoreId,
                    LongReference = model.Narration,
                    ReferenceN = model.Narration,
                    ChequeNo = String.Empty,
                    ChequeDate = String.Empty,
                    AddedDate = DateTime.UtcNow
                };
                _context.ManufacturerPosting.Add(purchaseManufacturer);
                _context.SaveChanges();


                //Tax
                if (model.TotalTax > 0)
                {
                    ManufacturerPosting vatManufacturer = new()
                    {
                        Date = model.Date,
                        NepaliDate = String.Empty,
                        ManufacturerId = 14,
                        Debit = model.TotalTax,
                        Credit = 0,
                        VoucherNo = model.VoucherNo,
                        DetailsId = model.PurchaseMasterId,
                        YearId = model.FinancialYearId,
                        InvoiceNo = model.VoucherNo,
                        VoucherTypeId = model.VoucherTypeId,
                        StoreId = model.StoreId,
                        LongReference = model.Narration,
                        ReferenceN = model.Narration,
                        ChequeNo = String.Empty,
                        ChequeDate = String.Empty,
                        AddedDate = DateTime.UtcNow
                    };
                    _context.ManufacturerPosting.Add(vatManufacturer);
                    _context.SaveChanges();
                }

                foreach (var deleteitem in model.listDelete)
                {
                    PurchaseDetails x = _context.PurchaseDetails.Find(deleteitem.PurchaseDetailsId);
                    _context.PurchaseDetails.Remove(x);
                    _context.SaveChanges();
                }
                foreach (var deleteitem in model.listDelete)
                {
                    var returnStockposting = (from Manufacturerpostingss in _context.StockPosting
                                              where Manufacturerpostingss.DetailsId == deleteitem.PurchaseDetailsId && Manufacturerpostingss.VoucherNo == model.VoucherNo && Manufacturerpostingss.VoucherTypeId == model.VoucherTypeId
                                              select Manufacturerpostingss.StockPostingId).FirstOrDefault();
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
        public List<PurchaseMasterView> PurchaseReportsdetails(DateTime FromDate, DateTime ToDate, int ManufacturerId, int WarehouseId, string Status)
        {
            using SqlConnection sqlcon = new(_conn.DbConn);
            var para = new DynamicParameters();
            para.Add("@FromDate", FromDate);
            para.Add("@ToDate", ToDate);
            para.Add("@ManufacturerId", ManufacturerId);
            para.Add("@WarehouseId", WarehouseId);
            para.Add("@Status", Status);
            var ListofPlan = sqlcon.Query<PurchaseMasterView>("PurchaseReports", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
            return ListofPlan;
        }
    }
}
