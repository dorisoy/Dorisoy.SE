using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IExpensesMaster
    {
        string GetVoucherNo(int StoreId, int FinancialYearId, int VoucherTypeId);
        bool ExpensesVoucherNoCheckExistence(int StoreId, int FinancialYearId, string VoucherNo);
        ExpenseMaster EdiById(int id);
        bool Save(ExpenseMaster model);
        bool Update(ExpenseMaster model);
        bool Delete( int ExpensiveMasterId, string VoucherNo, int VoucherTypeId, int StoreId, int FinancialYearId);

        List<ExpensesMasterView> ViewAll(int id);

    }
}
