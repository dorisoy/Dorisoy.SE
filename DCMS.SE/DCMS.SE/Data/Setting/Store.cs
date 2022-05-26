using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DCMS.SE.Data.Setting
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        public string StoreName { get; set; }
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
        public int TerminalId { get; set; }
        public int WarehouseId { get; set; }
        public string Logo { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        [NotMapped]
        public List<string> ImageUrls { get; set; }
    }
}
