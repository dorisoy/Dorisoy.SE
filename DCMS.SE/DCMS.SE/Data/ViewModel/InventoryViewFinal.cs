using System.ComponentModel.DataAnnotations;

namespace DCMS.SE.Data.ViewModel
{
    public class InventoryViewFinal
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string UnitName { get; set; }
        public string BatchNo { get; set; }
        public long ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal SalesRate { get; set; }
        public decimal Mrp { get; set; }
        public decimal Rate { get; set; }
        public decimal PurQty { get; set; }
        public decimal SalesQty { get; set; }
        public decimal SalesStockBalance { get; set; }
        public decimal PurchaseStockBal { get; set; }
        public decimal Stockvalue { get; set; }
        public decimal Stock { get; set; }
        public string PartNo { get; set; }
        public string VoucherTypeName { get; set; }
        public decimal InwardQty { get; set; }
        public decimal OutwardQty { get; set; }
        public decimal CurrentStock { get; set; }


        //Ledger
        public string LedgerName { get; set; }
        public string NepaliDate { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public decimal Opening { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public string VoucherNo { get; set; }
        public string Narration { get; set; }
    }
}
