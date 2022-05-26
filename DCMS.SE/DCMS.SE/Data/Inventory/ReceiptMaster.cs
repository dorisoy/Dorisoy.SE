using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DCMS.SE.Data.Inventory
{
    public class ReceiptMaster
    {
        [Key]
        public int ReceiptMasterId { get; set; }
        public int SalesMasterId { get; set; }
        [Required]
        public string VoucherNo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int TerminalId { get; set; }
        [Required]
        public Decimal Amount { get; set; }
        public string SerialNo { get; set; }
        public string Narration { get; set; }
        [Required]
        public int VoucherTypeId { get; set; }
        public string UserId { get; set; }
        public string PaymentType { get; set; }
        [Required]
        public int FinancialYearId { get; set; }
        [Required]
        public int StoreId { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        [NotMapped]
        public decimal PreviousDue { get; set; }
        [NotMapped]
        public bool POS { get; set; }
    }
}
