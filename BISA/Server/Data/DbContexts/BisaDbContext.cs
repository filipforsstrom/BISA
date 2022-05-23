using BISA.Shared.Entities;

namespace BISA.Server.Data.DbContexts
{
    public class BisaDbContext : DbContext
    {
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<EbookEntity> Ebooks { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<LoanEntity> LoansActive { get; set; }
        public DbSet<LoanHistoryEntity> LoansHistory { get; set; }
        public DbSet<LoanReservationEntity> LoansReservation { get; set; }
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<EventTypeEntity> EventType { get; set; }
        public DbSet<ItemInventoryEntity> ItemInventory { get; set; }
        public DbSet<TagEntity> Tags { get; set; }



        public BisaDbContext(DbContextOptions<BisaDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemEntity>()
            .HasDiscriminator<string>("Type")
            .HasValue<BookEntity>("Book")
            .HasValue<MovieEntity>("Movie")
            .HasValue<EbookEntity>("Ebook");

            modelBuilder.Entity<ItemEntity>()
            .Property(i => i.Type)
            .HasColumnName("Type");

            modelBuilder.Entity<BookEntity>()
                .HasData(
                new BookEntity { Id = 1, Title = "Hej", ISBN = "523577987" },
                new BookEntity { Id = 3, Title = "Dune", ISBN = "523577988" },
                new BookEntity { Id = 4, Title = "Lord of the Rings", ISBN = "523577989" }
                );

            modelBuilder.Entity<MovieEntity>()
                .HasData(
                new MovieEntity { Id = 2, Title = "Hej the movie", RuntimeInMinutes = 120, Creator = "Tolkien", Language = "English" });

            modelBuilder.Entity<UserEntity>()
                .HasData(
                new UserEntity { Id = 1, Email = "jaf@newton.co.uk", Warnings = 3, UserId = "b74ddd14-6340-4840-95c2-db12554843e6" },
                new UserEntity { Id = 2, Email = "peter@newton.co.uk", Warnings = 0 },
                new UserEntity { Id = 3, Email = "elsie@newton.co.uk", Warnings = 0 },
                new UserEntity { Id = 4, Email = "frank@newton.co.uk", Warnings = 0 },
                new UserEntity { Id = 5, Email = "lina@newton.co.uk", Warnings = 0 }
                );

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
                    new LoanHistoryEntity { Id = 7, ItemInventoryId = 4, UserId = 2 },
                    new LoanHistoryEntity { Id = 8, ItemInventoryId = 6, UserId = 3 },
                    new LoanHistoryEntity { Id = 9, ItemInventoryId = 6, UserId = 1 },
                    new LoanHistoryEntity { Id = 10, ItemInventoryId = 1, UserId = 4 },
                    new LoanHistoryEntity { Id = 11, ItemInventoryId = 6, UserId = 2 }
                );

            modelBuilder.Entity<TagEntity>()
                .HasData(
                new TagEntity { Id = 1, Tag = "Action" },
                new TagEntity { Id = 2, Tag = "Horror" },
                new TagEntity { Id = 3, Tag = "Comedy" },
                new TagEntity { Id = 4, Tag = "Romance" },
                new TagEntity { Id = 5, Tag = "Twilight" }
                );


            modelBuilder.Entity<EventTypeEntity>()
                .HasData(
                new EventTypeEntity { Id = 1, Capacity = 500, Description = "Barnaktivitet", Image = "högläsning.img", Type = "Högläsning för barn" },
                new EventTypeEntity { Id = 2, Capacity = 300, Description = "Musikevenemang", Image = "musik.img", Type = "Konsert" }
                );


            modelBuilder.Entity<EventEntity>()
                .HasData(
                new EventEntity { Id = 1, Date = DateTime.Now, Location = "Malmö", Organizer = "Kalle-Staffan", Subject = "Djur", EventTypeId = 1, Description = "Kalle-Staffan läser från sin bok 'Dom vilda djuren'." },
                new EventEntity { Id = 2, Date = DateTime.Now, Location = "Tranås", Organizer = "Fora", Subject = "Yttrandefrihet", EventTypeId = 2, Description = "Fora besöker vårt bibliotek för att diskutera yttrandefrihet." }
                );

            modelBuilder.Entity<ItemInventoryEntity>()
                .HasData(
                new ItemInventoryEntity { Id = 1, ItemId = 1, Available = false },
                new ItemInventoryEntity { Id = 2, ItemId = 1, Available = false },
                new ItemInventoryEntity { Id = 3, ItemId = 2, Available = true },
                new ItemInventoryEntity { Id = 4, ItemId = 2, Available = false },
                new ItemInventoryEntity { Id = 5, ItemId = 3, Available = true },
                new ItemInventoryEntity { Id = 6, ItemId = 3, Available = false }
                );
            modelBuilder.Entity<LoanReservationEntity>()
                .HasData(
                    new LoanReservationEntity { Id = 1, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(2), ItemId = 1, UserId = 3 },
                    new LoanReservationEntity { Id = 2, Date_From = DateTime.Now, Date_To = DateTime.Now.AddDays(1), ItemId = 1, UserId = 4 });

        }
    }
}
