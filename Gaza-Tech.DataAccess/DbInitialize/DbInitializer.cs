using Gaza_Tech.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.DataAccess.DbInitialize
{
	public class DbInitializer : IDbInitializer
	{
		private readonly ILogger<DbInitializer> _logger;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly AppDbContext _context;

		private const string AdminRole = "Admin";
		private const string EditorRole = "Editor";
		private const string CustomerRole = "Customer";
		public DbInitializer(
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager,
			ILogger<DbInitializer> logger,
			AppDbContext context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_logger = logger;
			_context = context;
		}

		public async Task InitializeAsync()
		{
			#region Update Database (Migrations)
			try
			{
				if (_context.Database.GetPendingMigrations().Count() > 0)
					await _context.Database.MigrateAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An Error Occurred During Appling The Migration");
			}
			#endregion
			#region Roles And User
			if (!await _roleManager.RoleExistsAsync(AdminRole))
			{
				await _roleManager.CreateAsync(new IdentityRole(AdminRole));
				await _roleManager.CreateAsync(new IdentityRole(EditorRole));
				await _roleManager.CreateAsync(new IdentityRole(CustomerRole));

				var newAdmin = new ApplicationUser
				{
					UserName = "Admin",
					Email = "Admin@gmail.com",
					FullName = "Administrator",
					PhoneNumber = "01013893864",
					Address = @"Salah Salem, Beni-Suif, Beni-Suif",
					City = "Beni-Suif",
				};

				await _userManager.CreateAsync(newAdmin, "941970@Ah");

				var administrator = await _context.ApplicationUsers.FirstOrDefaultAsync(a => a.FullName == newAdmin.FullName);

				 await _userManager.AddToRoleAsync(administrator, AdminRole);
			}
			#endregion
		}
	}
}
