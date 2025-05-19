using Gaza_Tech.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gaza_Tech.DataAccess
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserShoppingCart> UserShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<ApplicationUser>().ToTable("Users");

			builder.Entity<IdentityRole>().ToTable("Roles");

			builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles").HasKey(r => new { r.UserId, r.RoleId });

			builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");

			builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins").HasKey(l => new { l.LoginProvider, l.ProviderKey });

			builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

			builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens").HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
		}
	}
}
