using Dapper;
using DCMS.SE.Data;
using DCMS.SE.Data.Connection;
using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using DCMS.SE.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DCMS.SE.Services.Repository
{
    public class ExpensesMasterRepository : IExpensesMaster
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public ExpensesMasterRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }

        public bool ExpensesVoucherNoCheckExistence(int StoreId, int FinancialYearId, string VoucherNo)
        {
            var checkResult = (from progm in _context.ExpenseMaster
                               where progm.StoreId == StoreId && progm.FinancialYearId == FinancialYearId && progm.VoucherNo == VoucherNo
                               select progm.ExpensiveMasterId).Count();
            if (checkResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete( int ExpensiveMasterId , string VoucherNo, int VoucherTypeId, int FinancialYearId, int StoreId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("ExpenseDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@ExpensiveMasterId", SqlDbType.Int);
                para.Value = ExpensiveMasterId;
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
        public ExpenseMaster EdiById(int ExpensiveMasterId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@ExpensiveMasterId", ExpensiveMasterId);
                var result = sqlcon.Query<ExpenseMaster>("SELECT *FROM ExpenseMaster where ExpensiveMasterId=@ExpensiveMasterId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
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
                return val = sqlcon.Query<string>("SELECT ISNULL( MAX(SerialNo+1),1) FROM PaymentMaster where StoreId=@StoreId AND FinancialYearId=@FinancialYearId AND VoucherTypeId=@VoucherTypeId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
            }
        }

       

       
        
        public bool Save(ExpenseMaster model)
        {
            IDbContextTransaction dbTran = _context.Database.BeginTransaction();
            try
            {
                    _context.ExpenseMaster.Add(model);
                    _context.SaveChanges();
                    int id = model.ExpensiveMasterId;



                    //TerminalPosting
                    //Supplier
                    TerminalPosting ledger = new TerminalPosting();
                    ledger.Date = model.Date;
                    ledger.NepaliDate = String.Empty;
                    ledger.TerminalId = model.TerminalId;
                    ledger.Debit = model.Amount;
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

                    //Cash
                    TerminalPosting purchaseledger = new TerminalPosting();
                    purchaseledger.Date = model.Date;
                    purchaseledger.NepaliDate = String.Empty;
                    purchaseledger.TerminalId = 1;
                    purchaseledger.Debit = 0;
                    purchaseledger.Credit = model.Amount;
                    purchaseledger.VoucherNo = model.VoucherNo;
                    purchaseledger.DetailsId = id;
                    purchaseledger.YearId = 1;
                    purchaseledger.InvoiceNo = model.VoucherNo;
                    purchaseledger.VoucherTypeId = model.VoucherTypeId;
                    purchaseledger.StoreId = 1;
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

            catch (Exception)
            {
                dbTran.Rollback();
                return false;
            }
        }

        public bool Update(ExpenseMaster model)
        {
            IDbContextTransaction dbTran = _context.Database.BeginTransaction();
            try
            {
                _context.ExpenseMaster.Update(model);
                _context.SaveChanges();

                //DeleteTerminalPosting
                using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
                {
                    var paraScDelete = new DynamicParameters();
                    paraScDelete.Add("@DetailsId", model.ExpensiveMasterId);
                    paraScDelete.Add("@VoucherTypeId", model.VoucherTypeId);
                    var valueScDelete = sqlcon.Query<int>("DELETE FROM TerminalPosting where DetailsId=@DetailsId AND VoucherTypeId=@VoucherTypeId", paraScDelete, null, true, 0, commandType: CommandType.Text);
                }

                //TerminalPosting
                //Supplier
                TerminalPosting ledger = new TerminalPosting();
                ledger.Date = model.Date;
                ledger.NepaliDate = String.Empty;
                ledger.TerminalId = model.TerminalId;
                ledger.Debit = model.Amount;
                ledger.Credit = 0;
                ledger.VoucherNo = model.VoucherNo;
                ledger.DetailsId = model.ExpensiveMasterId;
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
                purchaseledger.DetailsId = model.ExpensiveMasterId;
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

            catch (Exception)
            {
                dbTran.Rollback();
                return false;
            }
        }

        public List<ExpensesMasterView> ViewAll(int id)
        {
            var varlist = (from a in _context.ExpenseMaster
                           join b in _context.Terminal on a.TerminalId equals b.TerminalId
                           join c in _context.Warehouse on a.WarehouseId equals c.WarehouseId
                           where a.StoreId == id
                           select new ExpensesMasterView
                           {
                               ExpensiveMasterId = a.ExpensiveMasterId,
                               Date = a.Date,
                               Amount = a.Amount,
                               VoucherNo = a.VoucherNo,
                               Narration = a.Narration,
                               TerminalName = b.TerminalName,
                               WarehouseName = c.Name
                           }).ToList();

            return varlist;
        }
    }
}
