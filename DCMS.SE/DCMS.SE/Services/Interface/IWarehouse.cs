using DCMS.SE.Data.Setting;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IWarehouse
    {
        List<Warehouse> GetAll();
        bool CheckName(string name);
        int CheckNameId(string name);
        Warehouse Edit(int id);
        int Save(Warehouse model);
        void Update(Warehouse model);
        bool Delete(int id);
    }
}
