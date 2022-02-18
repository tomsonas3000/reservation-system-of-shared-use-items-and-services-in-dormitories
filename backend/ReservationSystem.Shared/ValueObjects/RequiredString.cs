﻿using ReservationSystem.Shared.Utilities;

namespace ReservationSystem.Shared.ValueObjects
{
    public class RequiredString
    {
        public string Value { get; }
        
        private RequiredString(string value)
        {
            Value = value;
        }

        public static Result<RequiredString> Create(Result result, string? value, string propertyName, int length = 100)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result.AddError(propertyName, $"The {propertyName} can not be empty.");
                return (Result<RequiredString>)result;
            }

            var trimmedValue = value.Trim();

            if (trimmedValue.Length > length)
            {
                result.AddError(propertyName, $"The {propertyName} can not be longer than {length} characters.");
                return (Result<RequiredString>)result;
            }

            return new Result<RequiredString>(new RequiredString(trimmedValue));
        }
    }
}