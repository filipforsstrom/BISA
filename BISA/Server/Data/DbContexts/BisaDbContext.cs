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
        public DbSet<LoanHistoryEntity> LoanHistory { get; set; }
        public DbSet<LoanReservationEntity> LoanReservations { get; set; }
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<ItemInventoryEntity> ItemInventory { get; set; }



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
                new BookEntity { Id = 1, Title = "Hej", ISBN = "523577987" }
                );

            modelBuilder.Entity<MovieEntity>()
                .HasData(
                new MovieEntity { Id = 2, Title = "Hej the movie", RuntimeInMinutes = 120, Creator = "Tolkien", Language = "English" });

            modelBuilder.Entity<UserEntity>()
                .HasData(
                new UserEntity { Id = 1, Email = "jaf@newton.co.uk", Warnings = 3 }
                );

            modelBuilder.Entity<LoanEntity>()
                .HasData(
                new LoanEntity { Id = 1, ItemInventoryId = 1, UserId = 1 }
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
                new EventTypeEntity { Id = 1, Capacity = 500, Description = "Tjena hallå", Image = "Katt", Type = "Högläsning för barn" }
                );

            modelBuilder.Entity<EventEntity>()
                .HasData(
                new EventEntity { Id = 1, Date = DateTime.Now, Location = "Malmö", Organizer = "Kalle-Staffan", Subject = "Staffans kalle", EventTypeId = 1 }
                );
            modelBuilder.Entity<ItemInventoryEntity>()
                .HasData(
                new ItemInventoryEntity { Id = 1, ItemId = 1, Available = true },
                new ItemInventoryEntity { Id = 2, ItemId = 1, Available = false }
                );
        }
    }
}
