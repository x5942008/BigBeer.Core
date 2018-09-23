using System;
using System.Collections.Generic;
using System.Text;

namespace BigBeer.Framework.Mvc.ImageService
{
    /// <summary>
    /// http 返回值
    /// </summary>
    public class HttpResult
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; } = true;
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static HttpResult Success(string message = "ok", object data = null)
        {
            return new HttpResult() { Message = message, Data = data };
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static HttpResult Faild(string message = "error", object data = null)
        {
            return new HttpResult() { Message = message, Status = false, Data = data };
        }
    }
}
