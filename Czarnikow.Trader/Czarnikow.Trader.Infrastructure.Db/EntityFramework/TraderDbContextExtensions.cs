namespace Czarnikow.Trader.Infrastructure.Db.Extensions
{
    using System;
    using Czarnikow.Trader.Core.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class TraderDbContextExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var companyA = new Counterparty(1, "Company A");
            var companyB = new Counterparty(2, "Company B");

            modelBuilder.Entity<Counterparty>().HasData(companyA, companyB);

            //var tradeId1 = Trade.Create(1, 1, "Sugar", 100, 400.50m, new DateTime(2018, 1, 31), Direction.Buy);
            var tradeId1 = new Trade.Builder()
            {
                Date = new DateTime(2018, 1, 31),
                CounterpartyId = 1,
                Product = "Sugar",
                Quantity = 100,
                Price = 400.50m,
                Direction = Direction.Buy
            }.Build();

            tradeId1.SetId(1);            

            //var tradeId2 = Trade.Create(2, 2, "Sugar", 100, 450.10m, new DateTime(2018, 3, 31), Direction.Sell);
            var tradeId2 = new Trade.Builder()
            {
                Date = new DateTime(2018, 3, 31),
                CounterpartyId = 2,
                Product = "Sugar",
                Quantity = 100,
                Price = 450.10m,
                Direction = Direction.Sell
            }.Build();

            tradeId2.SetId(2);

            modelBuilder.Entity<Trade>().HasData(tradeId1, tradeId2);
        }

        private static void SetId<T>(this T entity, int value) where T : Entity<int?>
        {
            typeof(T).GetProperty(nameof(entity.Id)).SetValue(entity, value);
        }

        public static void Configure(this EntityTypeBuilder<Counterparty> entityTypeBuilder)
        {            
            entityTypeBuilder.ToTable("Counterparty");
            entityTypeBuilder.HasKey(c => c.Id).HasName("PK_Counterparty");
            entityTypeBuilder.Property(c => c.Id).UseIdentityColumn().HasColumnName("CounterpartyId");
            entityTypeBuilder.HasAlternateKey(c => c.Name);
            entityTypeBuilder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            entityTypeBuilder.HasCheckConstraint("CK_Name", "LEN(Name) > 0");
        }

        public static void Configure(this EntityTypeBuilder<Trade> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Trade");
            entityTypeBuilder.HasKey(t => t.Id).HasName("PK_Trade");
            entityTypeBuilder.Property(t => t.Id).UseIdentityColumn().HasColumnName("TradeId");
            entityTypeBuilder.Property(t => t.CounterpartyId).IsRequired();
            entityTypeBuilder.HasOne(t => t.Counterparty).WithMany().HasForeignKey(t => t.CounterpartyId);
            entityTypeBuilder.Property(t => t.Product).IsRequired().HasMaxLength(200);
            entityTypeBuilder.HasCheckConstraint("CK_Product", "LEN(Product) > 0");
            entityTypeBuilder.Property(t => t.Quantity).IsRequired();
            entityTypeBuilder.HasCheckConstraint("CK_Quantity", "Quantity >= 0");
            entityTypeBuilder.Property(t => t.Price).IsRequired();
            entityTypeBuilder.HasCheckConstraint("CK_Price", "Price >= 0");
            entityTypeBuilder.Property(t => t.Date).IsRequired().HasDefaultValueSql("GETDATE()");
            entityTypeBuilder.Property(t => t.Direction).IsRequired().HasColumnType("CHAR(1)");
            entityTypeBuilder.HasCheckConstraint("CK_Direction", "Direction = 'B' OR Direction = 'S'");
        }
    }
}