using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BigBeer.Core.WeChat.Pay
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class Config
    {
        private static IConfigurationRoot Configuration = new ConfigurationBuilder().AddJsonFile("config.json").Build();

        public string WeiAppId { get; set; } = Configuration["WeiAppId"]/* "wxe077b56c4aceeb7x"*/;

        public string WeiMachId { get; set; } = Configuration["WeiMachId"] /*"1485623012"*/;

        public string DefaultIp { get; set; } = Configuration["DefaultIp"]/*"192.168.0.122"*/;

        public string WeiKey { get; set; } = Configuration["WeiKey"] /*"pJvqTF10WimCYsTkXmUtYPZvucj6PmDX"*/;

        public string NoticeUrl { get; set; } = Configuration["noticeurl"] /*"http://10.0.0.5/WeChat/WxNotify"*/;

        public static Config GetConfig()
        {
            var isDebug = bool.Parse(Configuration["IsDebug"]);
            if (isDebug)
            {
                return new Config();
            }
            return new Config()
            {
                DefaultIp = Configuration["DefaultIp"],
                WeiKey = Configuration["WeiKey"],
                NoticeUrl = Configuration["noticeurl"]
            };
        }
    }
}
