using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.Core.HelperSample
{
   public  class StringHelper
    {
        /// <summary>
        /// 生成一个随机的验证码字符串
        /// </summary>
        /// <param name="num">需要多少位</param>
        /// <returns></returns>
        public static string RandomString(string sb = "asdfghjklqwertyuiopzxcvbnm23456789", int num = 6)
        {
            StringBuilder result = new StringBuilder();
            Random r = new Random();

            for (int i = 0; i < num; i++)
            {
                int ordinate = r.Next(sb.Length);

                result.Append(sb.Substring(ordinate, 1));
            }

            return result.ToString();
        }
    }
}
