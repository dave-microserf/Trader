namespace Czarnikow.Trader.Infrastructure.Db.EntityFramework
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    class TraderDbContextFactory : IDesignTimeDbContextFactory<TraderDbContext>
    {
        public TraderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TraderDbContext>();
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=TraderDb;Integrated Security=True");

            return new TraderDbContext(optionsBuilder.Options);
        }
    }
}