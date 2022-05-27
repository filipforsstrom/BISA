using BISA.Shared.Entities;

namespace BISA.Server.Data.DbContexts
{
    public static class BisaSeedDataItems
    {
        public static void SeedItems(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>()
                .HasData(
                    new BookEntity { Id = 1, Title = "Hej", ISBN = "523577987", Creator = "Hej Hejsson" },
                    new BookEntity { Id = 3, Title = "Dune", ISBN = "523577988", Creator = "Stephen King" },
                    new BookEntity { Id = 4, Title = "Lord of the Rings", ISBN = "523577989", Creator = "Tolkien" }
                );

            modelBuilder.Entity<MovieEntity>()
                .HasData(
                    new MovieEntity { Id = 2, Title = "Hej the movie", RuntimeInMinutes = 120, Creator = "Tolkien", Language = "English" });

            modelBuilder.Entity<EbookEntity>()
                .HasData(
                    new EbookEntity
                    {
                        Id = 5,
                        Date = "2022",
                        Creator = "A. Person",
                        Language = "eng",
                        Publisher = "S. Omeone",
                        Title = "Hej For Dummies",
                        Url = "https://www.google.com/"
                    });
        }
        public static void SeedTags(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagEntity>()
                .HasData(
                    new TagEntity { Id = 1, Tag = "Action" },
                    new TagEntity { Id = 2, Tag = "Horror" },
                    new TagEntity { Id = 3, Tag = "Comedy" },
                    new TagEntity { Id = 4, Tag = "Romance" },
                    new TagEntity { Id = 5, Tag = "Twilight" }
                );

            modelBuilder.Entity<ItemTagEntity>()
                .HasData(
                    new ItemTagEntity { ItemId = 1, TagId = 1 },
                    new ItemTagEntity { ItemId = 1, TagId = 2 },
                    new ItemTagEntity { ItemId = 2, TagId = 3 },
                    new ItemTagEntity { ItemId = 2, TagId = 4 },
                    new ItemTagEntity { ItemId = 5, TagId = 1 }
                );
        }

        public static void SeedItemInventory(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemInventoryEntity>()
                .HasData(
                    new ItemInventoryEntity { Id = 1, ItemId = 1, Available = false },
                    new ItemInventoryEntity { Id = 2, ItemId = 1, Available = false },
                    new ItemInventoryEntity { Id = 3, ItemId = 2, Available = true },
                    new ItemInventoryEntity { Id = 4, ItemId = 2, Available = false },
                    new ItemInventoryEntity { Id = 5, ItemId = 3, Available = true },
                    new ItemInventoryEntity { Id = 6, ItemId = 3, Available = false },
                    new ItemInventoryEntity { Id = 7, ItemId = 5, Available = true }
                );
        }
        public static void SeedLoans(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanEntity>()
                .HasData(
                    new LoanEntity { Id = 1, ItemInventoryId = 1, UserId = 1 },
                    new LoanEntity { Id = 2, ItemInventoryId = 2, UserId = 1 },
                    new LoanEntity { Id = 3, ItemInventoryId = 4, UserId = 2 },
                    new LoanEntity { Id = 4, ItemInventoryId = 6, UserId = 3 }
                );
            modelBuilder.Entity<LoanHistoryEntity>()
                .HasData(
                    new LoanHistoryEntity { Id = 5, ItemInventoryId = 1, UserId = 1 },
                    new LoanHistoryEntity { Id = 6, ItemInventoryId = 6, UserId = 1 },
                    new LoanHistoryEntity { Id = 7, ItemInventoryId = 3, UserId = 2 },
                    new LoanHistoryEntity { Id = 8, ItemInventoryId = 6, UserId = 3 },
                    new LoanHistoryEntity { Id = 9, ItemInventoryId = 6, UserId = 1 },
                    new LoanHistoryEntity { Id = 10, ItemInventoryId = 1, UserId = 4 },
                    new LoanHistoryEntity { Id = 11, ItemInventoryId = 6, UserId = 2 },
                    new LoanHistoryEntity { Id = 12, ItemInventoryId = 3, UserId = 1 }
                );
        }
        public static void SeedReservations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanReservationEntity>()
                .HasData(
                    new LoanReservationEntity { Id = 1, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(2), ItemId = 1, UserId = 3 },
                    new LoanReservationEntity { Id = 2, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(1), ItemId = 1, UserId = 4 },
                    new LoanReservationEntity { Id = 3, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(2), ItemId = 2, UserId = 1 }
                );
        }
    }
}
