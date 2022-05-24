using Dreamer.Data.Inventory;
using Dreamer.Data.ViewModel;
using System;
using System.Collections.Generic;

namespace Dreamer.Services.Interface
{
    public interface ISalesReturnInvoice
    {
        string GetVoucherNo(int CompanyId, int FinancialYearId, int VoucherTypeId);
        bool AccountSalesReturnInvoiceNoCheckExistence(int CompanyId, int FinancialYearId, string VoucherNo);
        int AccountSalesReturnInvoiceNoCheckExistenceid(string name);
        int Save(SalesReturnMaster model);
        bool Update(SalesReturnMaster model);
        List<SalesReturnMasterView> SalesReturnInvoiceView(int id);
        List<SalesReturnMasterView> SalesReturnInvoiceViewwarehouse(int id);
        List<ProductView> SalesReturnInvoiceDetails(int id);
        SalesReturnMaster Edit(int id);
        SalesReturnMasterView Print(int id);
        bool Delete(int id, string VoucherNo, int CompanyId, int FinancialYearId);
    }
}
