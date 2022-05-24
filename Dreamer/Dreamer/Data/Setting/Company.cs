using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamer.Data.Setting
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public int CurrencyId { get; set; }
        public int FinancialYearId { get; set; }
        public int NoofDecimal { get; set; }
        public string BusinessType { get; set; }
        public string Website { get; set; }
        public string PanNo { get; set; }
        public int LedgerId { get; set; }
        public int WarehouseId { get; set; }
        public string Logo { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        [NotMapped]
        public List<string> ImageUrls { get; set; }
    }
}
