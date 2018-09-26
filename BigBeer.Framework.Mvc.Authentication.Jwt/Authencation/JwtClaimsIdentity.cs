using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Framework.Mvc.Authentication.Jwt.Authencation
{
    public class JwtClaimsIdentity:ClaimsIdentity
    {

        public JwtClaimsIdentity(IEnumerable<Claim> claims, string authenticationType) : base(claims, authenticationType)
        {

        }
        protected string UserName { get; set; }

        protected bool IsUserAuthenticated { get; set; }

        public override string Name => UserName;
        public override bool IsAuthenticated => IsUserAuthenticated;
        /// <summary>
        /// 成功认证
        /// </summary>
        /// <param name="userNameOrId"></param>
        /// <param name="isAuthenticated"></param>
        /// <returns></returns>
        public JwtClaimsIdentity Authenticated(string userNameOrId = null, bool isAuthenticated = true)
        {
            UserName = userNameOrId;
            IsUserAuthenticated = isAuthenticated;
            return this;
        }
    }
}
