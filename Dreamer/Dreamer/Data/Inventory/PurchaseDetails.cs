using System.ComponentModel.DataAnnotations;

namespace Dreamer.Data.Inventory
{
    public class PurchaseDetails
    {
        [Key]
        public int PurchaseDetailsId { get; set; }
        public int PurchaseMasterId { get; set; }
        public int OrderDetailsId { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public int UnitId { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int TaxId { get; set; }
        public int BatchId { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Amount { get; set; }
    }
}
