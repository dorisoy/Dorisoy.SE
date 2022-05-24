using System;
using System.ComponentModel.DataAnnotations;

namespace Dreamer.Data.Inventory
{
    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }
        [Required]
        public string CurrencySymbol { get; set; }
        public string CurrencyName { get; set; }
        public int NoOfDecimalPlaces { get; set; }
        public bool IsDefault { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
