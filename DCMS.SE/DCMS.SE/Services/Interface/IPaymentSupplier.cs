using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using System;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IPaymentSupplier
    {
        string GetVoucherNo(int StoreId, int FinancialYearId, int VoucherTypeId);
        bool PaymentVoucherNoCheckExistence(int StoreId, int FinancialYearId, string VoucherNo);
        PaymentMaster EdiById(int id);
        bool Save(PaymentMaster model);
        bool Update(PaymentMaster model);
        PaymentMaster EditPaymentMaster(int id);
        bool DeletePaymentSupplier( int PaymentMasterId, string VoucherNo, int VoucherTypeId, int StoreId, int FinancialYearId);

        List<PaymentMaster> GetAllById(int id);
        //Other
        PaymentReceiveView GetPreviousDuesBalancesupplier(int TerminalId);
        PaymentReceiveView GetTotalReceiableAmount(int PurchaseMaterId);
        PaymentReceiveView PaymentSupplierView(int PaymentMasterId);

        List<PaymentReceiveView> PurchasePaymentView(DateTime FromDate, DateTime ToDate, int CustomerId, int PurchaseMasterId);
    }
}
