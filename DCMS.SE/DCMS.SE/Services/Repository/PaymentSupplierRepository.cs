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

        public bool PaymentVoucherNoCheckExistence(int CompanyId, int FinancialYearId, string VoucherNo)
        {
            var checkResult = (from progm in _context.PaymentMaster
                               where progm.CompanyId == CompanyId && progm.FinancialYearId == FinancialYearId && progm.VoucherNo == VoucherNo
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

        public bool DeletePaymentSupplier( int PaymentMasterId, string VoucherNo, int VoucherTypeId, int FinancialYearId, int CompanyId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
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
                returnView.BalanceDue = returnView.BalanceDue + model.Amount;
                returnView.PayAmount = returnView.GrandTotal - decBaDue;
                _context.PurchaseMaster.Update(returnView);
                _context.SaveChanges();
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("PaymentMasterDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@PaymentMasterId", SqlDbType.Int);
                para.Value = PaymentMasterId;
                para = cmd.Parameters.Add("@VoucherNo", SqlDbType.NVarChar);
                para.Value = VoucherNo;
                para = cmd.Parameters.Add("@VoucherTypeId", SqlDbType.Int);
                para.Value = VoucherTypeId;
                para = cmd.Parameters.Add("@FinancialYearId", SqlDbType.Int);
                para.Value = FinancialYearId;
                para = cmd.Parameters.Add("@CompanyId", SqlDbType.Int);
                para.Value = CompanyId;
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
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@PaymentMasterId", PaymentMasterId);
                var result = sqlcon.Query<PaymentMaster>("SELECT *FROM PaymentMaster where PaymentMasterId=@PaymentMasterId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
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
                return val = sqlcon.Query<string>("SELECT ISNULL( MAX(SerialNo+1),1) FROM PaymentMaster where CompanyId=@CompanyId AND FinancialYearId=@FinancialYearId AND VoucherTypeId=@VoucherTypeId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
            }
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
                    returnView.PayAmount = returnView.PayAmount + model.Amount;
                    _context.PurchaseMaster.Update(returnView);
                    _context.SaveChanges();
                    //LedgerPosting
                    //Supplier
                    LedgerPosting ledger = new LedgerPosting();
                    ledger.Date = model.Date;
                    ledger.NepaliDate = String.Empty;
                    ledger.LedgerId = model.LedgerId;
                    ledger.Debit = model.Amount;
                    ledger.Credit = 0;
                    ledger.VoucherNo = model.VoucherNo;
                    ledger.DetailsId = returnView.PurchaseMasterId;
                    ledger.YearId = 1;
                    ledger.InvoiceNo = model.VoucherNo;
                    ledger.VoucherTypeId = model.VoucherTypeId;
                    ledger.CompanyId = 1;
                    ledger.LongReference = model.Narration;
                    ledger.ReferenceN = model.Narration;
                    ledger.ChequeNo = String.Empty;
                    ledger.ChequeDate = String.Empty;
                    ledger.AddedDate = DateTime.UtcNow;
                    _context.LedgerPosting.Add(ledger);
                    _context.SaveChanges();

                    //Cash
                    LedgerPosting purchaseledger = new LedgerPosting();
                    purchaseledger.Date = model.Date;
                    purchaseledger.NepaliDate = String.Empty;
                    purchaseledger.LedgerId = 1;
                    purchaseledger.Debit = 0;
                    purchaseledger.Credit = model.Amount;
                    purchaseledger.VoucherNo = model.VoucherNo;
                    purchaseledger.DetailsId = returnView.PurchaseMasterId;
                    purchaseledger.YearId = 1;
                    purchaseledger.InvoiceNo = model.VoucherNo;
                    purchaseledger.VoucherTypeId = model.VoucherTypeId;
                    purchaseledger.CompanyId = 1;
                    purchaseledger.LongReference = model.Narration;
                    purchaseledger.ReferenceN = model.Narration;
                    purchaseledger.ChequeNo = String.Empty;
                    purchaseledger.ChequeDate = String.Empty;
                    purchaseledger.AddedDate = DateTime.UtcNow;
                    _context.LedgerPosting.Add(purchaseledger);
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



                    //DeleteLedgerPosting
                    using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
                    {
                        var paraScDelete = new DynamicParameters();
                        paraScDelete.Add("@DetailsId", model.PaymentMasterId);
                        paraScDelete.Add("@VoucherTypeId", model.VoucherTypeId);
                        var valueScDelete = sqlcon.Query<int>("DELETE FROM LedgerPosting where DetailsId=@DetailsId AND VoucherTypeId=@VoucherTypeId", paraScDelete, null, true, 0, commandType: CommandType.Text);
                    }
                    //LedgerPosting
                    //Supplier
                    LedgerPosting ledger = new LedgerPosting();
                    ledger.Date = model.Date;
                    ledger.NepaliDate = String.Empty;
                    ledger.LedgerId = model.LedgerId;
                    ledger.Debit = model.Amount;
                    ledger.Credit = 0;
                    ledger.VoucherNo = model.VoucherNo;
                    ledger.DetailsId = returnView.PurchaseMasterId;
                    ledger.YearId = 1;
                    ledger.InvoiceNo = model.VoucherNo;
                    ledger.VoucherTypeId = model.VoucherTypeId;
                    ledger.CompanyId = 1;
                    ledger.LongReference = model.Narration;
                    ledger.ReferenceN = model.Narration;
                    ledger.ChequeNo = String.Empty;
                    ledger.ChequeDate = String.Empty;
                    ledger.AddedDate = DateTime.UtcNow;
                    _context.LedgerPosting.Add(ledger);
                    _context.SaveChanges();

                    //Cash
                    LedgerPosting purchaseledger = new LedgerPosting();
                    purchaseledger.Date = model.Date;
                    purchaseledger.NepaliDate = String.Empty;
                    purchaseledger.LedgerId = 1;
                    purchaseledger.Debit = 0;
                    purchaseledger.Credit = model.Amount;
                    purchaseledger.VoucherNo = model.VoucherNo;
                    purchaseledger.DetailsId = returnView.PurchaseMasterId;
                    purchaseledger.YearId = 1;
                    purchaseledger.InvoiceNo = model.VoucherNo;
                    purchaseledger.VoucherTypeId = model.VoucherTypeId;
                    purchaseledger.CompanyId = 1;
                    purchaseledger.LongReference = model.Narration;
                    purchaseledger.ReferenceN = model.Narration;
                    purchaseledger.ChequeNo = String.Empty;
                    purchaseledger.ChequeDate = String.Empty;
                    purchaseledger.AddedDate = DateTime.UtcNow;
                    _context.LedgerPosting.Add(purchaseledger);
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

        public PaymentReceiveView GetPreviousDuesBalancesupplier(int LedgerId)
        {
            PaymentReceiveView info = new PaymentReceiveView();
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            SqlDataReader rdr = null;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("PaymentSupplierDue", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@LedgerId", SqlDbType.Int);
                sprmparam.Value = LedgerId;
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
            PaymentReceiveView info = new PaymentReceiveView();
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            SqlDataReader rdr = null;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("SELECT ISNULL(SUM(Amount), 0) as Amount FROM PaymentMaster where PurchaseMasterId=@PurchaseMasterId", sqlcon);
                sccmd.CommandType = CommandType.Text;
                SqlParameter sprmparam = new SqlParameter();
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
                           join b in _context.AccountLedger on a.LedgerId equals b.LedgerId
                           where a.PaymentMasterId == PaymentMasterId
                           select new PaymentReceiveView
                           {
                               PaymentMasterId = a.PaymentMasterId,
                               Amount = a.Amount,
                               VoucherNo = a.VoucherNo,
                               Date = a.Date,
                               PaymentType = a.PaymentType,
                               LedgerName = b.LedgerName,
                               Address = b.Address,
                               Mobile = b.Mobile,
                               Email = b.Email
                           }).FirstOrDefault();

            return varlist;
        }

        public List<PaymentReceiveView> PurchasePaymentView(DateTime FromDate , DateTime ToDate, int LedgerId , int PurchaseMasterId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@FromDate", FromDate);
                para.Add("@ToDate", ToDate);
                para.Add("@LedgerId", LedgerId);
                para.Add("@PurchaseMasterId", PurchaseMasterId);
                var ListofPlan = sqlcon.Query<PaymentReceiveView>("PurchasePaymentView", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }
    }
}
