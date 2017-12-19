using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.Core.PaySample
{
    /// <summary>
    /// 订单返回值
    /// </summary>
    public interface IPayResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        PayResultStatus Status { get; set; }
        /// <summary>
        /// type method为本地数据库的类型 
        /// 暂时用string代替
        /// </summary>
        (string Name, string Message, string Url, string OrderNo, string PayType ,string PayMethod) NotifyData { get;set; }
    }
    /// <summary>
    /// 订单处理状态
    /// </summary>
    public enum PayResultStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        Sucess,
        /// <summary>
        /// 失败
        /// </summary>
        Faild,
        /// <summary>
        /// 错误
        /// </summary>
        Error,
        /// <summary>
        /// 延误
        /// </summary>
        Daley,
        /// <summary>
        /// 关闭
        /// </summary>
        Close
    }
}
