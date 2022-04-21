using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.Tests.Constants;

namespace ReservationSystem.Tests.UnitTests
{
    public class DormitoryTests
    {
        [Theory]
        [InlineData("name", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(FixedLengthStrings.Length101String, false)]
        public void Should_Validate_Name_When_Creating(string name, bool expected)
        {
            var createResult = Dormitory.Create(name, "city", "address", Guid.NewGuid());
            createResult.IsSuccess.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("city", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(FixedLengthStrings.Length101String, false)]
        public void Should_Validate_City_When_Creating(string city, bool expected)
        {
            var createResult = Dormitory.Create("name", city, "address", Guid.NewGuid());
            createResult.IsSuccess.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("address", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(FixedLengthStrings.Length101String, false)]
        public void Should_Validate_Address_When_Creating(string address, bool expected)
        {
            var createResult = Dormitory.Create("name", "city", address, Guid.NewGuid());
            createResult.IsSuccess.Should().Be(expected);
        }

        [Theory]
        [InlineData("updated name", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(FixedLengthStrings.Length101String, false)]
        public void Should_Validate_Name_When_Updating(string updatedName, bool expected)
        {
            var dormitory = CreateDormitory();
            var updateResult = dormitory.Update(updatedName, "city", "address", Guid.NewGuid());
            
            updateResult.IsSuccess.Should().Be(expected);
        }

        [Theory]
        [InlineData("updated city", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(FixedLengthStrings.Length101String, false)]
        public void Should_Validate_City_When_Updating(string updatedCity, bool expected)
        {
            var dormitory = CreateDormitory();
            var updateResult = dormitory.Update("name", updatedCity, "address", Guid.NewGuid());
            
            updateResult.IsSuccess.Should().Be(expected);
        }

        [Theory]
        [InlineData("updated address", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(FixedLengthStrings.Length101String, false)]
        public void Should_Validate_Address_When_Updating(string updatedAddress, bool expected)
        {
            var dormitory = CreateDormitory();
            var updateResult = dormitory.Update("name", "city", updatedAddress, Guid.NewGuid());
            
            updateResult.IsSuccess.Should().Be(expected);
        }

        [Fact]
        public void Should_Create_Dormitory()
        {
            var name = "name";
            var city = "city";
            var address = "city";
            var managerId = Guid.NewGuid();

            var createResult = Dormitory.Create(name, city, address, managerId);

            createResult.IsSuccess.Should().Be(true);

            createResult.Value.Should().BeEquivalentTo(new
            {
                Name = name,
                City = city,
                Address = address,
                ManagerId = managerId,
            });
        }

        [Fact]
        public void Should_Validate_Add_Rooms()
        {
            var dormitory = CreateDormitory();

            var addRoomsResult = dormitory.AddRooms(new List<string> {"1", "1"});
            addRoomsResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Should_Validate_Update_Rooms()
        {
            var dormitory = CreateDormitory();
            dormitory.AddRooms(new List<string> {"1", "2"});

            var updateRoomsResult = dormitory.UpdateRooms(new List<string> {"1"});
            updateRoomsResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Should_Add_Rooms()
        {
            var dormitory = CreateDormitory();
            var rooms = new List<string> { "1", "2" };
            
            var addRoomsResult = dormitory.AddRooms(rooms);
            addRoomsResult.IsSuccess.Should().Be(true);
            dormitory.Rooms.Select(x => x.RoomName).Should().BeEquivalentTo(rooms);
        }

        [Fact]
        public void Should_Update_Rooms()
        {
            var dormitory = CreateDormitory();
            var updatedRooms = new List<string> { "1", "2", "3" };
            dormitory.AddRooms(new List<string> {"1", "2"});

            var updateRoomsResult = dormitory.UpdateRooms(updatedRooms);
            updateRoomsResult.IsSuccess.Should().Be(true);
            dormitory.Rooms.Select(x => x.RoomName).Should().BeEquivalentTo(updatedRooms);
        }

        [Fact]
        public void Should_Update_Dormitory()
        {
            var updatedName = "updated name";
            var updatedCity = "updated city";
            var updatedAddress = "updated address";
            var updatedManagerId = Guid.NewGuid();

            var dormitory = CreateDormitory();
            var updateResult = dormitory.Update(updatedName, updatedCity, updatedAddress, updatedManagerId);

            updateResult.IsSuccess.Should().Be(true);

            dormitory.Should().BeEquivalentTo(new
            {
                Name = updatedName,
                City = updatedCity,
                Address = updatedAddress,
                ManagerId = updatedManagerId,
            });
        }

        [Fact]
        public void Should_Update_Residents()
        {
            var dormitory = CreateDormitory();

        }

        private static Dormitory CreateDormitory()
        {
            return Dormitory.Create("name", "city", "address", Guid.NewGuid()).Value;
        }
        
        
    }
}