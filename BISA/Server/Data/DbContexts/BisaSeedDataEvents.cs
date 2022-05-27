using BISA.Shared.Entities;

namespace BISA.Server.Data.DbContexts
{
    public static class BisaSeedDataEvents
    {
        public static void SeedEvents(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventTypeEntity>()
                .HasData(
                    new EventTypeEntity
                    {
                        Id = 1,
                        Capacity = 500,
                        Description = "Barnaktivitet",
                        Image = "högläsning.img",
                        Type = "Högläsning för barn"
                    },
                    new EventTypeEntity
                    {
                        Id = 2,
                        Capacity = 300,
                        Description = "Musikevenemang",
                        Image = "musik.img",
                        Type = "Konsert"
                    }
                );


            modelBuilder.Entity<EventEntity>()
                .HasData(
                    new EventEntity
                    {
                        Id = 1,
                        Date = DateTime.Now,
                        Location = "Malmö",
                        Organizer = "Kalle-Staffan",
                        Subject = "Djur",
                        EventTypeId = 1,
                        Description = "Kalle-Staffan läser från sin bok 'Dom vilda djuren'."
                    },
                    new EventEntity
                    {
                        Id = 2,
                        Date = DateTime.Now,
                        Location = "Tranås",
                        Organizer = "Fora",
                        Subject = "Yttrandefrihet",
                        EventTypeId = 2,
                        Description = "Fora besöker vårt bibliotek för att diskutera yttrandefrihet."
                    }
                );
        }
    }
}
