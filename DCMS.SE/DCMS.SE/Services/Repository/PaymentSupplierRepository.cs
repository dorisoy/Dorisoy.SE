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
    public class PaymentSupplierRepository : IPaymentSupplier
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public PaymentSupplierRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }

        public bool PaymentVoucherNoCheckExistence(int StoreId, int FinancialYearId, string VoucherNo)
        {
            var checkResult = (from progm in _context.PaymentMaster
                               where progm.StoreId == StoreId && progm.FinancialYearId == FinancialYearId && progm.VoucherNo == VoucherNo
                               select progm.PaymentMasterId).Count();
            if (checkResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeletePaymentSupplier( int PaymentMasterId, string VoucherNo, int VoucherTypeId, int FinancialYearId, int StoreId)
        {
            SqlConnection sqlcon = new (_conn.DbConn);
            try
            {
                //GetPaymentSupplier
                PaymentMaster model = _context.PaymentMaster.Find(PaymentMasterId);
                //UpdatePurchaseInvoice

                PurchaseMaster returnView = _context.PurchaseMaster.Find(model.PurchaseMasterId);
                decimal decDue = 0;
                decimal decGrand = model.Amount;
                decimal decCashBankAmount = model.PreviousDue;
                decDue = decGrand - decCashBankAmount;
                if (decDue == 0)
                {
                    returnView.Status = "Paid";
                }
                else if (returnView.GrandTotal > decDue)
                {
                    returnView.Status = "部分支付";
                }
                else
                {
                    returnView.Status = "草稿";
                }
                decimal DueBalance = model.PreviousDue;
                decimal decBaDue = returnView.BalanceDue + model.Amount;
                returnView.BalanceDue += model.Amount;
                returnView.PayAmount = returnView.GrandTotal - decBaDue;
                _context.PurchaseMaster.Update(returnView);
                _context.SaveChanges();
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new("PaymentMasterDelete", sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter para = new ();
                para = cmd.Parameters.Add("@PaymentMasterId", SqlDbType.Int);
                para.Value = PaymentMasterId;
                para = cmd.Parameters.Add("@VoucherNo", SqlDbType.NVarChar);
                para.Value = VoucherNo;
                para = cmd.Parameters.Add("@VoucherTypeId", SqlDbType.Int);
                para.Value = VoucherTypeId;
                para = cmd.Parameters.Add("@FinancialYearId", SqlDbType.Int);
                para.Value = FinancialYearId;
                para = cmd.Parameters.Add("@StoreId", SqlDbType.Int);
                para.Value = StoreId;
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
        public PaymentMaster EditPaymentMaster(int PaymentMasterId)
        {
            using SqlConnection sqlcon = new(_conn.DbConn);
            var para = new DynamicParameters();
            para.Add("@PaymentMasterId", PaymentMasterId);
            var result = sqlcon.Query<PaymentMaster>("SELECT *FROM PaymentMaster where PaymentMasterId=@PaymentMasterId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
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
            return val = sqlcon.Query<string>("SELECT ISNULL( MAX(SerialNo+1),1) FROM PaymentMaster where StoreId=@StoreId AND FinancialYearId=@FinancialYearId AND VoucherTypeId=@VoucherTypeId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
        }

       

       
        
        public bool Save(PaymentMaster model)
        {
            IDbContextTransaction dbTran = _context.Database.BeginTransaction();
            try
            {
                if(model.PaymentMasterId == 0)
                {
                    _context.PaymentMaster.Add(model);
                    _context.SaveChanges();
                    int id = model.PaymentMasterId;



                    PurchaseMaster returnView = _context.PurchaseMaster.Find(model.PurchaseMasterId);
                    decimal decDue = 0;
                    decimal decGrand = model.Amount;
                    decimal decCashBankAmount = model.PreviousDue;
                    decDue = decGrand - decCashBankAmount;
                    if (decDue == 0)
                    {
                        returnView.Status = "Paid";
                    }
                    else if (returnView.GrandTotal > decDue)
                    {
                        returnView.Status = "部分支付";
                    }
                    else
                    {
                        returnView.Status = "草稿";
                    }
                    decimal DueBalance = model.PreviousDue;
                    returnView.BalanceDue = returnView.GrandTotal - (model.Amount + returnView.PayAmount);
                    returnView.PayAmount += model.Amount;
                    _context.PurchaseMaster.Update(returnView);
                    _context.SaveChanges();
                    //TerminalPosting
                    //Supplier
                    ManufacturerPosting ledger = new()
                    {
                        Date = model.Date,
                        NepaliDate = String.Empty,
                        ManufacturerId = model.ManufacturerId,
                        Debit = model.Amount,
                        Credit = 0,
                        VoucherNo = model.VoucherNo,
                        DetailsId = returnView.PurchaseMasterId,
                        YearId = 1,
                        InvoiceNo = model.VoucherNo,
                        VoucherTypeId = model.VoucherTypeId,
                        StoreId = 1,
                        LongReference = model.Narration,
                        ReferenceN = model.Narration,
                        ChequeNo = String.Empty,
                        ChequeDate = String.Empty,
                        AddedDate = DateTime.UtcNow
                    };
                    _context.ManufacturerPosting.Add(ledger);
                    _context.SaveChanges();

                    //Cash
                    TerminalPosting purchaseledger = new()
                    {
                        Date = model.Date,
                        NepaliDate = String.Empty,
                        TerminalId = 1,
                        Debit = 0,
                        Credit = model.Amount,
                        VoucherNo = model.VoucherNo,
                        DetailsId = returnView.PurchaseMasterId,
                        YearId = 1,
                        InvoiceNo = model.VoucherNo,
                        VoucherTypeId = model.VoucherTypeId,
                        StoreId = 1,
                        LongReference = model.Narration,
                        ReferenceN = model.Narration,
                        ChequeNo = String.Empty,
                        ChequeDate = String.Empty,
                        AddedDate = DateTime.UtcNow
                    };
                    _context.TerminalPosting.Add(purchaseledger);
                    _context.SaveChanges();
                    dbTran.Commit();
                    return true;
                }
                else
                {
                    _context.PaymentMaster.Update(model);
                    _context.SaveChanges();
                    int id = model.PaymentMasterId;



                    PurchaseMaster returnView = _context.PurchaseMaster.Find(model.PurchaseMasterId);
                    decimal decDue = 0;
                    decimal decGrand = returnView.GrandTotal;
                    decimal decCashBankAmount = returnView.BalanceDue;
                    decDue = decGrand - decCashBankAmount;
                    if (decDue == 0)
                    {
                        returnView.Status = "Paid";
                    }
                    else if (returnView.GrandTotal > decDue)
                    {
                        returnView.Status = "部分支付";
                    }
                    else
                    {
                        returnView.Status = "草稿";
                    }
                    decimal DueBalance = (returnView.GrandTotal) - (returnView.BalanceDue + model.Amount);
                    returnView.BalanceDue = DueBalance;
                    _context.PurchaseMaster.Update(returnView);
                    _context.SaveChanges();



                    //ManufacturerPosting
                    using (SqlConnection sqlcon = new (_conn.DbConn))
                    {
                        var paraScDelete = new DynamicParameters();
                        paraScDelete.Add("@DetailsId", model.PaymentMasterId);
                        paraScDelete.Add("@VoucherTypeId", model.VoucherTypeId);
                        var valueScDelete = sqlcon.Query<int>("DELETE FROM ManufacturerPosting where DetailsId=@DetailsId AND VoucherTypeId=@VoucherTypeId", paraScDelete, null, true, 0, commandType: CommandType.Text);
                    }
                    //ManufacturerPosting
                    //Supplier
                    ManufacturerPosting ledger = new ()
                    {
                        Date = model.Date,
                        NepaliDate = String.Empty,
                        ManufacturerId = model.ManufacturerId,
                        Debit = model.Amount,
                        Credit = 0,
                        VoucherNo = model.VoucherNo,
                        DetailsId = returnView.PurchaseMasterId,
                        YearId = 1,
                        InvoiceNo = model.VoucherNo,
                        VoucherTypeId = model.VoucherTypeId,
                        StoreId = 1,
                        LongReference = model.Narration,
                        ReferenceN = model.Narration,
                        ChequeNo = String.Empty,
                        ChequeDate = String.Empty,
                        AddedDate = DateTime.UtcNow
                    };
                    _context.ManufacturerPosting.Add(ledger);
                    _context.SaveChanges();

                    //Cash
                    TerminalPosting purchaseledger = new()
                    {
                        Date = model.Date,
                        NepaliDate = String.Empty,
                        TerminalId = 1,
                        Debit = 0,
                        Credit = model.Amount,
                        VoucherNo = model.VoucherNo,
                        DetailsId = returnView.PurchaseMasterId,
                        YearId = 1,
                        InvoiceNo = model.VoucherNo,
                        VoucherTypeId = model.VoucherTypeId,
                        StoreId = 1,
                        LongReference = model.Narration,
                        ReferenceN = model.Narration,
                        ChequeNo = String.Empty,
                        ChequeDate = String.Empty,
                        AddedDate = DateTime.UtcNow
                    };
                    _context.TerminalPosting.Add(purchaseledger);
                    _context.SaveChanges();
                    dbTran.Commit();
                    return true;
                }
                
            }
            
            catch (Exception)
            {
                dbTran.Rollback();
                return false;
            }
        }

        public bool Update(PurchaseMaster model)
        {
            IDbContextTransaction dbTran = _context.Database.BeginTransaction();
            try
            {
                return true;
            }
            catch (Exception)
            {
                dbTran.Rollback();
                return false;
            }
        }

        public bool Update(PaymentMaster model)
        {
            throw new NotImplementedException();
        }

        public PaymentReceiveView GetPreviousDuesBalancesupplier(int TerminalId)
        {
            PaymentReceiveView info = new ();
            SqlConnection sqlcon = new (_conn.DbConn);
            SqlDataReader rdr = null;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new("PaymentSupplierDue", sqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter sprmparam = new ();
                sprmparam = sccmd.Parameters.Add("@TerminalId", SqlDbType.Int);
                sprmparam.Value = TerminalId;
                rdr = sccmd.ExecuteReader();
                while (rdr.Read())
                {
                    info.DueBalance = Convert.ToDecimal(rdr["DueBalance"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                rdr.Close();
                sqlcon.Close();
            }
            return info;
        }

        public PaymentReceiveView GetTotalReceiableAmount(int PurchaseMasterId)
        {
            PaymentReceiveView info = new ();
            SqlConnection sqlcon = new (_conn.DbConn);
            SqlDataReader rdr = null;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new("SELECT ISNULL(SUM(Amount), 0) as Amount FROM PaymentMaster where PurchaseMasterId=@PurchaseMasterId", sqlcon)
                {
                    CommandType = CommandType.Text
                };
                SqlParameter sprmparam = new ();
                sprmparam = sccmd.Parameters.Add("@PurchaseMasterId", SqlDbType.Int);
                sprmparam.Value = PurchaseMasterId;
                rdr = sccmd.ExecuteReader();
                while (rdr.Read())
                {
                    info.Amount = Convert.ToDecimal(rdr["Amount"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                rdr.Close();
                sqlcon.Close();
            }
            return info;
        }

        public List<PaymentMaster> GetAllById(int id)
        {
            var varlist = (from a in _context.PaymentMaster
                           where a.PurchaseMasterId == id
                           select new PaymentMaster
                           {
                               PaymentMasterId = a.PaymentMasterId,
                               PurchaseMasterId = a.PurchaseMasterId,
                               Date = a.Date,
                               VoucherNo = a.VoucherNo,
                               Amount = a.Amount,
                               PaymentType = a.PaymentType
                           }).ToList();

            return varlist;
        }

        public PaymentMaster EdiById(int id)
        {
            PaymentMaster returnView = _context.PaymentMaster.Find(id);
            return returnView;
        }

        public PaymentReceiveView PaymentSupplierView(int PaymentMasterId)
        {
            var varlist = (from a in _context.PaymentMaster
                           join b in _context.Manufacturer on a.ManufacturerId equals b.ManufacturerId
                           where a.PaymentMasterId == PaymentMasterId
                           select new PaymentReceiveView
                           {
                               PaymentMasterId = a.PaymentMasterId,
                               Amount = a.Amount,
                               VoucherNo = a.VoucherNo,
                               Date = a.Date,
                               PaymentType = a.PaymentType,
                               ManufacturerName = b.ManufacturerName,
                               Address = b.Address,
                               Mobile = b.Mobile,
                               Email = b.Email
                           }).FirstOrDefault();

            return varlist;
        }

        public List<PaymentReceiveView> PurchasePaymentView(DateTime FromDate , DateTime ToDate, int TerminalId , int PurchaseMasterId)
        {
            using SqlConnection sqlcon = new(_conn.DbConn);
            var para = new DynamicParameters();
            para.Add("@FromDate", FromDate);
            para.Add("@ToDate", ToDate);
            para.Add("@TerminalId", TerminalId);
            para.Add("@PurchaseMasterId", PurchaseMasterId);
            var ListofPlan = sqlcon.Query<PaymentReceiveView>("PurchasePaymentView", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
            return ListofPlan;
        }
    }
}
