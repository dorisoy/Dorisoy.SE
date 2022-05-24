using System;
using System.ComponentModel.DataAnnotations;

namespace Dreamer.Data.Setting
{
    public class FinancialYear
    {
        [Key]
        public int FinancialYearId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CompanyId { get; set; }
        public string FiscalYear { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
