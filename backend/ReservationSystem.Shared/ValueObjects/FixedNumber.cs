using ReservationSystem.Shared.Utilities;

namespace ReservationSystem.Shared.ValueObjects
{
    public class FixedNumber
    {
        public int Value { get; }

        private FixedNumber(int value)
        {
            Value = value;
        }

        public static Result<FixedNumber> Create(Result result, int value, string propertyName, int minNumber = 0, int maxNumber = 0)
        {
            if (value > maxNumber || value < minNumber)
            {
                result.AddError(propertyName, $"{propertyName} must be within {minNumber} and {maxNumber}");
                return null!;
            }

            return new Result<FixedNumber>(new FixedNumber(value));
        }
    }
}