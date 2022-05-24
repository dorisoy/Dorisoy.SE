using Dreamer.Data.Setting;
using System.Collections.Generic;

namespace Dreamer.Services.Interface
{
    public interface ICompany
    {
        List<Company> GetAll();
        Company Edit(int id);
        void Update(Company model);
    }
}
