namespace Czarnikow.Trader.Infrastructure.Db.IntegrationTests
{
    using NUnit.Framework;
    using NUnit.Framework.Interfaces;
    using System;
    using System.Transactions;

    public class RollbackAttribute : Attribute, ITestAction
    {
        private TransactionScope transaction;

        public void BeforeTest(ITest test)
        {
            transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        public void AfterTest(ITest test)
        {
            transaction.Dispose();
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }
}