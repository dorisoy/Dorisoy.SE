using System;
using System.ComponentModel.DataAnnotations;

namespace DCMS.SE.Data.Setting
{
    public class VoucherType
    {
        [Key]
        public int VoucherTypeId { get; set; }
        public string VoucherTypeName { get; set; }
        public int StartIndex { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int StoreId { get; set; }
        public string ShowNote { get; set; }
        public string ShowAddress { get; set; }
        public string ShowEmail { get; set; }
        public string ShowPhone { get; set; }
        public string ShowDiscount { get; set; }
        public string ShowTax { get; set; }
        public string ShowBarcode { get; set; }
        public bool IsActive { get; set; }
    }
}
