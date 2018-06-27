using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace BigBeer.Core.Extensions.Html
{
    public static partial class Extensions
    {
        /// <summary>
        /// 将已经为HTTP传输进行过HTML编码的字符串转换为已解码的字符串。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String HtmlDecode(this String s)
        {
            return WebUtility.HtmlDecode(s);
        }
        /// <summary>
        ///  将字符串转换为 HTML 编码的字符串。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String HtmlEncode(this String s)
        {
            return WebUtility.HtmlEncode(s);
        }


        /// <summary>
        ///    将已经为在 URL 中传输而编码的字符串转换为解码的字符串。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String UrlDecode(this String str)
        {
            return WebUtility.UrlDecode(str);
        }


        /// <summary>
        ///   使用指定的解码对象将 URL 编码的字符串转换为已解码的字节数组。
        /// </summary>
        /// <param name="str">The string to decode.</param>
        /// <param name="e">The  object that specifies the decoding scheme.</param>
        /// <returns>A decoded array of bytes.</returns>
        public static Byte[] UrlDecodeToBytes(this String str)
        {
            var bffer = UTF8Encoding.UTF8.GetBytes(str);
            return WebUtility.UrlDecodeToBytes(bffer, 0, bffer.Length);
        }/// <summary>
         ///  对 URL 字符串进行编码。
         /// </summary>
         /// <param name="str">The text to encode.</param>
         /// <returns>An encoded string.</returns>
        public static String UrlEncode(this String str)
        {
            return WebUtility.UrlEncode(str);
        }

        /// <summary>
        ///     使用指定的编码对象将字符串转换为 URL 编码的字节数组。
        /// </summary>
        /// <param name="str">The string to encode.</param>
        /// <returns>An encoded array of bytes.</returns>
        public static Byte[] UrlEncodeToBytes(this String str)
        {
            var bffer = UTF8Encoding.UTF8.GetBytes(str);
            return WebUtility.UrlEncodeToBytes(bffer, 0, bffer.Length);
        }


        /// <summary>
        /// 是否是邮件格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string obj)
        {
            return Regex.IsMatch(obj, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        /// <summary>
        ///    是否是IP格式{0.0.0.0}
        /// </summary>
        /// <param name="obj">The obj to act on.</param>
        /// <returns>true if valid ip, false if not.</returns>
        public static bool IsValidIP(this string obj)
        {
            return Regex.IsMatch(obj, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
        }
    }
}
