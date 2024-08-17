using BLL.Classes;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            //applicationDbContext.Database.Migrate();
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddDbContext<ApplicationDbContext>(
                option => option.UseSqlServer(builder.Configuration.GetConnectionString("DEV")
                ));
            RepositoryDependancyInjection.AddRepositories(builder.Services);

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

            app.UseAuthorization();

            app.MapAreaControllerRoute(
                name: "MyAreaEvents",
                areaName: "Events",
                pattern: "Events/{controller=Event}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
            name: "MyAreaServices",
            areaName: "Services",
            pattern: "Services/{controller=Role}/{action=Create}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
