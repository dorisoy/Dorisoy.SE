using System;
using System.ComponentModel.DataAnnotations;

namespace Dreamer.Data.ViewModel
{
    public class AccountLedgerView
    {
        [Key]
        public int LedgerId { get; set; }
        public string AccountGroupName { get; set; }
        public string LedgerName { get; set; }
        public string LedgerCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public decimal OpeningBalance { get; set; } 
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
