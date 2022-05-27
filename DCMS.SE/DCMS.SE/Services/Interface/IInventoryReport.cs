using DCMS.SE.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;

namespace DCMS.SE.Services.Interface
{
    public interface IInventoryReport
    {
        DataSet CustomerTerminalOpening(DateTime fromDate, int TerminalId, int StoreId);
        DataSet CustomerTerminal(DateTime fromDate, DateTime toDate, int TerminalId, int StoreId);
        DataSet CustomerTerminalDue(DateTime fromDate, DateTime toDate, int TerminalId);
        DataSet CustomerTerminalDueSingle(DateTime fromDate, DateTime toDate, int TerminalId);
        List<PurchaseSales> CustomerCountSales(DateTime fromDate, DateTime toDate, int TerminalId, int VoucherTypeId);
        List<PurchaseSales> SaleReports(DateTime fromDate, DateTime toDate, int TerminalId, int VoucherTypeId);
        //Supplier
        DataSet SuppllierTerminalDue(DateTime fromDate, DateTime toDate, int TerminalId);
        DataSet SupplierTerminalDueSingle(DateTime fromDate, DateTime toDate, int TerminalId);
        List<PurchaseSales> SupplierCountPurchase(DateTime fromDate, DateTime toDate, int TerminalId, int VoucherTypeId);
        List<PurchaseSales> PurchaseRepports(DateTime fromDate, DateTime toDate, int TerminalId, int VoucherTypeId);
        public List<InventoryViewFinal> StockReport(int catagoryId, int ProductId, int StoreId);
        DataSet DayBook(DateTime fromDate, DateTime toDate, int VoucherTypeId, int TerminalId);
        DataSet TerminalcountReport(DateTime fromDate, DateTime toDate, int TerminalId, string TerminalName, int StoreId);
        //Dashboard
        DashboardView SalesTotal(int StoreId);
        DashboardView SalesTotalwarehouse(int StoreId);
        DashboardView PurchaseTotal(int StoreId);
        DashboardView PurchaseTotalwarehouse(int StoreId);
        DashboardView PurchaseReturnTotal(int StoreId);
        DashboardView PurchaseReturnTotalwarehouse(int StoreId);
        DashboardView SalesReturnTotal(int StoreId);
        DashboardView SalesReturnTotalwarehouse(int StoreId);
        List<FinancialReport> TopReceive(int StoreId);
        List<SalesMasterView> SalesInvoiceViewGraph(int StoreId, int FinancialYearId , DateTime FromDate , DateTime ToDate);
        List<PurchaseMasterView> ViewAllPurchseInvoiceGraph(int StoreId, int FinancialYearId ,DateTime FromDate, DateTime ToDate);
        List<FinancialReport> GettopProduct(int StoreId);

        List<PaymentReceiveView> PaymentSent(int StoreId);
        List<PaymentReceiveView> PaymentReceive(int StoreId);

        DataTable StockSearch(int catagoryId, int productId, string criteria, int StoreId);
    }
}
