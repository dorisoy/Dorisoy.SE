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
    public class ReceiveCustomerRepository : IReceiveCustomer
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public ReceiveCustomerRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }

        public bool ReceiveVoucherNoCheckExistence(int StoreId, int FinancialYearId, string VoucherNo)
        {
            var checkResult = (from progm in _context.ReceiptMaster
                               where progm.StoreId == StoreId && progm.FinancialYearId == FinancialYearId && progm.VoucherNo == VoucherNo
                               select progm.ReceiptMasterId).Count();
            if (checkResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteReceiveCustomer( int ReceiptMasterId, string VoucherNo, int VoucherTypeId, int FinancialYearId, int StoreId)
        {
            SqlConnection sqlcon = new (_conn.DbConn);
            try
            {



                //GetReceiveCustomer
                ReceiptMaster model = _context.ReceiptMaster.Find(ReceiptMasterId);
                //UpdateSalesInvoice

                SalesMaster returnView = _context.SalesMaster.Find(model.SalesMasterId);
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
                _context.SalesMaster.Update(returnView);
                _context.SaveChanges();
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new ("ReceiptMasterDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@ReceiptMasterId", SqlDbType.Int);
                para.Value = ReceiptMasterId;
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
        public ReceiptMaster EditReceiveMaster(int ReceiptMasterId)
        {
            using (SqlConnection sqlcon = new (_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@ReceiptMasterId", ReceiptMasterId);
                var result = sqlcon.Query<ReceiptMaster>("SELECT *FROM ReceiptMaster where ReceiptMasterId=@ReceiptMasterId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
                return result;
            }
        }

        public string GetVoucherNo(int StoreId, int FinancialYearId, int VoucherTypeId)
        {
            using (SqlConnection sqlcon = new (_conn.DbConn))
            {
                string val = string.Empty;
                var para = new DynamicParameters();
                para.Add("@StoreId", StoreId);
                para.Add("@FinancialYearId", FinancialYearId);
                para.Add("@VoucherTypeId", VoucherTypeId);
                return val = sqlcon.Query<string>("SELECT ISNULL( MAX(SerialNo+1),1) FROM ReceiptMaster where StoreId=@StoreId AND FinancialYearId=@FinancialYearId AND VoucherTypeId=@VoucherTypeId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
            }
        }

       

       
        
        public bool Save(ReceiptMaster model)
        {
            IDbContextTransaction dbTran = _context.Database.BeginTransaction();
            try
            {
                if(model.ReceiptMasterId == 0)
                {
                    _context.ReceiptMaster.Add(model);
                    _context.SaveChanges();
                    int id = model.ReceiptMasterId;



                    SalesMaster returnView = _context.SalesMaster.Find(model.SalesMasterId);
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
                    _context.SalesMaster.Update(returnView);
                    _context.SaveChanges();
                    //TerminalPosting
                    //Customer
                    TerminalPosting ledger = new TerminalPosting();
                    ledger.Date = model.Date;
                    ledger.NepaliDate = String.Empty;
                    ledger.TerminalId = model.TerminalId;
                    ledger.Debit = model.Amount;
                    ledger.Credit = 0;
                    ledger.VoucherNo = model.VoucherNo;
                    ledger.DetailsId = returnView.SalesMasterId;
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

                    //Cash
                    TerminalPosting purchaseledger = new TerminalPosting();
                    purchaseledger.Date = model.Date;
                    purchaseledger.NepaliDate = String.Empty;
                    purchaseledger.TerminalId = 1;
                    purchaseledger.Debit = 0;
                    purchaseledger.Credit = model.Amount;
                    purchaseledger.VoucherNo = model.VoucherNo;
                    purchaseledger.DetailsId = returnView.SalesMasterId;
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
                    dbTran.Commit();
                    return true;
                }
                else
                {
                    _context.ReceiptMaster.Update(model);
                    _context.SaveChanges();
                    int id = model.ReceiptMasterId;



                    SalesMaster returnView = _context.SalesMaster.Find(model.SalesMasterId);
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
                    _context.SalesMaster.Update(returnView);
                    _context.SaveChanges();



                    //DeleteTerminalPosting
                    using (SqlConnection sqlcon = new (_conn.DbConn))
                    {
                        var paraScDelete = new DynamicParameters();
                        paraScDelete.Add("@DetailsId", model.ReceiptMasterId);
                        paraScDelete.Add("@VoucherTypeId", model.VoucherTypeId);
                        var valueScDelete = sqlcon.Query<int>("DELETE FROM TerminalPosting where DetailsId=@DetailsId AND VoucherTypeId=@VoucherTypeId", paraScDelete, null, true, 0, commandType: CommandType.Text);
                    }
                    //TerminalPosting
                    //Customer
                    TerminalPosting ledger = new TerminalPosting();
                    ledger.Date = model.Date;
                    ledger.NepaliDate = String.Empty;
                    ledger.TerminalId = model.TerminalId;
                    ledger.Debit = 0;
                    ledger.Credit = model.Amount;
                    ledger.VoucherNo = model.VoucherNo;
                    ledger.DetailsId = returnView.SalesMasterId;
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

                    //Cash
                    TerminalPosting purchaseledger = new TerminalPosting();
                    purchaseledger.Date = model.Date;
                    purchaseledger.NepaliDate = String.Empty;
                    purchaseledger.TerminalId = 1;
                    purchaseledger.Debit = model.Amount;
                    purchaseledger.Credit = 0;
                    purchaseledger.VoucherNo = model.VoucherNo;
                    purchaseledger.DetailsId = returnView.SalesMasterId;
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

        public bool Update(ReceiptMaster model)
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
        public PaymentReceiveView GetPreviousDuesBalancesupplier(int TerminalId)
        {
            PaymentReceiveView info = new PaymentReceiveView();
            SqlConnection sqlcon = new (_conn.DbConn);
            SqlDataReader rdr = null;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new ("PaymentSupplierDue", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
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

        public PaymentReceiveView GetTotalReceiableAmount(int SalesMasterId)
        {
            PaymentReceiveView info = new PaymentReceiveView();
            SqlConnection sqlcon = new (_conn.DbConn);
            SqlDataReader rdr = null;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new ("SELECT ISNULL(SUM(Amount), 0) as Amount FROM ReceiptMaster where SalesMasterId=@SalesMasterId", sqlcon);
                sccmd.CommandType = CommandType.Text;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@SalesMasterId", SqlDbType.Int);
                sprmparam.Value = SalesMasterId;
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

        public List<ReceiptMaster> GetAllById(int id)
        {
            var varlist = (from a in _context.ReceiptMaster
                           where a.SalesMasterId == id
                           select new ReceiptMaster
                           {
                               ReceiptMasterId = a.ReceiptMasterId,
                               SalesMasterId = a.SalesMasterId,
                               Date = a.Date,
                               VoucherNo = a.VoucherNo,
                               Amount = a.Amount,
                               PaymentType = a.PaymentType
                           }).ToList();

            return varlist;
        }

        public ReceiptMaster EdiById(int id)
        {
            ReceiptMaster returnView = _context.ReceiptMaster.Find(id);
            return returnView;
        }

        public PaymentReceiveView ReceiveCustomerView(int ReceiptMasterId)
        {
            var varlist = (from a in _context.ReceiptMaster
                           join b in _context.Terminal on a.TerminalId equals b.TerminalId
                           where a.ReceiptMasterId == ReceiptMasterId
                           select new PaymentReceiveView
                           {
                               ReceiptMasterId = a.ReceiptMasterId,
                               Amount = a.Amount,
                               VoucherNo = a.VoucherNo,
                               Date = a.Date,
                               PaymentType = a.PaymentType,
                               TerminalName = b.TerminalName,
                               Address = b.Address,
                               Mobile = b.Mobile,
                               Email = b.Email
                           }).FirstOrDefault();

            return varlist;
        }

        public List<PaymentReceiveView> SalesReceiveView(DateTime FromDate, DateTime ToDate, int TerminalId, int SalesMasterId)
        {
            using (SqlConnection sqlcon = new (_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@FromDate", FromDate);
                para.Add("@ToDate", ToDate);
                para.Add("@TerminalId", TerminalId);
                para.Add("@SalesMasterId", SalesMasterId);
                var ListofPlan = sqlcon.Query<PaymentReceiveView>("SalesReceiveView", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }
    }
}
