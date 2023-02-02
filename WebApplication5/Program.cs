using Discord;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication5
{
    public class Program
    {
        private static readonly IConfiguration configuration;

        public static IConfiguration GetConfiguration()
        {
            return configuration;
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("WebApplication5ContextConnection") 
                ?? throw new InvalidOperationException("Connection string 'WebApplication5ContextConnection' not found.");

            builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer("Data Source=SQL5097.site4now.net,1433;Initial Catalog=db_a930a1_p01nt;User Id=db_a930a1_p01nt_admin;Password=ZWpdiN6fLB7Es@m;"));

            // установка конфигурации подключения
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = "/Account/Login";
                });
            builder.Services.AddControllersWithViews();
            // Add services to the container.
            builder.Services.AddAuthenticationCore();
			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Logs}/{id?}");
			GodBot.Action.Start();
			app.Run();
        }
    }
}