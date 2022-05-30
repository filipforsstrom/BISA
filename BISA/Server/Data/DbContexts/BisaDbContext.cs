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
        public DbSet<ItemTagEntity> ItemTags { get; set; }


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

            modelBuilder.Entity<ItemEntity>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.Items)
                .UsingEntity<ItemTagEntity>(
                    j => j
                        .HasOne(pt => pt.Tag)
                        .WithMany(t => t.ItemTags)
                        .HasForeignKey(pt => pt.TagId),
                    j => j
                        .HasOne(pt => pt.Item)
                        .WithMany(p => p.ItemTags)
                        .HasForeignKey(pt => pt.ItemId),
                    j =>
                    {
                        j.HasKey(t => new { t.ItemId, t.TagId });
                    });

            modelBuilder.SeedItems();
            modelBuilder.SeedItemInventory();
            modelBuilder.SeedTags();
            modelBuilder.SeedLoans();
            modelBuilder.SeedReservations();
            modelBuilder.SeedEvents();
            modelBuilder.SeedUsers();
        }
    }
}
