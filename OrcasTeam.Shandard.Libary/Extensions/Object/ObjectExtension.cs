using System;
using System.Collections.Generic;
using System.Text;

namespace OrcasTeam.Shandard.Libary.Extensions
{
   public  static  class ObjectExtension
    {
        /// <summary>
        ///     截取double类型小数点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="digits">截取小数位数</param>
        /// <returns></returns>
        public static string Truncate(this double x, int digits)
        {
            var str = "";
            if (Math.Abs(x) < 0)
            {
                for (int i = 0; i < digits; i++)
                {
                    str += "0";
                }

                return "0." + str;
            }

            for (int i = 0; i <= digits; i++)
            {
                str += "0";
            }

            var doubleValueStr = x.ToString("#." + str);
            return doubleValueStr.Substring(0, doubleValueStr.IndexOf(".") + 1 + digits);
        }

        /// <summary>
        ///     获取当前时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static double ToUnixTimeStampMs(this DateTime dateTime)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var diff = dateTime - origin;
            return diff.TotalMilliseconds;
        }
    }
}
