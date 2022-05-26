using System;
using System.ComponentModel.DataAnnotations;

namespace DCMS.SE.Data.ViewModel
{
    public class ManufacturerView
    {
        [Key]
        public int ManufacturerId { get; set; }
        public string AccountGroupName { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerCode { get; set; }
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
