using System;
using System.Collections.Generic;
using FluentAssertions;
using ReservationSystem.DataAccess.Entities;
using ReservationSystem.DataAccess.Enums;
using ReservationSystem.Tests.Constants;
using Xunit;

namespace ReservationSystem.Tests.UnitTests
{
    public class ServiceTests
    {
        [Fact]
        public void Should_Create_Service()
        {
            var name = "service";
            var type = "Volleyball";
            var maxTimeOfUse = 15;
            var roomId = Guid.NewGuid();
            var dormitoryId = Guid.NewGuid();

            var createResult = Service.Create(name, type, maxTimeOfUse, roomId, dormitoryId);
            createResult.IsSuccess.Should().BeTrue();
            createResult.Value.Should().BeEquivalentTo(new
            {
                Name = name,
                Type = ServiceType.Volleyball,
                MaxTimeOfUse = TimeSpan.FromMinutes(15),
                DormitoryId = dormitoryId,
                RoomId = roomId,
            });
        }
        
        [Fact]
        public void Should_Update_Service()
        {
            var updatedName = "service";
            var updatedMaxTimeOfUse = 30;
            var updatedRoomId = Guid.NewGuid();
            var updatedDormitoryId = Guid.NewGuid();

            var service = CreateService();

            var updateResult = service.Update(updatedName, "volleyball", updatedMaxTimeOfUse, updatedRoomId, updatedDormitoryId);
            updateResult.IsSuccess.Should().BeTrue();
            service.Should().BeEquivalentTo(new
            {
                Name = updatedName,
                Type = ServiceType.Volleyball,
                MaxTimeOfUse = TimeSpan.FromMinutes(updatedMaxTimeOfUse),
                DormitoryId = updatedDormitoryId,
                RoomId = updatedRoomId,
            });
        }

        [Theory]
        [InlineData("name", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(FixedLengthStrings.Length201String, false)]
        public void Should_Validate_Name_When_Creating(string name, bool expected)
        {
            var createResult = Service.Create(name, "volleyball", 15, Guid.NewGuid(), Guid.NewGuid());
            createResult.IsSuccess.Should().Be(expected);
        }

        [Theory]
        [InlineData("volleyball", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("volley", false)]
        [InlineData("1", true)]
        public void Should_Validate_Type_When_Creating(string type, bool expected)
        {
            var createResult = Service.Create("name", type, 15, Guid.NewGuid(), Guid.NewGuid());
            createResult.IsSuccess.Should().Be(expected);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(401, false)]
        [InlineData(10, true)]
        [InlineData(0, false)]
        [InlineData(null, false)]
        public void Should_Validate_MaxTimeOfUse_When_Creating(int maxTimeOfUse, bool expected)
        {
            var createResult = Service.Create("name", "volleyball", maxTimeOfUse, Guid.NewGuid(), Guid.NewGuid());
            createResult.IsSuccess.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("name", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(FixedLengthStrings.Length201String, false)]
        public void Should_Validate_Name_When_Updating(string updatedName, bool expected)
        {
            var service = CreateService();
            var updateResult = service.Update(updatedName, "volleyball", 15, Guid.NewGuid(), Guid.NewGuid());
            updateResult.IsSuccess.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("volleyball", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("volley", false)]
        [InlineData("7", true)]
        [InlineData("basketball", false)]
        [InlineData("2", false)]
        public void Should_Validate_Type_When_Updating(string updatedType, bool expected)
        {
            var service = CreateService();
            var updateResult = service.Update("name", updatedType, 15, Guid.NewGuid(), Guid.NewGuid());
            updateResult.IsSuccess.Should().Be(expected);
        }
        
        [Theory]
        [InlineData(-1, false)]
        [InlineData(401, false)]
        [InlineData(10, true)]
        [InlineData(0, false)]
        [InlineData(null, false)]
        public void Should_Validate_MaxTimeOfUse_When_Updating(int updatedMaxTimeOfUse, bool expected)
        {
            var service = CreateService();
            var updateResult = service.Update("name", "volleyball", updatedMaxTimeOfUse, Guid.NewGuid(), Guid.NewGuid());
            updateResult.IsSuccess.Should().Be(expected);
        }

        [Fact]
        public void Should_Add_Reservation()
        {
            var service = CreateService();
            var beginDate = DateTime.Now.AddMinutes(5);
            var endDate = DateTime.Now.AddMinutes(15);

            var reservation = CreateReservation(beginDate, endDate);

            var addReservationResult = service.AddReservation(reservation);
            addReservationResult.IsSuccess.Should().BeTrue();
            service.ReservationsList.Should().BeEquivalentTo(new List<object> {reservation});

        }

        [Fact]
        public void Should_Validate_If_Time_Is_Already_Taken_When_Adding_Reservation()
        {
            var service = CreateService();
            var beginDate1 = DateTime.Now.AddMinutes(5);
            var endDate1 = DateTime.Now.AddMinutes(15);
            var beginDate2 = DateTime.Now.AddMinutes(10);
            var endDate2 = DateTime.Now.AddMinutes(20);
        
            var reservation1 = CreateReservation(beginDate1, endDate1);
            var reservation2 = CreateReservation(beginDate2, endDate2);

            service.AddReservation(reservation1);
            
            var addReservationResult = service.AddReservation(reservation2);
            addReservationResult.IsSuccess.Should().BeFalse();
        }

        private static Service CreateService()
        {
            return Service.Create("service", "volleyball", 15, Guid.NewGuid(), Guid.NewGuid()).Value;
        }
        
        private static User CreateUser()
        {
            return User.Create("name", "surname", "test@email.com", "+37061111111").Value;
        }

        private static Reservation CreateReservation(DateTime beginDate, DateTime endDate)
        {
            var user = CreateUser();
            var service = CreateService();
            return Reservation.Create(service, user, beginDate.ToString(), endDate.ToString()).Value;
        }
    }
}