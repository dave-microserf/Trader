namespace Czarnikow.Trader.Core.Domain
{
    using System;
    using System.Collections.ObjectModel;

    public class ValidationErrors : Collection<string>
    {
        public void AddIfLessThan<T>(string name, T value, T minimum) where T : IComparable<T>
        {
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (value.CompareTo(minimum) < 0)
            {
                this.Add($"'{name}' cannot be less than {minimum}. Actual value: {value}.");
            }
        }

        public void AddIfGreaterThan<T>(string name, T value, T maximum) where T : IComparable<T>
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (value.CompareTo(maximum) > 0)
            {
                this.Add($"'{name}' cannot be greater than {maximum}. Actual value: {value}.");
            }
        }

        public void AddIfLengthLessThan(string name, string value, uint minimumLength)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            var length = value == null ? 0 : value.Length;

            if (length < minimumLength)
            {
                this.Add($"'{name}' cannot be less than {minimumLength} character(s). Actual value: {length}.");
            }
        }

        public void AddIfLengthGreaterThan(string name, string value, uint maximumLength)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var length = value == null ? 0 : value.Length;

            if (length > maximumLength)
            {
                this.Add($"'{name}' cannot be more than {maximumLength} character(s). Actual value: {length}.");
            }
        }
    }
}
