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

        public static Result<RequiredDate>? Create(Result result, string? value, string propertyName = default!)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result.AddError(propertyName, $"The {propertyName} can not be empty.");
                return result as Result<RequiredDate>;
            }

            if (!DateTime.TryParse(value, out var date))
            {
                result.AddError(propertyName, "Date can not be parsed.");
                return result as Result<RequiredDate>;
            }

            return new Result<RequiredDate>(new RequiredDate(new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, date.Kind)));
        }
    }
}