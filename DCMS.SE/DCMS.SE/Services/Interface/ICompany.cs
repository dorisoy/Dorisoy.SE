using DCMS.SE.Data.Setting;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface ICompany
    {
        List<Company> GetAll();
        Company Edit(int id);
        void Update(Company model);
    }
}
