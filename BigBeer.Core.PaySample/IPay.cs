using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Core.PaySample
{
    /// <summary>
    /// 订单处理接口
    /// </summary>
    public interface IPay
    {
        IPayResult OrderPay(string OrderNo);
        Task<IPayResult>OrderPayAsync(string OrderNo);
    }
}
