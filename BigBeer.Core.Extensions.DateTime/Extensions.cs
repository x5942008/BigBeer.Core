using System;
using System.Globalization;
namespace BigBeer.Core.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// 返回年龄
        /// </summary>
        /// <param name="Date类型的出生时间"> </param>
        /// <returns></returns>
        public static int Age(this DateTime @this)
        {
            if (DateTime.Today.Month < @this.Month ||
                DateTime.Today.Month == @this.Month &&
                DateTime.Today.Day < @this.Day)
            {
                return DateTime.Today.Year - @this.Year - 1;
            }
            return DateTime.Today.Year - @this.Year;
        }
        /// <summary>
        /// 返回一个时间与现在的时间间隔
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static TimeSpan Elapsed(this DateTime datetime)
        {
            return DateTime.Now - datetime;
        }
        /// <summary>
        /// 从json时间类型转换为datetime时间类型
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime FromJsonTime(this long @this)
        {
            DateTime BaseTime = new DateTime(1970, 1, 1);
            return BaseTime.AddTicks((@this + 8 * 60 * 60) * 10000000);
        }
        /// <summary>
        /// 返回一天结束的时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }
        /// <summary>
        /// 返回该月结束的时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime EndOfMonth(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, 1).AddMonths(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }
        /// <summary>
        /// 返回该周结束的时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="startDayOfWeek"></param>
        /// <returns></returns>
        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
        {
            DateTime end = dt;
            DayOfWeek endDayOfWeek = startDayOfWeek - 1;
            if (endDayOfWeek < 0)
            {
                endDayOfWeek = DayOfWeek.Saturday;
            }

            if (end.DayOfWeek != endDayOfWeek)
            {
                if (endDayOfWeek < end.DayOfWeek)
                {
                    end = end.AddDays(7 - (end.DayOfWeek - endDayOfWeek));
                }
                else
                {
                    end = end.AddDays(endDayOfWeek - end.DayOfWeek);
                }
            }

            return new DateTime(end.Year, end.Month, end.Day, 23, 59, 59, 999);
        }
        /// <summary>
        /// 返回该年结束的时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime EndOfYear(this DateTime @this)
        {
            return new DateTime(@this.Year, 1, 1).AddYears(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }
        /// <summary>
        /// 返回该周第一天开始的时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfWeek(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(-(int)@this.DayOfWeek);
        }
        /// <summary>
        /// 是否为下半天
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsAfternoon(this DateTime @this)
        {
            return @this.TimeOfDay >= new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
        }
        /// <summary>
        /// 是否为同一时间
        /// </summary>
        /// <param name="date"></param>
        /// <param name="dateToCompare"></param>
        /// <returns></returns>
        public static bool IsDateEqual(this DateTime date, DateTime dateToCompare)
        {
            return (date.Date == dateToCompare.Date);
        }
        /// <summary>
        /// 是否是将来的时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsFuture(this DateTime @this)
        {
            return @this > DateTime.Now;
        }
        /// <summary>
        /// 是否为上半天
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsMorning(this DateTime @this)
        {
            return @this.TimeOfDay < new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
        }
        /// <summary>
        /// 是否为现在
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNow(this DateTime @this)
        {
            return @this == DateTime.Now;
        }
        /// <summary>
        /// 是否为过去的时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsPast(this DateTime @this)
        {
            return @this < DateTime.Now;
        }
        /// <summary>
        /// 判断两天是否为同一时间
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeToCompare"></param>
        /// <returns></returns>
        public static bool IsTimeEqual(this DateTime time, DateTime timeToCompare)
        {
            return (time.TimeOfDay == timeToCompare.TimeOfDay);
        }
        /// <summary>
        /// 是否为今天
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsToday(this DateTime @this)
        {
            return @this.Date == DateTime.Today;
        }
        /// <summary>
        /// 是否为工作日
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsWeekDay(this DateTime @this)
        {
            return !(@this.DayOfWeek == DayOfWeek.Saturday || @this.DayOfWeek == DayOfWeek.Sunday);
        }
        /// <summary>
        /// 是否为周末
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsWeekendDay(this DateTime @this)
        {
            return (@this.DayOfWeek == DayOfWeek.Saturday || @this.DayOfWeek == DayOfWeek.Sunday);
        }
        /// <summary>
        /// 该周最后一天开始的时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime LastDayOfWeek(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(6 - (int)@this.DayOfWeek);
        }
        /// <summary>
        /// 将 System.DateTime 结构的新实例初始化为指定的小时
        /// </summary>
        /// <param name="current"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static DateTime SetTime(this DateTime current, int hour)
        {
            return SetTime(current, hour, 0, 0, 0);
        }
        /// <summary>
        /// 将 System.DateTime 结构的新实例初始化为指定的小时和分钟
        /// </summary>
        /// <param name="current"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static DateTime SetTime(this DateTime current, int hour, int minute)
        {
            return SetTime(current, hour, minute, 0, 0);
        }
        /// <summary>
        /// 将 System.DateTime 结构的新实例初始化为指定的小时、分钟和秒
        /// </summary>
        /// <param name="current"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime SetTime(this DateTime current, int hour, int minute, int second)
        {
            return SetTime(current, hour, minute, second, 0);
        }
        /// <summary>
        /// 将 System.DateTime 结构的新实例初始化为指定的小时、分钟、秒和毫秒
        /// </summary>
        /// <param name="current"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <param name="millisecond"></param>
        /// <returns></returns>
        public static DateTime SetTime(this DateTime current, int hour, int minute, int second, int millisecond)
        {
            return new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond);
        }
        /// <summary>
        /// 一天开始的时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime StartOfDay(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day);
        }
        /// <summary>
        /// 该月开始的时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime StartOfMonth(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, 1);
        }
        /// <summary>
        /// 该周开始的时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="startDayOfWeek"></param>
        /// <returns></returns>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
        {
            var start = new DateTime(dt.Year, dt.Month, dt.Day);

            if (start.DayOfWeek != startDayOfWeek)
            {
                int d = startDayOfWeek - start.DayOfWeek;
                if (startDayOfWeek <= start.DayOfWeek)
                {
                    return start.AddDays(d);
                }
                return start.AddDays(-7 + d);
            }

            return start;
        }
        /// <summary>
        /// 该年开始的时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime StartOfYear(this DateTime @this)
        {
            return new DateTime(@this.Year, 1, 1);
        }
        /// <summary>
        /// 从json时间类型转换为datetime时间类型
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static TimeSpan ToEpochTimeSpan(this DateTime @this)
        {
            return @this.Subtract(new DateTime(1970, 1, 1));
        }
        /// <summary>
        /// 明天该时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime Tomorrow(this DateTime @this)
        {
            return @this.AddDays(1);
        }
        /// <summary>
        /// 昨天该时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime Yesterday(this DateTime @this)
        {
            return @this.AddDays(-1);
        }

        /// <summary>
        /// 将时间转换为特定时区的时间
        /// </summary>
        /// <param name="dateTime"> 要转换的日期和时间。</param>
        /// <param name="destinationTimeZone">要将 dateTime 转换到的时区。</param>
        /// <returns>目标时区的日期和时间。</returns>
        public static DateTime ConvertTime(this DateTime dateTime, TimeZoneInfo destinationTimeZone)
        {
            return TimeZoneInfo.ConvertTime(dateTime, destinationTimeZone);
        }
        /// <summary>
        /// 将时间从一个时区转换为另一个时区
        /// </summary>
        /// <param name="dateTime">要转换的日期和时间</param>
        /// <param name="sourceTimeZone">dateTime 的时区</param>
        /// <param name="destinationTimeZone">要将 dateTime 转换到的时区。</param>
        /// <returns>目标时区中与源时区中的 dateTime 参数对应的日期和时间</returns>
        public static DateTime ConvertTime(this DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
        {
            return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone);
        }

        /// <summary>
        ///  A DateTime extension method that converts this object to a full datetime string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToFullDateTimeString(this DateTime @this)
        {
            return @this.ToString("F", DateTimeFormatInfo.CurrentInfo);
        }
        /// <summary>
        ///  A DateTime extension method that converts this object to a full datetime string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToFullDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("F", new CultureInfo(culture));
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a full datetime string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToFullDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("F", culture);
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a long date short time string
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToLongDateShortTimeString(this DateTime @this)
        {
            return @this.ToString("f", DateTimeFormatInfo.CurrentInfo);
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a long date short time string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToLongDateShortTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("f", new CultureInfo(culture));
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a long date short time string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToLongDateShortTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("f", culture);
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a long date  string
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToLongDateString(this DateTime @this)
        {
            return @this.ToString("D", DateTimeFormatInfo.CurrentInfo);
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a long date  string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToLongDateString(this DateTime @this, string culture)
        {
            return @this.ToString("D", new CultureInfo(culture));
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a long date  string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToLongDateString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("D", culture);
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a long datetime string
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToLongDateTimeString(this DateTime @this)
        {
            return @this.ToString("F", DateTimeFormatInfo.CurrentInfo);
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a long datetime string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToLongDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("F", new CultureInfo(culture));
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a long datetime string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToLongDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("F", culture);
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a long time string
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToLongTimeString(this DateTime @this)
        {
            return @this.ToString("T", DateTimeFormatInfo.CurrentInfo);
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a long time string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToLongTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("T", new CultureInfo(culture));
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a long time string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToLongTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("T", culture);
        }
        /// <summary>
        /// A DateTime extension method that converts this object to a month day string
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToMonthDayString(this DateTime @this)
        {
            return @this.ToString("m", DateTimeFormatInfo.CurrentInfo);
        }
        /// <summary>
        ///  A DateTime extension method that converts this object to a month day string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToMonthDayString(this DateTime @this, string culture)
        {
            return @this.ToString("m", new CultureInfo(culture));
        }
        /// <summary>
        ///  A DateTime extension method that converts this object to a month day string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToMonthDayString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("m", culture);
        }
        /// <summary>
        ///     A DateTime extension method that converts this object to a rfc 1123 string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToRFC1123String(this DateTime @this)
        {
            return @this.ToString("r", DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a rfc 1123 string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToRFC1123String(this DateTime @this, string culture)
        {
            return @this.ToString("r", new CultureInfo(culture));
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a rfc 1123 string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToRFC1123String(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("r", culture);
        }
        /// <summary>
        ///     A DateTime extension method that converts this object to a short date long time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToShortDateLongTimeString(this DateTime @this)
        {
            return @this.ToString("G", DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a short date long time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToShortDateLongTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("G", new CultureInfo(culture));
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a short date long time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToShortDateLongTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("G", culture);
        }
        /// <summary>
        ///     A DateTime extension method that converts this object to a short date string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToShortDateString(this DateTime @this)
        {
            return @this.ToString("d", DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a short date string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToShortDateString(this DateTime @this, string culture)
        {
            return @this.ToString("d", new CultureInfo(culture));
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a short date string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToShortDateString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("d", culture);
        }/// <summary>
         ///     A DateTime extension method that converts this object to a short date time string.
         /// </summary>
         /// <param name="this">The @this to act on.</param>
         /// <returns>The given data converted to a string.</returns>
        public static string ToShortDateTimeString(this DateTime @this)
        {
            return @this.ToString("g", DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a short date time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToShortDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("g", new CultureInfo(culture));
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a short date time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToShortDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("g", culture);
        }/// <summary>
         ///     A DateTime extension method that converts this object to a short time string.
         /// </summary>
         /// <param name="this">The @this to act on.</param>
         /// <returns>The given data converted to a string.</returns>
        public static string ToShortTimeString(this DateTime @this)
        {
            return @this.ToString("t", DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a short time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToShortTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("t", new CultureInfo(culture));
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a short time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToShortTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("t", culture);
        }/// <summary>
         ///     A DateTime extension method that converts this object to a sortable date time string.
         /// </summary>
         /// <param name="this">The @this to act on.</param>
         /// <returns>The given data converted to a string.</returns>
        public static string ToSortableDateTimeString(this DateTime @this)
        {
            return @this.ToString("s", DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a sortable date time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToSortableDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("s", new CultureInfo(culture));
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a sortable date time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToSortableDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("s", culture);
        }/// <summary>
         ///     A DateTime extension method that converts this object to an universal sortable date time string.
         /// </summary>
         /// <param name="this">The @this to act on.</param>
         /// <returns>The given data converted to a string.</returns>
        public static string ToUniversalSortableDateTimeString(this DateTime @this)
        {
            return @this.ToString("u", DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to an universal sortable date time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToUniversalSortableDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("u", new CultureInfo(culture));
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to an universal sortable date time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToUniversalSortableDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("u", culture);
        }/// <summary>
         ///     A DateTime extension method that converts this object to an universal sortable long date time string.
         /// </summary>
         /// <param name="this">The @this to act on.</param>
         /// <returns>The given data converted to a string.</returns>
        public static string ToUniversalSortableLongDateTimeString(this DateTime @this)
        {
            return @this.ToString("U", DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to an universal sortable long date time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToUniversalSortableLongDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("U", new CultureInfo(culture));
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to an universal sortable long date time string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToUniversalSortableLongDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("U", culture);
        }/// <summary>
         ///     A DateTime extension method that converts this object to a year month string.
         /// </summary>
         /// <param name="this">The @this to act on.</param>
         /// <returns>The given data converted to a string.</returns>
        public static string ToYearMonthString(this DateTime @this)
        {
            return @this.ToString("y", DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a year month string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToYearMonthString(this DateTime @this, string culture)
        {
            return @this.ToString("y", new CultureInfo(culture));
        }

        /// <summary>
        ///     A DateTime extension method that converts this object to a year month string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ToYearMonthString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("y", culture);
        }
        /// <summary>
        /// 是否处于两个时间之间
        /// </summary>
        /// <param name="this"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool Between(this DateTime @this, DateTime minValue, DateTime maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }
        /// <summary>
        /// 是否某时间处于时间数组中
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool In(this DateTime @this, params DateTime[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }
        /// <summary>
        /// 是否处于一定时间范围内
        /// </summary>
        /// <param name="this"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool InRange(this DateTime @this, DateTime minValue, DateTime maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }
        /// <summary>
        /// 时间不在某时间数组内
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool NotIn(this DateTime @this, params DateTime[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
        /// <summary>
        /// 把时间转换为Stamp
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToJavaimeStamp(this DateTime @this)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = @this.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return (long)ts.TotalMilliseconds;
        }
        /// <summary>
        /// 把时间转换为Stamp
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToJavaimeStamp(this TimeSpan @this)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = new DateTime(@this.Ticks).ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return (long)ts.TotalMilliseconds;
        }
        /// <summary>
        /// java时间数字转换为时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long @this)
        {
            long timeTricks = new DateTime(1970, 1, 1).Ticks + @this * 10000 + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 3600 * (long)10000000;
            return new DateTime(timeTricks);
        }
        /// <summary>
        /// java时间数字转换为时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this long @this)
        {
            long timeTricks = new DateTime(1970, 1, 1).Ticks + @this * 10000 + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 3600 * (long)10000000;
            return new TimeSpan(timeTricks);
        }
    }
}
