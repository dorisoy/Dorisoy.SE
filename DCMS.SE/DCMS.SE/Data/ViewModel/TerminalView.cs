using System;
using System.ComponentModel.DataAnnotations;

namespace DCMS.SE.Data.ViewModel
{
    public class TerminalView
    {
        [Key]
        public int TerminalId { get; set; }
        public string AccountGroupName { get; set; }
        public string TerminalName { get; set; }
        public string TerminalCode { get; set; }
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
