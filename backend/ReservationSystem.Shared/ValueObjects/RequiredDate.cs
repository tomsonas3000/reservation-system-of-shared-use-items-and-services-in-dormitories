using System;
using ReservationSystem.Shared.Utilities;

namespace ReservationSystem.Shared.ValueObjects
{
    public class RequiredDate
    {
        public DateTime Value { get; }

        private RequiredDate(DateTime value)
        {
            Value = value;
        }

        public static Result<RequiredDate> Create(Result result, string? value, string propertyName = default!)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result.AddError(propertyName, $"The {propertyName} can not be empty.");
                return (Result<RequiredDate>)result;
            }

            if (!DateTime.TryParse(value, out var date))
            {
                result.AddError(propertyName, "Date can not be parsed.");
            }

            return new Result<RequiredDate>(new RequiredDate(new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, date.Kind)));
        }
    }
}