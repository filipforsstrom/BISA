using BISA.Shared.Entities;

namespace BISA.Server.Data.DbContexts
{
    public static class BisaSeedDataUsers
    {
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasData(
                    new UserEntity { Id = 1, Email = "jaf@newton.co.uk", Warnings = 3, UserId = "b74ddd14-6340-4840-95c2-db12554843e6" },
                    new UserEntity { Id = 2, Email = "peter@newton.co.uk", Warnings = 0 },
                    new UserEntity { Id = 3, Email = "elsie@newton.co.uk", Warnings = 0 },
                    new UserEntity { Id = 4, Email = "frank@newton.co.uk", Warnings = 0 },
                    new UserEntity { Id = 5, Email = "lina@newton.co.uk", Warnings = 0 }
                );
        }
    }
}
