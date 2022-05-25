using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.SE.Data.Apimodel
{
    public class RefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
