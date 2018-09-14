using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Framework.Mvc.Authentication.Jwt.Authenzation
{
    /// <summary>
    /// 验证用户返回值
    /// </summary>
    public class VerifyOwnerResult
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; protected set; } = string.Empty;
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Successed { get; protected set; } = false;
        /// <summary>
        /// 是否没有验证
        /// </summary>
        public bool IsNone { get; protected set; } = false;
        /// <summary>
        /// 数据内容
        /// </summary>
        public IEnumerable<Claim> Cliams { get; protected set; }
        /// <summary>
        /// 没有验证
        /// </summary>
        /// <returns></returns>
        public static VerifyOwnerResult None()
        {
            return new VerifyOwnerResult()
            {
                IsNone = true
            };
        }
        /// <summary>
        /// 验证成功
        /// </summary>
        /// <param name="cliams"></param>
        /// <returns></returns>
        public static VerifyOwnerResult Sucess(Dictionary<string, string> cliams)
        {
            return new VerifyOwnerResult()
            {
                Successed = true,
                Cliams = cliams.Select(t => new Claim(t.Key, t.Value))
            };
        }
        /// <summary>
        /// 验证失败
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static VerifyOwnerResult Faild(string message)
        {
            return new VerifyOwnerResult()
            {
                Successed = false,
                Message = message
            };
        }
    }
}
