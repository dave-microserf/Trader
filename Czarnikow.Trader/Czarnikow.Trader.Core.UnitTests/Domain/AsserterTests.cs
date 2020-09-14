namespace Czarnikow.Trader.Core.UnitTests.Domain
{
    using Czarnikow.Trader.Core.Domain;
    using NUnit.Framework;

    public class AsserterTests
    {
        private Asserter asserter;

        [SetUp]
        public void SetUp()
        {
            this.asserter = new Asserter();
        }

        [Test]
        public void AddIfLessThan_ShouldAddErrorIfValueIsLessThanMinimum()
        {
            int testValue = 0;
           
            this.asserter.AddIfLessThan(nameof(testValue), testValue, 1);

            Assert.Contains("'testValue' cannot be less than 1. Actual value: 0.", this.asserter);
        }

        [Test]
        public void AddIfLessThan_ShouldNotAddErrorIfValueIsEqualToMinimum()
        {
            int testValue = 1;

            this.asserter.AddIfLessThan(nameof(testValue), testValue, 1);

            Assert.IsEmpty(this.asserter);
        }

        [Test]
        public void AddIfGreaterThan_ShouldAddErrorIfValueIsGreaterThanMaximum()
        {
            int testValue = 1;

            this.asserter.AddIfGreaterThan(nameof(testValue), testValue, 0);

            Assert.Contains("'testValue' cannot be greater than 0. Actual value: 1.", this.asserter);
        }

        [Test]
        public void AddIfGreaterThan_ShouldNotAddErrorIfValueIsEqualToMaximum()
        {
            int testValue = 1;

            this.asserter.AddIfGreaterThan(nameof(testValue), testValue, 1);

            Assert.IsEmpty(this.asserter);
        }

        [Test]
        public void AddIfLengthGreaterThan_ShouldAddErrorIfLengthIsGreaterThanMaximum()
        {
            var testValue = "This is a test";

            this.asserter.AddIfLengthGreaterThan(nameof(testValue), testValue, 10);

            Assert.Contains("'testValue' cannot be more than 10 character(s). Actual value: 14.", this.asserter);
        }

        [Test]
        public void AddIfLengthLessThan_ShouldAddErrorIfLengthIsLessThanMinimum()
        {
            var testValue = "This is a test";

            this.asserter.AddIfLengthLessThan(nameof(testValue), testValue, 100);

            Assert.Contains("'testValue' cannot be less than 100 character(s). Actual value: 14.", this.asserter);
        }
    }
}