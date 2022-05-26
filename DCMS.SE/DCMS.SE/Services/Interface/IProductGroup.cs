using DCMS.SE.Data.Setting;
using DCMS.SE.Data.ViewModel;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface ICatagory
    {
        List<Catagory> GetAll();
        List<CatagoryView> ViewAllCatagory();
        bool CheckName(string name);
        int CheckNameId(string name);
        Catagory Edit(int id);
        int Save(Catagory model);
        void Update(Catagory model);
        bool Delete(int id);
    }
}
