global using Smartbell.App.Contracts;
global using Smartbell.App.Services;
global using Smartbell.Shared.Entities;
global using Smartbell.Shared.Dtos;
global using Smartbell.App.Models;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using AutoMapper;
global using Smartbell.App.Data;

namespace Smartbell.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var _connectionString = builder.Configuration.GetConnectionString("DefaultSqlServerConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_connectionString));

            //// Identity configuration
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            //{}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            // Identity configuration
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient("smrtbell", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://smrtbell.tellimart.com/api/");
                //httpClient.BaseAddress = new Uri("https://localhost:7203/api/");
            });

			builder.Services.AddScoped<IConfigService, ConfigService>();
			builder.Services.AddScoped<IRingtoneService, RingtoneService>();
            builder.Services.AddScoped<IActivityService, ActivityService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}