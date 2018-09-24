using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using BigBeer.Framework.Mvc.Authentication.Jwt;
using BigBeer.Core.Extensions;
using BigBeer.Framework.Mvc.Authentication.Jwt.Authenzation;
using System.Collections.Generic;
using System;
using BigBeer.Framework.Mvc.ImageService;

namespace BigBeer.Framework.Mvc.AuthCore.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //添加认证服务
            services.AddAuthentication((options) => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearerAuthencation((o) => {
                o.Events = new JwtBearerEvents()
                {
                    //认证失败的回调
                    OnChallenge = c => {
                        c.Response.StatusCode = 200;
                        c.Response.ContentType = "application/json";
                        c.HandleResponse();
                        return c.Response.WriteAsync(new
                        {
                            c = 401,
                            m = "权限验证失败",
                            s = false
                        }.ToJson());
                    }
                };
                o.Serect = "bigbeer.copyright.cn".EncodeBase64();
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //登陆授权
            app.UseJwtAuthenzation(new JwtAuthenzationOptions
            {
                //加密字符串
                Secret = "bigbeer.copyright.cn".EncodeBase64(),
                //获取token的路径
                TokenEndpointPath = "/auth",
                //这里验证用户名密码
                //登录程序
                OnVerifyOwner = (context) => {

                    var forms = context.Forms();
                    if (!forms.ContainsKey("username") || !forms.ContainsKey("password"))
                    {
                        return VerifyOwnerResult.Faild("请输入账户名/密码");
                    }
                    var user = new
                    {
                        username = forms["username"],
                        password = forms["password"],
                        encodepassword = forms["password"].EncryptPassword()
                    };
                    if (user.username.Length < 6 || user.password.Length < 6)
                    {
                        return VerifyOwnerResult.Faild("帐户名/密码最少6位");
                    }
                    //获取数据库对象
                    //var db = context.RequestServices.GetService<CusumerDb>();
                    //查询数据库 ...
                    return VerifyOwnerResult.Sucess(new Dictionary<string, string>() {
                        { "id",Guid.NewGuid().ToString()},
                        { "name","大熊"},
                        { "nick","BigBeer"},
                        { "head","img" }
                        //...其他想存储的数据,这个数据只能作为用户标识,在token未过期时一直不变
                    });
                }

            });

            //权限验证
            app.UseAuthentication();

            app.UseImageService(new ImageServiceOptions() { });
            app.Use(async (context, next) =>
            {
                // Use this if there are multiple authentication schemes
                // var user = await context.Authentication.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

                var user = context.User; // We can do this because of there's only a single authentication scheme
                if (user?.Identity?.IsAuthenticated ?? false)
                {

                }
                await next();
            });
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
