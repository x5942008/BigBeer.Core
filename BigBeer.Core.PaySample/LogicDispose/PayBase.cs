using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Core.PaySample.LogicDispose
{
    public abstract class PayBase : IPay
    {
        public abstract IPayResult OrderPay(string OrderNo);
        public abstract Task<IPayResult> OrderPayAsync(string OrderNo);
    }
    public class PayResult : IPayResult
    {
        public PayResultStatus Status { get; set; } = PayResultStatus.Sucess;
        public (string Name, string Message, string Url, string OrderNo, string PayType, string PayMethod) NotifyData { get; set; }
    }
}
