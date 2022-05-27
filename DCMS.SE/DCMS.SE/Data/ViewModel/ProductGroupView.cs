using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.SE.Data.ViewModel
{
    public class CatagoryView
    {
        [Key]
        public int catagoryId { get; set; }
        public string groupName { get; set; }
        public long groupUnder { get; set; }
        public string Under { get; set; }
        public DateTime addedDate { get; set; }
        public DateTime modifyDate { get; set; }
    }
}
