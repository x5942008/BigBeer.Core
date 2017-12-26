using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Text;
using System.Xml.Serialization;
using System.Net;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using BigBeer.Core.PaySample;
using Microsoft.AspNetCore.Http;

namespace BigBeer.Core.PayNotifys.Controllers
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WeChatController : BaseController
    {
        private WeichatPay WxPayConfig = WeichatPay.GetConfig();

        /// <summary>
        /// 回调处理
        /// </summary>
        /// <returns></returns>
        public async Task<ContentResult> WxNotify()
        {
            string xml = @"<xml>
<return_code><![CDATA[SUCCESS]]></return_code>
<return_msg><![CDATA[OK]]></return_msg>
</xml>";
            try
            {
                //OnMessage("开始处理请求");
                string returnSign = "";//微信返回签名
                //var stream = Request.InputStream; 
                Stream temp = Request.Body;//代替上一个变量
                StreamReader reader = new StreamReader(temp/*stream*/);
                var str = reader.ReadToEnd();
                XmlDocument document = new XmlDocument();
                document.LoadXml(str);
                var element = document.DocumentElement.ChildNodes;
                SortedList<string, string> sort = new SortedList<string, string>();
                foreach (var item in element)
                {
                    var node = (XmlElement)item;
                    if (!node.Name.Equals("sign"))
                    {
                        sort.Add(node.Name, node.FirstChild.Value);
                    }
                    else
                    {
                        returnSign = node.FirstChild.Value;
                    }
                }
                var signStr = Sign(sort);
                //验证是否微信返回数据
                if (signStr.Equals(returnSign))
                {
                    Log("验证签成功");
                    if (sort["result_code"].Equals("SUCCESS"))
                    {
                        Log("处理支付流程");
                        //支付成功处理
                        var orderNo = sort["out_trade_no"];
                        IPay pay;
                        decimal money;
                        var payMoney = decimal.Parse(sort["total_fee"]) / 100;
                        string userid;
                        #region 返回页面的响应方法 
                        await Response.WriteAsync(xml);
                        //Response.end();//TODO 找不到core中对应的方法
                        #endregion
                        //Member user;
                        //using (var db = FinanceDb.Default)
                        //{
                        //    var order = db.CapitalRecords.FirstOrDefault(t => t.OrderNo == orderNo);
                        //    OnMessage($"订单号:{orderNo},订单类型:{order.Type.Display()}");
                        //    if (order == null)
                        //    {
                        //        Response.Write(xml);
                        //        Response.End();
                        //        return Content(xml);
                        //    }
                        //    if (order.Status != PayStatus.Create || order.Status != PayStatus.Paying)
                        //    {
                        //        Response.Write(xml);
                        //        Response.End();
                        //        return Content(xml);
                        //    }
                        //    pay = PayFunction.Find(order.Type);
                        //    if (pay == null)
                        //    {
                        //        Response.Write(xml);
                        //        Response.End();
                        //        return Content(xml);
                        //    }
                        //(3)修改订单支付方式
                        //    order.PayMethod = PayMethod.Weixin;
                        //    order.Status = PayStatus.Paying;
                        //    money = Math.Abs(order.Money);
                        //    if (money > payMoney)
                        //    {
                        //        order.Status = PayStatus.Fail;
                        //        order.Memo = $"{order.Memo}-支付金额不正确";
                        //        Response.Write(xml);
                        //        Response.End();
                        //        return Content(xml);
                        //    }
                        //    await db.SaveChangesAsync();

                        //    OnMessage($"金额:{money}");
                        //    userid = order.IdentityID;
                        //    user = await db.Members.FindAsync(userid);
                        //}
                        //var payRresult = await pay.PayOrderAsync(orderNo);
                        //(5)发送通知
                        //        if (payRresult.Status != PayResultStatus.Sucess)
                        //        {
                        //            OnMessage($"处理失败");
                        //TODO...
                        //            Response.Write(xml);
                        //            Response.End();
                        //            return Content(xml);
                        //        }
                        //        OnMessage($"处理成功");
                        //        //APP 消息
                        //        await SendMessage(new
                        //        {
                        //            Title = payRresult.NotifyData.Name,
                        //            Content = $"{payRresult.NotifyData.Message}, 成功支付 {money} 元.订单号 : {orderNo},支付方式:{payRresult.NotifyData.method.Display()} , 请注意账户信息.",
                        //            GroupName = "",
                        //            UserName = userid,
                        //            Type = 1,
                        //            From = 2
                        //        }, "QueueAppPush");

                        //        //Email 消息
                        //        if (user.Email.IsNotNullOrEmpty() || user.IsAuthenEmail)
                        //        {
                        //            await SendMessage(new
                        //            {
                        //                TemplateId = "regestor",
                        //                Address = user.Email,
                        //                Subject = "订单支付通知",
                        //                Content = $"订单支付成功,金额:{money},订单号:{orderNo},请注意账户信息."
                        //            }, "QueueEmail");
                        //        }
                        //        if (user.Phone.IsNotNullOrEmpty() || user.IsAuthenPhone)
                        //        {
                        //            //短信要收费,不乱发
                        //            //await SendMessage(new
                        //            //{
                        //            //    Type = "",
                        //            //    Phone = user.Phone,
                        //            //    Content = $"订单支付成功,金额:{money},订单号:{orderNo},请注意账户信息."
                        //            //}, "QueuePhone");
                        //        }
                    }
                }
                    //Response.Write(xml);
                    //Response.End();
                    return Content(xml);
            }
            catch (Exception e)
            {
                Log(e.Message);
                return Content("");
            }
        }

        /// <summary>
        /// 微信签名
        /// </summary>
        /// <param name="body">订单描述</param>
        /// <param name="tradeNo">订单号</param>
        /// <param name="money">金额</param>
        /// <returns></returns>
        public JsonResult GetWxPayOrder(string body, string tradeNo, decimal money)
        {
            //decimal m;
            //using (var db = FinanceDb.Default)
            //{
            //    var tempm  = db.CapitalRecords.FirstOrDefault(t => t.OrderNo == tradeNo);
            //    if (tempm == null)
            //    {
            //        m = money;
            //    }
            //    else
            //    {
            //        m = tempm.Money;
            //    }
            //}
            var total_fee = money * 100;
            Dictionary<string, string> parms = new Dictionary<string, string>()
            {
                {"body",body},
                {"out_trade_no",tradeNo},
                {"total_fee",Math.Round(total_fee,0).ToString()}
            };
            var sort = SetParams(parms);
            var param = GetXmlParms(sort);
            var returnCode = Post("https://api.mch.weixin.qq.com/pay/unifiedorder", param, null, "post", "text/xml");
            returnCode = returnCode.Replace("xml", "WxOrderCode");
            var orderCode = (WxOrderCode)Deserialize<WxOrderCode>(returnCode);
            SortedList<string, string> temp = new SortedList<string, string>
            {
                { "appid", orderCode.appid },
                { "partnerid", orderCode.mch_id },
                { "prepayid", orderCode.prepay_id },
                { "package", "Sign=WXPay" },
                { "noncestr", RandomCode(32).ToUpper() },
                { "timestamp", GetUnixTimestamp().ToString() }
            };
            var sign = Sign(temp);
            var result = new
            {
                apiKey = temp["appid"],
                orderId = temp["prepayid"],
                mchId = temp["partnerid"],
                nonceStr = temp["noncestr"],
                timeStamp = temp["timestamp"],
                package = temp["package"],
                sign = sign
            };
            return Json(new
            {
                c = 0,
                s = true,
                m = "",
                d = result
            });
        }

        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="paramses">参数</param>
        /// <param name="method">方法</param>
        /// <param name="headers">headers</param>
        /// <returns></returns>
        string Post(string url, string paramses = null, Dictionary<string, string> headers = null, string method = "post", string contentType = null, string encod = null)
        {
            //string baseUrl = "http://192.168.0.104:885/";
            //string strURl = $"{baseUrl}{url}?{param}";
            StringBuilder str = new StringBuilder();
            string param = paramses;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentLength = 0;
            request.Method = method;
            if (!string.IsNullOrEmpty(contentType))
            {
                request.ContentType = contentType;
            }
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }
            Encoding encoding;
            if (!string.IsNullOrEmpty(encod))
            {
                encoding = Encoding.GetEncoding(encod);
            }
            else
            {
                encoding = Encoding.UTF8;
            }
            if (paramses != null && method.Equals("post"))
            {
                byte[] bs = Encoding.UTF8.GetBytes(param);
                request.ContentLength = bs.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }
            }
            Stream stream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string result = reader.ReadToEnd();

            return result;
        }
        /// <summary>
        /// 设置请求参数
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        SortedList<string, string> SetParams(Dictionary<string, string> parms)
        {
            SortedList<string, string> result = new SortedList<string, string>()
            {
                {"appid",WxPayConfig.WeiAppId},
                {"mch_id",WxPayConfig.WeiMachId},
                {"notify_url",WxPayConfig.NoticUrl},
                {"spbill_create_ip",WxPayConfig.DefaultIp},
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
        string Sign(SortedList<string, string> parms)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in parms)
            {
                sb.Append($"{item.Key}={item.Value}&");
            }
            var str = sb.ToString().Substring(0, sb.ToString().Length - 1);
            string key = WxPayConfig.WeiKey;
            var stringSignTemp = $"{str}&key={key}";
            var result = MD5(stringSignTemp).ToUpper();
            return result;
        }
        /// <summary>
        /// 随机生产字符串
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        string RandomCode(int num)
        {
            string sb = "qwertyuiopasdfghjklzxcvbnm0123456789ASDFGHJKLZXCVBNMPOIUYTREWQ";
            StringBuilder result = new StringBuilder();
            Random r = new Random();


            for (int i = 0; i < num; i++)
            {
                int ordinate = r.Next(sb.Length);
                result.Append(sb.Substring(ordinate, 1));
            }

            return result.ToString();
        }
        /// <summary>
        /// 生成XML字符串
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        string GetXmlParms(SortedList<string, string> sort)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            foreach (var item in sort)
            {
                sb.Append($"<{item.Key}>{item.Value}</{item.Key}>");
            }
            sb.Append("</xml>");
            return sb.ToString();
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        string MD5(string s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }
            return ret.PadLeft(32, '0');
        }
        /// <summary>
        /// 获取时间戳毫秒数
        /// </summary>
        /// <returns></returns>
        long GetUnixTimestamp()
        {
            var epoch = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000 / 1000;
            return epoch;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        object Deserialize<T>(string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(typeof(T));
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// 获取外网Ip
        /// </summary>
        /// <returns></returns>
        string GetExtranetIp()
        {
            try
            {
                string tempip = "";
                WebRequest request = WebRequest.Create("http://ip.qq.com/");
                request.Timeout = 10000;
                WebResponse response = request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(resStream, System.Text.Encoding.Default);
                string htmlinfo = sr.ReadToEnd();
                //匹配IP的正则表达式
                Regex r = new Regex("((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|\\d)\\.){3}(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|[1-9])", RegexOptions.None);
                Match mc = r.Match(htmlinfo);
                //获取匹配到的IP
                tempip = mc.Groups[0].Value;

                resStream.Close();
                sr.Close();
                return tempip;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
