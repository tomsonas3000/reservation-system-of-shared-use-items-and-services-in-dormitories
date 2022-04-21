using System.Text.RegularExpressions;
using ReservationSystem.Shared.Utilities;

namespace ReservationSystem.Shared.ValueObjects
{
    public class RequiredEmail
    {
        public string Value { get; }

        private RequiredEmail(string value)
        {
            Value = value;
        }
        
        public static Result<RequiredEmail>? Create(Result result, string? value, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result.AddError(propertyName, $"The {propertyName} can not be empty.");
                return result as Result<RequiredEmail>;
            }

            var trimmedValue = value.Trim();

            if (trimmedValue.Length > 200)
            {
                result.AddError(propertyName, $"The {propertyName} can not be longer than 200 characters.");
                return result as Result<RequiredEmail>;
            }

            if (!Regex.IsMatch(trimmedValue, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                result.AddError(propertyName, $"The provided email is not valid.");
            }

            return new Result<RequiredEmail>(new RequiredEmail(trimmedValue));
        }
    }
}