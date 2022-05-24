using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamer.Data
{
    public class DeleteItem
    {
        [Key]
        public int id { get; set; }
        public int PurchaseDetailsId { get; set; }
        public int DeleteItemIdnext { get; set; }
        public int DeleteExpenditureId { get; set; }
        public int DeleteIncomeDetailsId { get; set; }
        public int SalesDetailsId { get; set; }
        public int DeletePurchaseReturnId { get; set; }
        public int DeleteSalesReturnId { get; set; }
    }
}
