using Alipay.AopSdk.Core;
using Alipay.AopSdk.Core.Domain;
using Alipay.AopSdk.Core.Request;
using Alipay.AopSdk.Core.Response;
using Alipay.AopSdk.Core.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BigBeer.Core.PaySample;
using BigBeer.Core.PaySample.LogicDispose;
using Microsoft.AspNetCore.Http;

namespace BigBeer.Core.PayNotifys.Controllers
{
        /// <summary>
        /// 支付宝支付
        /// </summary>
        public class AlipayController : BaseController
        {
        IPay PayServer;
        IPayResult payResult;
        public AlipayController(PayServer payServer,PayResult payResult)
        {
            PayServer = payServer;
            this.payResult = payResult;
        }
            /// <summary>
            /// 支付宝公钥
            /// </summary>
            string AlipaypublicKey { get; set; } = StaticKey.Alipay_PublicKey;
            /// <summary>
            /// 生成支付宝订单
            /// </summary>
            /// <param name="body">描述</param>
            /// <param name="subject">商品描述</param>
            /// <param name="totalamount">总价</param>
            /// <param name="orderNo">APP订单号</param>
            /// <param name="timeout">过期时间（默认30分钟）后缀可修改时间单位</param>
            /// <returns></returns>
            public JsonResult OrderSign(string body, string subject, string orderNo, string timeout = null)
            {
                string OrderNumber = "alipay" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string app_id = StaticKey.App_id;     //"你的app_id";
                string merchant_private_key = StaticKey.Merchant_private_key;// "你的应用私钥";、
                                                                             //merchant_private_key = HttpUtility.UrlDecode(merchant_private_key); 自己测试添加使用
                string alipay_public_key = StaticKey.Alipay_public_key;//"你的支付宝公钥";
                string timeout_express = StaticKey.Timeout_express; //订单有效时间（分钟）
                string postUrl = StaticKey.PostUrl;//支付宝请求的网关地址
                string sign_type = "RSA2";//加签方式 有两种RSA和RSA2（支付宝推荐的RSA2）
                string version = "1.0";//固定值 不用改
                string format = "json";//固定值
                                       //string Amount = "0.01";//订单金额
                string method = StaticKey.MethodApp;//调用接口 固定值 不需要再改 此处使用的是APP支付
                                                    //初始化
                IAopClient client = new DefaultAopClient(postUrl, app_id, merchant_private_key, format, version, sign_type, alipay_public_key, "UTF-8", true);
                AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
                request.SetNotifyUrl(StaticKey.SetNotifyUrl);//支付宝后端通知地址
                                                             //request.SetReturnUrl(StaticKey.SetReturnUrl);//支付宝前端回跳地址
                //var moneys = db.CapitalRecords.FirstOrDefault(t => t.OrderNo == orderNo).Money; //价格不作为参数  直接查询数据库得到 避免篡改
                //var totalamount = Math.Abs(moneys).ToString(); //有些订单为负数  保证订单价格不能为负数
                AlipayTradeAppPayModel model = new AlipayTradeAppPayModel
                {
                    Body = body,
                    Subject = subject,
                    //TotalAmount = totalamount,
                    ProductCode = "QUICK_MSECURITY_PAY",
                    OutTradeNo = orderNo,
                    TimeoutExpress = "30m"
                };
                if (timeout.IfNotNullOrEmpty()) model.TimeoutExpress = timeout;
                request.SetBizModel(model);
                AlipayTradeAppPayResponse response = client.SdkExecute(request);
                var result = HttpUtility.HtmlEncode(response.Body);//避免出现转义字符进行处理，返回JSON时内容多出了mmp,在前端页面进行了处理，返回string类型的时候正常
                return Json(new { result });
            }
            /// <summary>
            /// 验证签名
            /// </summary>
            /// <param name="formCollection"></param>
            /// <returns></returns>
            private bool CheckSignature(Dictionary<string, string> formCollection)
            {
                return AlipaySignature.RSACheckV1(formCollection, AlipaypublicKey, "utf-8", "RSA2", false);
            }

        /// <summary>
        /// 支付宝回调处理
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ContentResult> AlipayNotify()
        {
            Console.WriteLine("->alipay notify functions inside !");
            var formsString = new System.IO.StreamReader(Request.Body).ReadToEnd();
            if (formsString.IsNullOrEmpty())
            {
                Console.WriteLine("-> no parameters input");
                return Content("null paramters");
            }
            //log(formsString);
            var parameters = QueryHelpers.ParseQuery(formsString)
                .ToDictionary(t => t.Key, t => t.Value.ToString());

            var flag = CheckSignature(parameters);
            Console.WriteLine($"->alipay signar checked:{flag}");
            if (!flag)
            {
                return Content("error");
            }
            var orderid = parameters["out_trade_no"];
            var aliStatus = parameters["trade_status"];
            var aliMoney = decimal.Parse(parameters["total_amount"]);

            var param = $"aliStatus:{aliStatus},aliMoney:{aliMoney}";
            Console.WriteLine($"->alipay parameters : {param}");
            await log($"no:{orderid}");
            await log(aliMoney.ToString());
            await log(aliStatus);
            await log($"paytime:{parameters["gmt_payment"]}");
            await log($"buyer:{parameters["buyer_logon_id"]}");
            //var order = Db.CapitalRecords.Include(t => t.Member)
            //    .FirstOrDefault(t => t.No == orderid);
            //if (order.Status == PayStatus.Success)
            //    return Content("success", "text/plane");
            //if (order.Money != aliMoney)
            //    return Content("success", "text/plane");
            switch (aliStatus)
            {
                case "WAIT_BUYER_PAY":
                    return Content("success", "text/plane");
                case "TRADE_CLOSED":
                    //order.Status = PayStatus.Expired;
                    //Db.CapitalRecords.Where(t => t.ID == orderid).Update(t => new CapitalRecord() { Status = PayStatus.Expired });
                    break;
                case "TRADE_SUCCESS":
                    //order.Status = PayStatus.Success;
                    //Db.CapitalRecords.Where(t => t.ID == orderid).Update(t => new CapitalRecord() { Status = PayStatus.Success, Method = PayMethod.Alipay });
                    break;
                case "TRADE_FINISHED":
                    //order.Status = PayStatus.Finished;
                    //Db.CapitalRecords.Where(t => t.ID == orderid).Update(t => new CapitalRecord() { Status = PayStatus.Finished });
                    break;
            }
            //if (order.Status != PayStatus.Success)
            //{
            //    await Db.SaveChangesAsync();
            //    return Content("success", "text/plane");
            //}
            try
            {
                //await Db.SaveChangesAsync();
                Console.WriteLine($"-> begin pay ...");
                var result = await PayServer.OrderPayAsync(orderid);
                Console.WriteLine($"->alipay paystatus : {result.Status.ToString()}");
                await Log(result.Status.ToString());
                if (result.Status == payResult.Status)
                {
                    //var mid = order.Member.MapId(Db);
                    Action action = async () =>
                    {
                        //try
                        //{
                        //    await QueuePlugProvider.Create(Queue.Message)
                        //.SendAsync(new
                        //{
                        //    title = result.NotifyData.Name,
                        //    content = result.NotifyData.Message,
                        //    type = (int)result.NotifyData.Type,
                        //    sender = "系统",
                        //    user = mid
                        //});
                        await Log("消息发送成功");
                    };
                        //catch (Exception ex)
                        //{
                        //    await Log($"消息发送失败 ex :{ex.Message}");
                        //}
                    await Task.Run(action);
                    //await PayServer .Activity(HttpContext, Db, order.No);   如果有活动进行处理 增加扩展处理 在底部
                    return Content("success", "text/plane");
                }
                else
                    return Content("error", "text/plane");

            }
            catch (Exception ex)
            {
                log(ex.Message);
                log(formsString);
                return Content("error", "text/plane");
            }
        }
    }
    #region 活动处理扩展 上方的示范例子
    /// <summary>
    /// 扩展类
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 计算活动
        /// 续期1个月送6000两银两
        /// </summary>
        /// <param name="renew"></param>
        /// <param name="httpContent"></param>
        /// <param name="db"></param>
        /// <param name="orderNo">订单ID</param>
        /// <returns></returns>
        public static Task Activity(this IPay payment, HttpContext httpContext/*，CusumerDb Db*/, string orderNo)
        {
            //    //结束时间
            //    var endDate = new DateTime(2018, 1, 1);
            //    //活动结束
            //    if (endDate < DateTime.Now) return Task.CompletedTask;
            //    //活动主体为续期

            //    var order = Db.CapitalRecords.Include(t => t.Member).First(t => t.No == orderNo);
            //    if (order == null)
            //        return httpContext.Log($"订单不存在:{orderNo}", "IPay/Activity");
            //    if (order.Status != Cusumer.Lib.Enums.PayStatus.Success)
            //        return httpContext.Log($"订单状态不成功:{orderNo}", "IPay/Activity");
            //    //针对续期进行活动
            //    if (order.Type != CapitalRecordType.Renew)
            //    {
            //        return httpContext.Log($"该订单不存在活动:{order.Type.ToString()}", "IPay/Activity");
            //    }
            //    //续期1个月送6000两银子
            //    var money = 6000M * order.Number;

            //    var bag = Db.MoneyBags.Include(t => t.Member)
            //        .Where(t => t.Type == MoneyBagType.Gold)
            //        .Where(t => t.Member.ID == order.Member.ID)
            //        .First();
            //    //更新余额
            //    Db.MoneyBags.Where(t => t.ID == bag.ID).Update(t => new MoneyBag() { Money = t.Money + money });
            //    try
            //    {
            //        Db.SaveChanges();
            //        //系统消息
            //        Message message = new Message()
            //        {
            //            Content = $"恭喜参与首期续期送银两活动获得银两[{money}]两,银两可以参与[升官发财]提升官阶享受俸禄哦,恭喜大人,贺喜大人!",
            //            Member = order.Member,
            //            Sender = "系统消息",
            //            Type = MessageType.Sys,
            //            Title = "奖励通知"
            //        };
            //        Db.Messages.Add(message);
            //        //账单
            //        Db.CapitalRecords.Add(new CapitalRecord()
            //        {
            //            Content = message.Content,
            //            Member = order.Member,
            //            Method = PayMethod.Sys,
            //            Money = money,
            //            Number = 1,
            //            Status = PayStatus.Success,
            //            PayTime = DateTime.Now,
            //            Title = "活动奖励",
            //            Type = CapitalRecordType.SilverWage
            //        });

            //        Db.SaveChanges();
            //        var mid = order.Member.MapId(Db);
            //        httpContext.SendMessage(Queue.VerifyCode, new
            //        {
            //            phone = order.Member.Phone,
            //            message = message.Content
            //        }).GetAwaiter().GetResult();
            //        return httpContext.SendMessage(Queue.Message, new
            //        {
            //            title = "活动奖励",
            //            content = message.Content,
            //            type = (int)MessageType.Sys,
            //            sender = "系统",
            //            user = mid
            //        });
            //    }
            //    catch (Exception ex)
            //    {
            //        return httpContext.Log($"活动计算错误:{ex.Message}", "IPay/Activity");
            //    }
            //}
            return Task.CompletedTask; //多加的 防止报错
    }
}
    #endregion
}
