using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //这一行告知 ASP.NET Core，在任何时候，只要 ITodoItemService 被一个构造函数（或其它什么地方）被请求，就用这个 FakeTodoItemService的实现。AddSingleton 把你的服务作为 singleton 添加进服务容器。这意味着，只有一个FakeTodoItemService的实例被创建，并在每次被请求的时候都被复用。在后面，当你写另一个服务去跟数据库交互时，你会采用一个不同的方式（叫做 scoped）。我会在 运用数据库 一章里说明原因
            //services.AddSingleton<ITodoItemService, FakeTodoItemService>();//注册为Singleton
            services.AddScoped<ITodoItemService, TodoItemService>();//注册为Scoped

            //指定连接字符串
            services.AddDbContext<ApplicationDbContext>(options=> {
                options.UseSqlServer(Configuration.GetConnectionString("MSSQLConnectionStrings"));
            });
            //验证相关--确保是已经登录了的用户
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
         options =>
         {
             options.LoginPath = new PathString("/Home/Index");
             //options.AccessDeniedPath = new PathString("/Todo/Index");
         });
            //身份，不同身份具有不同权限
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
