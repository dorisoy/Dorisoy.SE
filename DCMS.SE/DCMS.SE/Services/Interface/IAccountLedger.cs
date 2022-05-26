﻿using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IAccountLedger
    {
        List<AccountLedgerView> GetAll(int id);
        List<AccountLedgerView> ViewAllCustomer(int id);
        List<AccountLedgerView> ViewAllExpensesCategory(int id);
        List<AccountGroup> ExpensesCategory();
        List<AccountLedgerView> ViewAllSupplier(int id);
        string SerialNoCode(int id);
        AccountLedger Edit(int id);
        int Save(AccountLedger model);
        void Update(AccountLedger model);
        bool Delete(int id);
        bool CheckName(string name);
        int CheckNameId(string name);
    }
}