namespace Czarnikow.Trader.IntegrationTests.Controllers
{
    using System;
    using System.Transactions;
    using Czarnikow.Trader.Api;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;

    public sealed class IntegrationTestOptionsStrategy : DbContextOptionsStrategy, IDisposable
    {
        public IntegrationTestOptionsStrategy()
        {
        }

        public override void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            if (this.Scope == null)
            {
                this.Scope = new TransactionScope(
                    TransactionScopeOption.Required, 
                    new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }, 
                    TransactionScopeAsyncFlowOption.Enabled);
            }

            if (this.Connection == null)
            {
                this.Connection = new SqlConnection(Startup.ConnectionString);
                this.Connection.Open();
            }

            optionsBuilder.UseSqlServer(this.Connection);
        }

        public void Dispose()
        {
            this.Scope?.Dispose();
            this.Connection?.Dispose();
        }

        public TransactionScope Scope
        { 
            get; private set;
        }

        public SqlConnection Connection 
        { 
            get; private set;
        }
    }
}