using BISA.Server.Entities;
using BISA.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BISA.Server.Data.DbContexts
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedUsers(builder);
            this.SeedRoles(builder);
            this.SeedUserRoles(builder);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            ApplicationUser admin = new()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                LockoutEnabled = false,
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "admin");

            ApplicationUser jaff = new()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e6",
                UserName = "Jaff",
                NormalizedUserName = "JAFF",
                Email = "jaff@gmail.com",
                NormalizedEmail = "JAFF@GMAIL.COM",
                LockoutEnabled = false,
            };
            jaff.PasswordHash = passwordHasher.HashPassword(jaff, "jaff");

            ApplicationUser micke = new()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e7",
                UserName = "Micke",
                NormalizedUserName = "MICKE",
                Email = "micke@gmail.com",
                NormalizedEmail = "MICKE@GMAIL.COM",
                LockoutEnabled = false,
            };
            micke.PasswordHash = passwordHasher.HashPassword(micke, "micke");

            ApplicationUser junne = new()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e8",
                UserName = "junne",
                NormalizedUserName = "JUNNE",
                Email = "junne@gmail.com",
                NormalizedEmail = "JUNNE@GMAIL.COM",
                LockoutEnabled = false,
            };
            junne.PasswordHash = passwordHasher.HashPassword(junne, "junne");


            builder.Entity<ApplicationUser>().HasData(admin, jaff, micke, junne);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "AdminId", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = "StaffId", Name = "Staff", ConcurrencyStamp = "2", NormalizedName = "STAFF" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "AdminId", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new IdentityUserRole<string>() { RoleId = "StaffId", UserId = "b74ddd14-6340-4840-95c2-db12554843e6" },
                new IdentityUserRole<string>() { RoleId = "AdminId", UserId = "b74ddd14-6340-4840-95c2-db12554843e8" }
                );
        }
    }
}
