using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IManufacturer
    {
        List<ManufacturerView> GetAll(int id);
        List<ManufacturerView> ViewAllCustomer(int id);
        List<ManufacturerView> ViewAllExpensesCategory(int id);
        List<AccountGroup> ExpensesCategory();
        List<ManufacturerView> ViewAllSupplier(int id);
        string SerialNoCode(int id);
        Manufacturer Edit(int id);
        int Save(Manufacturer model);
        void Update(Manufacturer model);
        bool Delete(int id);
        bool CheckName(string name);
        int CheckNameId(string name);
    }
}
