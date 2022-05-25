using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.SE.Data
{
    public class WeSetting
    {
        [Key]
        public long WesettingId { get; set; }
        public bool PrintdoubleReceipt { get; set; }
        public bool Printheaderbill { get; set; }
        public bool RoundOff { get; set; }
        public bool CreditBill { get; set; }
        public bool ShowRemarks { get; set; }
        public string ShowremarksDescription { get; set; }
        public bool CustomernoGenerate { get; set; }
        public bool WardNo { get; set; }
        public bool WardZone { get; set; }
        public bool PenaltyDiscount { get; set; }
    }
}
