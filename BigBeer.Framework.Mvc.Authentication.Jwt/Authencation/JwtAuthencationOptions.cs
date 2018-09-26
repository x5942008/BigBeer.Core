using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Framework.Mvc.Authentication.Jwt.Authencation
{
    public class JwtAuthencationOptions:JwtBearerOptions
    {
        public JwtAuthencationOptions() : base()
        {

        }
        /// <summary>
        /// 加密字符串
        /// </summary>
        public string Serect { get; set; }

        public new IList<ISecurityTokenValidator> SecurityTokenValidators => new List<ISecurityTokenValidator>() { new JwtAuthencationCoreSecurityTokenHandler(this) };
    }
}
