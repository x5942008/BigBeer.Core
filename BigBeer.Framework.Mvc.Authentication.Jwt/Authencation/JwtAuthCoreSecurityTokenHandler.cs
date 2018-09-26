using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using BigBeer.Core.Extensions;

namespace BigBeer.Framework.Mvc.Authentication.Jwt.Authencation
{
    public class JwtAuthencationCoreSecurityTokenHandler:JwtSecurityTokenHandler
    {
        protected JwtAuthencationOptions JwtAuthCoreBearerOptions { get; set; }
        public JwtAuthencationCoreSecurityTokenHandler(JwtAuthencationOptions options) : base()
        {
            JwtAuthCoreBearerOptions = options;
        }
        public override bool CanReadToken(string tokenString)
        {
            if (tokenString == null)
            {
                throw new ArgumentNullException("tokenString");
            }

            if (tokenString.Length * 2 > this.MaximumTokenSizeInBytes)
            {
                return false;
            }
            return true;
        }
        public override bool CanWriteToken => base.CanWriteToken;

        public override SecurityToken ReadToken(string token)
        {
            var tokenString = token.AESDecrypt(JwtAuthCoreBearerOptions.Serect);
            string[] tokenParts = tokenString.Split(new char[] { '.' }, 4);
            var header = JwtHeader.Base64UrlDeserialize(tokenParts[0]);
            JwtPayload payload;
            try
            {
                payload = JwtPayload.Base64UrlDeserialize(tokenParts[1]);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "JwtPayload error", "payload", tokenParts[1], tokenString), ex);
            }


            return new JwtSecurityToken(header, payload, tokenParts[0], tokenParts[1], tokenParts[2]);
        }
        public override string WriteToken(SecurityToken token)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            JwtSecurityToken jwt = token as JwtSecurityToken;
            if (jwt == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "", GetType(), typeof(JwtSecurityToken), token.GetType()));
            }

            string signature = string.Empty;
            string signingInput = string.Concat(jwt.EncodedHeader, ".", jwt.EncodedPayload);

            if (jwt.SigningCredentials != null)
            {
                signature = signingInput.AESEncrypt(JwtAuthCoreBearerOptions.Serect);
            }

            return string.Concat(signingInput, ".", signature).AESEncrypt(JwtAuthCoreBearerOptions.Serect);
        }


        public override ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            if (string.IsNullOrWhiteSpace(securityToken))
            {
                throw new ArgumentNullException("securityToken");
            }

            if (validationParameters == null)
            {
                throw new ArgumentNullException("validationParameters");
            }


            JwtSecurityToken jwt = this.ValidateSignature(securityToken, validationParameters);

            //验证过期时间,发布人,发布时间....
            if (jwt.Payload.TryGetValue("exp", out var value))
            {
                var time = long.Parse(value.ToString()).ToDateTime();
                if (time < DateTime.Now)
                    throw new SecurityTokenSignatureKeyNotFoundException(string.Format(CultureInfo.InvariantCulture, "token 过期", jwt.ToString()));
            }

            object issuer;
            jwt.Payload.TryGetValue("iss", out issuer);

            ClaimsIdentity identity = this.CreateClaimsIdentity(jwt, issuer.ToString(), validationParameters);


            validatedToken = jwt;
            return new JwtClaimsPrincipal(identity);
        }
        /// <summary>
        /// 验证加密签名
        /// </summary>
        /// <param name="token"></param>
        /// <param name="validationParameters"></param>
        /// <returns></returns>
        protected override JwtSecurityToken ValidateSignature(string token, TokenValidationParameters validationParameters)
        {
            JwtSecurityToken jwt = ReadToken(token) as JwtSecurityToken;
            var sigton = string.Concat(jwt.Header.Base64UrlEncode(), ".", jwt.Payload.Base64UrlEncode()).AESEncrypt(JwtAuthCoreBearerOptions.Serect);
            if (sigton != jwt.RawSignature)
            {

                throw new SecurityTokenSignatureKeyNotFoundException(string.Format(CultureInfo.InvariantCulture, "validate sugbature ", jwt.ToString()));
            }
            return jwt;
        }
        protected override ClaimsIdentity CreateClaimsIdentity(JwtSecurityToken jwt, string issuer, TokenValidationParameters validationParameters)
        {
            if (jwt == null)
            {
                throw new ArgumentNullException("jwt");
            }
            var userName = string.Empty;
            if (jwt.Payload.Claims.Any(t => t.Type.ToLower() == "id"))
            {
                userName = jwt.Payload.Claims.First(t => t.Type.ToLower() == "id").Value;
            }
            ClaimsIdentity identity = new JwtClaimsIdentity(jwt.Claims, "Bearer").Authenticated(userName, true);
            return identity;
        }
    }
}
