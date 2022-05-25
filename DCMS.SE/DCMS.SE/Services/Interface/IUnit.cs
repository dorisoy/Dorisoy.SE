using DCMS.SE.Data.Setting;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IUnit
    {
        List<Unit> GetAll();
        bool CheckName(string name);
        int CheckNameId(string name);
        Unit Edit(int id);
        int Save(Unit model);
        void Update(Unit model);
        bool Delete(int id);
    }
}
