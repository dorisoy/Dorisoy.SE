using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.SE.Data.ViewModel
{
    public class PurchaseSales
    {
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public string UnitName { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Amount { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PartNo { get; set; }

        //List
        public string VoucherNo { get; set; }
        public DateTime Date { get; set; }
        public string NepaliDate { get; set; }
        public string LedgerName { get; set; }
        public string VoucherTypeName { get; set; }
        public decimal GrandTotal { get; set; }
        public string UserId { get; set; }
        public string Narration { get; set; }
        public decimal TotalSales { get; set; }
    }
}
