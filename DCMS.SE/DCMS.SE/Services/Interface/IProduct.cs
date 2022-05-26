using DCMS.SE.Data.Setting;
using DCMS.SE.Data.ViewModel;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IProduct
    {
        List<ProductView> ViewAllProduct(int id);
        List<ProductView> ViewCategoryWiseProduct(int id);
        bool CheckName(string name);
        int CheckNameId(string name);
        string GetProductNo(int StoreId);
        Product Edit(int id);
        int Save(Product model);
        void Update(Product model);
        bool Delete(int id);
    }
}
