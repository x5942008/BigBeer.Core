using System;

namespace BigBeer.Core.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// 转化为 万元/百万/千万/亿 结尾
        /// </summary>
        /// <param name="this"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static string ToChineseExtension(this decimal @this, int digits = 3)
        {
            if (@this >= 10000)
                return $"{Math.Round((double)@this / 10000, digits).ToString() }万";
            if (@this >= 1000000)
                return $"{ Math.Round((double)@this / 1000000, digits).ToString()}百万";
            if (@this >= 10000000)
                return $"{Math.Round((double)@this / 10000000, digits).ToString() }千万";
            if (@this >= 1000000000)
                return $"{Math.Round((double)@this / 1000000000, digits).ToString() }亿";
            else
                return @this.ToString();
        }
        /// <summary>
        /// 保留2位小数点,并已,号分隔
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToMoney(this Decimal @this)
        {
            return Math.Round(@this, 2).ToString("C");
        }
        public static Decimal Abs(this Decimal value)
        {
            return Math.Abs(value);
        }
        public static Decimal Ceiling(this Decimal d)
        {
            return Math.Ceiling(d);
        }
        public static Decimal Floor(this Decimal d)
        {
            return Math.Floor(d);
        }
        public static Decimal Max(this Decimal val1, Decimal val2)
        {
            return Math.Max(val1, val2);
        }
        public static Decimal Min(this Decimal val1, Decimal val2)
        {
            return Math.Min(val1, val2);
        }
        /// <summary>
        ///     四舍五入
        /// </summary>
        /// <param name="d">A decimal number to be rounded.</param>
        /// <returns>
        ///     The integer nearest parameter . If the fractional component of  is halfway between two integers, one of which
        ///     is even and the other odd, the even number is returned. Note that this method returns a  instead of an
        ///     integral type.
        /// </returns>
        public static Decimal Round(this Decimal d)
        {
            return Math.Round(d);
        }
        public static Decimal Round(this Decimal d, Int32 decimals)
        {
            return Math.Round(d, decimals);
        }
        public static Decimal Round(this Decimal d, MidpointRounding mode)
        {
            return Math.Round(d, mode);
        }
        public static Decimal Round(this Decimal d, Int32 decimals, MidpointRounding mode)
        {
            return Math.Round(d, decimals, mode);
        }
        /// <summary>
        /// 保留整数
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Decimal Truncate(this Decimal d)
        {
            return Math.Truncate(d);
        }
    }
}
