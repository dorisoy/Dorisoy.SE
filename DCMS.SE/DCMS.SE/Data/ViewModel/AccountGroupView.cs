using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.SE.Data.ViewModel
{
    public class AccountGroupView
    {
        [Key]
        public int AccountGroupId { get; set; }
        [Required]
        public string AccountGroupName { get; set; }
        public string Under { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
