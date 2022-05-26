using System;
using System.ComponentModel.DataAnnotations;

namespace DCMS.SE.Data.Setting
{
    public class Privilege
    {
        [Key]
        public int PrivilegeId { get; set; }
        public string FormName { get; set; }
        public string FormNameNepali { get; set; }
        public bool AddAction { get; set; }
        public bool EditAction { get; set; }
        public bool DeleteAction { get; set; }
        public bool ShowAction { get; set; }
        public int RoleId { get; set; }
        public int StoreId { get; set; }
        public string SettingType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
