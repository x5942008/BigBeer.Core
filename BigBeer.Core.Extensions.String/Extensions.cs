
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Linq;
namespace BigBeer.Core.Extensions
{
    public static partial class Extensions
    {
        #region Encrypt
        #region DES 不支持
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="this"></param>
        /// <param name="Key">加密KEY</param>
        /// <param name="iv">向量 8位</param>
        /// <returns></returns>
        public static string DESEncrypt(this string @this, string Key, string iv = "12345678")
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(iv);
            byte[] byteArray = Encoding.UTF8.GetBytes(@this);

            string encrypt = null;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(mStream.ToArray());
                    }
                }
            }
            catch { }
            des.Clear();
            return encrypt;
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="this"></param>
        /// <param name="Key">加密key</param>
        /// <returns>明文</returns>
        public static string DESDecrypt(this string @this, string Key, string iv = "12345678")
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(iv);
            byte[] byteArray = Convert.FromBase64String(@this);
            string decrypt = null;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, des.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                    }
                }
            }
            catch { }
            des.Clear();

            return decrypt;
        }
        #endregion
        private static byte[] EncryptKeys = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="this"></param>
        /// <param name="Key">加密key,32位</param>
        /// <returns>密文</returns>
        public static string AESEncrypt(this string @this, string Key)
        {
            if (string.IsNullOrEmpty(@this)) return null;
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(Key.Substring(0, 16));
                byte[] rgbIV = EncryptKeys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(@this);
                var DCSP = Aes.Create();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message + @this;
            }
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="this"></param>
        /// <param name="Key">加密key</param>
        /// <returns>明文</returns>
        public static string AESDecrypt(this string @this, string Key)
        {
            if (string.IsNullOrEmpty(@this)) return null;
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(Key.Substring(0, 16));
                byte[] rgbIV = EncryptKeys;
                byte[] inputByteArray = Convert.FromBase64String(@this);
                var DCSP = Aes.Create();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                Byte[] inputByteArrays = new byte[inputByteArray.Length];
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message + @this;
            }
        }

        /// <summary>
        /// 进行sha刷法
        /// </summary>
        /// <param name="this"></param>
        /// <returns>Base64String</returns>
        public static string SHAEncrypt(this string @this)
        {
            //HashAlgorithm sha = HashAlgorithm.Create("SHA");
            var sha = IncrementalHash.CreateHash(HashAlgorithmName.SHA1);
            var input = Encoding.UTF8.GetBytes(@this);
            sha.AppendData(input);
            byte[] bytes = sha.GetHashAndReset();
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Md5Encrypt(this string @this)
        {
            MD5 md5 = MD5.Create();// new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(@this);
            bytes = md5.ComputeHash(bytes);
            //md5.();
            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }
            return ret.PadLeft(32, '0');
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="@this"></param>
        /// <returns></returns>
        public static string EncryptPassword(this string @this)
        {
            var key = @"wujixiong1314520";
            return @this.EncodeBase64().AESEncrypt(key);
        }

        /// <summary>
        /// 读取base64字符串
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string DecodeBase64(this string @this)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(@this));
        }

        /// <summary>
        /// 转换为base64
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string EncodeBase64(this string @this)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(@this));
        }
        #endregion

        #region Is
        /// <summary>
        /// 指示指定的字符串是 null 还是 System.String.Empty 字符串。
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }

        /// <summary>
        /// 不为null
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotNull(this String @this)
        {
            return @this != null;
        }

        /// <summary>
        /// 是否为null
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNull(this String @this)
        {
            return @this == null;
        }

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this String @this)
        {
            return string.IsNullOrWhiteSpace(@this);
        }

        /// <summary>
        /// 如果为空则设置默认值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string IfEmpty(this string value, string defaultValue)
        {
            return (value.IsNotEmpty() ? value : defaultValue);
        }

        /// <summary>
        /// 是否是字母
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsAlpha(this string @this)
        {
            return !Regex.IsMatch(@this, "[^a-zA-Z]");
        }

        /// <summary>
        /// 是否字母加数字
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(this string @this)
        {
            return !Regex.IsMatch(@this, "[^a-zA-Z0-9]");
        }

        /// <summary>
        /// 比较两个字符串内的字母是否一致
        /// </summary>
        /// <param name="this"></param>
        /// <param name="otherString"></param>
        /// <returns></returns>
        public static bool IsAnagram(this string @this, string otherString)
        {
            return @this
                .OrderBy(c => c)
                .SequenceEqual(otherString.OrderBy(c => c));
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string @this)
        {
            return @this == "";
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="this"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsLike(this string @this, string pattern)
        {
            // Turn the pattern into regex pattern, and match the whole string with ^$
            string regexPattern = "^" + Regex.Escape(pattern) + "$";

            // Escape special character ?, #, *, [], and [!]
            regexPattern = regexPattern.Replace(@"\[!", "[^")
                .Replace(@"\[", "[")
                .Replace(@"\]", "]")
                .Replace(@"\?", ".")
                .Replace(@"\*", ".*")
                .Replace(@"\#", @"\d");

            return Regex.IsMatch(@this, regexPattern);
        }

        /// <summary>
        /// 是否不为空
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this string @this)
        {
            return @this != "";
        }

        /// <summary>
        /// 是否不为空或者null
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string @this)
        {
            return !string.IsNullOrEmpty(@this);
        }

        /// <summary>
        /// 不是由null或者空格组成
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean IsNotNullOrWhiteSpace(this string value)
        {
            return !String.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string @this)
        {
            return !Regex.IsMatch(@this, "[^0-9]");
        }

        /// <summary>
        /// 是顺序倒序都一致
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsPalindrome(this string @this)
        {
            // Keep only alphanumeric characters

            var rgx = new Regex("[^a-zA-Z0-9]");
            @this = rgx.Replace(@this, "");
            return @this.SequenceEqual(@this.Reverse());
        }
        #endregion

        /// <summary>
        /// 转换为byte[]
        /// </summary>
        /// <param name="sourse"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string sourse)
        {
            if (string.IsNullOrEmpty(sourse)) return new byte[] { };
            return UTF8Encoding.UTF8.GetBytes(sourse);
        }

        /// <summary>
        /// 转换为枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string @this)
        {
            Type enumType = typeof(T);
            return (T)Enum.Parse(enumType, @this);
        }

        /// <summary>
        /// 转换为隐藏手机号
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToHidePhone(this string @this)
        {
            if (!@this.IsNumeric()) return string.Empty;
            var result = new StringBuilder();
            if (string.IsNullOrEmpty(@this)) return "";
            if (@this.Length < 11) return "****";
            result.Append(string.Join("", @this.ToCharArray().Take(3)));
            result.Append("****");
            result.Append(string.Join("", @this.ToCharArray().Skip(7).Take(4)));
            return result.ToString();
        }

        /// <summary>
        /// 截取长度 , 多余用 ...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Truncate(this string @this, int maxLength)
        {
            const string suffix = "...";

            if (@this == null || @this.Length <= maxLength)
            {
                return @this;
            }

            int strLength = maxLength - suffix.Length;
            return @this.Substring(0, strLength) + suffix;
        }

        /// <summary>
        /// 截取长度
        /// </summary>
        /// <param name="this"></param>
        /// <param name="maxLength"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string Truncate(this string @this, int maxLength, string suffix)
        {
            if (@this == null || @this.Length <= maxLength)
            {
                return @this;
            }

            int strLength = maxLength - suffix.Length;
            return @this.Substring(0, strLength) + suffix;
        }
    }
}
