namespace Czarnikow.Trader.Api
{
    using Microsoft.EntityFrameworkCore;

    public interface IDbContextOptionsStrategy
    {
        void Configure(DbContextOptionsBuilder optionsBuilder);
    }
}