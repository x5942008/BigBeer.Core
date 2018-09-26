using BigBeer.Framework.Mvc.Authentication.Jwt.Authencation;
using BigBeer.Core.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBeer.Framework.Mvc.Authentication.Jwt.Authenzation
{
    public class JwtAuthenzationMiddleware
    {
        public RequestDelegate Next { get; set; }
        public JwtAuthenzationOptions serverOptions { get; set; }
        public JwtAuthenzationMiddleware(RequestDelegate next, JwtAuthenzationOptions options)
        {
            Next = next;
            serverOptions = options;
        }
        public Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            if (context.Request.Method.ToLower() != "post")
            {
                var result = serverOptions.ResultFormat((false, "不允许的方法请 post 提交", 401, null));
                return context.Response.WriteAsync(result.ToJson());
            }
            //验证帐号
            var verifyResult = serverOptions.OnVerifyOwner(context);

            var message = string.Empty;
            if (!verifyResult.Successed)
            {
                var result = serverOptions.ResultFormat((false, verifyResult.Message, 401, null));
                return context.Response.WriteAsync(result.ToJson());
            }
            var jwttoken = new JwtToken()
            {
                Header = new JwtHeader(),
                Payload = new JwtPayload(verifyResult.Cliams),
                Secret = serverOptions.Secret
            };
            jwttoken.AddExp(DateTime.Now.AddMinutes(serverOptions.ExpireTimeSpan.TotalMinutes))
               .AddIss("@buday.gold.cusumer.auth")
               .AddIat(DateTime.Now);
            var singonture = string.Concat(jwttoken.Header.Base64UrlEncode(), ".", jwttoken.Payload.Base64UrlEncode());
            //签名
            jwttoken.Signature = serverOptions.OnSignature(singonture, serverOptions.Secret);
            if (jwttoken == null || jwttoken.Signature.IsNullOrEmpty())
            {
                message = "签名错误";
                if (context.Items.TryGetValue("error", out var value))
                {
                    message = value.ToString();
                }
                var result = serverOptions.ResultFormat((false, message, 402, null));
                return context.Response.WriteAsync(result.ToJson());
            }


            //生成客户票据
            var authTicket = serverOptions.OnCreateTicket(jwttoken);
            if (authTicket == null || authTicket.token.IsNullOrEmpty())
            {
                message = "token生成失败";
                if (context.Items.TryGetValue("error", out var value))
                {
                    message = value.ToString();
                }
                var result = serverOptions.ResultFormat((false, message, 403, null));
                return context.Response.WriteAsync(result.ToJson());
            }

            //提交给客户端
            var ticket = serverOptions.ResultFormat((true, message, 200, authTicket));
            return context.Response.WriteAsync(ticket.ToJson());
        }
    }
}
