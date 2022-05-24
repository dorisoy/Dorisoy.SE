using Dreamer.Data.Inventory;
using Dreamer.Data.ViewModel;
using System;
using System.Collections.Generic;

namespace Dreamer.Services.Interface
{
    public interface ISalesInvoice
    {
        string GetVoucherNo(int CompanyId, int FinancialYearId, int VoucherTypeId);
        bool AccountSalesInvoiceNoCheckExistence(int CompanyId, int FinancialYearId, string VoucherNo);
        int AccountSalesInvoiceNoCheckExistenceid(string name);
        int Save(SalesMaster model);
        bool Update(SalesMaster model);
        List<SalesMasterView> SalesInvoiceView(int id);
        List<SalesMasterView> SalesInvoiceViewWarehouse(int id);
        List<SalesMasterView> SaleReportsdetails(DateTime FromDate, DateTime ToDate , int CustomerId , int WarehouseId, string Status);
        List<ProductView> SalesInvoiceDetails(int SalesMasterId);
        SalesMaster EditSalesMaster(int id);
        SalesMasterView PrintSalesMasterView(int id);
        bool DeleteSalesInvoice(int id, string VoucherNo, int CompanyId, int FinancialYearId);
    }
}
