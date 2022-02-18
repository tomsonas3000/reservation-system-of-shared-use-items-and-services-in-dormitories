using System.Linq;
using System.Text.RegularExpressions;
using ReservationSystem.Shared.Utilities;

namespace ReservationSystem.Shared.ValueObjects
{
    public class RequiredPhoneNumber
    {
        public string Value { get; }

        private RequiredPhoneNumber(string value)
        {
            Value = value;
        }
        
        public static Result<RequiredPhoneNumber> Create(Result result, string? value, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result.AddError(propertyName, $"The {propertyName} can not be empty.");
                return (Result<RequiredPhoneNumber>)result;
            }

            var trimmedValue = value.Trim();

            if (trimmedValue.Length > 200)
            {
                result.AddError(propertyName, $"The {propertyName} can not be longer than 200 characters.");
                return (Result<RequiredPhoneNumber>)result;
            }

            if (!trimmedValue.StartsWith("+370") || trimmedValue.Length != 12 || trimmedValue.All(char.IsDigit))
            {
                result.AddError(propertyName, $"The provided phone number is not valid.");
            }

            return new Result<RequiredPhoneNumber>(new RequiredPhoneNumber(trimmedValue));
        }
    }
}