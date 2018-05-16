using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.ToDo.Data;
using AspNetCore.ToDo.Models;
using AspNetCore.ToDo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.ToDo {
    //可以添加中间件，它们用于处理和调整传入的请求、提供静态内容和错误页面，也可以向依赖注入容器中添加自己的服务
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseSqlite (Configuration.GetConnectionString ("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole> ()
                .AddEntityFrameworkStores<ApplicationDbContext> ()
                .AddDefaultTokenProviders ();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender> ();

            // services.AddSingleton<ITodoItemService, FakeTodoItemService> ();
            services.AddScoped<ITodoItemService, TodoItemService> ();

            services.AddAuthentication ().AddFacebook (Options => {
                Options.AppId = Configuration["Facebook:AppId"];
                Options.AppSecret = Configuration["Facebook:AppSecret"];
            });

            services.AddMvc ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
                app.UseDatabaseErrorPage ();

                //make sure there`s a test admin account
                EnsureRolesAsync (roleManager).Wait (); //为什么实例方法中可以调用静态方法
                EnsureTestAdminAsync (userManager).Wait ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
            }

            app.UseStaticFiles ();

            app.UseAuthentication ();

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private static async Task EnsureRolesAsync (RoleManager<IdentityRole> roleManager) {
            var alreadyExists = await roleManager.RoleExistsAsync (Constants.AdministratorRole);
            if (alreadyExists) return;
            await roleManager.CreateAsync (new IdentityRole (Constants.AdministratorRole));
        }

        private static async Task EnsureTestAdminAsync (UserManager<ApplicationUser> userManager) {
            var testAdmin = await userManager.Users.Where (x => x.UserName == "admin@todo.local").SingleOrDefaultAsync ();

            if (testAdmin != null) return;

            testAdmin = new ApplicationUser { UserName = "admin", Email = "2811918767@qq.com" };
            await userManager.CreateAsync (testAdmin, "NotSecure123!");
            await userManager.AddToRoleAsync (testAdmin, Constants.AdministratorRole);
        }

    }

    public static class Constants {
        public const string AdministratorRole = "Administrator";
    }
}