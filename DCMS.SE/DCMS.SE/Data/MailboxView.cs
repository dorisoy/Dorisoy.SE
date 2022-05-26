using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.SE.Data
{
    public class MailboxView
    {
        public long PurchaseMasterId { get; set; }
        public string VoucherNo { get; set; }
        public long TerminalId { get; set; }
        public string TerminalName { get; set; }
        public string TerminalCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string NepaliDate { get; set; }
        public DateTime Date { get; set; }
        public string Narration { get; set; }
        public decimal BillDiscount { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmounts { get; set; }
        public string UserId { get; set; }
        public decimal BankAmount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal PreviousDue { get; set; }
    }
}
