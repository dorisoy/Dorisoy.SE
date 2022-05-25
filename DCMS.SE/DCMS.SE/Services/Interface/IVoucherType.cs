using DCMS.SE.Data.Setting;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IVoucherType
    {
        VoucherType Edit(int id);
        List<VoucherType> GetAll();
        void Update(VoucherType voucherType);
    }
}
