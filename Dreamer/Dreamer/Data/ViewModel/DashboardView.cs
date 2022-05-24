using System.Collections.Generic;

namespace Dreamer.Data.ViewModel
{
    public class DashboardView
    {
        public int totalCustomer { get; set; }
        public int totalSupplier { get; set; }
        public int totalCustomerSupplier { get; set; }
        public decimal totalSale { get; set; }
        public decimal totalPurchase { get; set; }
        public decimal debit { get; set; }
        public decimal credit { get; set; }
        public decimal totalAmount { get; set; }
        public decimal Purchase { get; set; }
        public decimal Payment { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
