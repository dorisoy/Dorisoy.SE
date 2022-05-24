using Dreamer.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dreamer.Services.Interface
{
    public interface IInventoryReport
    {
        DataSet CustomerLedgerOpening(DateTime fromDate, int LedgerId, int CompanyId);
        DataSet CustomerLedger(DateTime fromDate, DateTime toDate, int LedgerId, int CompanyId);
        DataSet CustomerLedgerDue(DateTime fromDate, DateTime toDate, int LedgerId);
        DataSet CustomerLedgerDueSingle(DateTime fromDate, DateTime toDate, int LedgerId);
        List<PurchaseSales> CustomerCountSales(DateTime fromDate, DateTime toDate, int LedgerId, int VoucherTypeId);
        List<PurchaseSales> SaleReports(DateTime fromDate, DateTime toDate, int LedgerId, int VoucherTypeId);
        //Supplier
        DataSet SuppllierLedgerDue(DateTime fromDate, DateTime toDate, int ledgerId);
        DataSet SupplierLedgerDueSingle(DateTime fromDate, DateTime toDate, int ledgerId);
        List<PurchaseSales> SupplierCountPurchase(DateTime fromDate, DateTime toDate, int LedgerId, int VoucherTypeId);
        List<PurchaseSales> PurchaseRepports(DateTime fromDate, DateTime toDate, int LedgerId, int VoucherTypeId);
        public List<InventoryViewFinal> StockReport(int GroupId, int ProductId, int CompanyId);
        DataSet DayBook(DateTime fromDate, DateTime toDate, int VoucherTypeId, int LedgerId);
        DataSet LedgercountReport(DateTime fromDate, DateTime toDate, int LedgerId, string LedgerName, int CompanyId);
        //Dashboard
        DashboardView SalesTotal(int CompanyId);
        DashboardView SalesTotalwarehouse(int CompanyId);
        DashboardView PurchaseTotal(int CompanyId);
        DashboardView PurchaseTotalwarehouse(int CompanyId);
        DashboardView PurchaseReturnTotal(int CompanyId);
        DashboardView PurchaseReturnTotalwarehouse(int CompanyId);
        DashboardView SalesReturnTotal(int CompanyId);
        DashboardView SalesReturnTotalwarehouse(int CompanyId);
        List<FinancialReport> TopReceive(int CompanyId);
        List<SalesMasterView> SalesInvoiceViewGraph(int CompanyId, int FinancialYearId , DateTime FromDate , DateTime ToDate);
        List<PurchaseMasterView> ViewAllPurchseInvoiceGraph(int CompanyId, int FinancialYearId ,DateTime FromDate, DateTime ToDate);
        List<FinancialReport> GettopProduct(int CompanyId);

        List<PaymentReceiveView> PaymentSent(int CompanyId);
        List<PaymentReceiveView> PaymentReceive(int CompanyId);

        DataTable StockSearch(int groupId, int productId, string criteria, int companyId);
    }
}
