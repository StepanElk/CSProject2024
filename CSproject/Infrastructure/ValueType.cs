using System.Reflection;
using System.Text;
using System.Security.Cryptography;

namespace CSproject.Infrastructure
{
    public class ValueType<T> // Требует осмысления, т.к. пока просто взято из задания
    {
        public override int GetHashCode()
        {
            var hashCodeBasedOnData = new MD5CryptoServiceProvider()
                .ComputeHash(ASCIIEncoding.ASCII
                .GetBytes(this
                .ToString()));
            return BitConverter.ToInt32(hashCodeBasedOnData, 0);
        }

        public override string ToString()
        {
            var properties = from prop in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             orderby prop.Name
                             select $"{prop.Name}: {prop.GetValue(this) ?? ""}";
            return $"{GetType().Name}({string.Join("; ", properties)})";
        }

        public override bool Equals(object inputData)
        {
            if (inputData is null || GetType() != inputData.GetType())
                return false;

            var currentProperties = GetType().GetProperties();
            var inputProperties = inputData.GetType().GetProperties();

            if (currentProperties.Length != inputProperties.Length)
                return false;

            foreach (var property in currentProperties)
            {
                var currentValue = property.GetValue(this);
                var inputValue = property.GetValue(inputData);

                if (!AreValuesEqual(currentValue, inputValue))
                    return false;
            }
            return true;
        }

        private bool AreValuesEqual(object currentValue, object inputValue)
        {
            if (currentValue == null || inputValue == null)
                return currentValue == inputValue;
            return TryCompareDirectly(currentValue, inputValue)
                || TryCustomComparison(currentValue, inputValue);
        }

        private bool TryCompareDirectly(object currentValue, object inputValue)
        {
            if (currentValue is IComparable comparableCurrent
                && inputValue is IComparable comparableInput)
                return comparableCurrent.CompareTo(comparableInput) == 0;
            return false;
        }

        private bool TryCustomComparison(object currentValue, object inputValue)
        {
            switch (currentValue)
            {
                case string stringCurrent
                when inputValue is string stringInput:
                    return string.Compare(stringCurrent, stringInput) == 0;

                case DateTime dateTimeCurrent
                when inputValue is DateTime dateTimeInput:
                    return dateTimeCurrent == dateTimeInput;

                default:
                    return currentValue.Equals(inputValue);
            }
        }
    }
}
