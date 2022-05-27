using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.SE.Data.ViewModel
{
    public class PaymentReceiveView
    {
        public int PaymentMasterId { get; set; }
        public int ReceiptMasterId { get; set; }
        public string PurchaseVoucherNo { get; set; }
        public string VoucherNo { get; set; }
        public DateTime Date { get; set; }
        public string NepaliDate { get; set; }
        public string VoucherTypeName { get; set; }
        public string ManufacturerName { get; set; }
        public string TerminalName { get; set; }
        public string Narration { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        //Due
        public decimal DueBalance { get; set; }
        public long ManufacturerId { get; set; }
    }
}
