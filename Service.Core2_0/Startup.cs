using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BigBeerMiddelwareSample;
using BigBeerServiceSample;

namespace Service.Core2_0
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
            services.AddMvc();
            services.AddBigBeerService((option) =>
            {
                //option.Use("A", typeof(BigBeerAService));
                //option.Use("B", typeof(BigBeerBService));
                //option.Use("C", typeof(BigBeerCService));
                option.Use<BigBeerAService>("A");
                option.Use<BigBeerBService>("B");
                option.Use<BigBeerCService>("C");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseSession();
            app.UseMiddleware<BigBeerMiddlware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=X}/{id?}");
            });
        }
    }
}
