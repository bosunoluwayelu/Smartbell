global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Smartbell.Shared.Contracts;
global using Smartbell.Shared.Entities;
global using Smartbell.Api.Data;
global using Smartbell.Api.Contracts;
global using System.ComponentModel.DataAnnotations;
global using Smartbell.Shared.Dtos;
global using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Smartbell.Api.Repositories;
using Microsoft.Extensions.FileProviders;

namespace Smartbell.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var _connectionString = builder.Configuration.GetConnectionString("DefaultSqlServerConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_connectionString));

            // Identity configuration
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {

            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<IConfigRepository, ConfigRepository>();
            builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
            builder.Services.AddScoped<IRingtoneRepository, RingtoneRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Resources")), RequestPath = "/Resources"
            });

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}