using FluentAssertions;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.Shared.Utilities;
using ReservationSystem.Tests.Constants;
using Xunit;

namespace ReservationSystem.Tests.UnitTests
{
    public class RoomTests
    {
        [Theory]
        [InlineData("name", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(FixedLengthStrings.Length101String, false)]
        public void Should_Validate_Name(string name, bool expected)
        {
            var roomCreateResult = Room.Create(new Result(), name);
            if (expected)
            {
                roomCreateResult!.IsSuccess.Should().BeTrue();
            }
            else
            {
                roomCreateResult.Should().BeNull();
            }
        }
    }
}