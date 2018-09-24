using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigBeer.Framework.Mvc.Authentication.Jwt;
using BigBeer.Framework.Service.Framework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BigBeer.Core.Extensions;

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
