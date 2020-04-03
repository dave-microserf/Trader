namespace Czarnikow.Trader.Api
{
    using Microsoft.EntityFrameworkCore;

    public class DbContextOptionsStrategy : IDbContextOptionsStrategy
    {
        public virtual void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Startup.ConnectionString);
        }
    }
}