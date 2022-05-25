using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IAccountGroup
    {
        List<AccountGroup> GetAll();
        List<AccountGroupView> ViewAllAccountGroup();
        bool CheckName(string name);
        int CheckNameId(string name);
        AccountGroup Edit(int id);
        bool Save(AccountGroup model);
        bool Update(AccountGroup model);
        bool Delete(int id);
    }
}
