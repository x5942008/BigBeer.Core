using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Framework.Mvc.Authentication.Jwt
{
   public class JwtToken
    {
        /// <summary>
        /// 头部
        /// </summary>
        public JwtHeader Header { get; set; } = new JwtHeader();
        /// <summary>
        /// 载荷(内容)
        /// </summary>
        public JwtPayload Payload { get; set; } = new JwtPayload();
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; } = string.Empty;
        /// <summary>
        /// 加密密钥
        /// </summary>
        public string Secret { get; set; }
    }
}
