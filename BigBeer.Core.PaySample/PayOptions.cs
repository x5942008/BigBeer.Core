using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.Core.PaySample
{
    /// <summary>
    /// 通过类型查找执行类
    /// </summary>
     public  class PayOptions
    {

        /// <summary>
        /// 支付对象处理
        /// 枚举Enum暂时用string代替 需要用再修改
        /// </summary>
        public static IDictionary<string, Type> Options { get; } = new Dictionary<string, Type>();

        /// <summary>
        /// 新增一个订单处理对象
        /// </summary>
        /// <param name="payType"></param>
        /// <param name="pay"></param>
        /// <returns></returns>
        public PayOptions Use(string payType, IPay pay)
        {
            if (Options.ContainsKey(payType)) return this;
            Options.Add(payType, pay.GetType());
            return this;
        }
        /// <summary>
        /// 添加一个订单处理对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="payType"></param>
        /// <returns></returns>
        public PayOptions Use<T>(string payType) where T : IPay
        {
            if (Options.ContainsKey(payType)) return this;
            Options.Add(payType, typeof(T));
            return this;
        }
    }
}
