using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBeer.Core.PayNotifys
{
    public static class StaticKey
    {
        private static IConfigurationRoot ConfigRoot { get; set; }
        private static IConfigurationRoot Config
        {
            get
            {
                if (ConfigRoot == null)
                {
                    ConfigRoot = new ConfigurationBuilder()
           .AddJsonFile("Appsettings.json").Build();
                    return ConfigRoot;
                }
                return ConfigRoot;
            }
        }
        /// <summary>
        /// 日志保存路劲
        /// </summary>
        public static string LogPath
        {
            get
            {
                return Config["LogPath"];
            }
        }
        /// <summary>
        /// 支付宝AppID
        /// </summary>
        public static string App_id
        {
            get
            {
                return Config["App_id"];
            }
        }
        /// <summary>
        /// 支付宝应用公钥
        /// </summary>
        public static string Alipay_PublicKey
        {
            get
            {
                return Config[" Alipay_PublicKey"];
            }
        }
       

        /// <summary>
        /// 支付宝应用私钥
        /// </summary>
        public static string Merchant_private_key
        {
            get
            {
                return Config["Merchant_private_key"];
            }
        }
        /// <summary>
        /// 你的支付宝公钥
        /// </summary>
        public static string Alipay_public_key
        {
            get
            {
                return Config["Alipay_public_key"];
            }
        }
        /// <summary>
        /// 支付宝订单有效时间
        /// </summary>
        public static string Timeout_express
        {
            get
            {
                return Config["Timeout_express"];
            }
        }
        /// <summary>
        /// 支付宝请求的网关地址
        /// </summary>
        public static string PostUrl
        {
            get
            {
                return Config["PostUrl"];
            }
        }
        /// <summary>
        /// 支付类型
        /// APP支付 网页支付之类的
        /// </summary>
        public static string MethodApp
        {
            get
            {
                return Config[" MethodApp"];
            }
        }
        /// <summary>
        /// 支付宝后端通知地址
        /// 异步回调地址 
        /// 支付成功处理地址
        /// </summary>
        public static string SetNotifyUrl
        {
            get
            {
                return Config["SetNotifyUrl"];
            }
        }

    }
}
