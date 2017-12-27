using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BigBeer.Core.HelperSample
{
    /// <summary>
    /// 正则表达式
    /// </summary>
    public class RegularHelper
    {
        /// <summary>
        /// 手机号格式验证/OK
        /// </summary>
        /// <param name="s">验证字符串</param>
        /// <returns>正确返回true</returns>
        public static bool RegexPhone(string s)
        {

            if (Regex.IsMatch(s, @"^[1]+[1-9]+\d{9}$"))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 邮箱号格式验证/OK
        /// </summary>
        /// <param name="s">验证字符串</param>
        /// <returns>正确返回true</returns>
        public static bool RegexEmail(string s)
        {
            if (Regex.IsMatch(s, @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$"))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 身份证号格式验证/OK
        /// </summary>
        /// <param name="s">验证字符串</param>
        /// <returns>正确返回true</returns>
        public static bool RegexIdentityCard(string s)
        {
            if (Regex.IsMatch(s, @"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$"))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// QQ号格式验证/OK
        /// </summary>
        /// <param name="s">验证字符串</param>
        /// <returns>正确返回true</returns>
        public static bool RegexQQ(string s)
        {
            if (Regex.IsMatch(s, @"[1-9][0-9]{4,}"))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 验证URL格式验证/OK
        /// </summary>
        /// <param name="s">验证字符串</param>
        /// <returns>正确返回true</returns>
        public static bool RegexURL(string s)
        {
            if (Regex.IsMatch(s, @"[a-zA-z]+://[^\s]*"))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 真实姓名格式验证/OK
        /// </summary>
        /// <param name="s">验证字符串</param>
        /// <returns>正确返回true</returns>
        public static bool RegexRealName(string s)
        {
            if (Regex.IsMatch(s, @"^[\u4e00-\u9fa5]{2,6}$"))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 登录名号格式验证/OK
        /// </summary>
        /// <param name="s">验证字符串</param>
        /// <returns>正确返回true</returns>
        public static bool RegexLoginName(string s)
        {
            if (Regex.IsMatch(s, @"^[a-zA-Z\d_\s]*$"))
            {
                if (Regex.IsMatch(s, @"[\w]{5,}"))
                {
                    return true;
                }

                return false;
            }
            return false;
        }
        /// <summary>
        /// 登录密码号格式验证/OK
        /// </summary>
        /// <param name="s">验证字符串</param>
        /// <returns>正确返回true</returns>
        public static bool RegexLoginPassword(string s)
        {
            if (Regex.IsMatch(s, @"[\w]{6,}"))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 安全码号格式验证/OK
        /// </summary>
        /// <param name="s">验证字符串</param>
        /// <returns>正确返回true</returns>
        public static bool RegexSafePassword(string s)
        {
            if (Regex.IsMatch(s, @"[\w]{6,}"))
            {
                return true;
            }
            return false;
        }
    }
}
