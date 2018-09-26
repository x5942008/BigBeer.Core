using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigBeer.Core.Extensions;

namespace BigBeer.Framework.Mvc.Authentication.Jwt.Authenzation
{
    public class JwtAuthenzationOptions
    {

        #region 基本属性
        /// <summary>
        /// 加密字符串
        /// </summary>
        public string Secret { get; set; } = string.Empty;
        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan ExpireTimeSpan { get; set; } = TimeSpan.FromDays(30);
        /// <summary>
        /// token请求路径
        /// </summary>
        public string TokenEndpointPath { get; set; } = "/oauth";
        /// <summary>
        /// 认证类型
        /// </summary>
        public string AuthenticationType { get; set; } = "JWT";
        #endregion

        #region 事件
        /// <summary>
        /// 生成票据(生成token)
        /// </summary>
        public Func<JwtToken, JwtAuthTicket> OnCreateTicket { get; set; } = (jwt) =>
        {
            //再次加密token
            var ticket = new JwtAuthTicket()
            {
                token = jwt.Signature.AESEncrypt(jwt.Secret)
            };
            if (jwt.Payload.ContainsKey("exp"))
                ticket.expire = (long)(jwt.Payload["exp"]);
            if (jwt.Payload.ContainsKey("iss"))
                ticket.iss = jwt.Payload["iss"].ToString();
            if (jwt.Payload.ContainsKey("iat"))
                ticket.iat = (long)(jwt.Payload["iat"]);
            if (jwt.Payload.ContainsKey("type"))
                ticket.type = jwt.Payload["type"].ToString();
            return ticket;
        };
        /// <summary>
        /// 签名
        /// </summary>
        public Func<string, string, string> OnSignature { get; set; } =
            (befor, secret) =>
            {
                return string.Concat(befor, ".", befor.AESEncrypt(secret));
            };
        /// <summary>
        /// 验证用户
        /// </summary>
        public Func<HttpContext, VerifyOwnerResult> OnVerifyOwner { get; set; } = (context) => {
            return VerifyOwnerResult.None();
        };
        /// <summary>
        /// 格式化返回参数
        /// </summary>
        public Func<(bool status, string message, int code, object data), object> ResultFormat { get; set; } = (param) => {
            object result = new
            {
                s = param.status,
                m = param.message,
                c = param.code,
                d = param.data
            };
            return result;
        };
        #endregion
    }
}
