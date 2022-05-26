using System;
using System.ComponentModel.DataAnnotations;

namespace DCMS.SE.Data.Setting
{
    public class Catagory
    {
        [Key]
        public int GroupId { get; set; }
        [Required]
        public string GroupName { get; set; }
        [Required]
        public int GroupUnder { get; set; }
        public string Narration { get; set; }
        public int StoreId { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
