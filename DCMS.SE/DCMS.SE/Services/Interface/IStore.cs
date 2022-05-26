using DCMS.SE.Data.Setting;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IStore
    {
        List<Store> GetAll();
        Store Edit(int id);
        void Update(Store model);
    }
}
