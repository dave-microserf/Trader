namespace Czarnikow.Trader.Infrastructure.Db.Extensions
{
    using System;
    using Czarnikow.Trader.Core.Domain;
    using Microsoft.EntityFrameworkCore;

    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var companyA = new Counterparty(1, "Company A");
            var companyB = new Counterparty(2, "Company B");

            modelBuilder.Entity<Counterparty>().HasData(companyA, companyB);

            var tradeId1 = Trade.Create(1, 1, "Sugar", 100, 400.50m, new DateTime(2018, 1, 31), Direction.Buy);
            var tradeId2 = Trade.Create(2, 2, "Sugar", 100, 450.10m, new DateTime(2018, 3, 31), Direction.Sell);

            modelBuilder.Entity<Trade>().HasData(tradeId1, tradeId2);
        }
    }
}