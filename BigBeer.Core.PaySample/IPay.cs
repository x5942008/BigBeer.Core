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
        /// <summary>
        /// 支付接口
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        IPayResult OrderPay(string OrderNo);
        /// <summary>
        /// 多线程
        /// 支付接口
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        Task<IPayResult>OrderPayAsync(string OrderNo);
    }
}
