using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamer.Data.Inventory
{
    public class StockPosting
    {
        [Key]
        public int StockPostingId { get; set; }
        public DateTime Date { get; set; }
        public string NepaliDate { get; set; }
        public int VoucherTypeId { get; set; }
        public string VoucherNo { get; set; }
        public string InvoiceNo { get; set; }
        public int ProductId { get; set; }
        public int BatchId { get; set; }
        public int UnitId { get; set; }
        public int WarehouseId { get; set; }
        public int AgainstVoucherTypeId { get; set; }
        public string AgainstInvoiceNo { get; set; }
        public string AgainstVoucherNo { get; set; }
        public Decimal InwardQty { get; set; }
        public Decimal OutwardQty { get; set; }
        public Decimal Rate { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public int DetailsId { get; set; }
        public string StockCalculate { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
