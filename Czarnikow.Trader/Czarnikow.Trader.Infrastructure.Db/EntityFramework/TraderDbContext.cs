namespace Czarnikow.Trader.Infrastructure.Db.EntityFramework
{
    using Czarnikow.Trader.Core.Domain;
    using Czarnikow.Trader.Infrastructure.Db.Extensions;
    using Microsoft.EntityFrameworkCore;

    public class TraderDbContext : DbContext, ITraderDbContext
    {
        public TraderDbContext(DbContextOptions<TraderDbContext> options) : base(options)
        {                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Counterparty>().Configure();
            modelBuilder.Entity<Trade>().Configure();

            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Counterparty> Counterparties
        {
            get; set;
        }

        public DbSet<Trade> Trades
        {
            get; set;
        }
    }
}