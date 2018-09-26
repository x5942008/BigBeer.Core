using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BigBeer.Core.Extensions;

namespace BigBeer.Framework.Mvc.Authentication.Jwt.Authencation
{
    public class JwtClaimsPrincipal:ClaimsPrincipal
    {
        public JwtClaimsPrincipal(IIdentity identity) : base(identity)
        {

        }
        public override bool IsInRole(string role)
        {
            try
            {
                var claimsIdentity = (JwtClaimsIdentity)Identity;
                if (claimsIdentity.Claims.Any(t => t.Type == "role"))
                {
                    var roles = claimsIdentity.Claims.FirstOrDefault(t => t.Type == "role").Value.ToObject<string[]>();
                    return roles.Contains(role);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

    }
}
