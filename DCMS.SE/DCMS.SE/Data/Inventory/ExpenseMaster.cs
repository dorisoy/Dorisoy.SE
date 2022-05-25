using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DCMS.SE.Data.Inventory
{
    public class ExpenseMaster
    {
        [Key]
        public int ExpensiveMasterId { get; set; }
        [Required]
        public int LedgerId { get; set; }
        public int WarehouseId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int VoucherTypeId { get; set; }
        public string SerialNo { get; set; }
        [Required]
        public string VoucherNo { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public int FinancialYearId { get; set; }
        public int CompanyId { get; set; }
        public string UserId { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
