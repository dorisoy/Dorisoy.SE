using Dreamer.Data.Setting;
using System.Collections.Generic;

namespace Dreamer.Services.Interface
{
    public interface IVoucherType
    {
        VoucherType Edit(int id);
        List<VoucherType> GetAll();
        void Update(VoucherType voucherType);
    }
}
