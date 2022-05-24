using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Dreamer.Data
{
    class PublicVariables
    {
        public static int _decCurrentUserId = 1;
        public static int _roleId = 1;
        public static int _decCurrentCompanyId = 1;
        public static int _dtFromDateFinancial;//financial year starting  
        public static string _username;
        public static string _dtCurrentDate;
        public static string _dtCurrentFrom;
        public static string _dtCurrentto;
        public static string _dtFromDate = DateTime.Now.ToString();//financial year starting    
        public static string _dtToDate;//financial year ending         
        public static int _decCurrentFinancialYearId = 1;
        public static bool isMessageAdd = true;
        public static bool isMessageEdit = true;
        public static bool isMessageDelete = true;
        public static bool isMessageClose = true;
        public static decimal _decCurrencyId = 1;
        public static int _inNoOfDecimalPlaces = 2;
        public static string MessageToShow = string.Empty;
        public static string MessageHeadear = string.Empty;
        //FixedDate Format dd/MM/yyyy
        public static string dt_RenewDate;
    }
}
