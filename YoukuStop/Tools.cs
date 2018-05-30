using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoukuStop
{
    /// <summary>
    /// 工具类
    /// </summary>
    public  class Tools
    {
        private static IConfigurationRoot Config = new Microsoft.Extensions.Configuration.ConfigurationBuilder().AddJsonFile("config.json").Build();


        public static string Test => Config["Data:A:B:C:D"];

        //需要修改前往App.Config修改Key即可。
        public static string Process => ConfigurationManager.AppSettings["Process"];

        public static string One_Name => Config["Yu"];
        public static string Two_Name => Config["Data:Youku"];
        public static string Twoo_Name => Config["Data:wangyi"];
    }
}
