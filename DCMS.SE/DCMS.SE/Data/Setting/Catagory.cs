using System;
using System.ComponentModel.DataAnnotations;

namespace DCMS.SE.Data.Setting
{
    public class Catagory
    {
        [Key]
        public int CatagoryId { get; set; }

        [Required]
        public string CatagoryName { get; set; }

        [Required]
        public int CatagoryUnder { get; set; }
        public string Narration { get; set; }
        public int StoreId { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
