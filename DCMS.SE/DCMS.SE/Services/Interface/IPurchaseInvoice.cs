using DCMS.SE.Data;
using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using System;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IPurchaseInvoice
    {
        string GetVoucherNo(int CompanyId, int FinancialYearId, int VoucherTypeId);
        bool AccountPurchseInvoiceNoCheckExistence(int CompanyId, int FinancialYearId, string VoucherNo);
        int AccountPurchseInvoiceNoCheckExistenceid(string name);
        int Save(PurchaseMaster model);
        bool Update(PurchaseMaster model);
        List<PurchaseMasterView> PurchaseInvoiceView(int id);
        List<PurchaseMasterView> PurchaseInvoiceViewwarehouse(int id);
        List<PurchaseMasterView> PurchaseReportsdetails(DateTime FromDate, DateTime ToDate, int CustomerId, int WarehouseId, string Status);
        List<ProductView> PurchaseInvoiceDetails(int PurchaseMasterId);
        List<PurchaseMasterView> PurchaseInvoiceDetails(int id, DateTime fromDate, DateTime toDate);
        PurchaseMaster EditPurchaseMaster(int id);
        PurchaseMasterView PrintPurchaseMasterView(int id);
        bool DeletePurchseInvoice(int id, string VoucherNo, int CompanyId, int FinancialYearId);
    }
}
