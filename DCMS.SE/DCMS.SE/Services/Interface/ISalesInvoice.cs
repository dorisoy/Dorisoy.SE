using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using System;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface ISalesInvoice
    {
        string GetVoucherNo(int StoreId, int FinancialYearId, int VoucherTypeId);
        bool AccountSalesInvoiceNoCheckExistence(int StoreId, int FinancialYearId, string VoucherNo);
        int AccountSalesInvoiceNoCheckExistenceid(string name);
        int Save(SalesMaster model);
        bool Update(SalesMaster model);
        List<SalesMasterView> SalesInvoiceView(int id);
        List<SalesMasterView> SalesInvoiceViewWarehouse(int id);
        List<SalesMasterView> SaleReportsdetails(DateTime FromDate, DateTime ToDate , int CustomerId , int WarehouseId, string Status);
        List<ProductView> SalesInvoiceDetails(int SalesMasterId);
        SalesMaster EditSalesMaster(int id);
        SalesMasterView PrintSalesMasterView(int id);
        bool DeleteSalesInvoice(int id, string VoucherNo, int StoreId, int FinancialYearId);
    }
}
