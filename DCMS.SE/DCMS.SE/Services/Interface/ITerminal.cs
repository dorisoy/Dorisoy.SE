using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface ITerminal
    {
        List<TerminalView> GetAll(int id);
        List<TerminalView> ViewAllCustomer(int id);
        List<TerminalView> ViewAllExpensesCategory(int id);
        List<AccountGroup> ExpensesCategory();
        List<TerminalView> ViewAllSupplier(int id);
        string SerialNoCode(int id);
        Terminal Edit(int id);
        int Save(Terminal model);
        void Update(Terminal model);
        bool Delete(int id);
        bool CheckName(string name);
        int CheckNameId(string name);
    }
}
