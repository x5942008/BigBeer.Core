using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBeer.Core.PayNotifys
{
    /// <summary>
    /// 微信支付配置类
    /// </summary>
    public class WeichatPay
    {
        public string WeiAppId { get; set; } = "wxe077b56c4aceeb7d";

        public string WeiMachId { get; set; } = "1485623012";

        public string DefaultIp { get; set; } = "192.168.0.122";

        public string WeiKey { get; set; } = "pJvqTF10WimCYsTkXmUtYPZvucj6PmDH";

        public string NoticUrl { get; set; } = "http://pay.buydee.org/WeChat/WxNotify";


        /// <summary>
        /// 是否调试模式 
        /// </summary>
        /// <returns></returns>
        public static WeichatPay GetConfig()
        {
            var isDebug = bool.Parse(StaticKey.Configuration["IsDebug"]);
            if (isDebug)
            {
                return new WeichatPay();
            }
            return new WeichatPay()
            {
                DefaultIp = StaticKey.Configuration["DefaultIp"],
                WeiKey = StaticKey.Configuration["WeiKey"],
                NoticUrl = StaticKey.Configuration["NoticUrl"],
            };
        }
    }
}
