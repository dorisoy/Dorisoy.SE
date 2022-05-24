using Dreamer.Data.Setting;
using Dreamer.Data.ViewModel;
using System.Collections.Generic;

namespace Dreamer.Services.Interface
{
    public interface IProductGroup
    {
        List<ProductGroup> GetAll();
        List<ProductGroupView> ViewAllProductGroup();
        bool CheckName(string name);
        int CheckNameId(string name);
        ProductGroup Edit(int id);
        int Save(ProductGroup model);
        void Update(ProductGroup model);
        bool Delete(int id);
    }
}
