using System;
using ReservationSystem.Shared.Utilities;

namespace ReservationSystem.Shared.ValueObjects
{
    public class RequiredEnum<T> where T : struct
    {
        public T Value { get; }

        private RequiredEnum(T value)
        {
            Value = value;
        }

        public static Result<RequiredEnum<T>> Create(string? enumValue, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(enumValue))
            {
                return new Result<RequiredEnum<T>>();
            }

            var parsedEnum = Enum.TryParse(enumValue, true, out T parsedValue);

            if (!parsedEnum || !Enum.IsDefined(typeof(T), parsedValue))
            {
                var result = new Result<RequiredEnum<T>>();
                result.AddError(propertyName, "The enum value is invalid");
                return result;
            }

            return new Result<RequiredEnum<T>>(new RequiredEnum<T>(parsedValue));
        }
        
    }
}