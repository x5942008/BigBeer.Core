using BigBeer.Framework.Mvc.Authentication.Jwt.Authencation;
using BigBeer.Framework.Mvc.Authentication.Jwt.Authenzation;
using BigBeer.Core.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BigBeer.Framework.Mvc.Authentication.Jwt
{
    public static class AuthenticationExtensions
    {
        /// <summary>
        /// 注册授权服务(颁发token)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="authServerOptions"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJwtAuthenzation(this IApplicationBuilder @this, JwtAuthenzationOptions authServerOptions)
        {
            return @this.Map(new PathString(authServerOptions.TokenEndpointPath), (builder) =>
            {
                builder.UseMiddleware<JwtAuthenzationMiddleware>(authServerOptions);
            });
        }
        /// <summary>
        /// 添加验证jwt服务
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwtBearerAuthencation(this AuthenticationBuilder builder)
           => builder.AddJwtBearerAuthencation(JwtBearerDefaults.AuthenticationScheme, _ => { });
        /// <summary>
        /// 添加验证jwt服务
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwtBearerAuthencation(this AuthenticationBuilder builder, Action<JwtAuthencationOptions> configureOptions)
            => builder.AddJwtBearerAuthencation(JwtBearerDefaults.AuthenticationScheme, configureOptions);
        /// <summary>
        /// 添加验证jwt服务
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="authenticationScheme"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwtBearerAuthencation(this AuthenticationBuilder builder, string authenticationScheme, Action<JwtAuthencationOptions> configureOptions)
            => builder.AddJwtBearerAuthencation(authenticationScheme, displayName: null, configureOptions: configureOptions);
        /// <summary>
        /// 添加验证jwt服务
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="authenticationScheme"></param>
        /// <param name="displayName"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwtBearerAuthencation(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<JwtAuthencationOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<JwtAuthencationOptions>, JwtBearerPostConfigureOptions>());
            return builder.AddScheme<JwtAuthencationOptions,JwtAuthencationHandler>(authenticationScheme, displayName, configureOptions);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="this"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static JwtToken Add(this JwtToken @this, string key, object value)
        {
            if (@this.Payload.ContainsKey(key)) return @this;
            @this.Payload.Add(key, value);
            return @this;
        }
        /// <summary>
        /// 颁发人
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static JwtToken AddIss(this JwtToken @this, object value)
        {
            if (@this.Payload.ContainsKey("iss")) return @this;
            @this.Payload.Add("iss", value);
            return @this;
        }
        /// <summary>
        /// 颁发时间
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static JwtToken AddIat(this JwtToken @this, DateTime value)
        {
            if (@this.Payload.ContainsKey("iat")) return @this;
            @this.Payload.Add("iat", value.ToJavaimeStamp());
            return @this;
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static JwtToken AddExp(this JwtToken @this, DateTime value)
        {
            if (@this.Payload.ContainsKey("exp")) return @this;
            @this.Payload.Add("exp", value.ToJavaimeStamp());
            return @this;
        }
        /// <summary>
        /// 接收方
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static JwtToken AddAud(this JwtToken @this, object value)
        {
            if (@this.Payload.ContainsKey("aud")) return @this;
            @this.Payload.Add("aud", value);
            return @this;
        }
        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static JwtToken AddType(this JwtToken @this, string value)
        {
            if (@this.Payload.ContainsKey("type")) return @this;
            @this.Payload.Add("type", value);
            return @this;
        }
        /// <summary>
        /// 设置错误
        /// </summary>
        /// <param name="this"></param>
        public static void SetError(this HttpContext @this, string message)
        {
            @this.Items["error"] = message;
        }
        /// <summary>
        /// 获取客户端传递过来的数据
        /// json 格式
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Dictionary<string, string> Forms(this HttpContext @this)
        {
            var formsString = new System.IO.StreamReader(@this.Request.Body).ReadToEnd();
            if (formsString.StartsWith("{"))
                return formsString.ToObject<Dictionary<string, string>>();
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var p in formsString.Split('&'))
            {
                var pl = p.Split(new char[] { '=' });
                if (!pl.Any()) continue;
                if (pl.Count() < 2) continue;
                result.Add(pl[0], pl[1]);
            }
            return result;
        }
    }
}
