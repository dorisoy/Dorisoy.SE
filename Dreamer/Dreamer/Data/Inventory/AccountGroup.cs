using System;
using System.ComponentModel.DataAnnotations;

namespace Dreamer.Data.Inventory
{
    public class AccountGroup
    {
        [Key]
        public int AccountGroupId { get; set; }
        [Required]
        public string AccountGroupName { get; set; }
        public int GroupUnder { get; set; }
        public int CompanyId { get; set; }
        public string Narration { get; set; }
        public bool IsDefault { get; set; }
        public string Nature { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
