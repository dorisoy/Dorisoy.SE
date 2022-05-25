using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.SE.Data.Setting
{
    public class Tax
    {
        [Key]
        public int TaxId { get; set; }
        [Required]
        public string TaxName { get; set; }
        public Decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
