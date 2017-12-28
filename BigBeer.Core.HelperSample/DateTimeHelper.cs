using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.Core.HelperSample
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 获取当前日期本月的第一天的日期
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMothFirstDay()
        {
            DateTime date = DateTime.Now.Date;
            int a = int.Parse(date.Day.ToString());
            DateTime result = date.AddDays(-a + 1);
            return result;
        }

        /// <summary>
        /// 获取当前日期本周第一天的日期
        /// </summary>
        /// <returns></returns>
        public static DateTime GetWeekFirstDay()
        {
            DateTime result = DateTime.Now.Date;
            string temp = result.DayOfWeek.ToString();

            switch (temp)
            {
                case "Monday":
                    break;
                case "Tuesday":
                    result = result.AddDays(-1);
                    break;
                case "Wednesday":
                    result = result.AddDays(-2);
                    break;
                case "Saturday":
                    result = result.AddDays(-3);
                    break;
                case "Thursday":
                    result = result.AddDays(-4);
                    break;
                case "Friday":
                    result = result.AddDays(-5);
                    break;
                case "Sunday":
                    result = result.AddDays(-6);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 获取时间戳毫默认秒数
        /// </summary>
        /// <param name="flag">转换成其他单位区间</param>
        /// <returns></returns>
        public static long GetUnixTimestamp(int flag = 1)
        {
            var epoch = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000 / flag;
            return epoch;
        }

        /// <summary>
        /// 星期英文转换中文
        /// </summary>
        /// <returns></returns>
        public static string WeekToChinese(DayOfWeek week)
        {
            string result = "";
            switch (week)
            {
                case DayOfWeek.Sunday:
                    result = "周天";
                    break;
                case DayOfWeek.Monday:
                    result = "周一";
                    break;
                case DayOfWeek.Tuesday:
                    result = "周二";
                    break;
                case DayOfWeek.Wednesday:
                    result = "周三";
                    break;
                case DayOfWeek.Thursday:
                    result = "周四";
                    break;
                case DayOfWeek.Friday:
                    result = "周五";
                    break;
                case DayOfWeek.Saturday:
                    result = "周六";
                    break;
                default:
                    break;

            }
            return result;
        }
    }
}