using Dreamer.Data.Setting;
using Dreamer.Data.ViewModel;
using System.Collections.Generic;

namespace Dreamer.Services.Interface
{
    public interface IProduct
    {
        List<ProductView> ViewAllProduct(int id);
        List<ProductView> ViewCategoryWiseProduct(int id);
        bool CheckName(string name);
        int CheckNameId(string name);
        string GetProductNo(int companyId);
        Product Edit(int id);
        int Save(Product model);
        void Update(Product model);
        bool Delete(int id);
    }
}
