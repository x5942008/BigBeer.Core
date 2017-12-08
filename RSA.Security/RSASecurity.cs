using System;
using System.Text;

namespace RSA.Security
{
    public static class RSASecurity
    {
        private static readonly RSAHelper help = new RSAHelper();

        /// <summary>
        /// RSA2加密
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string EnStr(this string @this)
        {
            return help.Encrypt(@this);
        }
        /// <summary>
        /// RSA2解密
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string DeStr(this string @this)
        {
            return help.Decrypt(@this);
        }
        /// <summary>
        /// RSA2签名
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Sign(this string @this)
        {
            return help.Sign(@this);
        }
        /// <summary>
        /// RSA2验证签名
        /// </summary>
        /// <param name="this">签名返回值</param>
        /// <param name="str">原始字符串</param>
        /// <returns></returns>
        public static bool VerifyStr(this string @this,string str)
        {
            return help.Verify(str,@this);
        }
    }
}
