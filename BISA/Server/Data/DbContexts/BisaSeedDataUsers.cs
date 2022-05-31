using BISA.Shared.Entities;

namespace BISA.Server.Data.DbContexts
{
    public static class BisaSeedDataUsers
    {
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasData(
                    new UserEntity { Id = 1, Firstname = "Jeffrey", Lastname = "Anderson", Username = "jaff", Email = "jaff@gmail.com", UserId = "b74ddd14-6340-4840-95c2-db12554843e6" },
                    new UserEntity { Id = 2, Firstname = "Mikael", Lastname = "Kinder", Username = "micke", Email = "micke@gmail.com", UserId = "b74ddd14-6340-4840-95c2-db12554843e7" },
                    new UserEntity { Id = 3, Firstname = "Kalle", Lastname = "Ljungberg", Username = "junne", Email = "junne@gmail.com", UserId = "b74ddd14-6340-4840-95c2-db12554843e8" },
                    new UserEntity { Id = 4, Firstname = "Ralf", Lastname = "Gyllenhammarströmfors", Username = "admin", Email = "admin@gmail.com", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" },
                    new UserEntity { Id = 5, Firstname = "Alex", Lastname = "Urtti", Username = "alexurtti", Email = "alex@hotmail.com", UserId = "5a389f27-9fc1-4505-b779-ccb3020af009" },
                    new UserEntity { Id = 6, Firstname = "Boris", Lastname = "Kogen", Username = "bosse", Email = "bosse@hotmail.com", UserId = "c11686e5-daaa-4d0d-91de-cdcd6c618bbc" }
                );
        }
    }
}
