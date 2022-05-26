using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.SE.Data.ViewModel
{
    public class PurchaseReturnMasterView
    {
        [Key]
        public int PurchaseReturnMasterId { get; set; }
        public string VoucherNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public string NepaliDate { get; set; }
        public string VoucherTypeName { get; set; }
        public int TerminalId { get; set; }
        public string TerminalName { get; set; }
        public string TerminalCode { get; set; }
        public string Narration { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal BillDiscount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDue { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal TotalTax { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public string Pan { get; set; }
        public decimal BalanceDue { get; set; }
        public decimal TaxRate { get; set; }
        public string Status { get; set; }
        public string WarehouseName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
