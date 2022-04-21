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
        
        public static Result<RequiredPhoneNumber>? Create(Result result, string? value, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result.AddError(propertyName, $"The {propertyName} can not be empty.");
                return result as Result<RequiredPhoneNumber>;
            }

            var trimmedValue = value.Trim();
            
            if (!trimmedValue.StartsWith("+370") || trimmedValue.Length != 12 || !trimmedValue.Except("+").All(char.IsDigit))
            {
                result.AddError(propertyName, $"The provided phone number is not valid.");
                return result as Result<RequiredPhoneNumber>;
            }

            return new Result<RequiredPhoneNumber>(new RequiredPhoneNumber(trimmedValue));
        }
    }
}