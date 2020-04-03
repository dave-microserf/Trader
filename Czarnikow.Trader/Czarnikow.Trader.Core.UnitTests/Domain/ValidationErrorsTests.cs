namespace Czarnikow.Trader.Core.UnitTests.Domain
{
    using System;
    using Czarnikow.Trader.Core.Domain;
    using NUnit.Framework;

    public class ValidationErrorsTests
    {
        private ValidationErrors validationErrors;

        [SetUp]
        public void SetUp()
        {
            this.validationErrors = new ValidationErrors();
        }

        [Test]
        public void AddIfLessThan_ShouldAddErrorIfValueIsLessThanMinimum()
        {
            int testValue = 0;
           
            this.validationErrors.AddIfLessThan(nameof(testValue), testValue, 1);

            Assert.Contains("'testValue' cannot be less than 1. Actual value: 0.", this.validationErrors);
        }

        [Test]
        public void AddIfLessThan_ShouldNotAddErrorIfValueIsEqualToMinimum()
        {
            int testValue = 1;

            this.validationErrors.AddIfLessThan(nameof(testValue), testValue, 1);

            Assert.IsEmpty(this.validationErrors);
        }

        [Test]
        public void AddIfGreaterThan_ShouldAddErrorIfValueIsGreaterThanMaximum()
        {
            int testValue = 1;

            this.validationErrors.AddIfGreaterThan(nameof(testValue), testValue, 0);

            Assert.Contains("'testValue' cannot be greater than 0. Actual value: 1.", this.validationErrors);
        }

        [Test]
        public void AddIfGreaterThan_ShouldNotAddErrorIfValueIsEqualToMaximum()
        {
            int testValue = 1;

            this.validationErrors.AddIfGreaterThan(nameof(testValue), testValue, 1);

            Assert.IsEmpty(this.validationErrors);
        }

        [Test]
        public void AddIfLengthGreaterThan_ShouldAddErrorIfLengthIsGreaterThanMaximum()
        {
            var testValue = "This is a test";

            this.validationErrors.AddIfLengthGreaterThan(nameof(testValue), testValue, 10);

            Assert.Contains("'testValue' cannot be more than 10 character(s). Actual value: 14.", this.validationErrors);
        }

        [Test]
        public void AddIfLengthLessThan_ShouldAddErrorIfLengthIsLessThanMinimum()
        {
            var testValue = "This is a test";

            this.validationErrors.AddIfLengthLessThan(nameof(testValue), testValue, 100);

            Assert.Contains("'testValue' cannot be less than 100 character(s). Actual value: 14.", this.validationErrors);
        }
    }
}