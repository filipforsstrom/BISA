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
                    },
                    new EventTypeEntity
                    {
                        Id = 3,
                        Capacity = 500,
                        Description = "Insamlimg",
                        Image = "insamling.img",
                        Type = "Välgörenhet"
                    }
                );


            modelBuilder.Entity<EventEntity>()
                .HasData(
                    new EventEntity
                    {
                        Id = 1,
                        Date = new DateTime(2022, 08, 10),
                        Location = "Malmö",
                        Organizer = "Läsklubben",
                        Subject = "Exotiska djur och växter",
                        EventTypeId = 1,
                        Description = "Staffan läser från sin bok 'Den vilda naturen'."
                    },
                    new EventEntity
                    {
                        Id = 2,
                        Date = new DateTime(2022, 07, 14),
                        Location = "Tranås",
                        Organizer = "Fora",
                        Subject = "Klassisk musik",
                        EventTypeId = 2,
                        Description = "Fora besöker vårt bibliotek för en Österrikisk musikresa bakåt i tiden"
                    },
                    new EventEntity
                    {
                        Id = 3,
                        Date = new DateTime(2022, 10, 22),
                        Location = "Folkets Park Malmö",
                        Organizer = "Röda Korset",
                        Subject = "Kriget i Yemen",
                        EventTypeId = 3,
                        Description = "Malmös musik community samlar in pengar till förmån för utsatta människor i Yemen"
                    }
                );
        }
    }
}
