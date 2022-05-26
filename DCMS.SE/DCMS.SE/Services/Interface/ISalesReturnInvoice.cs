using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using System;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface ISalesReturnInvoice
    {
        string GetVoucherNo(int StoreId, int FinancialYearId, int VoucherTypeId);
        bool AccountSalesReturnInvoiceNoCheckExistence(int StoreId, int FinancialYearId, string VoucherNo);
        int AccountSalesReturnInvoiceNoCheckExistenceid(string name);
        int Save(SalesReturnMaster model);
        bool Update(SalesReturnMaster model);
        List<SalesReturnMasterView> SalesReturnInvoiceView(int id);
        List<SalesReturnMasterView> SalesReturnInvoiceViewwarehouse(int id);
        List<ProductView> SalesReturnInvoiceDetails(int id);
        SalesReturnMaster Edit(int id);
        SalesReturnMasterView Print(int id);
        bool Delete(int id, string VoucherNo, int StoreId, int FinancialYearId);
    }
}
