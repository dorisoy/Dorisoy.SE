using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamer.Data
{
    public class Roles
    {
        [Key]
        public short RoleId { get; set; }
        public string RoleDesc { get; set; }
    }
}
