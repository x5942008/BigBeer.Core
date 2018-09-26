using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Framework.Mvc.Authentication.Jwt
{
    public class JwtAuthTicket
    {
        /// <summary>
        /// token
        /// </summary>
        public string token { get; set; } = string.Empty;
        /// <summary>
        /// 过期时间
        /// </summary>
        public long expire { get; set; } = 0;
        /// <summary>
        /// 颁发人
        /// </summary>
        public string iss { get; set; } = "";
        /// <summary>
        /// 颁发时间
        /// </summary>
        public long iat { get; set; } = 0;
        /// <summary>
        /// 所属用户
        /// </summary>
        public string type { get; set; } = "Bearer";
    }
}
