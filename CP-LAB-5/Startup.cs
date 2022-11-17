using CP_LAB_5.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP_LAB_5
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
            services.AddControllersWithViews();
            services.AddDbContext<UserContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("UsersDataBase")));

            services.AddAuthentication(o => 
            {
                o.DefaultScheme = "UserCookieAuth";
                o.DefaultSignInScheme = "External";
                
            }).AddCookie("UserCookieAuth", opt =>
            {
                opt.Cookie.Name = "UserCookieAuth";
                opt.LoginPath = "/Account/Login";
                opt.ExpireTimeSpan = TimeSpan.FromDays(30);
                opt.SessionStore = services.BuildServiceProvider().GetService<ITicketStore>();
            }).AddCookie("External")
            .AddGoogle(options =>
            {
                options.ClientId =
                    Configuration.GetSection("Authentication:Google:ClientId").Value;
                options.ClientSecret =
                    Configuration.GetSection("Authentication:Google:ClientSecret").Value;
            }); 

            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Main}/{id?}");
            });
        }
    }
}
