using DCMS.SE.Data.Inventory;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface ICurrency
    {
        List<Currency> GetAll();
        bool CheckName(string name);
        int CheckNameId(string name);
        Currency Edit(int id);
        int Save(Currency model);
        void Update(Currency model);
        bool Delete(int id);
    }
}
