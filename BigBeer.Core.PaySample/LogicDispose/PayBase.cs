using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Core.PaySample.LogicDispose
{
    /// <summary>
    /// 支付基类抽象
    /// </summary>
    public abstract class PayBase : IPay
    {
        public abstract IPayResult OrderPay(string OrderNo);
        public abstract Task<IPayResult> OrderPayAsync(string OrderNo);
    }
    /// <summary>
    /// 返回数据类型接口实现
    /// </summary>
    public class PayResult : IPayResult
    {
        public PayResultStatus Status { get; set; } = PayResultStatus.Sucess;
        public (string Name, string Message, string Url, string OrderNo, string PayType, string PayMethod) NotifyData { get; set; }
    }
}
