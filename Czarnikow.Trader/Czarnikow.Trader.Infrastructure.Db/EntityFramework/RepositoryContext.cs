namespace Czarnikow.Trader.Infrastructure.Db.EntityFramework
{
    using Czarnikow.Trader.Core.Domain;
    using Czarnikow.Trader.Infrastructure.Db.Extensions;
    using Microsoft.EntityFrameworkCore;

    public class RepositoryContext : DbContext, IRepositoryContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Counterparty>().ToTable("Counterparty");
            modelBuilder.Entity<Counterparty>().HasKey(c => c.Id).HasName("CounterpartyId");
            modelBuilder.Entity<Counterparty>().Property(c => c.Id).UseIdentityColumn();
            modelBuilder.Entity<Counterparty>().HasAlternateKey(c => c.Name);
            modelBuilder.Entity<Counterparty>().Property(c => c.Name).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Counterparty>().HasCheckConstraint("CK_Name", "LEN(Name) > 0");

            modelBuilder.Entity<Trade>().ToTable("Trade");
            modelBuilder.Entity<Trade>().HasKey(t => t.Id).HasName("TradeId");
            modelBuilder.Entity<Trade>().Property(t => t.Id).UseIdentityColumn();
            modelBuilder.Entity<Trade>().Property(t => t.CounterpartyId).IsRequired();
            modelBuilder.Entity<Trade>().HasOne<Counterparty>().WithMany().HasForeignKey(t => t.CounterpartyId);
            modelBuilder.Entity<Trade>().Property(t => t.Product).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Trade>().HasCheckConstraint("CK_Product", "LEN(Product) > 0");
            modelBuilder.Entity<Trade>().Property(t => t.Quantity).IsRequired();
            modelBuilder.Entity<Trade>().HasCheckConstraint("CK_Quantity", "Quantity >= 0");
            modelBuilder.Entity<Trade>().Property(t => t.Price).IsRequired();
            modelBuilder.Entity<Trade>().HasCheckConstraint("CK_Price", "Price >= 0");
            modelBuilder.Entity<Trade>().Property(t => t.Date).IsRequired().HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Trade>().Property(t => t.Direction).IsRequired();
            modelBuilder.Entity<Trade>().HasCheckConstraint("CK_Direction", "Direction = 'B' OR Direction = 'S'");

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