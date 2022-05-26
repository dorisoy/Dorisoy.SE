namespace DCMS.SE
{
	using System;

    public class CommonHelper
    {

		/// <summary>
		/// 生成单据号
		/// </summary>
		/// <param name="billType">单据类型</param>
		/// <param name="storeId">经销商id</param>
		/// <returns>类型+7+12</returns>
		public static string GetBillNumber(string billType, int storeId)
		{
			string stamp = GetTimeStamp(DateTime.Now, 12);
			int realCount = 0;
			var str = storeId.ToString();
			//7位经销商编号，支持百万家
			if (str.Length > 7)
			{
				var start = int.Parse(str.Substring(0, 7));
				var end = int.Parse(str.Substring(7, str.Length - 7));
				storeId = start + end;
			}
			var numArry = GetNumHash(storeId, ref realCount);
			int[] arry = new int[7];
			for (var i = 0; i < 7; i++)
			{
				if (realCount > i)
				{
					arry[i] = numArry[i];
				}
				else
				{
					arry[i] = 0;
				}
			}
			string billNumber = billType + "" + string.Join("", arry) + "" + stamp;
			return billNumber;
		}

		/// <summary>
		///  将整型转成整型数组
		/// </summary>
		/// <example>10 转成 num[0]=1 num[1]=0 </example>
		/// <param name="showNumber">整型数字</param>
		/// <param name="realCount">返回的实际大小，即数组长度</param>
		/// <returns>整型数组</returns>
		public static int[] GetNumHash(int showNumber, ref int realCount)
		{
			int[] num_hash = new int[10];
			int index = 0;
			while (showNumber / 10 != 0)
			{
				num_hash[index] = (showNumber % 10);
				showNumber /= 10;
				index++;
			}
			num_hash[index] = showNumber;
			realCount = index + 1;
			return num_hash;
		}

		/// <summary>
		/// 获取时间戳
		/// </summary>
		/// <returns></returns>
		public static string GetTimeStamp(System.DateTime time, int length = 13)
		{
			long ts = ConvertDateTimeToInt(time);
			return ts.ToString().Substring(0, length);
		}


		/// <summary>  
		/// 将c# DateTime时间格式转换为Unix时间戳格式  
		/// </summary>  
		/// <param name="time">时间</param>  
		/// <returns>long</returns>  
		public static long ConvertDateTimeToInt(System.DateTime time)
		{
			System.DateTime startTime = System.TimeZoneInfo.ConvertTimeToUtc(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
			//TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
			long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
			return t;
		}
	}
}
