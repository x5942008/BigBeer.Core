using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.Core.HelperSample
{
    public static class DictionaryHelper
    {
        /// <summary>
        /// 把字典转换成Url请求参数
        /// 例如：name=haos&age=18
        /// </summary>
        /// <param name="paramses"></param>
        /// <returns></returns>
        public static string ToUrlPost(this Dictionary<string, string> paramses)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                string param = "";
                if (paramses != null)
                {
                    foreach (var item in paramses)
                    {
                        str.Append($"{item.Key}={item.Value}&");
                    }
                    var temp = str.ToString();
                    param = temp.Substring(0, str.Length - 1);
                }
                return param;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 把字典转换成Json请求参数
        /// 例如："{name:'bigbeer',age:18}"
        /// </summary>
        /// <param name="paramses"></param>
        /// <returns></returns>
        public static string ToJsonPost(this Dictionary<string, string> paramses)
        {
            try
            {
                string param = paramses.ToJson();
                return param;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
