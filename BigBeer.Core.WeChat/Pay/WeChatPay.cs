using BigBeer.Core.HelperSample;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Core.WeChat.Pay
{
public class WeChatPay
    {
        /// <summary>
        /// 获取默认实例
        /// </summary>
        /// <returns></returns>
        public WeChatPay GetExample()
        {
            return new WeChatPay();
        }

        /// <summary>
        /// 获取自定义配置实例
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public WeChatPay GetExample(Config config)
        {
            var wechatPay = new WeChatPay()
            {
                Config = config
            };
            return wechatPay;
        }

        /// <summary>
        /// 配置信息
        /// </summary>
        public Config Config { get; set; } = new Config();

        /// <summary>
        /// 订单描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 系统订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal Money { get; set; }
        
        /// <summary>
        /// 获取回调参数并返回验签结果
        /// </summary>
        /// <returns></returns>
        public SortedList<string, string> ValidateSign()
        {
            try
            {
                //TODO
                var stream = HttpContext.Current.Request.InputStream;
                StreamReader reader = new StreamReader(stream);
                var str = reader.ReadToEnd();
                string sginString = null;
                var requestDictionary = str.ToObjectXml<Dictionary<string, string>>();
                SortedList<string, string> sort = new SortedList<string, string>();
                foreach (var item in requestDictionary)
                {
                    if (!item.Key.Equals("sign"))
                    {
                        sort.Add(item.Key, item.Value);
                    }
                    else
                    {
                        sginString = item.Value;
                    }
                }
                var signNews = Sign(sort);
                if (signNews.Equals(sginString))
                {
                    
                    return sort;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }    
        }

        /// <summary>
        /// 获取签名字符串
        /// </summary>
        /// <returns></returns>
        public async Task<string> SignStringAsync()
        {
            try
            {
                var total_fee = Money * 100;
                Dictionary<string, string> parms = new Dictionary<string, string>()
            {
                {"body",Describe},
                {"out_trade_no",OrderNo},
                {"total_fee",Math.Round(total_fee,0).ToString()}
            };
                var sort = SetParams(parms);
                var param = sort.ToXml<SortedList<string, string>>();
                var postResult = await HttpHelper.PostAsync("https://api.mch.weixin.qq.com/pay/unifiedorder", param, contentType: "text/xml");
                var wxCode = postResult.ToObjectXml<Dictionary<string, string>>();
                SortedList<string, string> sortRresult = new SortedList<string, string>
            {
                { "appid", wxCode["appid"] },
                { "partnerid", wxCode["mch_id"]},
                { "prepayid", wxCode["prepay_id"]},
                { "package", "Sign=WXPay" },
                { "noncestr",RandomCode(32).ToUpper() },
                { "timestamp", DateTimeHelper.GetUnixTimestamp(1000).ToString() },
            };
                var sign = Sign(sortRresult);
                var result = new
                {
                    apiKey = sortRresult["appid"],
                    orderId = sortRresult["prepayid"],
                    mchId = sortRresult["partnerid"],
                    nonceStr = sortRresult["noncestr"],
                    timeStamp = sortRresult["timestamp"],
                    package = sortRresult["package"],
                    sign = sign
                };
                return result.ToJson();
            }
            catch (Exception)
            {
                return "false";
            }
        }
        
        /// <summary>
        /// 随机生产字符串
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private string RandomCode(int num = 32)
        {
            var result = StringHelper.RandomString("qwertyuiopasdfghjklzxcvbnm0123456789ASDFGHJKLZXCVBNMPOIUYTREWQ", num);
            return result;
        }
        
        /// <summary>
        /// 设置请求参数
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        private SortedList<string, string> SetParams(Dictionary<string, string> parms)
        {
            SortedList<string, string> result = new SortedList<string, string>()
            {
                {"appid",Config.WeiAppId},
                {"mch_id",Config.WeiMachId},
                {"notify_url",Config.NoticUrl},
                {"spbill_create_ip",Config.DefaultIp},
                {"nonce_str",RandomCode(32).ToUpper()},
                {"device_info","WEB"},
                {"sign_type","MD5"},
                {"fee_type","CNY"},
                {"trade_type","APP"}
            };
            foreach (var item in parms)
            {
                result.Add(item.Key, item.Value);
            }
            var sign = Sign(result);
            result.Add("sign", sign);
            return result;
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        private string Sign(SortedList<string, string> parms)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in parms)
            {
                sb.Append($"{item.Key}={item.Value}&");
            }
            var str = sb.ToString().Substring(0, sb.ToString().Length - 1);
            string key = Config.WeiKey;
            var stringSignTemp = $"{str}&key={key}";
            var result = stringSignTemp.EncrypMD5().ToUpper();
            return result;
        }
    }
}
