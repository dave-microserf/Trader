namespace Czarnikow.Trader.IntegrationTests
{
    using NUnit.Framework;
    using NUnit.Framework.Interfaces;
    using System;
    using System.Transactions;

    public class RollbackAttribute : Attribute, ITestAction
    {
        public void BeforeTest(ITest test)
        {
            this.Scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.Serializable },
                TransactionScopeAsyncFlowOption.Enabled);
        }

        public void AfterTest(ITest test)
        {
            this.Scope.Dispose();
        }
        
        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }

        public TransactionScope Scope
        {
            get; private set;
        }
    }
}