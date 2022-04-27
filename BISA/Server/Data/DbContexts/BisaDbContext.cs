namespace BISA.Server.Data.DbContexts
{
    public class BisaDbContext : DbContext
    {
        public BisaDbContext(DbContextOptions<BisaDbContext> options)
            : base(options)
        {

        }
    }
}
