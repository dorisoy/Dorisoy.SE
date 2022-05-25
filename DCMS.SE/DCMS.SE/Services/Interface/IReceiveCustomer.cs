using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using System;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IReceiveCustomer
    {
        string GetVoucherNo(int CompanyId, int FinancialYearId, int VoucherTypeId);
        bool ReceiveVoucherNoCheckExistence(int CompanyId, int FinancialYearId, string VoucherNo);
        ReceiptMaster EdiById(int id);
        bool Save(ReceiptMaster model);
        bool Update(ReceiptMaster model);
        ReceiptMaster EditReceiveMaster(int id);
        bool DeleteReceiveCustomer( int ReceiptMasterId, string VoucherNo, int VoucherTypeId, int CompanyId, int FinancialYearId);

        List<ReceiptMaster> GetAllById(int id);
        //Other
        PaymentReceiveView GetPreviousDuesBalancesupplier(int LedgerId);
        PaymentReceiveView GetTotalReceiableAmount(int SalesMaterId);
        PaymentReceiveView ReceiveCustomerView(int ReceiptMasterId);
        List<PaymentReceiveView> SalesReceiveView(DateTime FromDate, DateTime ToDate, int CustomerId, int SalesMasterId);
    }
}
