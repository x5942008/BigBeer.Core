using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoukuStop
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class Extensions
    {
        private static IConfigurationRoot Config = new ConfigurationBuilder().AddJsonFile("config.json").Build();

        /// <summary>
        /// Json获取
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetJson(this string @this)
        {
            if (@this.Contains(','))
            {
                var data = @this.Split(',');
                if (data.Count() > 1)
                {
                    var temp = data[0];
                    for (int i = 1; i < data.Count(); i++)
                    {
                        temp += ":" + data[i];
                    }
                    return Config[temp];
                }
            }
            return Config[@this];
        }
    }
}
