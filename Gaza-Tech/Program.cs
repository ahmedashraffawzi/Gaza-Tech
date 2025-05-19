using Gaza_Tech.BLL.Interfaces;
using Gaza_Tech.BLL.Repositories;
using Gaza_Tech.DataAccess;
using Microsoft.EntityFrameworkCore;
using Gaza_Tech.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using FileService = Gaza_Tech.BLL.Services.Implementation.FileService;
using Gaza_Tech.Utilities;
using Stripe;
using Gaza_Tech.Options;
using Gaza_Tech.DataAccess.Models;
using Gaza_Tech.DataAccess.DbInitialize;

namespace Gaza_Tech
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Create Host
            var builder = WebApplication.CreateBuilder(args);
			#endregion
			#region DI Container
			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
			builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
				builder.Configuration.GetConnectionString("DefaultConnection")
				));
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(4))
			.AddDefaultTokenProviders()
			.AddDefaultUI()
			.AddEntityFrameworkStores<AppDbContext>();
			builder.Services.AddSingleton<IEmailSender, EmailSender>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IFileService, FileService>();
			// Database Initializer
			builder.Services.AddScoped<IDbInitializer, DbInitializer>();
			//Configuring Session Services in ASP.NET Core
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession();
			builder.Services.Configure<StripeOptions>(builder.Configuration.GetSection("Stripe"));

			#endregion
			#region Build Project
			var app = builder.Build();
			#endregion
			#region Database Initializer
			
			using var scope = app.Services.CreateScope();

			var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

			await dbInitializer.InitializeAsync();
			
			#endregion
			#region Middlewares 
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
            app.UseSession();
            StripeConfiguration.ApiKey = builder.Configuration.GetSection("stripe:Secretkey").Get<string>();
            app.UseAuthorization();
            app.MapRazorPages();
			#endregion
			#region Routeing
			app.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                    name: "Admin",
                    pattern: "{area=Admin}/{controller=Dashboard}/{action=Index}/{id?}");
            #endregion
            app.Run();
        }
    }
}